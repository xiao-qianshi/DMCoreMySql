using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.SystemManage.User;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class UserController : BaseController
    {
        private readonly IUsersService _userApp = null;
        private readonly IMapper _mapper = null;

        public UserController(IUsersService userApp,IMapper mapper)
        {
            _userApp = userApp;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _userApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _userApp.FindUserAsync(keyValue);
            return Content(data.ToJson());
        }

        public IActionResult GetUserNameJson(string keyValue)
        {
            var data = _userApp.GetUserNameDict(keyValue).Select(t => new
            {
                id = t.F_RealName,
                text = t.F_RealName + "(" + t.F_Account + ")"
            }).ToList();
            return Content(data.ToJson());
        }

        [HttpGet]
        public IActionResult GetUserSelectJson(string keyValue)
        {
            var data = _userApp.GetUserNameDict(keyValue).Select(r => new
            {
                id = r.F_Id,
                text = r.F_RealName + "(" + r.F_Account + ")"
            }).ToList();
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<UserDto> input)
        {
            UserEntity entity = null;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<UserEntity>(input.Entity);
            }
            else
            {
                input.Entity.F_Password = null;
                entity = await _userApp.FindUserAsync(input.KeyValue);
            }

            await _userApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            var userEntity = await _userApp.FindUserAsync(input.KeyValue);
            userEntity.CheckArgumentIsNull(nameof(userEntity));
            userEntity.F_EnabledMark = false;
            userEntity.F_IsActive = false;
            await _userApp.UpdateForm(userEntity);
            return Success("删除成功。");
        }
        [HttpGet]
        public IActionResult RevisePassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRevisePassword([FromBody]RevisePasswordInput input)
        {
            var userEntity = await _userApp.FindUserAsync(input.KeyValue);
            userEntity.CheckArgumentIsNull(nameof(userEntity));
            var (succeeded, error) = await _userApp.RevisePasswordAsync(userEntity, input.UserPassword);
            return succeeded ? Success("重置密码成功") : Error(error);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitChangePassword([FromBody]ChangePasswordInput input)
        {
            var userId = _userApp.GetCurrentUserId();
            userId.CheckArgumentIsNull(nameof(userId));
            var userEntity = await _userApp.FindUserAsync(userId);
            userEntity.CheckArgumentIsNull(nameof(userEntity));
            var (succeeded, error) =
                await _userApp.ChangePasswordAsync(userEntity, input.OldUserPassword, input.UserPassword);
            return succeeded ? Success("重置密码成功") : Error(error);
        }

        [HttpPost]
        public async Task<IActionResult> DisabledAccount([FromBody]BaseInput input)
        {
            var userEntity = await _userApp.FindUserAsync(input.KeyValue);
            userEntity.CheckArgumentIsNull(nameof(userEntity));
            userEntity.F_EnabledMark = false;
            userEntity.F_IsActive = false;
            await _userApp.UpdateForm(userEntity);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        public async Task<IActionResult> EnabledAccount([FromBody]BaseInput input)
        {
            var userEntity = await _userApp.FindUserAsync(input.KeyValue);
            userEntity.CheckArgumentIsNull(nameof(userEntity));
            userEntity.F_EnabledMark = true;
            userEntity.F_IsActive = true;
            await _userApp.UpdateForm(userEntity);
            return Success("账户启用成功。");
        }

        [HttpGet]
        public IActionResult Info()
        {
            return View();
        }

    }
}
