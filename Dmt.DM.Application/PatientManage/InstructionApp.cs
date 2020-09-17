using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IInstructionApp : IScopedAppService
    {
        Task<List<InstructionEntity>> GetList(Pagination pagination, string keyword);
        Task<List<InstructionEntity>> GetList();
        Task<InstructionEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(InstructionEntity entity, bool isPartial = false);
        Task<int> SubmitForm(InstructionEntity entity, string keyValue);
    }

    public class InstructionApp : IInstructionApp
    {
        private readonly IRepository<InstructionEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public InstructionApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<InstructionEntity>();
            _httpContext = httpContext;
        }

        public Task<List<InstructionEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<InstructionEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_InstructionName.Contains(keyword));
                //expression = expression.Or(t => t.F_ItemSpec.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }


        public Task<List<InstructionEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<InstructionEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(InstructionEntity entity, bool isPartial = false)
        {
            return _service.UpdateAsync(entity, isPartial);
        }

        public Task<int> SubmitForm(InstructionEntity entity, string keyValue)
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
