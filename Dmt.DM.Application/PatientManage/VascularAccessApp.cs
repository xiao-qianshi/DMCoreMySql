using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IVascularAccessApp : IScopedAppService
    {
        Task<List<VascularAccessEntity>> GetList(Pagination pagination, string keyword);
        Task<List<VascularAccessEntity>> GetList();
        Task<List<VascularAccessEntity>> GetList(string keyword);
        Task<VascularAccessEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(VascularAccessEntity entity);
        Task<int> SubmitForm<TDto>(VascularAccessEntity entity, TDto dto) where TDto : class;
        Task<int> InsertForm(VascularAccessEntity entity);
    }

    public class VascularAccessApp : IVascularAccessApp
    {
        private readonly IRepository<VascularAccessEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IUsersService _usersService = null;

        public VascularAccessApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<VascularAccessEntity>();
            _usersService = usersService;
        }

        public Task<List<VascularAccessEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<VascularAccessEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                //expression = expression.And(t => t.F_DrugCode.ToString().Contains(keyword));
                //expression = expression.Or(t => t.F_DrugName.Contains(keyword));
                //expression = expression.Or(t => t.F_VascularAccesspell.Contains(keyword));
                expression = expression.And(t => t.F_Pid == keyword);
            }
            else
            {
                expression = expression.And(t => t.F_Pid == "");
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<VascularAccessEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<List<VascularAccessEntity>> GetList(string keyword)
        {
            var expression = ExtLinq.True<VascularAccessEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Pid == keyword);
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).OrderByDescending(t => t.F_OperateTime).ToListAsync();
        }

        public Task<VascularAccessEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(VascularAccessEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(VascularAccessEntity entity, TDto dto) where TDto : class
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
                if (entity.F_EnabledMark == null)
                {
                    entity.F_EnabledMark = true;
                }
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

        public Task<int> InsertForm(VascularAccessEntity entity)
        {
            if (entity.F_EnabledMark == null)
            {
                entity.F_EnabledMark = true;
            }
            entity.F_CreatorUserId = _usersService.GetCurrentUserId();
            return _service.InsertAsync(entity);
        }
    }
}
