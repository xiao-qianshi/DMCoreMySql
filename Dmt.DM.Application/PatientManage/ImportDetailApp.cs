using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IImportDetailApp : IScopedAppService
    {
        Task<List<ImportDetailEntity>> GetList(Pagination pagination, string keyword);
        Task<List<ImportDetailEntity>> GetList();

        /// <summary>
        /// 通过主记录Id查询
        /// </summary>
        /// <param name="keyValue">主记录ID</param>
        /// <returns></returns>
        Task<List<ImportDetailEntity>> GetList(string keyValue);

        Task<ImportDetailEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(ImportDetailEntity entity);
        Task<int> SubmitForm(ImportDetailEntity entity, string keyValue);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        Task<int> InsertBatch(List<ImportDetailEntity> entities);

        Task<int> DeleteBatch(string parentId);
    }

    public class ImportDetailApp : IImportDetailApp
    {
        private readonly IRepository<ImportDetailEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;

        public ImportDetailApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<ImportDetailEntity>();
            _usersService = usersService;
        }

        public Task<List<ImportDetailEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ImportDetailEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Code == keyword);
                expression = expression.Or(t => t.F_Name.Contains(keyword));
                //expression = expression.Or(t => t.F_DrugSpell.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<ImportDetailEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }
        /// <summary>
        /// 通过主记录Id查询
        /// </summary>
        /// <param name="keyValue">主记录ID</param>
        /// <returns></returns>
        public Task<List<ImportDetailEntity>> GetList(string keyValue)
        {
            var expression = ExtLinq.True<ImportDetailEntity>();
            expression = expression.And(t => t.F_ImpId == keyValue);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<ImportDetailEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
           return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(ImportDetailEntity entity)
        {
            return _service.UpdateAsync(entity);
        }
        public Task<int> SubmitForm(ImportDetailEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                return _service.UpdateAsync(entity);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        public async Task<int> InsertBatch(List<ImportDetailEntity> entities)
        {
            var count = 0;
            foreach (var item in entities)
            {
                count += await SubmitForm(item, item.F_Id);
            }
            return count;
        }

        public Task<int> DeleteBatch(string parentId)
        {
            return _service.DeleteAsync(t => t.F_ImpId == parentId);
        }

    }
}
