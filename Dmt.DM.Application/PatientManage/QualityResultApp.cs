using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IQualityResultApp:IScopedAppService
    {
        Task<List<QualityResultEntity>> GetList(Pagination pagination, string patientId, string resultType, DateTime? startDate, DateTime? endDate);
        Task<List<QualityResultEntity>> GetList(Pagination pagination, string itemId, string patientId, DateTime startDate, DateTime endDate);
        Task<List<QualityResultEntity>> GetList(string patientId, string itemId, DateTime startDate, DateTime endDate);
        Task<List<QualityResultEntity>> GetListByItemCode(DateTime startDate, DateTime endDate, string itemCode);
        Task<List<QualityResultEntity>> GetTrendList(DateTime startDate, DateTime endDate, string itemCode);
        IQueryable<QualityResultEntity> GetList();
        Task<QualityResultEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(QualityResultEntity entity);
        Task<int> SubmitForm(QualityResultEntity entity, string keyValue);
    }

    public class QualityResultApp : IQualityResultApp
    {
        private readonly IRepository<QualityResultEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IUsersService _usersService = null;

        public QualityResultApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<QualityResultEntity>();
            _usersService = usersService;
        }

        public Task<List<QualityResultEntity>> GetList(Pagination pagination, string patientId, string resultType, DateTime? startDate, DateTime? endDate)
        {
            var expression = ExtLinq.True<QualityResultEntity>();
            expression = expression.And(t => t.F_Pid == patientId);
            if (!string.IsNullOrEmpty(resultType))
                expression = expression.And(t => t.F_ItemType == resultType);
            if (startDate.HasValue)
            {
                var date = startDate.ToDate().Date;
                expression = expression.And(t => t.F_ReportTime >= date);
            }
            if (endDate.HasValue)
            {
                var date = endDate.ToDate().Date.AddDays(1);
                expression = expression.And(t => t.F_ReportTime <= date);
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<QualityResultEntity>> GetList(Pagination pagination, string itemId, string patientId, DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<QualityResultEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            expression = expression.And(t => t.F_Pid == patientId);
            expression = expression.And(t => t.F_ReportTime >= startDate);
            expression = expression.And(t => t.F_ReportTime <= endDate);

            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<QualityResultEntity>> GetList(string patientId, string itemId, DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<QualityResultEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            expression = expression.And(t => t.F_Pid == patientId);
            expression = expression.And(t => t.F_ReportTime >= startDate);
            expression = expression.And(t => t.F_ReportTime <= endDate);

            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        /// <summary>
        /// 根据时间段 ，项目ID ，查询最新的一条数据 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Task<List<QualityResultEntity>> GetListByItemCode(DateTime startDate, DateTime endDate, string itemCode)
        {
            var expression = ExtLinq.True<QualityResultEntity>();
            //过滤条件
            expression = expression.And(t => t.F_ReportTime >= startDate && t.F_ReportTime <= endDate);
            expression = expression.And(t => t.F_ItemId == itemCode);
            expression = expression.And(t => t.F_EnabledMark == true);
            expression = expression.And(t => t.F_DeleteMark != true);
            var query = _service.IQueryable(expression);
            ////排序
            //query = query.OrderBy(t => t.F_Pid).ThenByDescending(t => t.F_ReportTime);
            //分组 ，取第一条
            var groups = query.GroupBy(t => t.F_Pid);
            var result = new List<QualityResultEntity>();

            foreach (var item in groups)
            {
                //取时间最近的一条数据
                result.Add(item.OrderByDescending(t => t.F_ReportTime).FirstOrDefault());
            }
            return Task.FromResult(result);
        }

        public Task<List<QualityResultEntity>> GetTrendList(DateTime startDate, DateTime endDate, string itemCode)
        {
            var expression = ExtLinq.True<QualityResultEntity>();
            //过滤条件
            expression = expression.And(t => t.F_ReportTime >= startDate && t.F_ReportTime <= endDate);
            expression = expression.And(t => t.F_ItemId == itemCode);
            expression = expression.And(t => t.F_EnabledMark == true);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public IQueryable<QualityResultEntity> GetList()
        {
            var expression = ExtLinq.True<QualityResultEntity>();
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression);
        }

        public Task<QualityResultEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(QualityResultEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm(QualityResultEntity entity, string keyValue)
        {
            var userId = _usersService.GetCurrentUserId();
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.F_LastModifyUserId = userId;
                return _service.UpdateAsync(entity);
            }
            else
            {
                entity.Create();
                if (entity.F_EnabledMark == null)
                {
                    entity.F_EnabledMark = true;
                }
                entity.F_CreatorUserId = userId;
                return _service.InsertAsync(entity);
            }
        }
    }
}
