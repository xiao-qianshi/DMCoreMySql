using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dmt.DM.Application.SystemManage
{
    public interface IRoleApp : IScopedAppService
    {
        Task<List<RoleEntity>> GetList(string keyword = "");
        Task<RoleEntity> GetForm(string keyValue);
        Task DeleteForm(string keyValue);
        Task SubmitForm(RoleEntity roleEntity, string[] permissionIds, string keyValue);
        Task SubmitForm<TDto>(RoleEntity roleEntity, string[] permissionIds, TDto dto) where TDto : class;
    }

    public class RoleApp : IRoleApp
    {
        private readonly IRepository<RoleEntity> _service = null;
        private readonly IRepository<RoleAuthorizeEntity> _serviceRA = null;
        private readonly ModuleApp _moduleApp = null;
        private readonly ModuleButtonApp _moduleButtonApp = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;

        public RoleApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _httpContext = httpContext;
            _service = _uow.GetRepository<RoleEntity>();
            _serviceRA = _uow.GetRepository<RoleAuthorizeEntity>();
            _moduleApp = new ModuleApp(uow, httpContext);
            _moduleButtonApp = new ModuleButtonApp(uow, httpContext);
        }

        public Task<List<RoleEntity>> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<RoleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_Category == 1);
            return _service.IQueryable(expression).OrderBy(t => t.F_SortCode).ToListAsync();
        }
        public Task<RoleEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public async Task DeleteForm(string keyValue)
        {
            //_service.BeginTrans();
            await _service.DeleteAsync(t => t.F_Id == keyValue, false);
            await _serviceRA.DeleteAsync(t => t.F_ObjectId == keyValue);
            //_service.CommitTrans();
        }
        public async Task SubmitForm(RoleEntity roleEntity, string[] permissionIds, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.F_Id = keyValue;
            }
            else
            {
                roleEntity.F_Id = Common.GuId();
            }
            var moduledata = await _moduleApp.GetList();
            var buttondata = await _moduleButtonApp.GetList();
            var roleAuthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                var roleAuthorizeEntity = new RoleAuthorizeEntity
                {
                    F_Id = Common.GuId(), F_ObjectType = 1, F_ObjectId = roleEntity.F_Id, F_ItemId = itemId
                };
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }
            //_service.SubmitForm(roleEntity, roleAuthorizeEntitys, keyValue);
            //_service.BeginTrans();
            if (!keyValue.IsEmpty())
            {
                await _service.UpdateAsync(roleEntity, false, false);
            }
            else
            {
                roleEntity.F_Category = 1;
                await _service.InsertAsync(roleEntity, false);
            }

            await _serviceRA.DeleteAsync(t => t.F_ObjectId == roleEntity.F_Id);
            await _serviceRA.InsertAsync(roleAuthorizeEntitys);
            //_service.CommitTrans();
        }

        public async Task SubmitForm<TDto>(RoleEntity roleEntity, string[] permissionIds, TDto dto) where TDto : class
        {
            var isAdd = false;
            if (string.IsNullOrEmpty(roleEntity.F_Id))
            {
                roleEntity.F_Id = Common.GuId();
                isAdd = true;
            }
            var moduledata = await _moduleApp.GetList();
            var buttondata = await _moduleButtonApp.GetList();
            var roleAuthorizeEntitys = new List<RoleAuthorizeEntity>();
            foreach (var itemId in permissionIds)
            {
                var roleAuthorizeEntity = new RoleAuthorizeEntity
                {
                    F_Id = Common.GuId(),
                    F_ObjectType = 1,
                    F_ObjectId = roleEntity.F_Id,
                    F_ItemId = itemId
                };
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }
            //_service.SubmitForm(roleEntity, roleAuthorizeEntitys, keyValue);
            //_service.BeginTrans();
            if (!isAdd)
            {
                await _service.UpdateAsync(roleEntity, dto);
            }
            else
            {
                roleEntity.F_Category = 1;
                await _service.InsertAsync(roleEntity, false);
            }

            await _serviceRA.DeleteAsync(t => t.F_ObjectId == roleEntity.F_Id);
            await _serviceRA.InsertAsync(roleAuthorizeEntitys);
            //_service.CommitTrans();
        }
    }
}
