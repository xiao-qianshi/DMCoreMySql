using Dmt.DM.Domain.Entity.SystemManage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dmt.DM.Application
{
    public class JwtTokensData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenSerial { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }

    public interface ITokenFactoryService
    {
        Task<JwtTokensData> CreateJwtTokensAsync(UserEntity user);
        string GetRefreshTokenSerial(string refreshTokenValue);
    }

    public class TokenFactoryService : ITokenFactoryService
    {
        private readonly ISecurityService _securityService;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        private readonly IRolesService _rolesService;
        private readonly ILogger<TokenFactoryService> _logger;

        public TokenFactoryService(
            ISecurityService securityService,
            IRolesService rolesService,
            IOptionsSnapshot<BearerTokensOptions> configuration,
            ILogger<TokenFactoryService> logger)
        {
            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));

            _rolesService = rolesService;
            _rolesService.CheckArgumentIsNull(nameof(rolesService));

            _configuration = configuration;
            _configuration.CheckArgumentIsNull(nameof(configuration));

            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(logger));
        }

        public async Task<JwtTokensData> CreateJwtTokensAsync(UserEntity user)
        {
            var (accessToken, claims) = await CreateAccessTokenAsync(user);
            var (refreshTokenValue, refreshTokenSerial) = CreateRefreshToken();
            return new JwtTokensData
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                RefreshTokenSerial = refreshTokenSerial,
                Claims = claims
            };
        }

        private (string RefreshTokenValue, string RefreshTokenSerial) CreateRefreshToken()
        {
            var refreshTokenSerial = _securityService.CreateCryptographicallySecureGuid().ToString().Replace("-", "");

            var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer, ClaimValueTypes.String, _configuration.Value.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _configuration.Value.Issuer),
                // for invalidation
                new Claim(ClaimTypes.SerialNumber, refreshTokenSerial, ClaimValueTypes.String, _configuration.Value.Issuer)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.Now;
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.Issuer,
                audience: _configuration.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes),
                signingCredentials: creds);
            var refreshTokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return (refreshTokenValue, refreshTokenSerial);
        }

        public string GetRefreshTokenSerial(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return null;
            }

            ClaimsPrincipal decodedRefreshTokenPrincipal = null;
            try
            {
                decodedRefreshTokenPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                    refreshTokenValue,
                    new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key)),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                    },
                    out _
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to validate refreshTokenValue: `{refreshTokenValue}`.");
            }

            return decodedRefreshTokenPrincipal?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
        }

        private async Task<(string AccessToken, IEnumerable<Claim> Claims)> CreateAccessTokenAsync(UserEntity user)
        {
            var claims = new List<Claim>();
            // Unique Id for all Jwt tokes
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, _securityService.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _configuration.Value.Issuer));
            // Issuer
            claims.Add(new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer, ClaimValueTypes.String,
                _configuration.Value.Issuer));
            // Issued at
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64, _configuration.Value.Issuer));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.F_Id.ToString(), ClaimValueTypes.String,
                _configuration.Value.Issuer));
            claims.Add(new Claim(ClaimTypes.Name, user.F_RealName, ClaimValueTypes.String,
                _configuration.Value.Issuer));
            claims.Add(new Claim("Account", user.F_Account, ClaimValueTypes.String, _configuration.Value.Issuer));
            // to invalidate the cookie
            claims.Add(new Claim(ClaimTypes.SerialNumber, user.F_SerialNumber, ClaimValueTypes.String, _configuration.Value.Issuer));
            //// custom data
            claims.Add(new Claim("IsAdmin", user.F_IsAdministrator ?? false ? "true" : "false",ClaimValueTypes.String, _configuration.Value.Issuer));

            claims.Add(new Claim(ClaimTypes.UserData, user.F_Id.ToString(), ClaimValueTypes.String, _configuration.Value.Issuer));

            // add roles
            var role = await _rolesService.FindUserRoleAsync(user.F_RoleId);
            if (role != null)
            {
                claims.Add(new Claim("RoleId",role.F_Id,ClaimValueTypes.String,_configuration.Value.Issuer));
                claims.Add(new Claim("RoleCode", role.F_EnCode, ClaimValueTypes.String, _configuration.Value.Issuer));
                claims.Add(new Claim("RoleName", role.F_FullName, ClaimValueTypes.String, _configuration.Value.Issuer));
            }
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.Name, ClaimValueTypes.String, _configuration.Value.Issuer));
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.Now;
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.Issuer,
                audience: _configuration.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes),
                signingCredentials: creds);
            return (new JwtSecurityTokenHandler().WriteToken(token), claims);
        }
    }
}
