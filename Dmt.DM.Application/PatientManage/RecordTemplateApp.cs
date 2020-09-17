using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IRecordTemplateApp : IScopedAppService
    {
        Task<List<RecordTemplateEntity>> GetList(Pagination pagination, string userId);
        Task<RecordTemplateEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(RecordTemplateEntity entity);
        Task<int> InsertForm(RecordTemplateEntity entity);
        Task<int> SubmitForm(RecordTemplateEntity entity, string keyValue);
    }

    public class RecordTemplateApp : IRecordTemplateApp
    {
        private readonly IRepository<RecordTemplateEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public RecordTemplateApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<RecordTemplateEntity>();
            _httpContext = httpContext;
        }
        public Task<List<RecordTemplateEntity>> GetList(Pagination pagination, string userId)
        {
            var expression = ExtLinq.True<RecordTemplateEntity>();

            if (!string.IsNullOrEmpty(userId))
            {
                expression = expression.And(t => (t.F_CreatorUserId == userId && t.F_IsPrivate == true) || (t.F_IsPrivate == false));
            }
            else
            {
                expression = expression.And(t => t.F_IsPrivate == false);
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<RecordTemplateEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(RecordTemplateEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> InsertForm(RecordTemplateEntity entity)
        {
            return _service.InsertAsync(entity);
        }

        public Task<int> SubmitForm(RecordTemplateEntity entity, string keyValue)
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
                return _service.InsertAsync(entity);
            }
        }
    }
}
