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
    public interface IBookApp : IScopedAppService
    {
        Task<List<BookEntity>> GetList(Pagination pagination, string keyword);
        Task<List<BookEntity>> GetList();
        Task<BookEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(BookEntity entity);
        Task<int> SubmitForm(BookEntity entity, string keyValue);
    }

    public class BookApp : IBookApp
    {
        private readonly IRepository<BookEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;

        public BookApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<BookEntity>();
            _httpContext = httpContext;
        }

        public Task<List<BookEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<BookEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_BookName.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<BookEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<BookEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(BookEntity entity)
        {
            return _service.UpdateAsync(entity);
        }
        
        public Task<int> SubmitForm(BookEntity entity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            claim.CheckArgumentIsNull(nameof(claim));

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
