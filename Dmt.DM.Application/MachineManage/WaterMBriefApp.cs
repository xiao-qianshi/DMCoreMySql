using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmt.DM.Code;
using Dmt.DM.Code.Excel;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;

namespace Dmt.DM.Application.MachineManage
{
    public interface IWaterMBriefApp : IScopedAppService
    {
        Task<List<WaterMBriefEntity>> GetList(Pagination pagination, string keyword);
        IQueryable<WaterMBriefEntity> GetList();
        Task<WaterMBriefEntity> GetFormByDate(DateTime recordDate);
        Task<WaterMBriefEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(WaterMBriefEntity entity);
        Task<int> SubmitForm<TDto>(WaterMBriefEntity entity, TDto dto) where TDto : class;

        /// <summary>
        /// 根据日期段筛选
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<WaterMBriefEntity>> GetList(DateTime startDate, DateTime endDate);

        /// <summary>
        /// 检查文件是否存在，没有则新建一份
        /// </summary>
        /// <returns></returns>
        string CopyModelFile();

        void FillData(DateTime startDate, DateTime endDate, string filepath, string filename);
        DataTable GetMonthDate(DateTime startDate, DateTime endDate);
    }

    public class WaterMBriefApp : IWaterMBriefApp
    {
        private readonly IRepository<WaterMBriefEntity> _service;
        private readonly IWaterMObservationApp _waterMObservationApp;
        private readonly IUnitOfWork _uow;
        private readonly IUsersService _usersService;


        public WaterMBriefApp(IUnitOfWork uow, IUsersService usersService, IWaterMObservationApp waterMObservationApp)
        {
            _uow = uow;
            _service = _uow.GetRepository<WaterMBriefEntity>();
            _usersService = usersService;
            _waterMObservationApp = waterMObservationApp;
        }

        public Task<List<WaterMBriefEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<WaterMBriefEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                var json = keyword.ToJObject();
                var startDateStr = json.Value<string>("startDate");
                var endDateStr = json.Value<string>("endDate");
                if (!string.IsNullOrEmpty(startDateStr))
                {
                    if (DateTime.TryParse(startDateStr, out DateTime temp))
                    {
                        temp = temp.Date;
                        expression = expression.And(t => t.F_RecordDate >= temp);
                    }
                }
                if (!string.IsNullOrEmpty(endDateStr))
                {
                    if (DateTime.TryParse(endDateStr, out DateTime temp))
                    {
                        temp = temp.Date.AddDays(1);
                        expression = expression.And(t => t.F_RecordDate <= temp);
                    }
                }
            }
            //expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public IQueryable<WaterMBriefEntity> GetList()
        {
            return _service.IQueryable();
        }

        public Task<WaterMBriefEntity> GetFormByDate(DateTime recordDate)
        {
            return _service.FindEntityAsync(t => t.F_RecordDate == recordDate);
        }

        public Task<WaterMBriefEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(WaterMBriefEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(WaterMBriefEntity entity, TDto dto) where TDto : class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                //entity.F_Id = keyValue;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                return _service.InsertAsync(entity);
            }
        }
        /// <summary>
        /// 根据日期段筛选
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Task<List<WaterMBriefEntity>> GetList(DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<WaterMBriefEntity>();
            expression = expression.And(t => t.F_RecordDate >= startDate)
                .And(t => t.F_RecordDate <= endDate);
            //expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        /// <summary>
        /// 检查文件是否存在，没有则新建一份
        /// </summary>
        /// <returns></returns>
        public string CopyModelFile()
        {
            string targetFileName =
                Path.Combine(AppConsts.AppRootPath, "upload", "datareport", Common.CreateNo() + ".xls"); //"\\DataReport\\" + Common.CreateNo() + ".xls";
            string modelFilePath = Path.Combine(AppConsts.AppRootPath,"upload", "datareport", "血液透析用制水设备运行参数记录表.xls");
            if (FileHelper.IsExistFile(modelFilePath))
            {
                FileHelper.CopyFile(modelFilePath, targetFileName, true);
                return targetFileName;
            }
            else
            {
                return "";
            }
        }

        public void FillData(DateTime startDate, DateTime endDate, string filepath, string filename)
        {
            var table1 = GetMonthDate(startDate, endDate);
            var excel = new NPOIExcel();
            excel.ToExcel(table1, "1-1预处理部分", filepath, 4, 3);
            var table2 = _waterMObservationApp.GetMonthDate(startDate, endDate);
            excel.ToExcel(table2, "1-2水机", filepath, 5, 3);
        }

        public DataTable GetMonthDate(DateTime startDate, DateTime endDate)
        {
            var list = GetList()
                            .Where(t => t.F_RecordDate >= startDate && t.F_RecordDate < endDate && t.F_EnabledMark == true && t.F_CheckPerson != null)
                            .OrderBy(t => t.F_RecordDate)
                            .Select(t => new
                            {
                                t.F_RecordDate,
                                t.F_Value1,
                                t.F_Value2,
                                t.F_Value3,
                                t.F_Value4,
                                t.F_Value5,
                                t.F_Value6,
                                t.F_Value7,
                                t.F_Value8,
                                t.F_Value9,
                                t.F_Value10,
                                t.F_Value11,
                                t.F_Value12,
                                t.F_Value13,
                                t.F_Value14,
                                t.F_Value15,
                                t.F_Value16,
                                t.F_Value17,
                                t.F_Value18,
                                t.F_OperatePersonName,
                                t.F_CheckPersonName
                            })
                        .ToList();
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("col1"),
                new DataColumn("col2"),
                new DataColumn("col3"),
                new DataColumn("col4"),
                new DataColumn("col5"),
                new DataColumn("col6"),
                new DataColumn("col7"),
                new DataColumn("col8"),
                new DataColumn("col9"),
                new DataColumn("col10"),
                new DataColumn("col11"),
                new DataColumn("col12"),
                new DataColumn("col13"),
                new DataColumn("col14"),
                new DataColumn("col15"),
                new DataColumn("col16"),
                new DataColumn("col17"),
                new DataColumn("col18"),
                new DataColumn("col19"),
                new DataColumn("col120"),
                new DataColumn("col21"),
                new DataColumn("col22"),
                new DataColumn("col23"),
                new DataColumn("col24"),
                new DataColumn("col25"),
                new DataColumn("col26"),
                new DataColumn("col27"),
                new DataColumn("col28"),
                new DataColumn("col29"),
                new DataColumn("col30"),
                new DataColumn("col31")
            });
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());
            table.Rows.Add(table.NewRow());

            foreach (var item in list)
            {
                var dateOfMonth = item.F_RecordDate.Day;
                table.Rows[0][dateOfMonth - 1] = item.F_Value1;
                table.Rows[1][dateOfMonth - 1] = item.F_Value2;
                table.Rows[2][dateOfMonth - 1] = item.F_Value3;
                table.Rows[3][dateOfMonth - 1] = item.F_Value4;
                table.Rows[4][dateOfMonth - 1] = item.F_Value5;
                table.Rows[5][dateOfMonth - 1] = item.F_Value6;
                table.Rows[6][dateOfMonth - 1] = item.F_Value7;
                table.Rows[7][dateOfMonth - 1] = item.F_Value8;
                table.Rows[8][dateOfMonth - 1] = item.F_Value9;
                table.Rows[9][dateOfMonth - 1] = item.F_Value10;
                table.Rows[10][dateOfMonth - 1] = item.F_Value11;
                table.Rows[11][dateOfMonth - 1] = item.F_Value12;
                table.Rows[12][dateOfMonth - 1] = item.F_Value13;
                table.Rows[13][dateOfMonth - 1] = item.F_Value14;
                table.Rows[14][dateOfMonth - 1] = item.F_Value15 == true ? "√" : "×";
                table.Rows[15][dateOfMonth - 1] = item.F_Value16 == true ? "√" : "×";
                table.Rows[16][dateOfMonth - 1] = item.F_Value17 == true ? "√" : "/";
                table.Rows[17][dateOfMonth - 1] = item.F_Value18 == true ? "√" : "/";
                table.Rows[18][dateOfMonth - 1] = item.F_OperatePersonName;
                table.Rows[19][dateOfMonth - 1] = item.F_CheckPersonName;
            }

            return table;
        }

    }
}
