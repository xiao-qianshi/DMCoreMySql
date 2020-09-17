using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.Dm.Domain.Dto.Orders;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class OrdersController : BaseApiController
    {
        private readonly IOrdersApp _ordersApp;
        private readonly IPatientApp _patientApp;
        private readonly IUsersService _usersService;
        private readonly IDrugsApp _drugsApp;
        private readonly IMaterialApp _materialApp;
        private readonly ITreatmentApp _treatmentApp;
        private readonly IBillingApp _billingApp;
        private readonly IStorageApp _storageApp;
        private readonly IOrdersExecLogApp _ordersExecLogApp;

        public OrdersController(IOrdersApp ordersApp, IPatientApp patientApp, IUsersService usersService, IDrugsApp drugsApp, IMaterialApp materialApp, ITreatmentApp treatmentApp, IBillingApp billingApp, IStorageApp storageApp, IOrdersExecLogApp ordersExecLogApp)
        {
            _ordersApp = ordersApp;
            _patientApp = patientApp;
            _usersService = usersService;
            _drugsApp = drugsApp;
            _materialApp = materialApp;
            _treatmentApp = treatmentApp;
            _billingApp = billingApp;
            _storageApp = storageApp;
            _ordersExecLogApp = ordersExecLogApp;
        }

        /// <summary>
        /// 新建、修改医嘱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SubmitData([FromBody]SubmitDataInput input)
        {
            var user = await _usersService.GetCurrentUserAsync();
            if (string.IsNullOrEmpty(input.id))//新建医嘱
            {
                if (string.IsNullOrEmpty(input.patientId)) return BadRequest("患者主键未传值！");
                var patient = await _patientApp.GetForm(input.patientId);
                if (patient == null) return BadRequest("患者主键有误！");
                var drug = await _drugsApp.GetForm(input.orderCode);
                if (drug == null) return BadRequest("药品主键有误！");
                if (input.orderAmount == null || Math.Abs(input.orderAmount.ToFloat(3)) < 0.01) return BadRequest("药品剂量有误！");
                var entity = new OrdersEntity
                {
                    F_Id = Common.GuId(),
                    F_Pid = patient.F_Id,
                    F_DialysisNo = patient.F_DialysisNo,
                    F_RecordNo = patient.F_RecordNo,
                    F_OrderType = input.orderType ?? "药疗",
                    F_OrderStartTime = input.orderStartTime ?? DateTime.Now,
                    F_OrderCode = input.orderCode,
                    F_OrderText = drug.F_DrugName,
                    F_OrderUnitAmount = drug.F_DrugMiniAmount.ToFloat(2).ToString(CultureInfo.CurrentCulture),
                    F_OrderUnitSpec = drug.F_DrugMiniSpec,
                    F_OrderAmount = input.orderAmount,
                    F_OrderFrequency = input.orderFrequency,
                    F_OrderAdministration = input.orderAdministration,
                    F_IsTemporary = input.isTemporary,
                    F_DoctorOrderTime = DateTime.Now,
                    F_CreatorTime = DateTime.Now,
                    F_CreatorUserId = user.F_Id,
                    F_Doctor = user.F_RealName
                };
                _ordersApp.InsertForm(entity);
                return Ok(new { id = entity.F_Id });
            }
            else//修改医嘱
            {

            }
            return Ok(new { id = "" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            var entity = await _ordersApp.GetForm(input.KeyValue);
            if (entity == null) return BadRequest("医嘱ID有误");
            if (entity.F_OrderStatus.ToInt() > 0) return BadRequest("不能删除已提交医嘱");
            await _ordersApp.DeleteForm(input.KeyValue);
            return Ok("删除成功");
        }

        [HttpPost]
        public async Task<IActionResult> StopOrder([FromBody]BaseInput input)
        {
            var entity = await _ordersApp.GetForm(input.KeyValue);
            if (entity == null) return BadRequest("医嘱ID有误");
            if (entity.F_OrderStatus.ToInt() == 0) return BadRequest("医嘱未提交");
            entity.F_OrderStopTime = System.DateTime.Now;
            entity.F_OrderStatus = 3;
            await _ordersApp.UpdateForm(entity);
            return Ok("停用成功");
        }

        [HttpPost]
        public async Task<IActionResult> CheckOrder([FromBody]BaseInput input)
        {
            var entity = await _ordersApp.GetForm(input.KeyValue);
            if (entity == null) return BadRequest("医嘱ID有误");
            if (entity.F_OrderStatus.ToInt() > 0) return BadRequest("医嘱已提交");
            entity.F_OrderStatus = 1;
            entity.F_DoctorAuditTime = System.DateTime.Now;
            await _ordersApp.UpdateForm(entity);
            return Ok("提交成功");
        }

        /// <summary>
        /// 执行医嘱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ExecOrder([FromBody]ExecOrderInput input)
        {
            //await _ordersApp.ExecOrder(keyValue);
            //计费
            //var jArr = keyValue.ToJArrayObject();

            //if (jArr.Count == 0)
            //{
            //    return BadRequest("未选择医嘱");
            //}

            var c = (from r in input.items
                    join o in await _ordersApp.GetList() on r.id equals o.F_Id
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
                    }).ToList();

            var patient = await _patientApp.GetForm(c.FirstOrDefault()?.F_Pid);
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

            //添加执行历史记录
            foreach (var item in input.items)
            {
                var order = await _ordersApp.GetForm(item.id);
                if (order == null) continue;
                order.F_NurseOperatorTime = item.operateTime?.ToDate() ?? DateTime.Now;
                order.F_Nurse = user.F_RealName;//
                await _ordersApp.UpdateForm(order);
                //claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier));
                //添加执行日志
                var logEntity = new OrdersExecLogEntity
                {
                    F_Oid = order.F_Id,
                    F_Pid = order.F_Pid,
                    F_VisitDate = order.F_VisitDate,
                    F_VisitNo = order.F_VisitNo,
                    F_OrderType = order.F_OrderType,
                    F_OrderStartTime = order.F_OrderStartTime,
                    F_OrderStopTime = order.F_OrderStopTime,
                    F_OrderCode = order.F_OrderCode,
                    F_OrderText = order.F_OrderText,
                    F_OrderSpec = order.F_OrderSpec,
                    F_OrderUnitAmount = order.F_OrderUnitAmount,
                    F_OrderUnitSpec = order.F_OrderUnitSpec,
                    F_OrderAmount = order.F_OrderAmount,
                    F_OrderFrequency = order.F_OrderFrequency,
                    F_OrderAdministration = order.F_OrderAdministration,
                    F_IsTemporary = order.F_IsTemporary,
                    F_Doctor = order.F_Doctor,
                    F_DoctorOrderTime = order.F_DoctorOrderTime,
                    F_DoctorAuditTime = order.F_DoctorAuditTime,
                    F_Nurse = order.F_Nurse,
                    F_NurseId = user.F_Id,
                    F_NurseOperatorTime = order.F_NurseOperatorTime,
                    F_Memo = order.F_Memo
                };
                logEntity.Create();
                logEntity.F_CreatorUserId = user.F_Id;
                await _ordersExecLogApp.InsertForm(logEntity);
            }
     
            return Ok("执行成功");
        }
        /// <summary>
        /// 查询服药历史信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult GetDrugOrdersHistory([FromBody]GetDrugOrdersHistoryInput input)
        {
            if (string.IsNullOrEmpty(input.patientId)) return BadRequest("患者主键未传值");
            var startDate = input.startDate.ToDate().Date;
            var endDate = input.endDate.ToDate().AddDays(1).Date;

            var sourse = _ordersExecLogApp.GetList(input.patientId, input.keyword, input.startDate.ToDate(), input.endDate.ToDate())
                .Where(t => t.F_OrderType.Equals("药疗"))
                .OrderByDescending(t => t.F_NurseOperatorTime)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_NurseOperatorTime,
                    t.F_OrderText,
                    t.F_OrderAmount,
                    t.F_OrderUnitSpec,
                    t.F_OrderFrequency,
                    t.F_OrderAdministration,
                    t.F_IsTemporary,
                    t.F_Memo,
                    t.F_Doctor,
                    t.F_DoctorAuditTime,
                    t.F_Nurse,
                    t.F_NurseId,
                    t.F_Oid
                })
                .ToList();
            var list = sourse.Select(t => new
            {
                id = t.F_Id,
                exexDate = t.F_NurseOperatorTime.ToDate().Date,
                execTime = t.F_NurseOperatorTime,
                orderText = t.F_OrderText,
                amount = t.F_OrderAmount,
                unit = t.F_OrderUnitSpec,
                frequency = t.F_OrderFrequency,
                administration = t.F_OrderAdministration,
                isTemporary = t.F_IsTemporary,
                memo = t.F_Memo,
                doctorName = t.F_Doctor,
                doctorAuditTime = t.F_DoctorAuditTime,
                nurseName = t.F_Nurse,
                nurseId = t.F_NurseId,
                orderId = t.F_Oid
            });

            var data = list.GroupBy(t => t.exexDate).Select(t => new
            {
                key = t.Key,
                items = t.OrderByDescending(m => m.execTime)
            }).OrderByDescending(t => t.key)
            ;
            return Ok(data);
        }
    }
}
