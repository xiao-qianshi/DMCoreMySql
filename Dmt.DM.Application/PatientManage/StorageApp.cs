using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IStorageApp : IScopedAppService
    {
        Task<List<StorageEntity>> GetList(Pagination pagination, string keyword);
        Task<List<StorageEntity>> GetList();
        Task<StorageEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);

        /// <summary>
        /// 清空上次盘存时间
        /// </summary>
        Task ClearCheckTime();

        Task ClearStock(string keyValue);
        Task<StorageEntity> GetFormByItemId(string keyValue);
        Task<int> UpdateForm(StorageEntity entity);
        Task<int> SubmitForm(StorageEntity entity, string keyValue);
        Task<string> GetStorageReport(List<StorageCategory> categories);
        Task<string> GetStorageLogReport(List<StorageCategory> categories);

        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <param name="data"></param>
        Task<string> ConsumeStock(string data);
    }

    public class StorageApp : IStorageApp
    {
        private readonly IRepository<StorageEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IUsersService _usersService = null;

        public StorageApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<StorageEntity>();
            _usersService = usersService;
        }

        public Task<List<StorageEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<StorageEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Code == keyword);
                expression = expression.Or(t => t.F_Name.Contains(keyword));
                expression = expression.Or(t => t.F_Py.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<StorageEntity>> GetList()
        {
            var expression = ExtLinq.True<StorageEntity>();
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }
        public Task<StorageEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        /// <summary>
        /// 清空上次盘存时间
        /// </summary>
        public async Task ClearCheckTime()
        {
            foreach (var entity in await GetList())
            {
                entity.F_CheckTime = null;
                await UpdateForm(entity);
            }
        }

        public async Task ClearStock(string keyValue)
        {
            var entity = await GetForm(keyValue);
            if (entity != null)
            {
                entity.F_Amount = 0;
                entity.F_RealAmount = null;
                entity.F_TotalCharges = 0;
            }
            await UpdateForm(entity);
        }

        public Task<StorageEntity> GetFormByItemId(string keyValue)
        {
            return _service.FindEntityAsync(t => t.F_ItemId == keyValue);
        }

        public Task<int> UpdateForm(StorageEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }
        public Task<int> SubmitForm(StorageEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                return UpdateForm(entity);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

        public Task<string> GetStorageReport(List<StorageCategory> categories)
        {
            string reportFilePath = Path.Combine(AppConsts.AppRootPath,"upload" ,"reportfiles", "Storage.frx");//FileHelper.MapPath("\\ReportFiles\\Storage.frx");
            string dataSetName = "StorageCategories";
            string exportFormat = "html";
            return Task.FromResult(FastReportHelper.GetReportString(reportFilePath, dataSetName, categories, exportFormat));
        }

        public Task<string> GetStorageLogReport(List<StorageCategory> categories)
        {
            string reportFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "StorageLog.frx"); ;//FileHelper.MapPath("\\ReportFiles\\StorageLog.frx");
            string dataSetName = "StorageCategories";
            string exportFormat = "html";
            return Task.FromResult(FastReportHelper.GetReportString(reportFilePath, dataSetName, categories, exportFormat));
        }
        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <param name="data"></param>
        public async Task<string> ConsumeStock(string data)
        {
            string msg = string.Empty;
            foreach (var item in data.ToJArrayObject())
            {
                var itemId = item.Value<string>("itemId");
                var count = item.Value<int>("count");
                var entity = await GetFormByItemId(itemId);
                if (entity != null)
                {
                    entity.F_Amount = entity.F_Amount.ToInt() - count;
                    await UpdateForm(entity);
                }
                else
                {
                    //IStorageRepository service = new StorageRepository();
                }
            }
            return msg;
        }
    }
}
