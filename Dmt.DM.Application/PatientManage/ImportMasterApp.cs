using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IImportMasterApp : IScopedAppService
    {
        Task<List<ImportMasterEntity>> GetList(Pagination pagination, string keyword);
        Task<List<ImportMasterEntity>> GetList();
        Task<ImportMasterEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(ImportMasterEntity entity);
        Task<string> SubmitForm(ImportMasterEntity entity, string keyValue);
    }

    public class ImportMasterApp : IImportMasterApp
    {
        private readonly IRepository<ImportMasterEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IUsersService _usersService;

        public ImportMasterApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<ImportMasterEntity>();
            _usersService = usersService;
        }

        public Task<List<ImportMasterEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ImportMasterEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ImpType == keyword);
                expression = expression.Or(t => t.F_ImpNo.Contains(keyword));
                //expression = expression.Or(t => t.F_DrugSpell.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<ImportMasterEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }
        public Task<ImportMasterEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(ImportMasterEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }
        public Task<string> SubmitForm(ImportMasterEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                UpdateForm(entity);
            }
            else
            {
                entity.Create();
                //记账标识
                if (entity.F_IsAcct == null) entity.F_IsAcct = false;
                if (entity.F_ImpDate == null) entity.F_ImpDate = DateTime.Now;
                if (entity.F_EnabledMark == null) entity.F_EnabledMark = true;
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                _service.Insert(entity);
            }
            return Task.FromResult(entity.F_Id);
        }
    }
}
