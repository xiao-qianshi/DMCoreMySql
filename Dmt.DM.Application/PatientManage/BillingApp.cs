using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint;
using Dmt.DM.Mapper.Dto.Billing;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IBillingApp : IScopedAppService
    {
        Task<List<BillingEntity>> GetList(Pagination pagination, string keyword, string patientId, DateTime startDate, DateTime endDate, int classType, int statusType);
        IQueryable<BillingEntity> GetQueryable();
        Task<BillingEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(BillingEntity entity);
        Task<int> SubmitForm<TDto>(BillingEntity entity, TDto dto) where TDto: class;
        Task<int> SubmitFormBatch(List<BillingEntity> list);
        Task<int> InsertList(List<BillingEntity> list);
        Task<int> DisableForm(string keyValue);
        Task<string> GetReport(string patientId, string keyword, DateTime startDate, DateTime endDate, int classType, int statusType);
        Task<float> CreateBatch(AddFeeInput input);
        IQueryable<BillingEntity> GetFilterFee(string patientId, string keyword, DateTime startDate, DateTime endDate, int classType, int statusType);
    }

    public class BillingApp : IBillingApp
    {
        private readonly IRepository<BillingEntity> _service = null;
        private IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;
        private readonly IPatientApp _patientApp = null;
        private readonly IOrganizeApp _organizeApp = null;
        private readonly IUsersService _usersService = null;

        public BillingApp(IUnitOfWork uow, IHttpContextAccessor httpContext, IPatientApp patientApp, IOrganizeApp organizeApp, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<BillingEntity>();
            _httpContext = httpContext;
            _patientApp = patientApp;
            _organizeApp = organizeApp;
            _usersService = usersService;
        }
       
        public Task<List<BillingEntity>> GetList(Pagination pagination, string keyword, string patientId, DateTime startDate, DateTime endDate, int classType, int statusType)
        {
            var expression = ExtLinq.True<BillingEntity>();
            //endDate = endDate.AddDays(1);
            expression = expression.And(t => t.F_BillingDateTime >= startDate).And(t => t.F_BillingDateTime <= endDate);
            expression = expression.And(t => t.F_Pid == patientId);
            if (!string.IsNullOrEmpty(keyword)) expression = expression.And(t => t.F_ItemName.Contains(keyword) || t.F_ItemCode.Contains(keyword));
            switch (classType)
            {
                case 1:
                    expression = expression.Or(t => t.F_ItemClass=="药品");
                    break;
                case 2:
                    expression = expression.Or(t => t.F_ItemClass == "耗材");
                    break;
                case 4:
                    expression = expression.Or(t => t.F_ItemClass == "诊疗");
                    break;

                case 3:
                    expression = expression.Or(t => t.F_ItemClass == "药品" || t.F_ItemClass == "耗材");
                    break;
                case 5:
                    expression = expression.Or(t => t.F_ItemClass == "药品" || t.F_ItemClass == "诊疗");
                    break;
                case 6:
                    expression = expression.Or(t => t.F_ItemClass == "耗材" || t.F_ItemClass == "诊疗");
                    break;
            }
            switch (statusType)
            {
                case 1:
                    expression = expression.Or(t => t.F_IsAcct == true);
                    break;
                case 2:
                    expression = expression.Or(t => t.F_IsAcct == false);
                    break;
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }


        public Task<BillingEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(BillingEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public IQueryable<BillingEntity> GetQueryable()
        {
            return _service.IQueryable();
        }

        public Task<int> SubmitForm<TDto>(BillingEntity entity, TDto dto) where TDto: class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _usersService?.GetCurrentUserId();
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService?.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

        public Task<int> SubmitFormBatch(List<BillingEntity> list)
        {

            foreach (var entity in list)
            {
                entity.Create();
                entity.F_CreatorTime=DateTime.Now;
                entity.F_CreatorUserId = _usersService?.GetCurrentUserId();
                entity.F_IsAcct = false;
                entity.F_EnabledMark = true;
            }
            return _service.InsertAsync(list);
        }
        
        public Task<int> InsertList(List<BillingEntity> list)
        {
            return _service.InsertAsync(list);
        }

        private string GetBillReport(List<FeeCategory> categories)
        {
            var reportFilePath = Path.Combine(AppConsts.AppRootPath, "ReportFiles", "Bill.frx");
            var dataSetName = "FeeCategories";
            var exportFormat = "html";
            return FastReportHelper.GetReportString(reportFilePath, dataSetName, categories, exportFormat);
        }

        public async Task<int> DisableForm(string keyValue)
        {
            var user = await _usersService.GetCurrentUserAsync();
            var entity = await _service.FindEntityAsync(keyValue);
            if (entity == null) return 0;
            entity.F_EnabledMark = false;
            entity.F_DisabledPerson = user?.F_RealName;
            return await _service.UpdatePartialAsync(entity);
        }

        public async Task<string> GetReport(string patientId, string keyword, DateTime startDate, DateTime endDate, int classType, int statusType)
        {
            var list = GetFilterFee(patientId, keyword, startDate, endDate.AddDays(1), classType, statusType);
            var feeCategory = new FeeCategory();
            foreach (var item in list.Select(t=>new
            {
                t.F_ItemClass,t.F_ItemId,t.F_ItemCode,t.F_ItemName,t.F_ItemSpec,t.F_Amount,t.F_Charges,t.F_Costs,t.F_ItemUnit
            }))
            {
                var billsum = feeCategory.BillSummaries.FirstOrDefault(t => t.FeeType == item.F_ItemClass);
                if (billsum == null)
                {
                    billsum = new BillSummary
                    {
                        FeeType = item.F_ItemClass,
                        Costs = 0f,
                        BillDetails = new List<BillDetail>()
                    };
                    feeCategory.BillSummaries.Add(billsum);
                }
                billsum.Costs = billsum.Costs + item.F_Costs.ToFloat(2);
                var bill = billsum.BillDetails.FirstOrDefault(t => t.ItemId == item.F_ItemId);
                if (bill == null)
                {
                    bill = new BillDetail
                    {
                        ItemId = item.F_ItemId,
                        ItemCode = item.F_ItemCode,
                        ItemName = item.F_ItemName,
                        ItemSpec = item.F_ItemSpec,
                        Amount = item.F_Amount.ToFloat(2),
                        Charges = item.F_Charges.ToFloat(2),
                        Price = item.F_Charges.ToFloat(2),
                        Costs = item.F_Costs.ToFloat(2),
                        Unit = item.F_ItemUnit
                    };
                    billsum.BillDetails.Add(bill);
                }
                else
                {
                    bill.Amount = bill.Amount + item.F_Amount.ToFloat(2);
                    bill.Charges = bill.Charges + item.F_Charges.ToFloat(2);
                    bill.Costs = bill.Costs + item.F_Costs.ToFloat(2);
                }
            }
            //基本信息
            var first = list.FirstOrDefault();
            if (first != null)
            {
                var patient = await _patientApp.GetForm(patientId);
                if (feeCategory.BirthDay != null)
                {
                    feeCategory.BirthDay = patient.F_BirthDay.ToChineseDateString();
                    feeCategory.AgeString = ((int)((DateTime.Now - patient.F_BirthDay.ToDate()).TotalDays / 365)).ToString() + "岁";
                }

                feeCategory.Costs = list.Sum(t => t.F_Costs).ToFloat(2);
                feeCategory.DialysisNo = patient.F_DialysisNo.ToString();
                feeCategory.EndDate = list.Max(t => t.F_BillingDateTime).ToChineseDateString(); //json.Value<DateTime>("endDate").ToChineseDateString();
                feeCategory.Gender = patient.F_Gender;
  
                feeCategory.HospialName = await _organizeApp.GetHospitalName();
                feeCategory.HospialLogo = await _organizeApp.GetHospitalLogo();
                feeCategory.IdNo = patient.F_IdNo;
                feeCategory.InsuranceNo = patient.F_InsuranceNo;
                feeCategory.Name = patient.F_Name;
                feeCategory.PatientNo = patient.F_PatientNo;
                feeCategory.RecordNo = patient.F_RecordNo;
                feeCategory.StartDate = list.Min(t => t.F_BillingDateTime).ToChineseDateString();//json.Value<DateTime>("startDate").ToChineseDateString();

            }

            return GetBillReport(new List<FeeCategory> {feeCategory});
        }

        public async Task<float> CreateBatch(AddFeeInput input)
        {
            var list = new List<BillingEntity>();
            var patient = await _uow.GetRepository<PatientEntity>().FindEntityAsync(input.PatientId);
            var date = DateTime.Now;
            var user = await _usersService.GetCurrentUserAsync();
            foreach (var item in input.Items)
            {
                switch (item.BillType)
                {
                    case 1:
                        if (item.Amount > 0)
                        {
                            var dict = await _uow.GetRepository<DrugsEntity>().FindEntityAsync(item.ItemId);

                            list.Add(new BillingEntity
                            {
                                F_Amount = item.Amount,
                                F_BillingDateTime = date,
                                F_BillingPerson = user?.F_RealName,
                                F_BillingPersonId = user?.F_Id,
                                F_Charges = dict.F_Charges,
                                F_Costs = (dict.F_Charges * item.Amount).ToFloat(2),
                                F_DialylisNo = patient.F_DialysisNo,
                                F_Pid = patient.F_Id,
                                F_PGender = patient.F_Gender,
                                F_PName = patient.F_Name,
                                F_ItemClass = "药品",
                                F_ItemCode = dict.F_DrugCode,
                                F_ItemId = dict.F_Id,
                                F_ItemName = dict.F_DrugName,
                                F_ItemSpec = dict.F_DrugSpec,
                                F_ItemSpell = dict.F_DrugSpell,
                                F_ItemUnit = dict.F_DrugUnit,
                                F_Supplier = dict.F_DrugSupplier
                            });
                        }
                        break;
                    case 2:
                        if (item.Amount > 0)
                        {
                            var dict = await _uow.GetRepository<MaterialEntity>().FindEntityAsync(item.ItemId);

                            list.Add(new BillingEntity
                            {
                                F_Amount = item.Amount,
                                F_BillingDateTime = date,
                                F_BillingPerson = user?.F_RealName,
                                F_BillingPersonId = user?.F_Id,
                                F_Charges = dict.F_Charges,
                                F_Costs = (dict.F_Charges * item.Amount).ToFloat(2),
                                F_DialylisNo = patient.F_DialysisNo,
                                F_Pid = patient.F_Id,
                                F_PGender = patient.F_Gender,
                                F_PName = patient.F_Name,
                                F_ItemClass = "耗材",
                                F_ItemCode = dict.F_MaterialCode,
                                F_ItemId = dict.F_Id,
                                F_ItemName = dict.F_MaterialName,
                                F_ItemSpec = dict.F_MaterialSpec,
                                F_ItemSpell = dict.F_MaterialSpell,
                                F_ItemUnit = dict.F_MaterialUnit,
                                F_Supplier = dict.F_MaterialSupplier
                            });
                        }
                        break;
                    case 3:
                        if (item.Amount > 0)
                        {
                            var dict = await _uow.GetRepository<TreatmentEntity>().FindEntityAsync(item.ItemId);

                            list.Add(new BillingEntity
                            {
                                F_Amount = item.Amount,
                                F_BillingDateTime = date,
                                F_BillingPerson = user?.F_RealName,
                                F_BillingPersonId = user?.F_Id,
                                F_Charges = dict.F_Charges,
                                F_Costs = (dict.F_Charges * item.Amount).ToFloat(2),
                                F_DialylisNo = patient.F_DialysisNo,
                                F_Pid = patient.F_Id,
                                F_PGender = patient.F_Gender,
                                F_PName = patient.F_Name,
                                F_ItemClass = "诊疗",
                                F_ItemCode = dict.F_TreatmentCode,
                                F_ItemId = dict.F_Id,
                                F_ItemName = dict.F_TreatmentName,
                                F_ItemSpec = dict.F_TreatmentSpec,
                                F_ItemSpell = dict.F_TreatmentSpell,
                                F_ItemUnit = dict.F_TreatmentUnit
                            });
                        }
                        break;
                }
            }

            await SubmitFormBatch(list);
            //扣减库存
            //if (type == "耗材") || type == "药品"))
            //{
            //    var storageList = (from r in NFine.Code.Json.ToJArrayObject(data)
            //                       select new
            //                       {
            //                           itemId = r.Value<string>("F_ItemId"),
            //                           count = r.Value<float>("F_Amount").ToInt()
            //                       }).ToList();
            //    new StorageApp().ConsumeStock(storageList.ToJson());
            //}
            return list.Sum(t => t.F_Costs * t.F_Amount).ToFloat(2);
        }

        public IQueryable<BillingEntity> GetFilterFee(string patientId, string keyword, DateTime startDate, DateTime endDate, int classType, int statusType)
        {
            var expression = ExtLinq.True<BillingEntity>();
           expression = expression.And(t => t.F_Pid == patientId);
            if (!string.IsNullOrEmpty(keyword)) expression = expression.And(t => t.F_ItemName.Contains(keyword) || t.F_ItemCode.Contains(keyword));
            switch (classType)
            {
                case 1:
                    expression = expression.Or(t => t.F_ItemClass == "药品");
                    break;
                case 2:
                    expression = expression.Or(t => t.F_ItemClass == "耗材");
                    break;
                case 4:
                    expression = expression.Or(t => t.F_ItemClass == "诊疗");
                    break;

                case 3:
                    expression = expression.Or(t => t.F_ItemClass == "药品" || t.F_ItemClass == "耗材");
                    break;
                case 5:
                    expression = expression.Or(t => t.F_ItemClass == "药品" || t.F_ItemClass == "诊疗");
                    break;
                case 6:
                    expression = expression.Or(t => t.F_ItemClass == "耗材" || t.F_ItemClass == "诊疗");
                    break;
            }
            switch (statusType)
            {
                case 1:
                    expression = expression.Or(t => t.F_IsAcct == true);
                    break;
                case 2:
                    expression = expression.Or(t => t.F_IsAcct == false);
                    break;
            }
            expression = expression.And(t => t.F_BillingDateTime >= startDate && t.F_BillingDateTime <= endDate);
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).OrderBy(t => t.F_ItemName);
        }
    }
}
