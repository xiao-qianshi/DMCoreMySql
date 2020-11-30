using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.Dm.Domain.Dto.PatVisit;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint;
using Dmt.DM.Domain.Entity.ReportPrint.DialysisRecord;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class PatVisitController : BaseApiController
    {
        private readonly IPatientApp _patientApp;
        private readonly IPatVisitApp _patVisitApp;
        private readonly IOrdersExecLogApp _ordersExecLogApp;
        private readonly IOrdersApp _ordersApp;
        private readonly IDrugsApp _drugsApp;
        private readonly IMaterialApp _materialApp;
        private readonly IUsersService _usersService;
        private readonly IDialysisMachineApp _dialysisMachineApp;
        private readonly IPunctureApp _punctureApp;
        private readonly IDialysisObservationApp _dialysisObservationApp;
        private readonly IConclusionTemplateApp _conclusionTemplateApp;

        public PatVisitController(IPatientApp patientApp, IPatVisitApp patVisitApp, IOrdersExecLogApp ordersExecLogApp, IOrdersApp ordersApp, IDrugsApp drugsApp, IMaterialApp materialApp, IUsersService usersService, IDialysisMachineApp dialysisMachineApp, IPunctureApp punctureApp, IDialysisObservationApp dialysisObservationApp, IConclusionTemplateApp conclusionTemplateApp)
        {
            _patientApp = patientApp;
            _patVisitApp = patVisitApp;
            _ordersExecLogApp = ordersExecLogApp;
            _ordersApp = ordersApp;
            _drugsApp = drugsApp;
            _materialApp = materialApp;
            _usersService = usersService;
            _dialysisMachineApp = dialysisMachineApp;
            _punctureApp = punctureApp;
            _dialysisObservationApp = dialysisObservationApp;
            _conclusionTemplateApp = conclusionTemplateApp;
        }

        /// <summary>
        /// 患者-体重曲线
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetWeightCharts(BaseInput input)
        {
            var list = _patVisitApp.GetList()
                .Where(t =>t.F_Pid == input.KeyValue && t.F_DialysisEndTime != null && t.F_WeightTQ != null && t.F_WeightTH != null)
                //.Where(t => t.F_Pid.Equals(input.keyValue))
                .OrderByDescending(t => t.F_VisitDate)
                .Take(15)
                .Select(t => new
                {
                    t.F_VisitDate,
                    t.F_WeightTQ,
                    t.F_WeightTH
                })
                .ToList();
            var ideaWeight = await _patientApp.GetIdeaWeight(input.KeyValue);
            var data = new
            {
                ideaWeight,
                count = list.Count,
                rows = list.Select(t => new
                {
                    visitDate = t.F_VisitDate.ToDate(),
                    weightTQ = t.F_WeightTQ.ToFloat(2),
                    weightTH = t.F_WeightTH.ToFloat(2),
                })
            };
            return Ok(data);
        }
        /// <summary>
        /// 患者-透析历史记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetBrieflyList(GetBrieflyListInput input)
        {
            var query = _patVisitApp.GetList()
                 .Where(t =>t.F_Pid == input.keyValue && t.F_DialysisEndTime != null && t.F_VisitDate >= input.startDate.Date && t.F_VisitDate <= input.endDate.Date);
            if (input.dialysisType != null)
                query = query.Where(t => t.F_DialysisType.Equals(input.dialysisType));
            var list = query
                .OrderByDescending(t => t.F_VisitDate)
                .Take(50)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_VisitDate,
                    t.F_VisitNo,
                    t.F_GroupName,
                    t.F_DialysisBedNo,
                    t.F_DialysisType,
                    t.F_DialysisStartTime,
                    t.F_DialysisEndTime,
                    t.F_VascularAccess,
                    t.F_AccessName,
                    t.F_DialyzerType1,
                    t.F_HeparinType,
                    t.F_HeparinAmount,
                    t.F_HeparinUnit,
                    t.F_HeparinAddAmount,
                    t.F_HeparinAddSpeedUnit,
                    t.F_WeightTQ,
                    t.F_WeightTH,
                    t.F_WeightYT,
                    t.F_WeightST
                })
                .ToList()
                .Select(t => new
                {
                    id = t.F_Id,
                    monthDesc = t.F_VisitDate.ToDateString().Substring(0, 7),
                    visitDate = t.F_VisitDate.ToDate(),
                    visitNo = t.F_VisitNo.ToInt(),
                    groupName = t.F_GroupName,
                    bedNo = t.F_DialysisBedNo,
                    dialysisType = t.F_DialysisType,
                    dialysisStartTime = t.F_DialysisStartTime.ToDate(),
                    dialysisEndTime = t.F_DialysisEndTime.ToDate(),
                    startTimeStr = t.F_DialysisStartTime == null ? "" : t.F_DialysisStartTime.ToTimeString(true),
                    endTimeStr = t.F_DialysisEndTime == null ? "" : t.F_DialysisEndTime.ToTimeString(true),
                    vascularAccess = t.F_VascularAccess,
                    accessName = t.F_AccessName,
                    dialyzerType = t.F_DialysisType,
                    heparinType = t.F_HeparinType,
                    heparinAmount = t.F_HeparinAmount,
                    heparinUnit = t.F_HeparinUnit,
                    heparinAddAmount = t.F_HeparinAddAmount,
                    heparinAddSpeedUnit = t.F_HeparinAddSpeedUnit,
                    weightYT = t.F_WeightYT.ToFloat(2),
                    weightTQ = t.F_WeightTQ.ToFloat(2),
                    weightTH = t.F_WeightTH.ToFloat(2),
                    weightST = t.F_WeightST.ToFloat(2)
                })
                .ToList();
            //查询药物医嘱执行记录
            var orderList = await _ordersExecLogApp.GetList(input.keyValue, input.startDate.Date, input.endDate.Date.AddDays(1));
            //存储肝素、透析器字典
            Hashtable table = new Hashtable();
            var months = list.Select(t => t.monthDesc).Distinct().OrderByDescending(t => t);
            var data = new List<GetBrieflyListOutput>();
            foreach (var month in months)
            {
                var find = list.Where(t => t.monthDesc.Equals(month)).ToArray();
                var output = new GetBrieflyListOutput
                {
                    monthDesc = month,
                    count = find.Count()
                };
                foreach (var item in find)
                {
                    if (item.heparinType != null && !table.ContainsKey(item.heparinType))
                    {
                        var drug = await _drugsApp.GetForm(item.heparinType);
                        table.Add(item.heparinType, drug?.F_DrugName);
                    }
                    if (item.dialyzerType != null && !table.ContainsKey(item.dialyzerType))
                    {
                        var m = await _materialApp.GetForm(item.dialyzerType);
                        table.Add(item.dialyzerType, m?.F_MaterialName);
                    }
                    var brieflyItem = new BrieflyItem
                    {
                        id = item.id,
                        visitDate = item.visitDate,
                        visitNo = item.visitNo,
                        groupName = item.groupName,
                        bedNo = item.groupName,
                        dialysisType = item.dialysisType,
                        dialysisStartTime = item.dialysisStartTime,
                        dialysisEndTime = item.dialysisEndTime,
                        startTimeStr = item.startTimeStr,
                        endTimeStr = item.endTimeStr,
                        vascularAccess = item.vascularAccess,
                        accessName = item.accessName,
                        dialyzerType = item.dialyzerType == null ? "" : table[item.dialyzerType] + "",
                        heparinType = item.heparinType == null ? "" : table[item.heparinType] + "",
                        heparinAmount = item.heparinAmount ?? 0,
                        heparinUnit = item.heparinUnit,
                        heparinAddAmount = item.heparinAddAmount ?? 0,
                        heparinAddSpeedUnit = item.heparinAddSpeedUnit,
                        weightYT = item.weightYT,
                        weightTQ = item.weightTQ,
                        weightTH = item.weightTH,
                        weightST = item.weightST
                    };
                    var filterOrders = orderList.Where(t => t.F_NurseOperatorTime >= item.visitDate && t.F_NurseOperatorTime <= item.visitDate.AddDays(1));
                    foreach (var child in filterOrders)
                    {
                        brieflyItem.orders.Add(new DrugOrderItem
                        {
                            drugName = child.F_OrderText,
                            drugAmount = child.F_OrderAmount.ToFloat(2),
                            drugUnit = child.F_OrderUnitSpec
                        });
                    }
                    output.items.Add(brieflyItem);
                }
                data.Add(output);
            }
            return Ok(data);
        }
        /// <summary>
        /// 今日就诊-首页 记录单卡片
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetCardList(GetCardListInput input)
        {
            var query = _patVisitApp.GetList() //input.visitDate, input.groupName, input.visitNo
                .Where(t=>t.F_VisitDate == input.visitDate && t.F_GroupName == input.groupName && t.F_VisitNo == input.visitNo)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_Pid,
                    t.F_DialysisType,
                    t.F_GroupName,
                    t.F_DialysisBedNo,
                    t.F_DialyzerType1,
                    t.F_HeparinType,
                    t.F_HeparinAmount,
                    t.F_HeparinUnit,
                    t.F_DialysisStartTime,
                    t.F_DialysisEndTime,
                    t.F_VascularAccess,
                    t.F_AccessName
                });
            var list = query.ToList();
            //床位排序
            var beds = await _dialysisMachineApp.GetItemList(input.groupName);
            var result = new List<GetCardListOutput>();
            //存储肝素、透析器字典
            Hashtable table = new Hashtable();
            foreach (var item in list)
            {
                if (item.F_HeparinType != null && !table.ContainsKey(item.F_HeparinType))
                {
                    var drug = await _drugsApp.GetForm(item.F_HeparinType);
                    table.Add(item.F_HeparinType, drug?.F_DrugName);
                }
                if (item.F_DialyzerType1 != null && !table.ContainsKey(item.F_DialyzerType1))
                {
                    var m = await _materialApp.GetForm(item.F_DialyzerType1);
                    table.Add(item.F_DialyzerType1, m?.F_MaterialName);
                }

                var output = new GetCardListOutput
                {
                    id = item.F_Id,
                    groupName = item.F_GroupName,
                    bedNo = item.F_DialysisBedNo,
                    dialysisType = item.F_DialysisType,
                    dialysisStartTime = item.F_DialysisStartTime,
                    dialysisEndTime = item.F_DialysisEndTime,
                    vascularAccess = item.F_VascularAccess,
                    accessName = item.F_AccessName,
                    heparinAmount = item.F_HeparinAmount,
                    heparinUnit = item.F_HeparinUnit,
                    heparinType = item.F_HeparinType == null ? "" : table[item.F_HeparinType].ToString(),
                    dialyzerType = item.F_DialyzerType1 == null ? "" : table[item.F_DialyzerType1].ToString()
                };
                if (item.F_Pid == null) continue;
                else
                {
                    var patient = await _patientApp.GetForm(item.F_Pid);
                    if (patient == null) continue;
                    output.patientId = patient.F_Id;
                    if (patient.F_BirthDay != null) output.patientAge = (DateTime.Today - patient.F_BirthDay.ToDate()).TotalDays.ToInt() / 365;
                    output.patientName = patient.F_Name;
                    output.beInfected = "+".Equals(patient.F_Tp) || "+".Equals(patient.F_Hiv) || "+".Equals(patient.F_HBsAg) || "+".Equals(patient.F_HBeAg) || "+".Equals(patient.F_HBeAb);//阳性患者判断规则
                    output.headIcon = patient.F_HeadIcon;
                }
                var bed = beds.First(t => t.F_DialylisBedNo.Equals(item.F_DialysisBedNo));
                if (bed == null) output.sortNo = 999;
                else output.sortNo = bed.F_ShowOrder.ToInt();
                if (item.F_DialysisStartTime == null)
                {
                    output.percent = 0;
                }
                else if (item.F_DialysisEndTime != null)
                {
                    output.percent = 100;
                }
                else
                {
                    output.percent = ((DateTime.Now - item.F_DialysisStartTime.ToDate()).TotalHours / 4).ToFloat(2);
                }
                result.Add(output);
            }
            var data = result.OrderBy(t => t.sortNo);
            return Ok(data);
        }
        /// <summary>
        /// 今日就诊-治疗单详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetIntactInfo(BaseInput input)
        {
            var entity = await _patVisitApp.GetForm(input.KeyValue);
            var patient = await _patientApp.GetForm(entity.F_Pid);
            var data = new GetIntactInfoOutput
            {
                checkPerson = entity.F_CheckPerson,
                dialysisStartTime = entity.F_DialysisStartTime,
                dialysisEndTime = entity.F_DialysisEndTime,
                endPerson = entity.F_EndPerson,
                memo = entity.F_Memo,
                percent = 0,
                puncturePerson = entity.F_PuncturePerson,
                startPerson = entity.F_StartPerson
            };
            data.patient = new Patients
            {
                patientId = patient.F_Id,
                headIcon = patient.F_HeadIcon,
                ideaWeight = patient.F_IdealWeight,
                maritalStatus = patient.F_MaritalStatus,
                patientGender = patient.F_Gender,
                patientName = patient.F_Name
            };
            if (patient.F_BirthDay != null) data.patient.patientAge = ((DateTime.Now - patient.F_BirthDay.ToDate()).TotalDays / 365).ToInt();
            data.vitalSigns = new VitalSigns
            {
                diastolicPressure = entity.F_DiastolicPressure,
                systolicPressure = entity.F_SystolicPressure,
                temperature = entity.F_Temperature,
                pulse = entity.F_Pulse
            };
            data.dialysisParameter = new DialysisParameter
            {
                accessName = entity.F_AccessName,
                bloodSpeed = entity.F_BloodSpeed,
                Ca = entity.F_Ca,
                dialysateTemperature = entity.F_DialysateTemperature,
                dialysisType = entity.F_DialysisType,
                dialyzerType1 = entity.F_DialyzerType1,
                dialyzerType2 = entity.F_DialyzerType2,
                dilutionType = entity.F_DilutionType,
                estimateHours = entity.F_EstimateHours,
                exchangeAmount = entity.F_ExchangeAmount,
                exchangeSpeed = entity.F_ExchangeSpeed,
                Hco3 = entity.F_Hco3,
                heparinAddAmount = entity.F_HeparinAddAmount,
                heparinAddSpeedUnit = entity.F_HeparinAddSpeedUnit,
                heparinAmount = entity.F_HeparinAmount,
                heparinType = entity.F_HeparinType,
                heparinUnit = entity.F_HeparinUnit,
                K = entity.F_K,
                LowCa = entity.F_LowCa,
                Na = entity.F_Na,
                vascularAccess = entity.F_VascularAccess
            };
            data.weightSet = new WeightSet
            {
                weightYT = entity.F_WeightYT,
                weightST = entity.F_WeightST,
                weightTQ = entity.F_WeightTQ,
                weightTH = entity.F_WeightTH
            };
            var punctureRecord = await _punctureApp.GetSingle(entity.F_Pid, entity.F_VisitDate.ToDate());
            if (punctureRecord != null)
                data.punctureRecord = new PunctureRecord
                {
                    point1 = punctureRecord.F_Point1,
                    point2 = punctureRecord.F_Point2
                };
            //OrdersApp ordersApp = new OrdersApp();
            var orders = (await _ordersApp.GetList(entity.F_Pid, entity.F_VisitDate.ToDate(), entity.F_VisitDate.ToDate().AddDays(1), true))
                .Where(t => t.F_OrderType.Equals("药疗")).OrderBy(t => t.F_IsTemporary).ToList();
            //医生医嘱
            foreach (var item in orders)
            {
                data.doctorOrders.Add(new DoctorOrder
                {
                    id = item.F_Id,
                    isTemporary = item.F_IsTemporary,
                    orderText = item.F_OrderText,
                    orderUnitSpec = item.F_OrderUnitSpec,
                    orderAmount = item.F_OrderAmount,
                    orderFrequency = item.F_OrderFrequency,
                    orderAdministration = item.F_OrderAdministration,
                    orderStatus = item.F_OrderStatus
                });
            }
            //护士医嘱
            //查询医嘱执行记录
            var orderExecRecord = await _ordersApp.GetPerformedOrderListJson(entity.F_Pid, entity.F_VisitDate.ToDate());
            foreach (var item in orders.Where(t => t.F_OrderStatus >= 1))
            {
                var nurserOrder = new NurserOrder
                {
                    id = item.F_Id,
                    isTemporary = item.F_IsTemporary,
                    orderText = item.F_OrderText,
                    orderUnitSpec = item.F_OrderUnitSpec,
                    orderAmount = item.F_OrderAmount,
                    orderFrequency = item.F_OrderFrequency,
                    orderAdministration = item.F_OrderAdministration
                };
                var find = orderExecRecord.FirstOrDefault(t => t.F_Oid.Equals(item.F_Id));
                if (find != null)
                {
                    nurserOrder.execTime = find.F_NurseOperatorTime;
                    nurserOrder.nurseName = find.F_Nurse;
                }
                data.nurserOrders.Add(nurserOrder);
            }
            //观察记录
            var obs = _dialysisObservationApp.GetListByVisit(entity.F_Id);
            foreach (var item in obs.OrderBy(t => t.F_NurseOperatorTime))
            {
                data.observations.Add(new Observations
                {
                    id = item.F_Id,
                    ssy = item.F_SSY,
                    szy = item.F_SZY,
                    hr = item.F_HR,
                    a = item.F_A,
                    bf = item.F_BF,
                    ufr = item.F_UFR,
                    v = item.F_V,
                    c = item.F_C,
                    t = item.F_T,
                    ufv = item.F_UFV,
                    tmp = item.F_TMP,
                    gsl = item.F_GSL,
                    memo = item.F_MEMO,
                    nurseName = item.F_Nurse,
                    operatorTime = item.F_NurseOperatorTime
                });
            }
            return Ok(data);
        }

        /// <summary>
        /// 刷新医生医嘱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetDoctorOrders(BaseInput input)
        {
            var entity = await _patVisitApp.GetForm(input.KeyValue);
            //OrdersApp ordersApp = new OrdersApp();
            var orders = (await _ordersApp.GetList(entity.F_Pid, entity.F_VisitDate.ToDate(), entity.F_VisitDate.ToDate().AddDays(1), true))
                .Where(t => t.F_OrderType.Equals("药疗")).OrderBy(t => t.F_IsTemporary);
            //医生医嘱

            var data = orders.Select(t => new
            {
                id = t.F_Id,
                isTemporary = t.F_IsTemporary,
                orderText = t.F_OrderText,
                orderUnitSpec = t.F_OrderUnitSpec,
                orderAmount = t.F_OrderAmount,
                orderFrequency = t.F_OrderFrequency,
                orderAdministration = t.F_OrderAdministration,
                orderStatus = t.F_OrderStatus
            });

            return Ok(data);
        }

        /// <summary>
        /// 刷新护士医嘱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet, Route("GetNurseOrders")]
        public async Task<IActionResult> GetNurseOrders(BaseInput input)
        {
            var entity = await _patVisitApp.GetForm(input.KeyValue);
            var orders = (await _ordersApp.GetList(entity.F_Pid, entity.F_VisitDate.ToDate(), entity.F_VisitDate.ToDate().AddDays(1), true))
                .Where(t => t.F_OrderType.Equals("药疗")).OrderBy(t => t.F_IsTemporary);
            //查询医嘱执行记录
            var orderExecRecord = await _ordersApp.GetPerformedOrderListJson(entity.F_Pid, entity.F_VisitDate.ToDate());
            //护士医嘱
            var data = new List<NurserOrder>();
            foreach (var item in orders.Where(t => t.F_OrderStatus >= 1))
            {
                var nurserOrder = new NurserOrder
                {
                    id = item.F_Id,
                    isTemporary = item.F_IsTemporary,
                    orderText = item.F_OrderText,
                    orderUnitSpec = item.F_OrderUnitSpec,
                    orderAmount = item.F_OrderAmount,
                    orderFrequency = item.F_OrderFrequency,
                    orderAdministration = item.F_OrderAdministration
                };
                var find = orderExecRecord.FirstOrDefault(t => t.F_Oid.Equals(item.F_Id));
                if (find != null)
                {
                    nurserOrder.execTime = find.F_NurseOperatorTime;
                    nurserOrder.nurseName = find.F_Nurse;
                }
                data.Add(nurserOrder);
            }

            return Ok(data);
        }

        /// <summary>
        /// 今日就诊-治疗单详情-更新生命体征
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateVitalSigns([FromBody]UpdateVitalSignsInput input)
        {
            var entity = new PatVisitEntity
            {
                F_Id = input.id,
                F_SystolicPressure = input.systolicPressure,
                F_DiastolicPressure = input.diastolicPressure,
                F_Pulse = input.pulse,
                F_Temperature = input.temperature
            };
            await _patVisitApp.UpdateForm(entity);
            return Ok("操作成功");
        }
        /// <summary>
        /// 今日就诊-治疗单详情-更新透析参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateParameter([FromBody]UpdateParameterInput input)
        {
            var entity = new PatVisitEntity
            {
                F_Id = input.id,
                F_DialysisType = input.dialysisType,
                F_DilutionType = input.dilutionType,
                F_ExchangeAmount = input.exchangeAmount,
                F_ExchangeSpeed = input.exchangeSpeed,
                F_BloodSpeed = input.bloodSpeed,
                F_DialysateTemperature = input.dialysateTemperature,
                F_EstimateHours = input.estimateHours,
                F_VascularAccess = input.vascularAccess,
                F_AccessName = input.accessName,
                F_DialyzerType1 = input.dialyzerType1,
                F_DialyzerType2 = input.dialyzerType2,
                F_HeparinType = input.heparinType,
                F_HeparinAmount = input.heparinAmount,
                F_HeparinUnit = input.heparinUnit,
                F_HeparinAddAmount = input.heparinAddAmount,
                F_HeparinAddSpeedUnit = input.heparinAddSpeedUnit,
                F_Ca = input.Ca,
                F_K = input.K,
                F_Na = input.Na,
                F_Hco3 = input.Hco3,
                F_LowCa = input.LowCa
            };
            await _patVisitApp.UpdateForm(entity);
            return Ok("操作成功");
        }

        /// <summary>
        /// 今日就诊-治疗单详情-修改干体重
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateIdeaWeight([FromBody]UpdateWeightInput input)
        {
            if (input.id == null || input.weight == null || input.weight < 20) return BadRequest("参数有误！");
            var entity = await _patVisitApp.GetForm(input.id);
            if (entity == null || string.IsNullOrEmpty(entity.F_Pid)) return BadRequest("主键ID有误！");
            //修改干体重记录
            await _patientApp.UpdateWeight(entity.F_Pid, _usersService.GetCurrentUserId(), input.weight.ToFloat(2));
            return Ok("操作成功");
        }

        /// <summary>
        /// 今日就诊-治疗单详情-修改预脱量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateWeightYT([FromBody]UpdateWeightInput input)
        {
            if (input.id == null || input.weight == null || input.weight < 20) return BadRequest("参数有误！");
            var entity = await _patVisitApp.GetForm(input.id);
            entity.F_WeightYT = input.weight;
            await _patVisitApp.UpdateForm(entity);
            return Ok("操作成功");
        }

        /// <summary>
        /// 今日就诊-治疗单详情-修改预脱量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateWeightTQ([FromBody]UpdateWeightInput input)
        {
            if (input.id == null || input.weight == null || input.weight < 20) return BadRequest("参数有误！");
            var entity = await _patVisitApp.GetForm(input.id);
            entity.F_WeightTQ = input.weight;
            await _patVisitApp.UpdateForm(entity);
            return Ok("操作成功");
        }

        /// <summary>
        /// 今日就诊-治疗单详情-修改预脱量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateWeightTH")]
        public async Task<IActionResult> UpdateWeightTH([FromBody]UpdateWeightInput input)
        {
            if (input.id == null || input.weight == null || input.weight < 20) return BadRequest("参数有误！");
            var entity = await _patVisitApp.GetForm(input.id);
            entity.F_WeightTH = input.weight;
            await _patVisitApp.UpdateForm(entity);
            return Ok("操作成功");
        }

        /// <summary>
        /// 今日就诊-更改透析记录状态（上机、审核、下机）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromBody]ChangeStatusInput input)
        {
            if (string.IsNullOrEmpty(input.id))
            {
                return BadRequest("未传值：透析单ID！");
            }
            var entity = await _patVisitApp.GetForm(input.id);
            if (entity == null)
            {
                return BadRequest("透析单ID错误！");
            }
            if (string.IsNullOrEmpty(input.operateType))
            {
                return BadRequest("未传值：操作类型！");
            }
            else
            {
                switch (input.operateType)
                {
                    case "上机":
                        if (entity.F_DialysisStartTime == null)
                        {
                            entity.F_DialysisStartTime = input.operateTime ?? DateTime.Now;
                            entity.F_StartPerson = _usersService.GetCurrentUserId();
                            await _patVisitApp.UpdateForm(entity);
                        }
                        else
                        {
                            return BadRequest("已执行【上机】操作");
                        }
                        break;
                    case "下机":
                        if (entity.F_DialysisEndTime == null)
                        {
                            entity.F_DialysisEndTime = input.operateTime ?? DateTime.Now;
                            entity.F_EndPerson = _usersService.GetCurrentUserId();
                            await _patVisitApp.UpdateForm(entity);
                        }
                        else
                        {
                            return BadRequest("已执行【下机】操作");
                        }
                        break;
                    case "审核":
                        if (entity.F_CheckPerson == null)
                        {
                            entity.F_CheckPerson = _usersService.GetCurrentUserId();
                            await _patVisitApp.UpdateForm(entity);
                        }
                        else
                        {
                            return BadRequest("已执行【审核】操作");
                        }
                        break;
                    default:
                        return BadRequest("操作类型（上机、下机、审核）传值有误！");
                }
            }
            return Ok("操作成功");
        }
        /// <summary>
        /// 保存治疗小节
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CommitContent(string id, string content)
        {
            var entity = await _patVisitApp.GetForm(id);
            if (entity == null) return BadRequest("治疗单主键有误");
            entity.F_Memo = content;
            await _patVisitApp.UpdateForm(entity);
            return Ok("操作成功");
        }

        /// <summary>
        /// 新建治疗小节模板
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddConclusionTemplate([FromBody]AddConclusionTemplateInput input)
        {
            var userId = _usersService.GetCurrentUserId();
            //var conclusionTemplateApp = new ConclusionTemplateApp();
            var entity = new ConclusionTemplateEntity
            {
                F_Id = Common.GuId(),
                F_EnabledMark = true,
                F_CreatorTime = DateTime.Now,
                F_CreatorUserId = userId,
                F_Title = input.title,
                F_Content = input.content,
                F_IsPrivate = input.isPrivate
            };
            await _conclusionTemplateApp.InsertForm(entity);
            var data = new
            {
                id = entity.F_Id
            };
            return Ok(data);
        }

        /// <summary>
        /// 删除治疗小节模板
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteConclusionTemplate([FromBody]BaseInput input)
        {
            //var user = LoginInfoHelper.GetCurrentLoginUser();
            //var conclusionTemplateApp = new ConclusionTemplateApp();
            //var entity = new ConclusionTemplateEntity
            //{
            //    F_Id = Common.GuId(),
            //    F_EnabledMark = true,
            //    F_CreatorTime = DateTime.Now,
            //    F_CreatorUserId = user.F_Id,
            //    F_Title = input.title,
            //    F_Content = input.content,
            //    F_IsPrivate = input.isPrivate
            //};
            _conclusionTemplateApp.DeleteForm(input.KeyValue);
            //var data = new
            //{
            //    id = entity.F_Id
            //};
            return Ok("删除成功");
        }

        /// <summary>
        /// 查询治疗小节模板列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetConclusionTemplateList()
        {
            //var conclusionTemplateApp = new ConclusionTemplateApp();
            var data = _conclusionTemplateApp.GetList(_usersService.GetCurrentUserId())
                .OrderByDescending(t => t.F_CreatorTime)
                .Select(t => new
                {
                    id = t.F_Id,
                    title = t.F_Title,
                    content = t.F_Content,
                    isPrivate = t.F_IsPrivate
                }).ToList();
            return Ok(data);
        }

        /// <summary>
        /// 单一透析记录单查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetFormJson(BaseInput input)
        {
            var data = await _patVisitApp.GetForm(input.KeyValue);
            return Ok(data);
        }

        /// <summary>
        /// 根据患者ID、时间段查询待执行医嘱信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSubmitOrderJson(GetSubmitOrderJsonInput input/*string keyValue, DateTime startDate, DateTime endDate*/)
        {
            //var patientVisit = _patVisitApp.GetForm(keyValue);
            //int dialysisNo = (int)patientVisit.F_DialysisNo;
            //var patient = patientApp.GetFormById(dialysisNo.ToString());
            var c = from r in (await _ordersApp.GetList(input.id, input.startDate, input.endDate)).OrderBy(t => t.F_OrderType)
                    select new
                    {
                        F_IsTemporary = r.F_IsTemporary == null ? "临时" : ((bool)r.F_IsTemporary) ? "长期" : "临时",
                        F_Nurse = r.F_Nurse ?? "",
                        F_NurseOperatorTime = r.F_NurseOperatorTime == null ? "" : ((DateTime)r.F_NurseOperatorTime).ToString("MM-dd HH:mm"),
                        r.F_OrderCode,
                        r.F_OrderSpec,
                        r.F_OrderStatus,
                        r.F_OrderType,
                        r.F_OrderText,
                        r.F_OrderAmount,
                        F_OrderFrequency = "" + r.F_OrderFrequency + "  " + r.F_Memo,
                        r.F_Id
                    };
            return Ok(c);
        }

        /// <summary>
        /// 根据日期查询透析记录单简要信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetPatVisitListJson(GetPatVisitListJsonInput input/*DateTime visitDate*/)
        {
            var list = _patVisitApp.GetList().Where(t => t.F_VisitDate == input.visitDate);
            //患者
            var p = (from c in await _patientApp.GetList()
                     where list.Any(t => t.F_Pid.Equals(c.F_Id))
                     select c).ToList();
            //耗材
            var m = (from c in await _materialApp.GetList()
                     where list.Any(t => c.F_Id.Equals(t.F_DialyzerType1) || c.F_Id.Equals(t.F_DialyzerType2))
                     select c).ToList();
            //药品
            var d = (from c in await _drugsApp.GetList()
                     where list.Any(t => c.F_Id.Equals(t.F_HeparinType))
                     select c).ToList();


            //查询治疗记录
            var data = new List<PatVisitListResult>();
            foreach (var item in list)
            {
                var temp = new PatVisitListResult
                {
                    Id = item.F_Id,
                    Pid = item.F_Pid,
                    GroupName = item.F_GroupName,
                    DialysisBedNo = item.F_DialysisBedNo
                };
                var patient = p.FirstOrDefault(t => t.F_Id.Equals(item.F_Pid));
                if (patient == null) continue;
                temp.Name = patient.F_Name ?? "";
                temp.Gender = patient.F_Gender ?? "";
                temp.VisitDate = item.F_VisitDate.ToDateString();
                temp.VisitNo = item.F_VisitNo.ToInt();
                temp.DialysisNo = patient.F_DialysisNo.ToInt();
                temp.DialysisType = item.F_DialysisType ?? "";
                temp.DialyzerType1 = item.F_DialyzerType1 == null ? "" : m.First(t => t.F_Id.Equals(item.F_DialyzerType1)).F_MaterialName;
                temp.DialyzerType2 = item.F_DialyzerType2 == null ? "" : m.First(t => t.F_Id.Equals(item.F_DialyzerType2)).F_MaterialName;
                temp.VascularAccess = item.F_VascularAccess ?? "";
                temp.HeparinType = item.F_HeparinType == null ? "" : d.First(t => t.F_Id.Equals(item.F_HeparinType)).F_DrugName;
                temp.BirthDay = patient.F_BirthDay.ToDateString();
                temp.AgeDesc = patient.F_BirthDay == null ? "" : ((int)((DateTime.Now - patient.F_BirthDay.ToDate()).TotalDays / 365)).ToString() + "岁";
                temp.StartTime = item.F_DialysisStartTime == null ? "" : item.F_DialysisStartTime.ToDateTimeString();
                temp.EndTime = item.F_DialysisEndTime == null ? "" : item.F_DialysisEndTime.ToDateTimeString();
                temp.Py = (patient.F_PY ?? "").ToLower();
                temp.StartPerson = item.F_StartPerson;
                temp.EndPerson = item.F_EndPerson;
                temp.CheckPerson = item.F_CheckPerson;
                temp.PuncturePerson = item.F_PuncturePerson;
                data.Add(temp);
            }
            return Ok(data);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetTvShowList()
        {
            var visitRecords = _patVisitApp.GetList()
                .Where(t => t.F_VisitDate == DateTime.Today && t.F_EnabledMark != false && t.F_DeleteMark != true);
            var list = from v in visitRecords
                join d in await _drugsApp.GetList() on v.F_HeparinType equals d.F_Id into temp
                from dt in temp.DefaultIfEmpty()
                join b in _dialysisMachineApp.GetQueryable() on new
                    {GroupName = v.F_GroupName, BedNo = v.F_DialysisBedNo} equals new
                    {GroupName = b.F_GroupName, BedNo = b.F_DialylisBedNo} into btemp
                from bt in btemp.DefaultIfEmpty()
                select new
                {
                    Id = v.F_Id,
                    VisitDate = v.F_VisitDate,
                    VisitNo = v.F_VisitNo,
                    DialysisType = v.F_DialysisType,
                    PId = v.F_Pid,
                    Name = v.F_Name,
                    StartTime = v.F_DialysisStartTime,
                    EndTime = v.F_DialysisEndTime,
                    Status = v.F_DialysisStartTime == null ? 1 : v.F_DialysisEndTime == null ? 2 : 3,
                    GroupName = v.F_GroupName,
                    BedNo = v.F_DialysisBedNo,
                    ShowNo = bt == null ? 99 : bt.F_ShowOrder,
                    VascularAccess = v.F_VascularAccess,
                    AccessName = v.F_AccessName,
                    EstimateHours = v.F_EstimateHours ?? 4,
                    WeightYT = v.F_WeightYT,
                    Heparin = new
                    {
                        Id = v.F_HeparinType,
                        Name = dt == null ? "" : dt.F_DrugName,
                        Amount = v.F_HeparinAmount,
                        Unit = v.F_HeparinUnit
                    }
                };
            var data = list.GroupJoin(_dialysisObservationApp.GetList().Where(o => o.F_DeleteMark != true),
                        l => new {pid = l.PId, visitDate = l.VisitDate},
                        o => new {pid = o.F_Pid, visitDate = o.F_VisitDate}, (l, o) => new
                        {
                            Record = l,
                            Observation = o.Select(r => new
                                {
                                    Id = r.F_Id,
                                    OperatorTime = r.F_NurseOperatorTime,
                                    NurseName = r.F_NurseName,
                                    Ssy = r.F_SSY,
                                    Szy = r.F_SZY,
                                    Hr = r.F_HR,
                                    A = r.F_A,
                                    Bf = r.F_BF,
                                    Ufr = r.F_UFR,
                                    V = r.F_V,
                                    C = r.F_C,
                                    T = r.F_T,
                                    Ufv = r.F_UFV,
                                    Tmp = r.F_TMP,
                                    Gsl = r.F_GSL,
                                    Memo = r.F_MEMO
                                })
                                .OrderByDescending(r => r.OperatorTime)
                        })
                    .OrderBy(n => n.Record.VisitNo).ThenBy(n => n.Record.GroupName).ThenBy(n => n.Record.ShowNo)
                ;

            return Ok(data
            //    .ToList().GroupBy(t => t.Record.VisitNo, (key, rows) => new
            //{
            //    VisitNo = key,
            //    Items = rows.Select(r => new
            //    {
            //        r.Record.Id,
            //        r.Record.VisitDate,
            //        r.Record.Name,
            //        r.Record.StartTime,
            //        r.Record.EndTime,
            //        Status = r.Record.StartTime == null ? 1 : r.Record.EndTime == null ? 2 : 3,
            //        r.Record.GroupName,
            //        r.Record.BedNo,
            //        r.Record.ShowNo,
            //        r.Record.VascularAccess,
            //        r.Record.AccessName,
            //        EstimateHours = r.Record.EstimateHours ?? 4,
            //        r.Record.Heparin,
            //        Observation = r.Observation.OrderByDescending(o => o.OperatorTime)
            //    }).OrderBy(i => i.GroupName).ThenBy(i => i.ShowNo)
            //}).OrderBy(n => n.VisitNo)
            );
        }
    }

    public class PatVisitListResult
    {
        public string Id { get; set; }
        public string Pid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string VisitDate { get; set; }
        public int VisitNo { get; set; }
        public string GroupName { get; set; }
        public string DialysisBedNo { get; set; }
        public int DialysisNo { get; set; }
        public string DialysisType { get; set; }
        public string DialyzerType1 { get; set; }
        public string DialyzerType2 { get; set; }
        public string VascularAccess { get; set; }
        public string HeparinType { get; set; }
        public string BirthDay { get; set; }
        public string AgeDesc { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartPerson { get; set; }
        public string EndPerson { get; set; }
        public string CheckPerson { get; set; }
        public string PuncturePerson { get; set; }
        public string Py { get; set; }
    }
}
