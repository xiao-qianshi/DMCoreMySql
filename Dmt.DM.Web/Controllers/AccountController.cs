﻿using Dmt.DM.Application;
using Dmt.DM.Code;
using Dmt.DM.EntityFrameWork;
using Dmt.DM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Dmt.DM.UOW;

namespace Dmt.DM.Web.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class AccountController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly ITokenStoreService _tokenStoreService;
        private readonly IUnitOfWork<HsDbContext> _uow;
        private readonly IAntiForgeryCookieService _antiforgery;
        private readonly ITokenFactoryService _tokenFactoryService;

        public AccountController(
            IUsersService usersService,
            ITokenStoreService tokenStoreService,
            ITokenFactoryService tokenFactoryService,
            IUnitOfWork<HsDbContext> uow,
            IAntiForgeryCookieService antiforgery)
        {
            _usersService = usersService;
            _usersService.CheckArgumentIsNull(nameof(usersService));

            _tokenStoreService = tokenStoreService;
            _tokenStoreService.CheckArgumentIsNull(nameof(tokenStoreService));

            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _antiforgery = antiforgery;
            _antiforgery.CheckArgumentIsNull(nameof(antiforgery));

            _tokenFactoryService = tokenFactoryService;
            _tokenFactoryService.CheckArgumentIsNull(nameof(tokenFactoryService));
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]  LoginModel loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("user is not set.");
            }
            var user = await _usersService.FindUserAsync(loginUser.Username, loginUser.Password);
            if (user?.F_IsActive != true)
            {
                return Content(new AjaxResult
                {
                    state = ResultType.error.ToString(),
                    message = "登录失败！"
                }.ToJson());
            }

            var result = await _tokenFactoryService.CreateJwtTokensAsync(user);
            await _tokenStoreService.AddUserTokenAsync(user, result.RefreshTokenSerial, result.AccessToken, null);
            await _uow.SaveChangesAsync();

            _antiforgery.RegenerateAntiForgeryCookies(result.Claims);

            //return Ok(new { access_token = result.AccessToken, refresh_token = result.RefreshToken });
            return Content(new AjaxResult
            {
                state = ResultType.success.ToString(),
                message = "登录成功。",
                data = new
                {
                    access_token = result.AccessToken,
                    refresh_token = result.RefreshToken
                }
            }.ToJson());
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public async Task<IActionResult> CheckLogin([FromBody]  LoginModel loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("user is not set.");
            }
            var user = await _usersService.FindUserAsync(loginUser.Username, loginUser.Password);
            if (user?.F_IsActive != true)
            {
                return Content(new AjaxResult
                {
                    state = ResultType.error.ToString(),
                    message = "登录失败！"
                }.ToJson());
            }

            var result = await _tokenFactoryService.CreateJwtTokensAsync(user);
            await _tokenStoreService.AddUserTokenAsync(user, result.RefreshTokenSerial, result.AccessToken, null);
            await _uow.SaveChangesAsync();

            _antiforgery.RegenerateAntiForgeryCookies(result.Claims);

            return Ok(new
            {
                access_token = result.AccessToken,
                refresh_token = result.RefreshToken
            });
        }

        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody]Token model)
        {
            var refreshTokenValue = model.RefreshToken;
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return BadRequest("refreshToken is not set.");
            }

            var token = await _tokenStoreService.FindTokenAsync(refreshTokenValue);
            if (token == null)
            {
                return Unauthorized();
            }

            var result = await _tokenFactoryService.CreateJwtTokensAsync(token.User);
            await _tokenStoreService.AddUserTokenAsync(token.User, result.RefreshTokenSerial, result.AccessToken, _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue));
            await _uow.SaveChangesAsync();

            _antiforgery.RegenerateAntiForgeryCookies(result.Claims);

            return Ok(new { access_token = result.AccessToken, refresh_token = result.RefreshToken });
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdValue = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            await _tokenStoreService.RevokeUserBearerTokensAsync(userIdValue, refreshToken);
            await _uow.SaveChangesAsync();

            _antiforgery.DeleteAntiForgeryCookies();

            return Ok(true);
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public IActionResult IsAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var data = await _usersService.FindUserAsync(
                claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
            return Ok(data);
        }
    }
}
 