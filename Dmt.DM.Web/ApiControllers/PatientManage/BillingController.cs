using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.Dm.Domain.Dto.Billing;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto.Billing;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class BillingController : BaseApiController
    {
        private readonly IBillingApp _billingApp;
        private readonly IPatientApp _patientApp;
        private readonly IUsersService _usersService;
        private readonly IDrugsApp _drugsApp;
        private readonly IMaterialApp _materialApp;
        private readonly ITreatmentApp _treatmentApp;
        
        public BillingController(IBillingApp billingApp, IPatientApp patientApp, IUsersService usersService, IDrugsApp drugsApp, IMaterialApp materialApp, ITreatmentApp treatmentApp)
        {
            _billingApp = billingApp;
            _patientApp = patientApp;
            _usersService = usersService;
            _drugsApp = drugsApp;
            _materialApp = materialApp;
            _treatmentApp = treatmentApp;
        }

        public IActionResult GetList(GetListInput input)
        {
            var listSource =_billingApp.GetQueryable()
                .Where(t => t.F_Pid.Equals(input.patientId))
                .Where(t => t.F_BillingDateTime >= input.startDate)
                .Where(t => t.F_BillingDateTime <= input.endDate.AddDays(1))
                .Where(t => string.IsNullOrEmpty(input.billType) || t.F_ItemClass.Equals(input.billType))
                .Where(t => t.F_EnabledMark == true)
                .Where(t => t.F_DeleteMark != true)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_BillingDateTime,
                    t.F_BillingPerson,
                    t.F_ItemClass,
                    t.F_ItemCode,
                    t.F_ItemName,
                    t.F_ItemSpec,
                    t.F_ItemUnit,
                    t.F_Amount,
                    t.F_Costs
                })
                .ToList();
            var list = listSource.Select(t => new
            {
                t.F_Id,
                F_BillingDate = t.F_BillingDateTime.Date,
                t.F_BillingDateTime,
                t.F_BillingPerson,
                t.F_ItemClass,
                t.F_ItemCode,
                t.F_ItemName,
                t.F_ItemSpec,
                t.F_ItemUnit,
                F_Amount = t.F_Amount.ToFloat(2),
                F_Costs = t.F_Costs.ToFloat(2)
            });
            //合计费用
            var totalCosts = list.Sum(t => t.F_Costs);
            //费用日期
            var dates = list.Select(t => t.F_BillingDate).Distinct().ToList().OrderByDescending(t => t);
            var detail = new List<BillModel>();
            foreach (var date in dates)
            {
                var model = new BillModel
                {
                    billingDate = date
                };
                var filter = list.Where(t => t.F_BillingDate == date).OrderBy(t => t.F_ItemClass).ThenBy(t => t.F_BillingDateTime).ToList();
                foreach (var item in filter)
                {
                    model.items.Add(new BillItem
                    {
                        id = item.F_Id,
                        billingTime = item.F_BillingDateTime,
                        billingPerson = item.F_BillingPerson,
                        itemClass = item.F_ItemClass,
                        itemCode = item.F_ItemCode,
                        itemName = item.F_ItemName,
                        itemSpec = item.F_ItemSpec,
                        itemUnit = item.F_ItemUnit,
                        amount = item.F_Amount,
                        costs = item.F_Costs
                    });
                }
                model.sum = filter.Sum(t => t.F_Costs);
                detail.Add(model);
            }
            var data = new
            {
                totalCosts,
                rows = detail
            };
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddFee([FromBody]AddFeeInput input)
        {
            var patient = await _patientApp.GetForm(input.PatientId);
            if (patient == null)
            {
                return BadRequest("患者Id有误！");
            }
            var drugDict = _drugsApp;
            var materials = _materialApp;
            var treatments = _treatmentApp;
            var user = await _usersService.GetCurrentUserAsync();
            var billingDateTime = DateTime.Now;
            var totalCosts = 0f;
            var list = new List<BillingEntity>();
            foreach (var item in input.Items.FindAll(t => t.Amount > 0 && (t.BillType == 1 || t.BillType == 2 || t.BillType == 3)))
            {
                var entity = new BillingEntity
                {
                    F_Id = Common.GuId(),
                    F_Pid = input.PatientId,
                    F_DialylisNo = patient.F_DialysisNo,
                    F_PName = patient.F_Name,
                    F_PGender = patient.F_Gender,
                    F_BillingDateTime = billingDateTime,
                    F_BillingPersonId = user.F_Id,
                    F_BillingPerson = user.F_RealName,
                    F_CreatorTime = DateTime.Now,
                    F_CreatorUserId = user.F_Id,
                    F_EnabledMark = true
                };
                switch (item.BillType)
                {
                    case 1:
                        entity.F_ItemClass = "药品";
                        var findDrug = await drugDict.GetForm(item.ItemId);
                        if (findDrug != null)
                        {
                            entity.F_ItemId = item.ItemId;
                            entity.F_ItemCode = findDrug.F_DrugCode;
                            entity.F_ItemName = findDrug.F_DrugName;
                            entity.F_ItemSpec = findDrug.F_DrugSpec;
                            entity.F_ItemUnit = findDrug.F_DrugUnit;
                            entity.F_Amount = item.Amount;
                            entity.F_Charges = entity.F_Costs = item.Amount * findDrug.F_Charges;
                            entity.F_Supplier = findDrug.F_DrugSupplier;
                            list.Add(entity);
                        }
                        break;
                    case 2:
                        entity.F_ItemClass = "耗材";
                        var findMaterials = await materials.GetForm(item.ItemId);
                        if (findMaterials != null)
                        {
                            entity.F_ItemId = item.ItemId;
                            entity.F_ItemCode = findMaterials.F_MaterialCode;
                            entity.F_ItemName = findMaterials.F_MaterialName;
                            entity.F_ItemSpec = findMaterials.F_MaterialSpec;
                            entity.F_ItemUnit = findMaterials.F_MaterialUnit;
                            entity.F_Amount = item.Amount;
                            entity.F_Charges = entity.F_Costs = item.Amount * findMaterials.F_Charges;
                            entity.F_Supplier = findMaterials.F_MaterialSupplier;
                            list.Add(entity);
                        }
                        break;
                    case 3:
                        entity.F_ItemClass = "诊疗";
                        var treatmentEntity = await treatments.GetForm(item.ItemId);
                        if (treatmentEntity != null)
                        {
                            entity.F_ItemId = item.ItemId;
                            entity.F_ItemCode = treatmentEntity.F_TreatmentCode;
                            entity.F_ItemName = treatmentEntity.F_TreatmentName;
                            entity.F_ItemSpec = treatmentEntity.F_TreatmentSpec;
                            entity.F_ItemUnit = treatmentEntity.F_TreatmentUnit;
                            entity.F_Amount = item.Amount;
                            entity.F_Charges = entity.F_Costs = item.Amount * treatmentEntity.F_Charges;
                            //entity.F_Supplier = findtreatments;
                            list.Add(entity);
                        }
                        break;
                }
            }
            await _billingApp.InsertList(list);
            totalCosts = list.Sum(t => t.F_Costs).ToFloat(2);
            var data = new
            {
                totalCosts
            };
            return Ok(data);
        }

    }

    public class BillModel
    {
        public DateTime billingDate { get; set; }
        public float sum { get; set; }
        public List<BillItem> items { get; set; }
        public BillModel()
        {
            items = new List<BillItem>();
        }
    }

    public class BillItem
    {
        public string id { get; set; }
        public DateTime billingTime { get; set; }
        public string billingPerson { get; set; }
        public string itemClass { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public string itemSpec { get; set; }
        public string itemUnit { get; set; }
        public float amount { get; set; }
        public float costs { get; set; }
    }
}
