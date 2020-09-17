using Dmt.DM.Application.PatientManage;
using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.DialysisSchedule;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class DialysisScheduleController : BaseController
    {
        private readonly IDialysisScheduleApp _dialysisScheduleApp = null;
        private readonly IDialysisMachineApp _machineApp = null;
        private readonly IOrganizeApp _organizeApp = null;
        private readonly ISettingApp _settingApp = null;
        private readonly IPatientApp _patientApp = null;
        private readonly IPatVisitApp _patVisitApp = null;

        public DialysisScheduleController(
            IDialysisScheduleApp dialysisScheduleApp,
            IDialysisMachineApp machineApp,
            IOrganizeApp organizeApp,
            ISettingApp settingApp,
            IPatientApp patientApp,
            IPatVisitApp patVisitApp
            )
        {
            _dialysisScheduleApp = dialysisScheduleApp;
            _machineApp = machineApp;
            _organizeApp = organizeApp;
            _settingApp = settingApp;
            _patientApp = patientApp;
            _patVisitApp = patVisitApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword="",string groupName = "", string startDate="")
        {
            if (!DateTime.TryParse(startDate, out var mondayDate))
            {
                mondayDate = DateTime.Today;
            }
            var i = mondayDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;
            mondayDate = mondayDate.Subtract(new TimeSpan(i, 0, 0, 0));

            var rows =
                await _dialysisScheduleApp.GetWeeklyData(pagination, keyword, groupName, mondayDate);
            var data = new
            {
                rows,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetScheduleJson(DateTime visitDate,int visitNo)
        {
            var list = await _dialysisScheduleApp.GetScheduleList(visitDate, visitNo);
            var data = list.Select(t => new
            {
                t.F_Id,
                t.F_Name,
                t.F_DialysisNo,
                t.F_DialysisType,
                t.F_GroupName,
                t.F_DialysisBedNo,
                t.F_BId
            })
                .Join(_machineApp.GetQueryable().Select(t => new { t.F_Id, t.F_ShowOrder }),
                a => a.F_BId,
                b => b.F_Id,
                (a, b) => new
                {
                    a.F_Id,
                    a.F_Name,
                    a.F_DialysisNo,
                    a.F_DialysisType,
                    a.F_GroupName,
                    a.F_DialysisBedNo,
                    ShowOrder = b.F_ShowOrder.HasValue ? b.F_ShowOrder.ToInt() : 99
                })
                .OrderBy(r => r.F_GroupName).ThenBy(r => r.ShowOrder)
                ;
            return Content(data.ToJson());
        }

        /// <summary>
        /// 查询透析历史记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetHistoryListJson(string keyword)
        {
            var list = await _dialysisScheduleApp.GetHistoryList(keyword);
            return Content(list.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> ImportDatas([FromBody]ImportDatasInput input)
        {
            var startDate = input.StartDate.Date;
            var dayOfweek = startDate.DayOfWeek;
            startDate = dayOfweek == DayOfWeek.Monday ? startDate :
                dayOfweek == DayOfWeek.Tuesday ? startDate.AddDays(-1) :
                dayOfweek == DayOfWeek.Wednesday ? startDate.AddDays(-2) :
                dayOfweek == DayOfWeek.Thursday ? startDate.AddDays(-3) :
                dayOfweek == DayOfWeek.Friday ? startDate.AddDays(-4) :
                dayOfweek == DayOfWeek.Saturday ? startDate.AddDays(-5) :
                dayOfweek == DayOfWeek.Sunday ? startDate.AddDays(-6) :
                startDate;

            var list = await _dialysisScheduleApp.ImportDatas(startDate, input.FilePath);
            var maxDate = list.Select(t => t.F_VisitDate.ToDate()).Max();
            var eixstsRecords = _dialysisScheduleApp.GetList(startDate, maxDate).ToList();
            _dialysisScheduleApp.CreateItems(list, eixstsRecords);
            return Success("操作成功。");
        }
        /// <summary>
        /// 修改多条记录的透析方式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateBatch([FromBody]BaseInput input)
        {
            await _dialysisScheduleApp.SaveValues(input.KeyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFormCreatePatVisit([FromBody]BaseInput input)
        {
            //总数
            int count = 0;
            //新增数
            int addnum = 0;
            //SettingApp settingApp = new SettingApp();
            //var list = keyValue.ToJArrayObject().Select(t =>t.Value<string>("F_Id"));
            foreach (var id in input.KeyValue.ToJArrayObject().Select(t => t.Value<string>("F_Id")))
            {
                var schedule = await _dialysisScheduleApp.GetForm(id);
                count++;
                var patient = await _patientApp.GetForm(schedule.F_PId);

                //判断是否存在申请单 
                if (_patVisitApp.GetList().Count(t => t.F_Pid == patient.F_Id && t.F_VisitDate == schedule.F_VisitDate) > 0) continue;

                //查询透析参数
                var settings = (await _settingApp.GetList(schedule.F_PId)).OrderByDescending(t => t.F_CreatorTime);

                if (string.IsNullOrEmpty(schedule.F_DialysisType))
                {
                    schedule.F_DialysisType = "HD";
                }

                var firstSetting = settings.FirstOrDefault(t => t.F_DialysisType == schedule.F_DialysisType);
                PatVisitEntity entity;
                if (firstSetting != null)
                {
                    entity = new PatVisitEntity
                    {
                        F_VisitDate = schedule.F_VisitDate,
                        F_VisitNo = schedule.F_VisitNo,
                        F_BirthDay = patient.F_BirthDay,
                        F_Pid = patient.F_Id,
                        F_DialysisBedNo = schedule.F_DialysisBedNo,
                        F_DialysisNo = patient.F_DialysisNo,
                        F_DialysisType = schedule.F_DialysisType,
                        F_EnabledMark = true,
                        F_Gender = patient.F_Gender,
                        F_GroupName = schedule.F_GroupName,
                        F_HeparinAddAmount = firstSetting.F_HeparinAddAmount,
                        F_HeparinAmount = firstSetting.F_HeparinAmount,
                        F_HeparinType = firstSetting.F_HeparinType,
                        F_HeparinUnit = firstSetting.F_HeparinUnit,
                        F_AccessName = firstSetting.F_AccessName,
                        F_BloodSpeed = firstSetting.F_BloodSpeed,
                        F_Ca = firstSetting.F_Ca,
                        F_K = firstSetting.F_K,
                        F_Na = firstSetting.F_Na,
                        F_Hco3 = firstSetting.F_Hco3,
                        F_LowCa = firstSetting.F_LowCa,
                        F_DialysateTemperature = firstSetting.F_DialysateTemperature,
                        F_DialyzerType1 = firstSetting.F_DialyzerType1,
                        F_DialyzerType2 = firstSetting.F_DialyzerType2,
                        F_DilutionType = firstSetting.F_DilutionType,
                        F_EstimateHours = firstSetting.F_EstimateHours,
                        F_VascularAccess = firstSetting.F_VascularAccess,
                        F_InpNo = patient.F_PatientNo,
                        F_RecordNo = patient.F_RecordNo,
                        F_IsCritical = false,
                        F_Name = patient.F_Name,
                        F_PatientSourse = null
                    };
                }
                else
                {
                    entity = new PatVisitEntity
                    {
                        F_VisitDate = schedule.F_VisitDate,
                        F_VisitNo = schedule.F_VisitNo,
                        F_BirthDay = patient.F_BirthDay,
                        F_Pid = patient.F_Id,
                        F_DialysisBedNo = schedule.F_DialysisBedNo,
                        F_DialysisNo = patient.F_DialysisNo,
                        F_DialysisType = schedule.F_DialysisType,
                        F_EnabledMark = true,
                        F_Gender = patient.F_Gender,
                        F_GroupName = schedule.F_GroupName,
                        F_HeparinAddAmount = null,
                        F_HeparinAmount = null,
                        F_HeparinType = null,
                        F_HeparinUnit = null,
                        F_InpNo = patient.F_PatientNo,
                        F_RecordNo = patient.F_RecordNo,
                        F_IsCritical = false,
                        F_Name = patient.F_Name,
                        F_PatientSourse = null
                    };
                }
                await _patVisitApp.SubmitForm(entity, new object());
                addnum++;
            }
            return Success("操作成功,新增治疗单 " + addnum + " 个  , 总数：" + count);
        }

        //monday: new Date(weekStart).Format("yyyy-MM-dd"),
        //                id: id,
        //                pid: availableTags[id].id,
        //                dialysisNo: availableTags[id].recordNo,
        //                pname: availableTags[id].name,
        //                colname: name,
        //                val: val,
        //                iRow: iRow,
        //                iCol: iCol,
        //                group: $gridList.getCell(iRow, 1),
        //                bedNo: $gridList.getCell(iRow, 1),
        //                bid: $gridList.getCell(iRow, 'F_BId')

        /// <summary>
        /// 保存排班单元格数据
        /// </summary>
        /// <param name="monday"></param>
        /// <param name="pid"></param>
        /// <param name="pname"></param>
        /// <param name="dialysisNo"></param>
        /// <param name="colname"></param>
        /// <param name="value"></param>
        /// <param name="iRow"></param>
        /// <param name="iCol"></param>
        /// <param name="group"></param>
        /// <param name="bedNo"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        //[HttpPost]
        public async Task<IActionResult> SaveCellData(DateTime monday, string pid, string pname, int dialysisNo,
            string colname, string value, string iRow, string iCol, string group, string bedNo, string bid)
        {
            var visitDate = monday.Date;
            var visitNo = 0;
            switch (colname)
            {
                case "F_Monday1":
                    visitDate = visitDate.AddDays(0);
                    visitNo = 1;
                    break;
                case "F_Monday2":
                    visitDate = visitDate.AddDays(0);
                    visitNo = 2;
                    break;
                case "F_Monday3":
                    visitDate = visitDate.AddDays(0);
                    visitNo = 3;
                    break;
                case "F_Tuesday1":
                    visitDate = visitDate.AddDays(1);
                    visitNo = 1;
                    break;
                case "F_Tuesday2":
                    visitDate = visitDate.AddDays(1);
                    visitNo = 2;
                    break;
                case "F_Tuesday3":
                    visitDate = visitDate.AddDays(1);
                    visitNo = 3;
                    break;
                case "F_Wednesday1":
                    visitDate = visitDate.AddDays(2);
                    visitNo = 1;
                    break;
                case "F_Wednesday2":
                    visitDate = visitDate.AddDays(2);
                    visitNo = 2;
                    break;
                case "F_Wednesday3":
                    visitDate = visitDate.AddDays(2);
                    visitNo = 3;
                    break;
                case "F_Thursday1":
                    visitDate = visitDate.AddDays(3);
                    visitNo = 1;
                    break;
                case "F_Thursday2":
                    visitDate = visitDate.AddDays(3);
                    visitNo = 2;
                    break;
                case "F_Thursday3":
                    visitDate = visitDate.AddDays(3);
                    visitNo = 3;
                    break;
                case "F_Friday1":
                    visitDate = visitDate.AddDays(4);
                    visitNo = 1;
                    break;
                case "F_Friday2":
                    visitDate = visitDate.AddDays(4);
                    visitNo = 2;
                    break;
                case "F_Friday3":
                    visitDate = visitDate.AddDays(4);
                    visitNo = 3;
                    break;
                case "F_Saturday1":
                    visitDate = visitDate.AddDays(5);
                    visitNo = 1;
                    break;
                case "F_Saturday2":
                    visitDate = visitDate.AddDays(5);
                    visitNo = 2;
                    break;
                case "F_Saturday3":
                    visitDate = visitDate.AddDays(5);
                    visitNo = 3;
                    break;
                case "F_Sunday1":
                    visitDate = visitDate.AddDays(6);
                    visitNo = 1;
                    break;
                case "F_Sunday2":
                    visitDate = visitDate.AddDays(6);
                    visitNo = 2;
                    break;
                case "F_Sunday3":
                    visitDate = visitDate.AddDays(6);
                    visitNo = 3;
                    break;
            }
            if (visitNo==0) return Success("参数错误！");

            var bedEntity = await _machineApp.GetForm(bid);

            var entity = _dialysisScheduleApp.GetList().FirstOrDefault(r =>
                r.F_BId == bid && r.F_VisitDate == visitDate && r.F_VisitNo == visitNo);
                         
            if (entity == null)
            {
                if (string.IsNullOrWhiteSpace(pid)) {
                   return Success("操作成功,无数据变化");
                }
                else
                {
                    entity = new DialysisScheduleEntity();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(pid))
                {
                    await _dialysisScheduleApp.DeleteForm(entity.F_Id);
                    return Success("操作成功,数据已删除");
                }


            }
            if (!string.IsNullOrWhiteSpace(pid))
            {
                var record = _dialysisScheduleApp.GetList()
                    .FirstOrDefault(t => t.F_PId == pid && t.F_VisitDate == visitDate);
                if (record!=null)
                {
                    return Error(record.F_Name + "今日已排班，床号：" + record.F_DialysisBedNo + ",班次：" + record.F_VisitNo);
                }

            }

            entity.F_BId = bedEntity.F_Id;
            entity.F_DialysisBedNo = bedEntity.F_DialylisBedNo;
            entity.F_DialysisNo = dialysisNo;
            entity.F_DialysisType = bedEntity.F_DefaultType;
            entity.F_GroupName = bedEntity.F_GroupName;
            entity.F_Name = pname;
            entity.F_PId = pid;
            entity.F_Sort = bedEntity.F_ShowOrder;
            entity.F_VisitDate = visitDate;
            entity.F_VisitNo = visitNo;

            await _dialysisScheduleApp.SubmitForm(entity, entity.F_Id);

            return Success("操作成功。");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _dialysisScheduleApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> Copy([FromBody]BaseInput input)
        {
            var arr = input.KeyValue.Split('^');
            var bedno = string.Empty;
            DateTime date1;//= DateTime.Parse(arr[0]).Date;//本周周一时间
            DateTime date2;//= date1.AddDays(-7).Date;//上周周一时间
            if (arr.Length >= 2)
            {
                bedno = arr[0];
                date1 = DateTime.Parse(arr[1]).Date;
            }
            else
            {
                date1 = DateTime.Parse(input.KeyValue).Date;
            }
            date2 = date1.AddDays(-7).Date;
            //本周已排班信息
            var list1 = _dialysisScheduleApp.GetList(date1, date1.AddDays(6)).ToList();
            //上次周排班信息
            var list2 = _dialysisScheduleApp.GetList(date2,date2.AddDays(6)).Where(c => c.F_PId != null).ToList();

            await _dialysisScheduleApp.CreateItems(list2, list1, 7);
           
            return Success("生成排班成功。");
        }

        [HttpPost]
        public async Task<IActionResult> CustomCopy([FromBody]CustomCopyInput input)
        {
            //源周周一时间
            var sourceDate = input.SourceDate.Date;
            var targetDate = input.TargetDate.Date;
            //目标周周一时间
            targetDate = targetDate.DayOfWeek == DayOfWeek.Sunday ? targetDate.AddDays(1 - 7) :
                    targetDate.DayOfWeek == DayOfWeek.Monday ? targetDate.AddDays(7 - 7) :
                    targetDate.DayOfWeek == DayOfWeek.Tuesday ? targetDate.AddDays(6 - 7) :
                    targetDate.DayOfWeek == DayOfWeek.Wednesday ? targetDate.AddDays(5 - 7) :
                    targetDate.DayOfWeek == DayOfWeek.Thursday ? targetDate.AddDays(4 - 7) :
                    targetDate.DayOfWeek == DayOfWeek.Friday ? targetDate.AddDays(3 - 7) :
                    targetDate.DayOfWeek == DayOfWeek.Saturday ? targetDate.AddDays(2 - 7) : targetDate;
            if (targetDate <= sourceDate) return Error("日期选择有误！");
            //已排班信息
            var list1 = _dialysisScheduleApp.GetList(targetDate, targetDate.AddDays(6)).ToList();
            //源周排班信息
            var list2 = _dialysisScheduleApp.GetList(sourceDate, sourceDate.AddDays(6)).ToList();

            await _dialysisScheduleApp.CreateItems(list2.FindAll(c => c.F_PId != null), list1, (targetDate - sourceDate).TotalDays.ToInt());
            return Success("生成排班成功。");
        }

        [HttpPost]
        public async Task<IActionResult> SaveToNextWeek([FromBody]BaseInput input)
        {
            if (DateTime.TryParse(input.KeyValue,out var startDate))
            {
                var currentDate = DateTime.Now.Date;
                //下周周一时间
                var monday = currentDate.DayOfWeek == DayOfWeek.Sunday ? currentDate.AddDays(1) :
                    currentDate.DayOfWeek == DayOfWeek.Monday ? currentDate.AddDays(7) :
                    currentDate.DayOfWeek == DayOfWeek.Tuesday ? currentDate.AddDays(6) :
                    currentDate.DayOfWeek == DayOfWeek.Wednesday ? currentDate.AddDays(5) :
                    currentDate.DayOfWeek == DayOfWeek.Thursday ? currentDate.AddDays(4) :
                    currentDate.DayOfWeek == DayOfWeek.Friday ? currentDate.AddDays(3) :
                    currentDate.DayOfWeek == DayOfWeek.Saturday ? currentDate.AddDays(2) : currentDate;
                if (startDate >= monday) return Error("数据源日期【" + startDate.ToDateString() + "】不能大于" + monday.ToDateString());
                var nextlist = _dialysisScheduleApp.GetList(monday, monday.AddDays(6)).ToList();
                var interval = (monday - startDate).TotalDays.ToInt();
                if (interval <= 0)
                {
                    return Error("起始日期错误,不能是本周一！");
                }
                ////按照床号 患者ID 过滤  患者停用  床停用等
                //var list = from d in _dialysisScheduleApp.GetList(startDate, startDate.AddDays(6)).FindAll(c => c.F_PId != null)
                //           join m in machineApp.GetList() on d.F_BId equals m.F_Id
                //           join p in patientApp.GetList() on d.F_PId equals p.F_Id
                //           select d;
                await _dialysisScheduleApp.CreateItems((_dialysisScheduleApp.GetList(startDate, startDate.AddDays(6)).ToList()).FindAll(c => c.F_PId != null), nextlist, interval);
            }
            else
            {
                return Error("起始日期错误");
            }
            return Success("操作成功。");
        }
        
        /// <summary>
        /// 删除某一床位指定周次的排班信息
        /// </summary>
        /// <param name="input">床号和星期一日期</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Clear([FromBody]ClearInput input)
        {

            var bid = input.Bid;
            //var startDate - input.Monday;
            var startDate = input.Monday.Date;
            var endDate = startDate.AddDays(6);
            await _dialysisScheduleApp.DeleteForm(bid, startDate, endDate);
            return Success("删除排班成功。");
        }

        /// <summary>
        /// 获取打印报告
        /// </summary>
        /// <param name="input">星期一日期字符串</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetScheduleImage([FromBody]BaseInput input)
        {
            return Content(_dialysisScheduleApp.GetReport(input.KeyValue));
        }
        
        [HttpPost]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult>  Download(string keyValue)
        {
            var hospitalName = await _organizeApp.GetHospitalName();
            var filepath = _dialysisScheduleApp.CopyModelFile();// Server.MapPath(data.F_FilePath);
            //填充数据
            await _dialysisScheduleApp.FillData(DateTime.Parse(keyValue), filepath, hospitalName);
            var fileExt = FileHelper.GetExtension(filepath);
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            var data = FileHelper.GetFileData(filepath);
            return File(data, memi, FileHelper.GetFileName(filepath));
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> RecieveFiles(IFormFile reportFile, int file_id)
        {
            var targetPath = Path.Combine(AppConsts.AppRootPath, "upload", "download");
            FileHelper.CreateDirectory(targetPath);
            var serialNo = Common.CreateNo();
            var fileName = Path.Combine(targetPath, serialNo + Path.GetExtension(reportFile.FileName));
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                await reportFile.CopyToAsync(stream);
                stream.Flush();
            }
            var data = new
            {
                pathUrl = fileName
            };
            return Success(data.ToJson());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult FormZLD()
        {
            return View();
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChooseDateForm()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Import()
        {
            return View();
        }
    }
}
