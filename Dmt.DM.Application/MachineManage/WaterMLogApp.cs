using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Domain.Entity.ReportPrint;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dmt.DM.Application.MachineManage
{
    public interface IWaterMLogApp : IScopedAppService
    {
        Task<List<WaterMLogEntity>> GetList(Pagination pagination, string keyword);
        Task<List<WaterMLogEntity>> GetList();
        Task<WaterMLogEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(WaterMLogEntity entity);
        Task<int> SubmitForm<TDto>(WaterMLogEntity entity, TDto dto) where TDto :class;

        /// <summary>
        /// 根据日期段筛选
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<WaterMLogEntity>> GetList(DateTime startDate, DateTime endDate);

        string GetReport(WaterMLog waterMLog);
    }

    public class WaterMLogApp : IWaterMLogApp
    {
        private readonly IRepository<WaterMLogEntity> _service;
        private readonly IUnitOfWork _uow;
        private readonly IUsersService _usersService;

        public WaterMLogApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<WaterMLogEntity>();
            _usersService = usersService;
        }

        public Task<List<WaterMLogEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<WaterMLogEntity>();
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
                        expression = expression.And(t => t.F_LogDate >= temp);
                    }
                }
                if (!string.IsNullOrEmpty(endDateStr))
                {
                    if (DateTime.TryParse(endDateStr, out DateTime temp))
                    {
                        temp = temp.Date.AddDays(1);
                        expression = expression.And(t => t.F_LogDate <= temp);
                    }
                }
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
        public Task<List<WaterMLogEntity>> GetList()
        {
            return _service.IQueryable().ToListAsync();
        }

        public Task<WaterMLogEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(WaterMLogEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(WaterMLogEntity entity, TDto dto) where TDto :class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
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
        public Task<List<WaterMLogEntity>> GetList(DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<WaterMLogEntity>();
            expression = expression.And(t => t.F_LogDate >= startDate)
                .And(t => t.F_LogDate <= endDate);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public string GetReport(WaterMLog waterMLog)
        {
            var FBusinessObject = new List<WaterMLog>
            {
                waterMLog
            };
            var reportFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "WaterMLog.frx");//FileHelper.MapPath("\\ReportFiles\\WaterMLog.frx");
            var dataSetName = "WaterMLog";
            var exportFormat = "html";
            return FastReportHelper.GetReportString(reportFilePath, dataSetName, FBusinessObject, exportFormat);
        }

    }
}
