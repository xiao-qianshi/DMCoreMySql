using Dmt.DM.Code;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Application.PatientManage
{
    public interface ISettingModelApp : IScopedAppService
    {
        /// <summary>
        /// 根据数据创建者ID查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SettingModelEntity>> GetList(string userId);

        Task<SettingModelEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(SettingModelEntity entity, bool isPartialUpdate = false);
        Task<int> SubmitForm(SettingModelEntity entity, string keyValue);
        Task<int> InsertForm(SettingModelEntity entity);
    }

    public class SettingModelApp : ISettingModelApp
    {
        private readonly IRepository<SettingModelEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public SettingModelApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<SettingModelEntity>();
            _httpContext = httpContext;
        }
        /// <summary>
        /// 根据数据创建者ID查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<SettingModelEntity>> GetList(string userId)
        {
            var expression = ExtLinq.True<SettingModelEntity>();
            expression = expression.And(t => t.F_CreatorUserId == userId);
            expression = expression.And(t => t.F_EnabledMark == true);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<SettingModelEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }

        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(SettingModelEntity entity, bool isPartialUpdate = false)
        {
            return _service.UpdateAsync(entity, isPartialUpdate);
        }

        public Task<int> SubmitForm(SettingModelEntity entity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.F_LastModifyUserId = claim?.Value;
                return _service.UpdateAsync(entity);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = claim?.Value;
                if (entity.F_EnabledMark == null)
                {
                    entity.F_EnabledMark = true;
                }
                return _service.InsertAsync(entity);
            }
        }

        public Task<int> InsertForm(SettingModelEntity entity)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            if (entity.F_EnabledMark == null)
            {
                entity.F_EnabledMark = true;
            }

            entity.F_CreatorUserId = claim?.Value;
            return _service.InsertAsync(entity);
        }

    }
}
