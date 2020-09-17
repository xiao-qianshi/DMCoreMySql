using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IConclusionTemplateApp : IScopedAppService
    {
        Task<List<ConclusionTemplateEntity>> GetList(Pagination pagination, string keyword);
        IQueryable<ConclusionTemplateEntity> GetSelectList(string keyword = "");
        IQueryable<ConclusionTemplateEntity> GetList(string userId);
        Task<ConclusionTemplateEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(ConclusionTemplateEntity entity);
        Task<int> InsertForm(ConclusionTemplateEntity entity);
        Task<int> SubmitForm<TDto>(ConclusionTemplateEntity entity, TDto dto) where TDto:class;
    }

    public class ConclusionTemplateApp : IConclusionTemplateApp
    {
        private readonly IRepository<ConclusionTemplateEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;
        private IUsersService _usersService = null;

        public ConclusionTemplateApp(IUnitOfWork uow, IHttpContextAccessor httpContext,IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<ConclusionTemplateEntity>();
            _httpContext = httpContext;
            _usersService = usersService;
        }

        public Task<List<ConclusionTemplateEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ConclusionTemplateEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Title.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public IQueryable<ConclusionTemplateEntity> GetSelectList(string keyword = "")
        {
            var expression = ExtLinq.True<ConclusionTemplateEntity>();
            if (!string.IsNullOrEmpty(keyword)) expression = expression.And(t => t.F_Title.Contains(keyword));
            expression = expression.And(t => t.F_EnabledMark == true)
               .And(t => t.F_DeleteMark == null);
            //var userCode = OperatorProvider.Provider.GetCurrent().UserId;
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            var userCode = claim?.Value;
            expression = expression.And(t => t.F_IsPrivate != true || t.F_CreatorUserId == userCode);
            return _service.IQueryable(expression);
        }

        public IQueryable<ConclusionTemplateEntity> GetList(string userId)
        {
            var expression = ExtLinq.True<ConclusionTemplateEntity>();
            expression = expression.And(t => t.F_EnabledMark == true)
               .And(t => t.F_DeleteMark == null);
            expression = expression.And(t => t.F_IsPrivate != true || t.F_CreatorUserId == userId);
            return _service.IQueryable(expression);
        }

        public Task<ConclusionTemplateEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(ConclusionTemplateEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> InsertForm(ConclusionTemplateEntity entity)
        {
            return _service.InsertAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(ConclusionTemplateEntity entity, TDto dto) where TDto:class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                entity.F_EnabledMark = true;
                return _service.InsertAsync(entity);
            }
        }

    }
}
