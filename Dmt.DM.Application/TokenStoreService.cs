using Dmt.DM.Domain.Entity.SystemManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.EntityFrameWork;
using Dmt.DM.UOW;

namespace Dmt.DM.Application
{
    public interface ITokenStoreService
    {
        Task AddUserTokenAsync(UserTokenEntity userToken);
        Task AddUserTokenAsync(UserEntity user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial);
        Task<bool> IsValidTokenAsync(string accessToken, string userId);
        Task DeleteExpiredTokensAsync();
        Task<UserTokenEntity> FindTokenAsync(string refreshTokenValue);
        Task DeleteTokenAsync(string refreshTokenValue);
        Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenIdHashSource);
        Task InvalidateUserTokensAsync(string userId);
        Task RevokeUserBearerTokensAsync(string userIdValue, string refreshTokenValue);
    }

    public class TokenStoreService : ITokenStoreService
    {
        private readonly ISecurityService _securityService;
        private readonly IUnitOfWork<HsDbContext> _uow;
        private readonly DbSet<UserTokenEntity> _tokens;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        private readonly ITokenFactoryService _tokenFactoryService;

        public TokenStoreService(
            IUnitOfWork<HsDbContext> uow,
            ISecurityService securityService,
            IOptionsSnapshot<BearerTokensOptions> configuration,
            ITokenFactoryService tokenFactoryService)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));

            _tokens = _uow.Set<UserTokenEntity>();

            _configuration = configuration;
            _configuration.CheckArgumentIsNull(nameof(configuration));

            _tokenFactoryService = tokenFactoryService;
            _tokenFactoryService.CheckArgumentIsNull(nameof(tokenFactoryService));
        }

        public async Task AddUserTokenAsync(UserTokenEntity userToken)
        {
            if (!_configuration.Value.AllowMultipleLoginsFromTheSameUser)
            {
                await InvalidateUserTokensAsync(userToken.F_UserId);
            }
            await DeleteTokensWithSameRefreshTokenSourceAsync(userToken.F_RefreshTokenIdHashSource);
            _tokens.Add(userToken);
        }

        public async Task AddUserTokenAsync(UserEntity user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial)
        {
            var now = DateTimeOffset.Now;
            var token = new UserTokenEntity
            {
                F_UserId = user.F_Id,
                // Refresh token handles should be treated as secrets and should be stored hashed
                F_RefreshTokenIdHash = _securityService.GetSha256Hash(refreshTokenSerial),
                F_RefreshTokenIdHashSource = string.IsNullOrWhiteSpace(refreshTokenSourceSerial) ?
                                           null : _securityService.GetSha256Hash(refreshTokenSourceSerial),
                F_AccessTokenHash = _securityService.GetSha256Hash(accessToken),
                F_RefreshTokenExpiresDateTime = now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes),
                F_AccessTokenExpiresDateTime = now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes)
            };
            await AddUserTokenAsync(token);
        }

        public async Task DeleteExpiredTokensAsync()
        {
            var now = DateTimeOffset.Now;
            await _tokens.Where(x => x.F_RefreshTokenExpiresDateTime < now)
                        .ForEachAsync(userToken => _tokens.Remove(userToken));
        }

        public async Task DeleteTokenAsync(string refreshTokenValue)
        {
            var token = await FindTokenAsync(refreshTokenValue);
            if (token != null)
            {
                _tokens.Remove(token);
            }
        }

        public async Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenIdHashSource)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenIdHashSource))
            {
                return;
            }

            await _tokens.Where(t => t.F_RefreshTokenIdHashSource == refreshTokenIdHashSource ||
                                     t.F_RefreshTokenIdHash == refreshTokenIdHashSource &&
                                      t.F_RefreshTokenIdHashSource == null)
                .ForEachAsync(userToken => _tokens.Remove(userToken));
        }

        public async Task RevokeUserBearerTokensAsync(string userIdValue, string refreshTokenValue)
        {
            if (!string.IsNullOrWhiteSpace(userIdValue)/*&& int.TryParse(userIdValue, out int userId)*/)
            {
                if (_configuration.Value.AllowSignoutAllUserActiveClients)
                {
                    await InvalidateUserTokensAsync(userIdValue.Trim());
                }
            }

            if (!string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);
                if (!string.IsNullOrWhiteSpace(refreshTokenSerial))
                {
                    var refreshTokenIdHashSource = _securityService.GetSha256Hash(refreshTokenSerial);
                    await DeleteTokensWithSameRefreshTokenSourceAsync(refreshTokenIdHashSource);
                }
            }

            await DeleteExpiredTokensAsync();
        }

        public Task<UserTokenEntity> FindTokenAsync(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return Task.FromResult<UserTokenEntity>(null);
            }

            var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);
            if (string.IsNullOrWhiteSpace(refreshTokenSerial))
            {
                return Task.FromResult<UserTokenEntity>(null);
            }

            var refreshTokenIdHash = _securityService.GetSha256Hash(refreshTokenSerial);
            return _tokens.Include(x => x.User).FirstOrDefaultAsync(x => x.F_RefreshTokenIdHash == refreshTokenIdHash);
        }

        public async Task InvalidateUserTokensAsync(string userId)
        {
            await _tokens.Where(x => x.F_UserId.Equals(userId))
                        .ForEachAsync(userToken => _tokens.Remove(userToken));
        }

        public async Task<bool> IsValidTokenAsync(string accessToken, string userId)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);
            var userToken = await _tokens.FirstOrDefaultAsync(
                x => x.F_AccessTokenHash == accessTokenHash && x.F_UserId.Equals(userId));
            return userToken?.F_AccessTokenExpiresDateTime >= DateTimeOffset.Now;
        }
    }
}
