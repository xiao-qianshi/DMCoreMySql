using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IStorageLogApp : IScopedAppService
    {
        Task<List<StorageLogEntity>> GetList();
        Task<StorageLogEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(StorageLogEntity entity);

        /// <summary>
        /// 删除所有数据
        /// </summary>
        Task<int> DeleteAll();

        /// <summary>
        /// 批量插入
        /// </summary>
        Task<int> InsertBatch(List<StorageLogEntity> list);
    }

    public class StorageLogApp : IStorageLogApp
    {
        private readonly IRepository<StorageLogEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public StorageLogApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<StorageLogEntity>();
            _httpContext = httpContext;
        }

        public Task<List<StorageLogEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }
        public Task<StorageLogEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(StorageLogEntity entity)
        {
            return _service.UpdateAsync(entity);
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public Task<int> DeleteAll()
        {
            return _service.DeleteAsync(t => t.F_Id.Length > 0);
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        public Task<int> InsertBatch(List<StorageLogEntity> list)
        {
            return _service.InsertAsync(list);
        }
    }
}
