using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IBillingModelApp : IScopedAppService
    {
        Task<List<BillingModelEntity>> GetList(Pagination pagination, string dialysisType, string vascularAccess);
        Task<List<BillingModelEntity>> GetList();
        Task<List<BillingModelEntity>> GetList(string keyword = "");
        Task<BillingModelEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(BillingModelEntity entity);
        Task<int> SubmitForm<TDto>(BillingModelEntity entity, TDto dto) where TDto : class;
    }

    public class BillingModelApp : IBillingModelApp
    {
        private readonly IRepository<BillingModelEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;

        public BillingModelApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<BillingModelEntity>();
            _httpContext = httpContext;
        }

        public Task<List<BillingModelEntity>> GetList(Pagination pagination, string dialysisType, string vascularAccess)
        {
            var expression = ExtLinq.True<BillingModelEntity>();
            if (!string.IsNullOrEmpty(dialysisType))
            {
                expression = expression.And(t => t.F_DialysisType == dialysisType);
            }
            if (!string.IsNullOrEmpty(vascularAccess))
            {
                expression = expression.And(t => t.F_VascularAccess == vascularAccess);
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<BillingModelEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<List<BillingModelEntity>> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<BillingModelEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemCode.Contains(keyword));
                expression = expression.Or(t => t.F_ItemName.Contains(keyword));
            }
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).OrderBy(t => t.F_ItemName).ToListAsync();
        }

        public Task<BillingModelEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(BillingModelEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(BillingModelEntity entity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            claim.CheckArgumentIsNull(nameof(claim));

            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = claim?.Value;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = claim?.Value;
                return _service.InsertAsync(entity);
            }
        }
    }
}
