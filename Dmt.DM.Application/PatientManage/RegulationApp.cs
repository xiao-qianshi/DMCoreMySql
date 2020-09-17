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
    public interface IRegulationApp : IScopedAppService
    {
        Task<List<RegulationEntity>> GetList(Pagination pagination, string keyword);
        Task<List<RegulationEntity>> GetList();
        Task<RegulationEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(RegulationEntity entity);
        Task<int> SubmitForm(RegulationEntity entity, string keyValue);
        Task<int> SubmitFormBatch(List<RegulationEntity> list);
    }

    public class RegulationApp : IRegulationApp
    {
        private readonly IRepository<RegulationEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public RegulationApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<RegulationEntity>();
            _httpContext = httpContext;
        }

        public Task<List<RegulationEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<RegulationEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_RegulationName.Contains(keyword));
                //expression = expression.Or(t => t.F_ItemSpec.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<RegulationEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<RegulationEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(RegulationEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm(RegulationEntity entity, string keyValue)
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

        public Task<int> SubmitFormBatch(List<RegulationEntity> list)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            foreach (var entity in list)
            {
                entity.Create();
                entity.F_CreatorUserId = claim?.Value;
                entity.F_EnabledMark = true;
            }
            return _service.InsertAsync(list);
        }
    }
}
