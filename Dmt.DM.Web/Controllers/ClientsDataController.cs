using Dmt.DM.Application;
using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Controllers
{
    public class ClientsDataController : Controller
    {
        private readonly IDutyApp _dutyApp;
        private readonly IItemsApp _itemsApp;
        private readonly IItemsDetailApp _itemsDetailApp;
        private readonly IOrganizeApp _organizeApp;
        private readonly IRoleApp _roleApp;
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        private readonly IUsersService _usersService;

        private UserEntity user;

        public ClientsDataController(
            IItemsDetailApp itemsDetailApp, 
            IItemsApp itemsApp, 
            IOrganizeApp organizeApp,
            IRoleApp roleApp, 
            IDutyApp dutyApp, 
            IRoleAuthorizeApp roleAuthorizeApp, 
            IUsersService usersService)
        {
            _itemsDetailApp = itemsDetailApp;
            _itemsApp = itemsApp;
            _organizeApp = organizeApp;
            _roleApp = roleApp;
            _dutyApp = dutyApp;
            _roleAuthorizeApp = roleAuthorizeApp;
            _usersService = usersService;
        }

        public async Task<IActionResult> GetClientsDataJson()
        {
            user = await _usersService.GetCurrentUserAsync();
            if (user==null)
                return RedirectToAction("Index", "Login");

            var data = new
            {
                dataItems = await GetDataItemList(),
                organize = await GetOrganizeList(),
                role = await GetRoleList(),
                duty = await GetDutyList(),
                user = GetUserInfo(),
                authorizeMenu = await GetMenuList(),
                authorizeButton = await GetMenuButtonList()
            };
            return Content(data.ToJson());
        }

        private async Task<Dictionary<string, Dictionary<string, string>>> GetDataItemList()
        {
            var itemdata = await _itemsDetailApp.GetList();
            var dictionaryItem = new Dictionary<string, Dictionary<string, string>>();
            foreach (var item in await _itemsApp.GetList())
            {
                var dataItemList = itemdata.FindAll(t => t.F_ItemId.Equals(item.F_Id));
                var dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList) dictionaryItemList.Add(itemList.F_ItemCode, itemList.F_ItemName);
                dictionaryItem.Add(item.F_EnCode, dictionaryItemList);
            }

            return dictionaryItem;
        }

        private async Task<Dictionary<string, object>> GetOrganizeList()
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var item in await _organizeApp.GetList())
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }

            return dictionary;
        }

        private async Task<Dictionary<string, object>> GetRoleList()
        {
            var data = await _roleApp.GetList();
            var dictionary = new Dictionary<string, object>();
            foreach (var item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }

            return dictionary;
        }

        private async Task<Dictionary<string, object>> GetDutyList()
        {
            var data = await _dutyApp.GetList();
            var dictionary = new Dictionary<string, object>();
            foreach (var item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }

            return dictionary;
        }

        private OperatorModel GetUserInfo()
        {
            return new OperatorModel
            {
                UserId = user.F_Id,
                UserCode = user.F_Account,
                UserName = user.F_RealName,
                CompanyId = user.F_OrganizeId,
                DepartmentId = user.F_DepartmentId,
                RoleId = user.F_RoleId,
                LoginIPAddress = "",
                LoginIPAddressName = "",
                LoginToken = "",
                LoginTime = (DateTime) user.F_LastLoggedIn?.LocalDateTime,
                IsSystem = user.F_IsAdministrator ?? false
            };
        }

        private async Task<string> GetMenuList()
        {
            //var claim = _claimsIdentity?.FindFirst(t => t.Type.Equals("RoleId"));
            //claim.CheckArgumentIsNull(nameof(claim));
            var roleId = user.F_RoleId;// claim?.Value; 
            return ToMenuJson(await _roleAuthorizeApp.GetMenuList(roleId), "0");
        }

        private string ToMenuJson(List<ModuleEntity> data, string parentId)
        {
            var sbJson = new StringBuilder();
            sbJson.Append("[");
            var entitys = data.FindAll(t => t.F_ParentId == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    var strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.F_Id) + "");
                    sbJson.Append(strJson + ",");
                }

                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }

            sbJson.Append("]");
            return sbJson.ToString();
        }

        private async Task<Dictionary<string, object>> GetMenuButtonList()
        {
            var roleId = user.F_RoleId; //_claimsIdentity.FindFirst(t => t.Type.Equals("RoleId"))?.Value;

            var data = await _roleAuthorizeApp.GetButtonList(roleId);
            var dataModuleId = data.Distinct(new ExtList<ModuleButtonEntity>("F_ModuleId"));
            var dictionary = new Dictionary<string, object>();
            foreach (var item in dataModuleId)
            {
                var buttonList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId));
                dictionary.TryAdd(item.F_ModuleId, buttonList);
            }

            return dictionary;
        }
    }
}