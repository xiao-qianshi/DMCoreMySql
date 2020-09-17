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
    public interface IDialysisMachineApp:IScopedAppService
    {
        Task<List<DialysisMachineEntity>> GetList(Pagination pagination, string keyword);
        IQueryable<DialysisMachineEntity> GetQueryable();
        Task<List<DialysisMachineEntity>> GetList();
        Task<DialysisMachineEntity> GetMachineByBedNo(string keyword);
        Task<List<DialysisMachineEntity>> GetItemList(string enCode);
        Task<DialysisMachineEntity> GetForm(string keyValue);
        Task<DialysisMachineEntity> GetForm(string groupName, string bedNo);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(DialysisMachineEntity entity);
        Task<int> SubmitForm<TDto>(DialysisMachineEntity entity, TDto dto) where TDto : class;
    }

    public class DialysisMachineApp : IDialysisMachineApp
    {
        private readonly IRepository<DialysisMachineEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;

        public DialysisMachineApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<DialysisMachineEntity>();
            _httpContext = httpContext;
        }

        public Task<List<DialysisMachineEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<DialysisMachineEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_DialylisBedNo.Contains(keyword));
                expression = expression.Or(t => t.F_GroupName == keyword);
                //expression = expression.Or(t => t.F_MachineName.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public IQueryable<DialysisMachineEntity> GetQueryable()
        {
            var expression = ExtLinq.True<DialysisMachineEntity>();
            expression = expression.And(t => t.F_DeleteMark != true);
            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.IQueryable(expression);
        }

        public Task<List<DialysisMachineEntity>> GetList()
        {
            var expression = ExtLinq.True<DialysisMachineEntity>();
            expression = expression.And(t => t.F_DeleteMark != true);
            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.IQueryable(expression).ToListAsync();
        }

        /// <summary>
        /// 通过床号查询数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Task<DialysisMachineEntity> GetMachineByBedNo(string keyword)
        {
            var expression = ExtLinq.True<DialysisMachineEntity>();
            expression = expression.And(t => t.F_DialylisBedNo == keyword);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).FirstOrDefaultAsync();
        }

        public Task<List<DialysisMachineEntity>> GetItemList(string enCode)
        {
            //return _service.GetItemList(enCode);
            var expression = ExtLinq.True<DialysisMachineEntity>();
            if (!string.IsNullOrEmpty(enCode)) expression = expression.And(t => t.F_GroupName == enCode);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).OrderBy(r => r.F_GroupName).ToListAsync();

        }

        public Task<DialysisMachineEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }

        public Task<DialysisMachineEntity> GetForm(string groupName, string bedNo)
        {
            var expression = ExtLinq.True<DialysisMachineEntity>();
            expression = expression.And(t => t.F_DialylisBedNo == bedNo);
            expression = expression.And(t => t.F_GroupName == groupName);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).FirstOrDefaultAsync();
        }

        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(DialysisMachineEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(DialysisMachineEntity entity, TDto dto) where TDto : class
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
