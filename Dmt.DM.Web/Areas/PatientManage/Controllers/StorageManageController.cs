using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.StorageManage;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class StorageManageController : BaseController
    {
        private readonly IImportMasterApp _importMasterApp;
        private readonly IImportDetailApp _importDetailApp;
        private readonly IStorageApp _storageApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IDrugsApp _drugsApp;
        private readonly IMaterialApp _materialApp;

        public StorageManageController(
            IImportMasterApp importMasterApp, 
            IImportDetailApp importDetailApp,
            IStorageApp storageApp,
            IMapper mapper,
            IUsersService usersService,
            IDrugsApp drugsApp,
            IMaterialApp materialApp
            )
        {
            _importMasterApp = importMasterApp;
            _importDetailApp = importDetailApp;
            _storageApp = storageApp;
            _mapper = mapper;
            _usersService = usersService;
            _drugsApp = drugsApp;
            _materialApp = materialApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _importMasterApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetImportDetailGridJson(string keyValue)
        {
            var data = await _importDetailApp.GetList(keyValue);
            if (data.Count == 0)
            {
                data = new List<ImportDetailEntity> {
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0} ,
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0} ,
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0} ,
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0} ,
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0} ,
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0} ,
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0} ,
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0} ,
                    new ImportDetailEntity{F_Amount=1,F_TotalCharges=0,F_Charges=0}
                };
            }
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _importMasterApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存入库单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]ImportDto input)
        {
            ImportMasterEntity masterEntity;
            var details = new List<ImportDetailEntity>();
            if (input.KeyValue.IsEmpty()) //新增
            {
                masterEntity = new ImportMasterEntity
                {
                    F_Contacts = input.Master.F_Contacts,
                    F_Costs = input.Master.F_Costs,
                    F_ImpClass = input.Master.F_ImpClass,
                    F_ImpDate = input.Master.F_ImpDate?.ToDate()??DateTime.Now,
                    F_ImpNo = input.Master.F_ImpNo,
                    F_ImpType =input.Master.F_ImpType,
                    F_Storage = input.Master.F_Storage,
                    F_Supplier = input.Master.F_Storage,
                    F_SupplierPhone = input.Master.F_SupplierPhone
                };
            }
            else//修改
            {
                masterEntity = await _importMasterApp.GetForm(input.KeyValue);
                masterEntity.F_Contacts = input.Master.F_Contacts;
                masterEntity.F_Costs = input.Master.F_Costs;
                masterEntity.F_ImpClass = input.Master.F_ImpClass;
                masterEntity.F_ImpDate = input.Master.F_ImpDate?.ToDate() ?? DateTime.Now;
                masterEntity.F_ImpNo = input.Master.F_ImpNo;
                masterEntity.F_ImpType = input.Master.F_ImpType;
                masterEntity.F_Storage = input.Master.F_Storage;
                masterEntity.F_Supplier = input.Master.F_Storage;
                masterEntity.F_SupplierPhone = input.Master.F_SupplierPhone;
                
            }
            var masterId = await _importMasterApp.SubmitForm(masterEntity, input.KeyValue);
            switch (input.Master.F_ImpClass)
            {
                case "药品入库":
                    {
                        var drugs = (await _drugsApp.GetList()).ToList();
                        details = (from item in input.Details
                                   let amount = item.F_Amount.ToInt()
                                   where amount != 0
                                   let drug = drugs.FirstOrDefault(t => t.F_Id == item.F_ItemId)
                                   where drug != null
                                   select new ImportDetailEntity
                                   {
                                       F_Id = item.F_Id,
                                       F_ImpId = masterId,
                                       F_ItemId = item.F_ItemId,
                                       F_Amount = amount,
                                       F_Code = drug.F_DrugCode,
                                       F_Charges = drug.F_Charges,
                                       F_Name = drug.F_DrugName,
                                       F_Unit = drug.F_DrugUnit,
                                       F_Spec = drug.F_DrugSpec,
                                       F_TotalCharges = (drug.F_Charges * amount).ToFloat(2)
                                   }).ToList();

                        await _importDetailApp.InsertBatch(details);
                        break;
                    }
                case "耗材入库":
                    var materials = (await _materialApp.GetList()).ToList();
                    details = (from item in input.Details
                               let amount = item.F_Amount.ToInt()
                               where amount != 0
                               let material = materials.FirstOrDefault(t => t.F_Id == item.F_ItemId)
                               where material != null
                               select new ImportDetailEntity
                               {
                                   F_Id = item.F_Id,
                                   F_ImpId = masterId,
                                   F_ItemId = item.F_ItemId,
                                   F_Amount = amount,
                                   F_Code = material.F_MaterialCode,
                                   F_Charges = material.F_Charges,
                                   F_Name = material.F_MaterialName,
                                   F_Unit = material.F_MaterialUnit,
                                   F_Spec = material.F_MaterialSpec,
                                   F_TotalCharges = (material.F_Charges * amount).ToFloat(2)
                               }).ToList();

                    await _importDetailApp.InsertBatch(details);
                    break;
            }
            //更新主记录总金额
            masterEntity = await _importMasterApp.GetForm(masterId);
            masterEntity.F_Costs = details.Sum(t => t.F_TotalCharges);
            await _importMasterApp.UpdateForm(masterEntity);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _importMasterApp.DeleteForm(input.KeyValue);
            await _importDetailApp.DeleteBatch(input.KeyValue);
            return Success("删除成功。");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDetailForm([FromBody]BaseInput input)
        {
            await _importDetailApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AcctImport([FromBody]BaseInput input)
        {
            var master = await _importMasterApp.GetForm(input.KeyValue);
            var list = await _importDetailApp.GetList(master.F_Id);
            foreach (var item in list)
            {
                var storageEntity = await _storageApp.GetFormByItemId(item.F_ItemId) ?? new StorageEntity();
                if (storageEntity.F_Amount == null) storageEntity.F_Amount = 0;
                storageEntity.F_Amount = storageEntity.F_Amount.ToInt() + item.F_Amount.ToInt();
                storageEntity.F_Charges = item.F_Charges;
                storageEntity.F_Code = item.F_Code;
                storageEntity.F_ImpClass = master.F_ImpClass;
                storageEntity.F_ItemId = item.F_ItemId;
                storageEntity.F_Name = item.F_Name;
                storageEntity.F_Spec = item.F_Spec;
                storageEntity.F_Storage = master.F_Storage;
                storageEntity.F_Unit = item.F_Unit;
                storageEntity.F_TotalCharges = (storageEntity.F_Charges * storageEntity.F_Amount).ToFloat(2);
                await _storageApp.SubmitForm(storageEntity, storageEntity.F_Id);
            }
            //更改记账标识
            master.F_IsAcct = true;
            await _importMasterApp.SubmitForm(master, input.KeyValue);
            return Success("增加库存成功。");
        }

        [AllowAnonymous]
        public IActionResult Form1()
        {
            return View();
        }
    }
}
