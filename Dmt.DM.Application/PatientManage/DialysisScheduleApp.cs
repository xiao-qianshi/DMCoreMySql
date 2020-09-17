using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Code.Excel;
using Dmt.DM.Code.Excel.Model;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IDialysisScheduleApp : IScopedAppService
    {
        Task<List<DialysisScheduleEntity>> GetList(Pagination pagination, string keyword);
        IQueryable<DialysisScheduleEntity> GetList();

        /// <summary>
        /// 根据起始截至日期查询排班信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IEnumerable<DialysisScheduleEntity> GetList(DateTime startDate, DateTime endDate);

        Task<List<DialysisScheduleEntity>> GetScheduleList(DateTime visitDate, int visitNo);
        Task<DialysisScheduleEntity> GetForm(string keyValue);
        Task<DialysisScheduleEntity> FindForm(string bid, DateTime visitDate, int visitNo);
        Task<bool> ExistsForm(string bid, DateTime visitDate, int visitNo);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(DialysisScheduleEntity entity);
        Task<int> SubmitForm(DialysisScheduleEntity entity, string keyValue);
        Task<List<DialysisScheduleEntity>> GetHistoryList(string keyword);
        Task SaveValues(string keyValue);

        /// <summary>
        /// 生成并返回Excel文件名
        /// </summary>
        /// <returns></returns>
        string CopyModelFile();

        Task FillData(DateTime monday, string filepath, string hospitalName);
        Task<List<ImportScheduleModel>> ImportDatas(DateTime startDate, string filePath);
        Task<IEnumerable<ScheduleWeekEntity>> GetWeeklyData(Pagination pagination, string keyword, string groupName, DateTime mondayDate);
        Task<int> DeleteForm(string bid, DateTime startDate, DateTime endDate);
        string GetReport(string keyValue);

        /// <summary>
        /// 复制排班 逻辑
        /// </summary>
        /// <param name="source">源排班信息</param>
        /// <param name="target">目标已排班信息</param>
        /// <param name="interval">间隔天数</param>
        Task CreateItems(IEnumerable<DialysisScheduleEntity> source, List<DialysisScheduleEntity> target, int interval);

        void CreateItems(List<ImportScheduleModel> source, List<DialysisScheduleEntity> target);
    }

    public class DialysisScheduleApp : IDialysisScheduleApp
    {
        private readonly IRepository<DialysisScheduleEntity> _service = null;
        private IUnitOfWork _uow = null;
        //private readonly IHttpContextAccessor _httpContext = null;
        private readonly IItemsDetailApp _itemsDetailApp = null;

        private readonly IUsersService _usersService = null;
        private readonly IDialysisMachineApp _dialysisMachineApp = null;
        private readonly IPatientApp _patientApp = null;
    

        public DialysisScheduleApp(IUnitOfWork uow, IUsersService usersService, IItemsDetailApp itemsDetailApp ,IDialysisMachineApp dialysisMachineApp, IPatientApp patientApp)
        {
            _uow = uow;
            _service = _uow.GetRepository<DialysisScheduleEntity>();
            _usersService = usersService;
            _itemsDetailApp = itemsDetailApp;
            _dialysisMachineApp = dialysisMachineApp;
            _patientApp = patientApp;
        }

        public Task<List<DialysisScheduleEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_DialysisBedNo == keyword);
                expression = expression.Or(t => t.F_GroupName == keyword);
                expression = expression.Or(t => t.F_Name.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public IQueryable<DialysisScheduleEntity> GetList()
        {
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression);
        }
       
        /// <summary>
        /// 根据起始截至日期查询排班信息
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<DialysisScheduleEntity> GetList(DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            expression = expression.And(t => t.F_VisitDate >= startDate);
            expression = expression.And(t => t.F_VisitDate <= endDate);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression);
        }

        public Task<List<DialysisScheduleEntity>> GetScheduleList(DateTime visitDate, int visitNo)
        {
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            expression = expression.And(t => t.F_VisitDate == visitDate);
            expression = expression.And(t => t.F_VisitNo == visitNo);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<DialysisScheduleEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }

        public async Task<DialysisScheduleEntity> FindForm(string bid, DateTime visitDate, int visitNo)
        {
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            expression = expression.And(t => t.F_VisitDate == visitDate);
            expression = expression.And(t => t.F_VisitNo == visitNo);
            expression = expression.And(t => t.F_BId == bid);
            expression = expression.And(t => t.F_DeleteMark != true);
            var result = await _service.IQueryable(expression).FirstOrDefaultAsync();
            return result ?? new DialysisScheduleEntity();
        }

        public Task<bool> ExistsForm(string bid, DateTime visitDate, int visitNo)
        {
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            expression = expression.And(t => t.F_VisitDate == visitDate);
            expression = expression.And(t => t.F_VisitNo == visitNo);
            expression = expression.And(t => t.F_BId == bid);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).AnyAsync();
        }

        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(DialysisScheduleEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm(DialysisScheduleEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                return _service.UpdateAsync(entity);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

        public Task<List<DialysisScheduleEntity>> GetHistoryList(string keyword)
        {
            var list = new List<DialysisScheduleEntity>();
            var json = keyword.ToJObject();
            var bid = json.Value<string>("bid");
            var pid = json.Value<string>("pid");
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate");
            if (!string.IsNullOrEmpty(pid))
            {
                var expression3 = ExtLinq.True<DialysisScheduleEntity>();
                expression3 = expression3.And(t => t.F_PId == pid)
                    .And(t => t.F_VisitDate >= startDate)
                    .And(t => t.F_VisitDate <= endDate)
                    .And(t => t.F_DeleteMark != true);
                return _service.IQueryable(expression3).OrderBy(t => t.F_VisitDate).ToListAsync();
            }
            if (string.IsNullOrEmpty(bid))
            {
                return Task.FromResult(list);
            }
            var visitDate = json.Value<DateTime>("visitDate");
            var visitNo = json.Value<int>("visitNo");
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            expression = expression.And(t => t.F_BId == bid)
                .And(t => t.F_VisitDate == visitDate)
                .And(t => t.F_VisitNo == visitNo)
                .And(t => t.F_DeleteMark != true);
            var entity = _service.FindEntity(expression);
            if (entity == null || string.IsNullOrEmpty(entity.F_PId))
            {
                return Task.FromResult(list);
            }
            var expression2 = ExtLinq.True<DialysisScheduleEntity>();
            expression2 = expression2.And(t => t.F_PId == entity.F_PId)
                .And(t => t.F_VisitDate >= startDate)
                .And(t => t.F_VisitDate <= endDate)
                .And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression2).OrderBy(t => t.F_VisitDate).ToListAsync();
        }

        public Task SaveValues(string keyValue)
        {
            foreach (var item in keyValue.ToJArrayObject())
            {
                var id = item.Value<string>("id");
                var value = item.Value<string>("value");
                var entity = _service.FindEntity(id);
                if (entity.F_DialysisType.Equals(value)) continue;
                entity.F_DialysisType = value;
                _service.Update(entity);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 生成并返回Excel文件名
        /// </summary>
        /// <returns></returns>
        public string CopyModelFile()
        {
            return Path.Combine(AppConsts.AppRootPath, "upload", "download", Common.CreateNo() + ".xls");
        }

        public async Task FillData(DateTime monday, string filepath, string hospitalName)
        {
            NPOIExcel excel = new NPOIExcel();
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("GroupName"),
                new DataColumn("BedNo"),
                new DataColumn("Col1"),
                new DataColumn("Col2"),
                new DataColumn("Col3"),
                new DataColumn("Col4"),
                new DataColumn("Col5"),
                new DataColumn("Col6"),
                new DataColumn("Col7"),
                new DataColumn("Col8"),
                new DataColumn("Col9"),
                new DataColumn("Col10"),
                new DataColumn("Col11"),
                new DataColumn("Col12"),
                new DataColumn("Col13"),
                new DataColumn("Col14"),
                new DataColumn("Col15"),
                new DataColumn("Col16"),
                new DataColumn("Col17"),
                new DataColumn("Col18"),
                new DataColumn("Col19"),
                new DataColumn("Col20"),
                new DataColumn("Col21"),
                new DataColumn("ShowOrder",typeof(Int32))
            });
            var list = new List<string>
            {
                "星期一" + "(" + monday.ToDateString().Replace("-","") + ")",
                "星期二" + "(" + monday.AddDays(1).ToDateString().Replace("-","") + ")",
                "星期三" + "(" + monday.AddDays(2).ToDateString().Replace("-","") + ")",
                "星期四" + "(" + monday.AddDays(3).ToDateString().Replace("-","") + ")",
                "星期五" + "(" + monday.AddDays(4).ToDateString().Replace("-","") + ")",
                "星期六" + "(" + monday.AddDays(5).ToDateString().Replace("-","") + ")",
                "星期日" + "(" + monday.AddDays(6).ToDateString().Replace("-","") + ")",
            };
            var startDate = monday.Date;
            var endDate = startDate.AddDays(6);
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            expression = expression.And(t => t.F_VisitDate >= startDate);
            expression = expression.And(t => t.F_VisitDate <= endDate);
            expression = expression.And(t => t.F_DeleteMark != true);
            expression = expression.And(t => t.F_EnabledMark != false);

            var records = _service.IQueryable(expression).Select(t => new
            {
                VisitDate = t.F_VisitDate,
                VisitNo = t.F_VisitNo,
                GroupName = t.F_GroupName,
                BedNo = t.F_DialysisBedNo,
                Name = t.F_Name,
                DialysisType = t.F_DialysisType
            }).ToList();
            var results = records.Select(t => new
            {
                VisitDate = t.VisitDate.ToDate(),
                VisitNo = t.VisitNo.ToInt(),
                t.GroupName,
                t.BedNo,
                t.Name,
                t.DialysisType
            });
            //默认透析模式 不显示
            var filterTypes = new string[] { await _itemsDetailApp.GetDefaultDialysisType() ?? "HD" };

            foreach (var item in results)
            {
                var row = table.Select("GroupName='" + item.GroupName + "' and BedNo='" + item.BedNo + "'").FirstOrDefault();
                //row = row ?? table.NewRow();
                if (row == null)
                {
                    row = table.NewRow();
                    row[0] = item.GroupName;
                    row[1] = item.BedNo;
                    if (!int.TryParse(item.BedNo, out int showOrder)) showOrder = 9999;
                    row[23] = showOrder;
                    table.Rows.Add(row);
                }
                var interval = (item.VisitDate - startDate).TotalDays.ToInt();
                row[2 + interval * 3 + item.VisitNo - 1] = item.Name + (filterTypes.Contains(item.DialysisType) ? "" : item.DialysisType);
            }
            var tableView = table.DefaultView;
            tableView.Sort = "GroupName asc, ShowOrder asc";
            table = tableView.ToTable();
            excel.ToExcelForSchedule(table, hospitalName + "排班表", "日期：" + startDate.ToDateString() + " ~ " + endDate.ToDateString(), list, "合计：    人次", "排班明细", filepath);
        }

        public async Task<List<ImportScheduleModel>> ImportDatas(DateTime startDate, string filePath)
        {
            var defaultDialysisType = await _itemsDetailApp.GetDefaultDialysisType() ?? "HD";
            var list = new List<ImportScheduleModel>();
            NPOIExcel excel = new NPOIExcel();
            var dialysisTypes = (await _itemsDetailApp.GetItemList("DialysisType")).Select(t => t.F_ItemCode).OrderByDescending(t => t.Length).ToList();
            var records = excel.ToListForSchedule("排班明细", filePath, dialysisTypes, 4, 0);
            //var groups = 
            var groups = (await _itemsDetailApp.GetItemList("BedGroup")).Select(t => t.F_ItemCode).ToList();
            var beds = await _dialysisMachineApp.GetList();
            var patients = await _patientApp.GetList();
            foreach (var item in records.GroupBy(t => t.F_GroupName))
            {
                if (!groups.Contains(item.Key)) continue;
                foreach (var child in item.GroupBy(t => t.F_DialysisBedNo))
                {
                    var findBed = beds.Find(t => t.F_GroupName == item.Key && t.F_DialylisBedNo == child.Key);
                    if (findBed == null) continue;
                    foreach (var ele in child)
                    {
                        var patient = patients.Find(t => t.F_Name == ele.F_Name.Trim());
                        if (patient == null) continue;
                        list.Add(new ImportScheduleModel
                        {
                            F_GroupName = item.Key,
                            F_DialysisBedNo = child.Key,
                            F_BId = findBed.F_Id,
                            F_VisitDate = startDate.AddDays(ele.DayOfWeek - 1),
                            F_VisitNo = ele.F_VisitNo,
                            F_PId = patient.F_Id,
                            F_DialysisNo = patient.F_DialysisNo,
                            F_Name = patient.F_Name,
                            F_DialysisType = ele.F_DialysisType ?? defaultDialysisType,
                            F_Sort = findBed.F_ShowOrder.ToInt()
                        });
                    }

                }
            }
            return list;
        }

        public async Task<IEnumerable<ScheduleWeekEntity>> GetWeeklyData(Pagination pagination, string keyword, string groupName, DateTime mondayDate)
        {
            //床位信息分页
            var machineService = _uow.GetRepository<DialysisMachineEntity>();
            var expression = ExtLinq.True<DialysisMachineEntity>();
            if (!string.IsNullOrEmpty(groupName)) expression = expression.And(t => t.F_GroupName == groupName);
            expression = expression.And(t => t.F_DeleteMark != true);
            pagination.sidx = "F_ShowOrder asc";
            var beds = await machineService.FindListAsync(expression, pagination);
            var queryList = GetList(mondayDate, mondayDate.AddDays(6));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                queryList = queryList.Where(t => t.F_Name.Contains(keyword));
            }
            var listSum = queryList
                .Select(t => new
                {
                    t.F_Id,
                    t.F_PId,
                    t.F_DialysisBedNo,
                    t.F_GroupName,
                    t.F_BId,
                    t.F_Name,
                    t.F_VisitDate,
                    t.F_DialysisType,
                    t.F_VisitNo
                }).ToList();

            var resultList = new List<ScheduleWeekEntity>();

            foreach (var machine in beds)
            {
                var entity = new ScheduleWeekEntity
                {
                    F_BId = machine.F_Id,
                    F_DialysisBedNo = machine.F_DialylisBedNo,
                    F_GroupName = machine.F_GroupName
                };
                var list = listSum.FindAll(e => e.F_BId == machine.F_Id);
                if (list.Any())
                {
                    var listTemp = list.FindAll(e => e.F_VisitDate == mondayDate);
                    //mon
                    var temp = listTemp.Find(e => e.F_VisitNo == 1);
                    if (temp != null)
                    {
                        entity.F_PId1 = temp.F_PId;
                        entity.F_DialysisType1 = temp.F_DialysisType;
                        entity.F_Monday1 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 2);
                    if (temp != null)
                    {
                        entity.F_PId2 = temp.F_PId;
                        entity.F_DialysisType2 = temp.F_DialysisType;
                        entity.F_Monday2 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 3);
                    if (temp != null)
                    {
                        entity.F_PId3 = temp.F_PId;
                        entity.F_DialysisType3 = temp.F_DialysisType;
                        entity.F_Monday3 = temp.F_Name;
                    }
                    //tues
                    listTemp = list.FindAll(e => e.F_VisitDate == mondayDate.AddDays(1));
                    temp = listTemp.Find(e => e.F_VisitNo == 1);
                    if (temp != null)
                    {
                        entity.F_PId4 = temp.F_PId;
                        entity.F_DialysisType4 = temp.F_DialysisType;
                        entity.F_Tuesday1 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 2);
                    if (temp != null)
                    {
                        entity.F_PId5 = temp.F_PId;
                        entity.F_DialysisType5 = temp.F_DialysisType;
                        entity.F_Tuesday2 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 3);
                    if (temp != null)
                    {
                        entity.F_PId6 = temp.F_PId;
                        entity.F_DialysisType6 = temp.F_DialysisType;
                        entity.F_Tuesday3 = temp.F_Name;
                    }
                    //wed
                    listTemp = list.FindAll(e => e.F_VisitDate == mondayDate.AddDays(2));
                    temp = listTemp.Find(e => e.F_VisitNo == 1);
                    if (temp != null)
                    {
                        entity.F_PId7 = temp.F_PId;
                        entity.F_DialysisType7 = temp.F_DialysisType;
                        entity.F_Wednesday1 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 2);
                    if (temp != null)
                    {
                        entity.F_PId8 = temp.F_PId;
                        entity.F_DialysisType8 = temp.F_DialysisType;
                        entity.F_Wednesday2 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 3);
                    if (temp != null)
                    {
                        entity.F_PId9 = temp.F_PId;
                        entity.F_DialysisType9 = temp.F_DialysisType;
                        entity.F_Wednesday3 = temp.F_Name;
                    }
                    //thur
                    listTemp = list.FindAll(e => e.F_VisitDate == mondayDate.AddDays(3));
                    temp = listTemp.Find(e => e.F_VisitNo == 1);
                    if (temp != null)
                    {
                        entity.F_PId10 = temp.F_PId;
                        entity.F_DialysisType10 = temp.F_DialysisType;
                        entity.F_Thursday1 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 2);
                    if (temp != null)
                    {
                        entity.F_PId11 = temp.F_PId;
                        entity.F_DialysisType11 = temp.F_DialysisType;
                        entity.F_Thursday2 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 3);
                    if (temp != null)
                    {
                        entity.F_PId12 = temp.F_PId;
                        entity.F_DialysisType12 = temp.F_DialysisType;
                        entity.F_Thursday3 = temp.F_Name;
                    }
                    //fri
                    listTemp = list.FindAll(e => e.F_VisitDate == mondayDate.AddDays(4));
                    temp = listTemp.Find(e => e.F_VisitNo == 1);
                    if (temp != null)
                    {
                        entity.F_PId13 = temp.F_PId;
                        entity.F_DialysisType13 = temp.F_DialysisType;
                        entity.F_Friday1 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 2);
                    if (temp != null)
                    {
                        entity.F_PId14 = temp.F_PId;
                        entity.F_DialysisType14 = temp.F_DialysisType;
                        entity.F_Friday2 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 3);
                    if (temp != null)
                    {
                        entity.F_PId15 = temp.F_PId;
                        entity.F_DialysisType15 = temp.F_DialysisType;
                        entity.F_Friday3 = temp.F_Name;
                    }
                    //sat
                    listTemp = list.FindAll(e => e.F_VisitDate == mondayDate.AddDays(5));
                    temp = listTemp.Find(e => e.F_VisitNo == 1);
                    if (temp != null)
                    {
                        entity.F_PId16 = temp.F_PId;
                        entity.F_DialysisType16 = temp.F_DialysisType;
                        entity.F_Saturday1 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 2);
                    if (temp != null)
                    {
                        entity.F_PId17 = temp.F_PId;
                        entity.F_DialysisType17 = temp.F_DialysisType;
                        entity.F_Saturday2 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 3);
                    if (temp != null)
                    {
                        entity.F_PId18 = temp.F_PId;
                        entity.F_DialysisType18 = temp.F_DialysisType;
                        entity.F_Saturday3 = temp.F_Name;
                    }
                    //sun
                    listTemp = list.FindAll(e => e.F_VisitDate == mondayDate.AddDays(6));
                    temp = listTemp.Find(e => e.F_VisitNo == 1);
                    if (temp != null)
                    {
                        entity.F_PId19 = temp.F_PId;
                        entity.F_DialysisType19 = temp.F_DialysisType;
                        entity.F_Sunday1 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 2);
                    if (temp != null)
                    {
                        entity.F_PId20 = temp.F_PId;
                        entity.F_DialysisType20 = temp.F_DialysisType;
                        entity.F_Sunday2 = temp.F_Name;
                    }
                    temp = listTemp.Find(e => e.F_VisitNo == 3);
                    if (temp != null)
                    {
                        entity.F_PId21 = temp.F_PId;
                        entity.F_DialysisType21 = temp.F_DialysisType;
                        entity.F_Sunday3 = temp.F_Name;
                    }
                }
                resultList.Add(entity);
            }
            return resultList;
        }

        public Task<int> DeleteForm(string bid, DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<DialysisScheduleEntity>();
            expression = expression.And(t => t.F_BId == bid);
            expression = expression.And(t => t.F_VisitDate >= startDate && t.F_VisitDate <= endDate);
            return _service.DeleteAsync(expression);
        }

        public string GetReport(string keyValue)
        {
            var reportFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "Schedule.frx");//.MapPath("\\ReportFiles\\Schedule.frx");
            string dataSetName = "HSDataSet";
            DataSet data = new DataSet();
            DataTable title = new DataTable("ScheduleTitle");
            title.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("VisitDate1"),
                new DataColumn("VisitDate2"),
                new DataColumn("VisitDate3"),
                new DataColumn("VisitDate4"),
                new DataColumn("VisitDate5"),
                new DataColumn("VisitDate6"),
                new DataColumn("VisitDate7")
            });
            DateTime date = DateTime.Parse(keyValue);
            title.Rows.Add(new object[]
            {
                date.ToString("yyyy-MM-dd"),
                date.AddDays(1).ToString("yyyy-MM-dd"),
                date.AddDays(2).ToString("yyyy-MM-dd"),
                date.AddDays(3).ToString("yyyy-MM-dd"),
                date.AddDays(4).ToString("yyyy-MM-dd"),
                date.AddDays(5).ToString("yyyy-MM-dd"),
                date.AddDays(6).ToString("yyyy-MM-dd")
            });

            data.Tables.Add(title);
            DataTable detail = new DataTable("ScheduleDetail");
            detail.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id"),
                new DataColumn("BId"),
                new DataColumn("Name1"),
                new DataColumn("Name2"),
                new DataColumn("Name3"),
                new DataColumn("Name4"),
                new DataColumn("Name5"),
                new DataColumn("Name6"),
                new DataColumn("Name7"),
                new DataColumn("Name8"),
                new DataColumn("Name9"),
                new DataColumn("Name10"),
                new DataColumn("Name11"),
                new DataColumn("Name12"),
                new DataColumn("Name13"),
                new DataColumn("Name14"),
                new DataColumn("Name15"),
                new DataColumn("Name16"),
                new DataColumn("Name17"),
                new DataColumn("Name18"),
                new DataColumn("Name19"),
                new DataColumn("Name20"),
                new DataColumn("Name21")
            });
            foreach (var e in GetList(date, date.AddDays(6)))
            {
                if (string.IsNullOrEmpty(e.F_PId)) continue;
                if (e.F_VisitDate == null) continue;
                DataRow[] rows = detail.Select("Id='" + e.F_BId + "'");
                DataRow row;
                bool isAdd = false;
                if (rows.Length > 0)
                {
                    row = rows[0];
                }
                else
                {
                    isAdd = true;
                    row = detail.NewRow();
                }
                DateTime visitDate = (DateTime)e.F_VisitDate;
                int ts = (int)visitDate.Subtract(date).TotalDays;
                int bc = (int)e.F_VisitNo;
                row[ts * 3 + 1 + bc] = e.F_Name;
                if (isAdd)
                {
                    row[0] = e.F_BId;
                    row[1] = e.F_GroupName + e.F_DialysisBedNo;
                    detail.Rows.Add(row);
                }
            }
            detail.DefaultView.Sort = "BId ASC";
            detail = detail.DefaultView.ToTable();
            data.Tables.Add(detail);
            //string exportFormat = "png";
            string exportFormat = "html";
            return FastReportHelper.GetReportString(reportFilePath, dataSetName, data, exportFormat);
        }
        /// <summary>
        /// 复制排班 逻辑
        /// </summary>
        /// <param name="source">源排班信息</param>
        /// <param name="target">目标已排班信息</param>
        /// <param name="interval">间隔天数</param>
        public async Task CreateItems(IEnumerable<DialysisScheduleEntity> source, List<DialysisScheduleEntity> target, int interval)
        {
            //按照床号 患者ID 过滤  患者停用  床停用等
            source = from d in source
                     join m in await _dialysisMachineApp.GetList() on d.F_BId equals m.F_Id
                     join p in await _patientApp.GetList() on d.F_PId equals p.F_Id
                     select d;

            foreach (var e in source)
            {
                var targetDate = e.F_VisitDate.ToDate().Date.AddDays(interval);
                var find = target.Find(t => t.F_BId == e.F_BId && t.F_VisitDate == targetDate && t.F_VisitNo == e.F_VisitNo);
                if (find == null) //床位是否空缺
                {
                    //判断患者当日有无排班信息
                    var check = target.Exists(t => t.F_PId == e.F_PId && t.F_VisitDate == targetDate);
                    if (check) continue; //已有排班，返回
                    DialysisScheduleEntity entity = new DialysisScheduleEntity
                    {
                        F_BId = e.F_BId,
                        F_DialysisBedNo = e.F_DialysisBedNo,
                        F_DialysisNo = e.F_DialysisNo,
                        F_DialysisType = e.F_DialysisType,
                        F_GroupName = e.F_GroupName,
                        F_Name = e.F_Name,
                        F_PId = e.F_PId,
                        F_VisitDate = targetDate,
                        F_VisitNo = e.F_VisitNo
                    };
                    await SubmitForm(entity, null);
                    //添加新增的记录至列表
                    target.Add(entity);
                }
                else //床位排过班
                {
                    if (string.IsNullOrEmpty(find.F_PId)) //排班后，做过清空处理
                    {
                        //判断患者当日有无排班信息
                        var check = target.Exists(t => t.F_PId == e.F_PId && t.F_VisitDate == targetDate);
                        if (check) continue; //已有排班，返回
                        find.F_BId = e.F_BId;
                        find.F_DialysisBedNo = e.F_DialysisBedNo;
                        find.F_DialysisNo = e.F_DialysisNo;
                        find.F_DialysisType = e.F_DialysisType;
                        find.F_GroupName = e.F_GroupName;
                        find.F_Name = e.F_Name;
                        find.F_PId = e.F_PId;
                        find.F_VisitDate = targetDate;
                        find.F_VisitNo = e.F_VisitNo;
                        await UpdateForm(find);
                    }
                    else
                    {
                        //已有自定义排班数据，不做处理
                    }
                }

            }
        }

        public void CreateItems(List<ImportScheduleModel> source, List<DialysisScheduleEntity> target)
        {
            foreach (var e in source)
            {
                var targetDate = e.F_VisitDate.ToDate().Date;
                var find = target.Find(t => t.F_BId == e.F_BId && t.F_VisitDate == targetDate && t.F_VisitNo == e.F_VisitNo);
                if (find == null) //床位是否空缺
                {
                    //判断患者当日有无排班信息
                    var check = target.Exists(t => t.F_PId == e.F_PId && t.F_VisitDate == targetDate);
                    if (check) continue; //已有排班，返回
                    DialysisScheduleEntity entity = new DialysisScheduleEntity
                    {
                        F_BId = e.F_BId,
                        F_DialysisBedNo = e.F_DialysisBedNo,
                        F_DialysisNo = e.F_DialysisNo,
                        F_DialysisType = e.F_DialysisType,
                        F_GroupName = e.F_GroupName,
                        F_Name = e.F_Name,
                        F_PId = e.F_PId,
                        F_VisitDate = targetDate,
                        F_VisitNo = e.F_VisitNo
                    };
                    SubmitForm(entity, null);
                    //添加新增的记录至列表
                    target.Add(entity);
                }
                else //床位排过班
                {
                    if (string.IsNullOrEmpty(find.F_PId)) //排班后，做过清空处理
                    {
                        //判断患者当日有无排班信息
                        var check = target.Exists(t => t.F_PId == e.F_PId && t.F_VisitDate == targetDate);
                        if (check) continue; //已有排班，返回
                        find.F_BId = e.F_BId;
                        find.F_DialysisBedNo = e.F_DialysisBedNo;
                        find.F_DialysisNo = e.F_DialysisNo;
                        find.F_DialysisType = e.F_DialysisType;
                        find.F_GroupName = e.F_GroupName;
                        find.F_Name = e.F_Name;
                        find.F_PId = e.F_PId;
                        find.F_VisitDate = targetDate;
                        find.F_VisitNo = e.F_VisitNo;
                        UpdateForm(find);
                    }
                    else
                    {
                        //已有自定义排班数据，不做处理
                    }
                }

            }
        }
    }
}
