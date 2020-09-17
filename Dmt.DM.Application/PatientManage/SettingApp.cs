using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface ISettingApp : IScopedAppService
    {
        Task<List<SettingEntity>> GetList(Pagination pagination, string keyword);
        Task<List<SettingEntity>> GetList();
        Task<List<SettingEntity>> GetSelectList(string keyword = "");
        Task<List<SettingEntity>> GetList(string keyword = "");
        Task<SettingEntity> GetFormByType(string keyValue);
        Task<SettingEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(SettingEntity entity, bool isPartialUpdate = false);
        Task<int> SubmitForm<TDto>(SettingEntity entity, TDto dto) where TDto : class;
        Task<int> InsertForm(SettingEntity entity);
    }

    public class SettingApp : ISettingApp
    {
        private readonly IRepository<SettingEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;

        public SettingApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<SettingEntity>();
            _usersService = usersService;
        }

        public Task<List<SettingEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<SettingEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Pid == keyword);
            }
            else
            {
                expression = expression.And(t => t.F_Pid == "");
            }

            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<SettingEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<List<SettingEntity>> GetSelectList(string keyword = "")
        {
            var expression = ExtLinq.True<SettingEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Pid.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<List<SettingEntity>> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<SettingEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Pid.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression)./*OrderBy(t => t.F_DrugName)*/ToListAsync();
        }

        public Task<SettingEntity> GetFormByType(string keyValue)
        {
            return _service.FindEntityAsync(t => t.F_DialysisType == keyValue);
        }


        public Task<SettingEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            //_service.Delete(t => t.F_Id == keyValue);
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(SettingEntity entity, bool isPartialUpdate = false)
        {
            //if (entity.F_EnabledMark == null)
            //{
            //    entity.F_EnabledMark = true;
            //}
            return _service.UpdateAsync(entity, isPartialUpdate);
        }

        public Task<int> SubmitForm<TDto>(SettingEntity entity, TDto dto) where TDto : class
        {
            var userId = _usersService.GetCurrentUserId();
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = userId;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = userId;
                if (entity.F_EnabledMark == null)
                {
                    entity.F_EnabledMark = true;
                }
                return _service.InsertAsync(entity);
            }
        }

        public Task<int> InsertForm(SettingEntity entity)
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
