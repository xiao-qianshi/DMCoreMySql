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
    public interface IIdeaWeightApp : IScopedAppService
    {
        Task<List<IdeaWeightEntity>> GetList(Pagination pagination, string keyword);
        Task<List<IdeaWeightEntity>> GetList(string keyword = "");
        Task<IdeaWeightEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(IdeaWeightEntity entity);
        Task<int> SubmitForm(IdeaWeightEntity entity, string keyValue);
    }

    public class IdeaWeightApp : IIdeaWeightApp
    {
        private readonly IRepository<IdeaWeightEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public IdeaWeightApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<IdeaWeightEntity>();
            _httpContext = httpContext;
        }

        public Task<List<IdeaWeightEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<IdeaWeightEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Pid == keyword);
                expression = expression.Or(t => t.F_Name.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<IdeaWeightEntity>> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<IdeaWeightEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Pid == keyword);
                expression = expression.Or(t => t.F_Name.Contains(keyword));
            }
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).OrderByDescending(t => t.F_CreatorTime).ToListAsync();
        }

        public Task<IdeaWeightEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
            //var entity = _service.FindEntity(keyValue);
            //entity.F_DeleteMark = true;
            //UpdateForm(entity);
        }

        public Task<int> UpdateForm(IdeaWeightEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm(IdeaWeightEntity entity, string keyValue)
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
