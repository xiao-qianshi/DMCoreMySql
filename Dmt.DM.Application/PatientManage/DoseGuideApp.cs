using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IDoseGuideApp : IScopedAppService
    {
        Task<List<DoseGuideEntity>> GetList(Pagination pagination, string keyword);
        IEnumerable<DoseGuideEntity> GetList();
        Task<DoseGuideEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(DoseGuideEntity entity);
        Task<int> SubmitForm<TDto>(DoseGuideEntity entity, TDto dto) where TDto:class;
    }

    public class DoseGuideApp : IDoseGuideApp
    {
        private readonly IRepository<DoseGuideEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IUsersService _usersService = null;

        public DoseGuideApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<DoseGuideEntity>();
            _usersService = usersService;
        }

        public Task<List<DoseGuideEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<DoseGuideEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_DrugEnName.Contains(keyword));
                expression = expression.Or(t => t.F_DrugName.Contains(keyword));
                expression = expression.Or(t => t.F_Py.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public IEnumerable<DoseGuideEntity> GetList()
        {
            return _service.IQueryable();
        }

        public Task<DoseGuideEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(DoseGuideEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(DoseGuideEntity entity, TDto dto) where TDto:class
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
                entity.F_EnabledMark = true;
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }
    }
}
