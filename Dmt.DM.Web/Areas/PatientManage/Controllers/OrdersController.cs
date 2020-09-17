using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.Orders;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Web.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class OrdersController : BaseController
    {
        private readonly IOrdersApp _ordersApp;
        private readonly IMapper _mapper;
        private readonly IPatientApp _patientApp;
        private readonly IDrugsApp _drugsApp;
        private readonly IUsersService _usersService;
        private readonly IBillingApp _billingApp;
        private readonly ITreatmentApp _treatmentApp;
        private readonly IStorageApp _storageApp;
        private readonly IHubContext<HubTChat> _hubContext;

        public OrdersController(
            IOrdersApp ordersApp, 
            IMapper mapper, 
            IPatientApp patientApp, 
            IDrugsApp drugsApp, 
            IUsersService usersService,
            IBillingApp billingApp,
            ITreatmentApp treatmentApp,
            IStorageApp storageApp,
            IHubContext<HubTChat> hubContext)
        {
            _ordersApp = ordersApp;
            _mapper = mapper;
            _patientApp = patientApp;
            _drugsApp = drugsApp;
            _usersService = usersService;
            _billingApp = billingApp;
            _treatmentApp = treatmentApp;
            _storageApp = storageApp;
            _hubContext = hubContext;
            //_notify = notify;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = await _ordersApp.GetList(pagination, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _ordersApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 查询已执行的医嘱
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetPerformedOrderListJson(string pid, DateTime visitDate)
        {
            var data = await _ordersApp.GetPerformedOrderListJson(pid, visitDate);
            return Content(data.ToJson());
        }
        
        [HttpPost]
        public async Task<IActionResult> SubmitDrugOrder([FromBody]SubmitDrugOrderInput input)
        {
            var patient = await _patientApp.GetForm(input.Pid);
            var drug = await _drugsApp.GetForm(input.Items.First().OrderCode);
            var user = await _usersService.GetCurrentUserAsync();
            var list = new List<OrdersEntity>();
            var firstEntity = new OrdersEntity
            {
                F_Pid = patient.F_Id,
                F_SubIndex = 0,
                F_DialysisNo = patient.F_DialysisNo,
                F_RecordNo = patient.F_RecordNo,
                F_OrderType = "药疗",
                F_OrderStartTime = input.OrderStartTime?.ToDate() ?? DateTime.Now,
                F_OrderCode = drug.F_Id,
                F_OrderText = drug.F_DrugName,
                F_OrderSpec = drug.F_DrugSpec,
                F_OrderUnitAmount = drug.F_DrugMiniAmount.ToFloat(2).ToString(CultureInfo.InvariantCulture),
                F_OrderUnitSpec = drug.F_DrugMiniSpec,
                F_OrderAmount = input.Items.First().OrderAmount,
                F_OrderFrequency = input.OrderFrequency,
                F_OrderAdministration = input.OrderAdministration,
                F_IsTemporary = input.IsTemporary,
                F_Doctor = user.F_RealName,
                F_DoctorOrderTime = DateTime.Now,
                F_OrderStatus = 0,
                F_EnabledMark = true
            };
            firstEntity.Create();
            list.Add(firstEntity);
            for (int i = 1; i < input.Items.Count; i++)
            {
                drug = await _drugsApp.GetForm(input.Items[i].OrderCode);
                var entity = new OrdersEntity
                {
                    F_Pid = patient.F_Id,
                    F_ParentId = firstEntity.F_Id,
                    F_SubIndex = i,
                    F_DialysisNo = patient.F_DialysisNo,
                    F_RecordNo = patient.F_RecordNo,
                    F_OrderType = "药疗",
                    F_OrderStartTime = input.OrderStartTime?.ToDate() ?? DateTime.Now,
                    F_OrderCode = drug.F_Id,
                    F_OrderText = drug.F_DrugName,
                    F_OrderSpec = drug.F_DrugSpec,
                    F_OrderUnitAmount = drug.F_DrugMiniAmount.ToFloat(2).ToString(),
                    F_OrderUnitSpec = drug.F_DrugMiniSpec,
                    F_OrderAmount = input.Items[i].OrderAmount,
                    F_OrderFrequency = input.OrderFrequency,
                    F_OrderAdministration = input.OrderAdministration,
                    F_IsTemporary = input.IsTemporary,
                    F_Doctor = user.F_RealName,
                    F_DoctorOrderTime = firstEntity.F_DoctorOrderTime,
                    F_OrderStatus = 0,
                    F_EnabledMark = true
                };
                entity.Create();
                list.Add(entity);
            }
            await _ordersApp.InsertBatch(list);
            return Success("操作成功。");
        }

        public async Task<IActionResult> GetDrugsListJson(string patientId, DateTime? startDate, DateTime? endDate)
        {
            var _startDate = startDate?.ToDate() ?? DateTime.Today;
            var _endDate = endDate?.ToDate() ?? DateTime.Today;
            _endDate = _endDate.AddDays(1);
            var source = (await _ordersApp.GetList(patientId, _startDate, _endDate, true)).Where(t => t.F_OrderType == "药疗");
            var ordersEntities = source.ToList();
            var data = ordersEntities.Where(t => t.F_SubIndex == 0).Select(t => new
            {
                id = t.F_Id,
                orderStartTime = t.F_OrderStartTime,
                isTemporary = t.F_IsTemporary,
                orderStatus = t.F_OrderStatus,
                orderFrequency = t.F_OrderFrequency + "",
                doctorAuditTime = t.F_DoctorAuditTime,
                orderStopTime = t.F_OrderStopTime,
                doctor = t.F_Doctor,
                rows = ordersEntities.Where(f => f.F_Id == t.F_Id || f.F_ParentId == t.F_Id).Select(f => new
                {
                    orderText = f.F_OrderText,
                    orderFrequency = f.F_OrderFrequency,
                    orderAdministration = f.F_OrderAdministration,
                    orderAmount = f.F_OrderAmount,
                    orderUnitSpec = f.F_OrderUnitSpec,
                    orderNo = f.F_SubIndex
                }).OrderBy(r => r.orderNo)
            }).OrderByDescending(t => t.orderStartTime);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> BatchAuditDrugOrder([FromBody]BatchAuditDrugOrderInput input)
        {
            var auditDateTime = DateTime.Now;
            var startDate = input.StartDate?.ToDate() ?? DateTime.Today;
            var endDate = input.EndDate?.ToDate() ?? DateTime.Today;
            endDate = endDate.AddDays(1);
            var source = (await _ordersApp.GetList(input.PatientId, startDate, endDate, true)).Where(t => t.F_OrderType == "药疗" && t.F_OrderStatus == 0).ToList();
            foreach (var item in source)
            {
                item.F_OrderStatus = 1;
                item.F_DoctorAuditTime = auditDateTime;
                await _ordersApp.UpdateForm(item);
            }
            return Success("操作成功");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            var entity = await _ordersApp.GetForm(input.KeyValue);
            if (entity != null)
            {
                entity.F_DeleteMark = true;
                entity.F_EnabledMark = false;
                await _ordersApp.UpdateForm(entity);
                var childrens = await _ordersApp.GetChildrens(input.KeyValue);
                foreach (var item in childrens)
                {
                    item.F_DeleteMark = true;
                    item.F_EnabledMark = false;
                    await _ordersApp.UpdateForm(item);
                }
                return Success("删除成功。");
            }
            return Error("医嘱ID有误。");
        }


        [HttpPost]
        public async Task<IActionResult> StopOrder([FromBody]BaseInput input)
        {
            var orderStopTime = System.DateTime.Now;
            var entity = await _ordersApp.GetForm(input.KeyValue);
            if (entity != null)
            {
                entity.F_OrderStopTime = orderStopTime;
                entity.F_OrderStatus = 3;
                await _ordersApp.UpdateForm(entity);
                var childrens = await _ordersApp.GetChildrens(input.KeyValue);
                foreach (var item in childrens)
                {
                    item.F_OrderStopTime = orderStopTime;
                    item.F_OrderStatus = 3;
                    await _ordersApp.UpdateForm(item);
                }
                return Success("停用成功。");
            }
            return Error("医嘱ID有误。");
        }

        [HttpPost]
        public async Task<IActionResult> CheckOrder([FromBody]BaseInput input)
        {
            var doctorAuditTime = System.DateTime.Now;
            var entity = await _ordersApp.GetForm(input.KeyValue);
            if (entity != null)
            {
                if (entity.F_OrderStatus == 0)
                {
                    entity.F_DoctorAuditTime = doctorAuditTime;
                    entity.F_OrderStatus = 1;
                    await _ordersApp.UpdateForm(entity);
                    var childrens = await _ordersApp.GetChildrens(input.KeyValue);
                    foreach (var item in childrens)
                    {
                        item.F_DoctorAuditTime = doctorAuditTime;
                        item.F_OrderStatus = 1;
                        await _ordersApp.UpdateForm(item);
                    }
                    var patient = await _patientApp.GetForm(entity.F_Pid);
                    var notify = new NotifyModel
                    {
                        Title = "新医嘱",
                        Content = patient.F_Name + System.Environment.NewLine + entity.F_OrderText + entity.F_OrderAmount + entity.F_OrderUnitSpec
                    };
       
                    await Task.Run(() =>
                    {
                        _hubContext.Clients.All.SendCoreAsync("addTasksNotify", new object[] { notify.ToJson() });
                    });
                    return Success("提交成功。");
                }
                else
                {
                    return Error("医嘱已提交。");
                }

            }
            return Error("医嘱ID有误。");
        }

        [HttpPost]
        public async Task<IActionResult> ExecOrder(string keyValue)
        {
            await _ordersApp.ExecOrder(keyValue);
            //计费
            var jArr = keyValue.ToJArrayObject();

            if (jArr.Count == 0)
            {
                return Error("未选择医嘱");
            }

            var c = from r in jArr
                    join o in await _ordersApp.GetList() on r.Value<string>("id") equals o.F_Id
                    select new
                    {
                        o.F_Nurse,
                        o.F_NurseOperatorTime,
                        o.F_OrderAdministration,
                        o.F_OrderAmount,
                        o.F_OrderCode,
                        o.F_OrderFrequency,
                        o.F_OrderSpec,
                        o.F_OrderText,
                        o.F_OrderType,
                        o.F_OrderUnitAmount,
                        o.F_OrderUnitSpec,
                        o.F_Pid
                    };

            var patient = await _patientApp.GetForm(c.FirstOrDefault().F_Pid);
            var user = await _usersService.GetCurrentUserAsync();
            var date = DateTime.Now;
            var drugs = (from r in c
                         where r.F_OrderType == "药疗"
                         select r).ToList();
            if (drugs.Count > 0)
            {
                //DrugsApp drugsApp = new DrugsApp();
                //StorageApp storageApp = new StorageApp();
                foreach (var item in drugs)
                {
                    //var sl = item.F_OrderAmount.ToFloat(2) * 100  / item.F_OrderUnitAmount.ToFloat(2) * 100;
                    //int i = (int)sl;
                    var fz = (item.F_OrderAmount.ToFloat(4) * 10000).ToInt();
                    var fm = (item.F_OrderUnitAmount.ToFloat(4) * 10000).ToInt();
                    int count = fz % fm == 0 ? fz / fm : fz / fm + 1;  //非整数倍  数量追加1 
                    var dict = await _drugsApp.GetForm(item.F_OrderCode);
                    //计费
                    await _billingApp.SubmitForm(new BillingEntity
                    {
                        F_Amount = count,
                        F_BillingDateTime = date,
                        F_BillingPerson = user.F_RealName,
                        F_BillingPersonId = user.F_Id,
                        F_Charges = dict.F_Charges,
                        F_Costs = (dict.F_Charges * count).ToFloat(2),
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
                        F_Supplier = dict.F_DrugSupplier,
                        F_EnabledMark = true,
                        F_IsAcct = false
                    }, new object());
                    //减库存
                    var storage = await _storageApp.GetFormByItemId(item.F_OrderCode);
                    if (storage != null)
                    {
                        storage.F_Amount -= count;
                        await _storageApp.UpdateForm(storage);
                    }

                }
            }

            var treatments = (from r in c
                              where r.F_OrderType == "治疗"
                              select r).ToList();
            if (treatments.Count > 0)
            {
                //TreatmentApp treatmentApp = new TreatmentApp();
                foreach (var item in treatments)
                {
                    var dict = await _treatmentApp.GetForm(item.F_OrderCode);
                    var amount = item.F_OrderAmount.ToFloat(2);
                    //计费
                    await _billingApp.SubmitForm(new BillingEntity
                    {
                        F_Amount = amount,
                        F_BillingDateTime = date,
                        F_BillingPerson = user.F_RealName,
                        F_BillingPersonId = user.F_Id,
                        F_Charges = dict.F_Charges,
                        F_Costs = (dict.F_Charges * amount).ToFloat(2),
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
                        F_ItemUnit = dict.F_TreatmentUnit,
                        F_EnabledMark = true,
                        F_IsAcct = false
                    }, new object());
                }
            }
            return Success("医嘱执行成功。");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Form1()
        {
            return View();
        }
    }
}
