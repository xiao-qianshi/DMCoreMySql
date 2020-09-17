using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface INutritionDietaryApp : IScopedAppService
    {
        Task<List<NutritionDietaryEntity>> GetList(Pagination pagination, string keyword);
        IEnumerable<NutritionDietaryEntity> GetList();
        Task<NutritionDietaryEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(NutritionDietaryEntity entity);
        Task<int> SubmitForm<TDto>(NutritionDietaryEntity entity, TDto dto) where TDto : class;
    }

    public class NutritionDietaryApp : INutritionDietaryApp
    {
        private readonly IRepository<NutritionDietaryEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IUsersService _usersService = null;

        public NutritionDietaryApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<NutritionDietaryEntity>();
            _usersService = usersService;
        }

        public Task<List<NutritionDietaryEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<NutritionDietaryEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Name.Contains(keyword));
                expression = expression.Or(t => t.F_Py.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public IEnumerable<NutritionDietaryEntity> GetList()
        {
            return _service.IQueryable();
        }

        public Task<NutritionDietaryEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(NutritionDietaryEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(NutritionDietaryEntity entity, TDto dto) where TDto : class
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
