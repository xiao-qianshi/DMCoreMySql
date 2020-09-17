using AutoMapper;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.StorageManage;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class StockCheckController : BaseController
    {
        private readonly IStorageApp _storageApp;
        private readonly IStorageLogApp _storageLogApp;
        private readonly IMapper _mapper;
        private readonly IOrganizeApp _organizeApp;

        public StockCheckController(IStorageApp storageApp, IStorageLogApp storageLogApp,IMapper mapper, IOrganizeApp organizeApp)
        {
            _storageApp = storageApp;
            _storageLogApp = storageLogApp;
            _mapper = mapper;
            _organizeApp = organizeApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _storageApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _storageApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        
        [HttpPost]
        public async Task<IActionResult> SetBalance([FromBody]SetBalanceInput input)
        {
            var entity = await _storageApp.GetForm(input.KeyValue);
            entity.F_MinAmount = input.MinAmount;
            entity.F_MaxAmount = input.MaxAmount;
            await _storageApp.UpdateForm(entity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _storageApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> ResetAmount([FromBody]BaseInput input)
        {
            await _storageApp.ClearStock(input.KeyValue);
            return Success("清除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCheckTime()
        {
            await _storageApp.ClearCheckTime();
            return Success("清除成功。");
        }


        [HttpPost]
        public async Task<IActionResult> SetCheckedAmount([FromBody]SetCheckedAmountInput input)
        {
            var entity = await _storageApp.GetForm(input.KeyValue);
            entity.F_RealAmount = input.Count;
            await _storageApp.UpdateForm(entity);
            return Success("更新成功。");
        }


        [HttpPost]
        public async Task<IActionResult> SaveCheckData()
        {
            await _storageLogApp.DeleteAll();
            var dt = DateTime.Now;
            var c = from r in await _storageApp.GetList()
                    where r.F_EnabledMark != false
                    select r;
            var list = new List<StorageLogEntity>();
            foreach (var entity in c)
            {
                var log = new StorageLogEntity();
                entity.F_CheckTime = dt;
                log.F_Id = entity.F_Id;
                log.F_ItemId = entity.F_ItemId;
                log.F_ImpClass = entity.F_ImpClass.Contains("药品") ? "药品" : "耗材";
                log.F_Storage = entity.F_Storage;
                log.F_Code = entity.F_Code;
                log.F_Name = entity.F_Name;
                log.F_Spec = entity.F_Spec;
                log.F_Unit = entity.F_Unit;
                log.F_Charges = entity.F_Charges;
                var count = entity.F_RealAmount.ToInt() - entity.F_Amount.ToInt();
                log.F_Amount = count;
                if (count < 0)
                {
                    log.F_CheckType = "盘亏";
                }
                else if (count > 0)
                {
                    log.F_CheckType = "盘盈";
                }
                else
                {
                    log.F_CheckType = "持平";
                }
                log.F_CheckTime = dt;
                log.F_TotalCharges = count * (log.F_Charges.ToFloat(2));
                log.F_RealAmount = entity.F_RealAmount.ToInt();
                list.Add(log);
                entity.F_Amount = entity.F_RealAmount.ToInt();
                entity.F_RealAmount = 0;
                entity.F_TotalCharges = ((entity.F_Amount.ToInt()) * (entity.F_Charges.ToFloat(2))).ToFloat(2);
                await _storageApp.UpdateForm(entity);
            }
            await _storageLogApp.InsertBatch(list);
            return Success("数据保存成功。");
        }

        /// <summary>
        /// 库存消耗
        /// </summary>
        /// <param name="data">itemId 项目代码    count  数量 </param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ConsumeStock([FromBody]BaseInput input)
        {
            _storageApp.ConsumeStock(input.KeyValue);
            return Success("数据保存成功。");
        }

        [HttpPost]
        public async Task<IActionResult> GetStockReport()
        {
            var category = new StorageCategory();
            var list = await _storageApp.GetList();

            category.HospialName = await _organizeApp.GetHospitalName();
            category.HospialLogo = await _organizeApp.GetHospitalLogo();
            category.PrintDate = DateTime.Now.ToChineseDateString();
            category.Costs = list.Sum(t => t.F_TotalCharges).ToFloat(2);

            foreach (var item in list)
            {
                var storagesum = category.StorageSummaries.FirstOrDefault(t => t.StorageType == item.F_ImpClass);
                if (storagesum == null)
                {
                    storagesum = new StorageSummary
                    {
                        StorageType = item.F_ImpClass,
                        Costs = 0f
                    };
                    category.StorageSummaries.Add(storagesum);
                }
                storagesum.Costs = storagesum.Costs + item.F_TotalCharges.ToFloat(2);

                var storage = storagesum.StorageDetails.FirstOrDefault(t => t.ItemId == item.F_ItemId);
                if (storage == null)
                {
                    storage = new StorageDetail
                    {
                        ItemId = item.F_ItemId,
                        ItemCode = item.F_Code,
                        ItemName = item.F_Name,
                        ItemSpec = item.F_Spec,
                        Amount = item.F_Amount.ToFloat(2),
                        Charges = item.F_TotalCharges.ToFloat(2),
                        Price = item.F_Charges.ToFloat(2),
                        Unit = item.F_Unit
                    };
                    storagesum.StorageDetails.Add(storage);
                }
                else
                {
                    storage.Amount += item.F_Amount.ToFloat(2);
                    storage.Charges += item.F_TotalCharges.ToFloat(2);
                }
            }

            return Content(await _storageApp.GetStorageReport(new List<StorageCategory> { category }));
        }

        [HttpPost]
        public async Task<IActionResult> GetStockLogReport()
        {
            StorageCategory category = new StorageCategory();
            var list = await _storageLogApp.GetList();
 
            category.HospialName = await _organizeApp.GetHospitalName();
            category.HospialLogo = await _organizeApp.GetHospitalLogo();
            var first = list.FirstOrDefault();
            category.PrintDate = first == null ? "" : first.F_CheckTime.ToChineseDateString();
            category.Costs = list.Sum(t => t.F_TotalCharges).ToFloat(2);

            foreach (var item in list)
            {
                var storagesum = category.StorageSummaries.FirstOrDefault(t => t.StorageType.Equals(item.F_ImpClass));
                if (storagesum == null)
                {
                    storagesum = new StorageSummary
                    {
                        StorageType = item.F_ImpClass,
                        Costs = 0f
                    };
                    category.StorageSummaries.Add(storagesum);
                }
                storagesum.Costs = storagesum.Costs + item.F_TotalCharges.ToFloat(2);

                var storage = storagesum.StorageDetails.FirstOrDefault(t => t.ItemId.Equals(item.F_ItemId));
                if (storage == null)
                {
                    storage = new StorageDetail
                    {
                        ItemId = item.F_ItemId,
                        ItemCode = item.F_Code,
                        ItemName = item.F_Name,
                        ItemSpec = item.F_Spec,
                        Amount = item.F_Amount.ToFloat(2),
                        Charges = item.F_TotalCharges.ToFloat(2),
                        Price = item.F_Charges.ToFloat(2),
                        Unit = item.F_Unit,
                        CheckResultType = item.F_CheckType,
                        RealAmount = item.F_RealAmount.ToInt()
                    };
                    storagesum.StorageDetails.Add(storage);
                }
                else
                {
                    storage.Amount += item.F_Amount.ToFloat(2);
                    storage.Charges += item.F_TotalCharges.ToFloat(2);
                }
            }



            return Content(await _storageApp.GetStorageLogReport(new List<StorageCategory> { category }));
        }

    }
}
