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
    public interface ITransferLogApp : IScopedAppService
    {
        Task<List<TransferLogEntity>> GetList(string pid);
        Task<TransferLogEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(TransferLogEntity entity);
        Task<int> SubmitForm(TransferLogEntity entity, string pid);
    }

    public class TransferLogApp : ITransferLogApp
    {
        private readonly IRepository<TransferLogEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public TransferLogApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<TransferLogEntity>();
            _httpContext = httpContext;
        }

        public Task<List<TransferLogEntity>> GetList(string pid)
        {
            var expression = ExtLinq.True<TransferLogEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<TransferLogEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(TransferLogEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm(TransferLogEntity entity, string pid)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            if (pid.IsEmpty()) return Task.FromResult(0);
            entity.Create();
            if (entity.F_EnabledMark == null)
            {
                entity.F_EnabledMark = true;
            }
            entity.F_CreatorUserId = claim?.Value;
            entity.F_Pid = pid;
            return _service.InsertAsync(entity);
        }
    }
}
