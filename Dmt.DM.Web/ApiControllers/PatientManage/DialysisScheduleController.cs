using System;
using System.Collections.Generic;
using System.Linq;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.Dm.Domain.Dto.DialysisSchedule;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class DialysisScheduleController : BaseApiController
    {
        private readonly IDialysisScheduleApp _dialysisScheduleApp;
        private readonly IDialysisMachineApp _dialysisMachineApp;
        private readonly IPatientApp _patientApp;
        private readonly IPatVisitApp _patVisitApp;

        public DialysisScheduleController(IDialysisScheduleApp dialysisScheduleApp, IDialysisMachineApp dialysisMachineApp, IPatientApp patientApp, IPatVisitApp patVisitApp)
        {
            _dialysisScheduleApp = dialysisScheduleApp;
            _dialysisMachineApp = dialysisMachineApp;
            _patientApp = patientApp;
            _patVisitApp = patVisitApp;
        }

        public IActionResult GetWeeklyList(GetWeeklyListInput input = null)
        {
            if (input == null)
            {
                input = new GetWeeklyListInput();
            }
            var startDate = input.scheduleDate.ToDate().Date;
            startDate = startDate.DayOfWeek == DayOfWeek.Monday ? startDate
                : startDate.DayOfWeek == DayOfWeek.Tuesday ? startDate.AddDays(-1)
                : startDate.DayOfWeek == DayOfWeek.Wednesday ? startDate.AddDays(-2)
                : startDate.DayOfWeek == DayOfWeek.Thursday ? startDate.AddDays(-3)
                : startDate.DayOfWeek == DayOfWeek.Friday ? startDate.AddDays(-4)
                : startDate.DayOfWeek == DayOfWeek.Saturday ? startDate.AddDays(-5)
                : startDate.DayOfWeek == DayOfWeek.Sunday ? startDate.AddDays(-6)
                : startDate;
            var endDate = startDate.AddDays(6);
            var visitNo = input.visitNo.ToInt();

            var beds = _dialysisMachineApp.GetQueryable().Select(t => new
            {
                groupName = t.F_GroupName,
                bedId = t.F_Id,
                bedNo = t.F_DialylisBedNo,
                showOrder = t.F_ShowOrder,
                machineName = t.F_MachineName,
                machineNo = t.F_MachineNo,
                defaultType = t.F_DefaultType
            }).ToList();

            var query = _dialysisScheduleApp.GetList()
                .Where(t => t.F_VisitDate >= startDate && t.F_VisitDate <= endDate)
                .Where(t => t.F_VisitNo == visitNo);
            if (!string.IsNullOrEmpty(input.dialysisTypes))
            {
                var typesArr = input.dialysisTypes.Split(',');
                query = query.Where(t => typesArr.Contains(t.F_DialysisType));
            }
            if (!string.IsNullOrEmpty(input.patientId))
            {
                query = query.Where(t => t.F_PId.Equals(input.patientId));
            }
            var sourse = query.Select(t => new
            {
                scheduleId = t.F_Id,
                visitDate = t.F_VisitDate,
                pid = t.F_PId,
                patientName = t.F_Name,
                dialysisType = t.F_DialysisType,
                bid = t.F_BId
            }).ToList();
            var list = sourse.Select(t => new
            {
                bedId = t.bid,
                t.scheduleId,
                t.visitDate,
                dayOfWeek = t.visitDate.ToDate().DayOfWeek,
                patientId = t.pid,
                t.patientName,
                t.dialysisType
            });

            var data = beds.GroupBy(t => t.groupName).Select(t => new
            {
                groupName = t.Key,
                rows = t.GroupJoin(list, a => a.bedId, b => b.bedId, (a, b) => new
                {
                    a.bedId,
                    a.bedNo,
                    a.showOrder,
                    a.machineName,
                    a.machineNo,
                    a.defaultType,
                    items = b.OrderBy(v => v.visitDate)
                }).OrderBy(s => s.showOrder)
            }).OrderBy(g => g.groupName);

            return Ok(data);
        }
        /// <summary>
        /// 生成治疗单 刷选患者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult GetSchedulePatientList(GetSchedulePatientListInput input)
        {
            var query = _dialysisScheduleApp.GetList()
                .Where(t => t.F_VisitDate == input.visitDate)
                .Where(t => t.F_VisitNo == input.visitNo);
            var sourse = query.Select(t => new
            {
                scheduleId = t.F_Id,
                visitDate = t.F_VisitDate,
                pid = t.F_PId,
                patientName = t.F_Name,
                dialysisType = t.F_DialysisType,
                bid = t.F_BId
            }).ToList();
            var data = sourse.Select(t => new
            {
                bedId = t.bid,
                t.scheduleId,
                t.visitDate,
                dayOfWeek = t.visitDate.ToDate().DayOfWeek,
                patientId = t.pid,
                t.patientName,
                t.dialysisType
            });
            return Ok(data);
        }
        
        public IActionResult GetFilterList(GetFilterListInput input)
        {
            //设置筛选条件
            var query = _dialysisScheduleApp.GetList()
                .Where(t => t.F_PId.Equals(input.patientId))
                .Where(t => t.F_VisitDate >= input.startDate && t.F_VisitDate <= input.endDate)
                .Where(t => t.F_EnabledMark == true);
            if (!string.IsNullOrEmpty(input.dialysisType))
            {
                query = query.Where(t => t.F_DialysisType.Equals(input.dialysisType));
            }
            //查询数据
            var list = query.Select(t => new
            {
                t.F_Id,
                t.F_GroupName,
                t.F_DialysisBedNo,
                t.F_VisitDate,
                t.F_VisitNo,
                t.F_DialysisType
            })
            .ToList()
            .Select(t => new
            {
                id = t.F_Id,
                groupName = t.F_GroupName,
                dialysisBedNo = t.F_DialysisBedNo,
                visitDate = t.F_VisitDate,
                monthDesc = t.F_VisitDate.ToDateString().Substring(0, 7),
                weekDesc = t.F_VisitDate.ToDate().DayOfWeek,
                visitNo = t.F_VisitNo.ToInt(),
                dialysisType = t.F_DialysisType
            });
            var months = list.Select(t => t.monthDesc).Distinct().OrderByDescending(t => t);
            var data = new List<GetFilterListOutput>();
            foreach (var month in months)
            {
                var entity = new GetFilterListOutput
                {
                    monthDesc = month
                };
                foreach (var child in list.Where(t => t.monthDesc.Equals(month)).OrderByDescending(t => t.visitDate))
                {
                    var date = child.visitDate.ToDate();
                    var week = date.DayOfWeek == DayOfWeek.Monday ? "周一" :
                        date.DayOfWeek == DayOfWeek.Tuesday ? "周二" :
                        date.DayOfWeek == DayOfWeek.Wednesday ? "周三" :
                        date.DayOfWeek == DayOfWeek.Thursday ? "周四" :
                        date.DayOfWeek == DayOfWeek.Friday ? "周五" :
                        date.DayOfWeek == DayOfWeek.Saturday ? "周六" :
                        "周日";
                    entity.items.Add(new ScheduleItem
                    {
                        dayDesc = date.Day.ToString() + "号",
                        weekDesc = week,
                        visitNo = child.visitNo,
                        groupName = child.groupName,
                        dialysisBedNo = child.dialysisBedNo,
                        dialysisType = child.dialysisType
                    });
                }
                data.Add(entity);
            }
            return Ok(data);
        }
        /// <summary>
        /// 根据刷选条件查询排班信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IActionResult GetListJson(GetListJsonInput input)
        {
            var query = _dialysisScheduleApp.GetList(input.startDate, input.endDate);
            query = input.patientId.IsEmpty() ? query : query.Where(t => t.F_PId == input.patientId);
            query = input.bedId.IsEmpty() ? query : query.Where(t => t.F_BId == input.bedId);
            if (input.firstFlag == true && input.secondFlag == true && input.thirdFlag == true)
            {
            }
            else if (input.firstFlag == true && input.secondFlag == true)
            {
                query = query.Where(t => t.F_VisitNo == 1 || t.F_VisitNo == 2);
            }
            else if (input.firstFlag == true && input.thirdFlag == true)
            {
                query = query.Where(t => t.F_VisitNo == 1 || t.F_VisitNo == 3);
            }
            else if (input.secondFlag == true && input.thirdFlag == true)
            {
                query = query.Where(t => t.F_VisitNo == 2 || t.F_VisitNo == 3);
            }
            else if (input.firstFlag == true)
            {
                query = query.Where(t => t.F_VisitNo == 1);
            }
            else if (input.secondFlag == true)
            {
                query = query.Where(t => t.F_VisitNo == 2);
            }
            else if (input.thirdFlag == true)
            {
                query = query.Where(t => t.F_VisitNo == 3);
            }
            var data = query.Select(t => new
            {
                t.F_Id,
                t.F_PId,
                t.F_BId,
                t.F_DialysisBedNo,
                t.F_GroupName,
                t.F_DialysisNo,
                t.F_DialysisType,
                t.F_Name,
                t.F_VisitDate,
                t.F_VisitNo
            });
            return Ok(data);
        }
        /// <summary>
        /// 查找单一排班信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFormJson")]
        public IActionResult GetFormJson(BaseInput input)
        {
            return Ok(_dialysisScheduleApp.GetForm(input.KeyValue));
        }
    }
}
