using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IOrdersExecLogApp : IScopedAppService
    {
        /// <summary>
        /// 根据患者ID ，日期段 查询执行医嘱情况
        /// </summary>
        /// <returns></returns>
        Task<List<OrdersExecLogEntity>> GetList(string pid, DateTime startDate, DateTime endDate);

        IQueryable<OrdersExecLogEntity> GetList(string pid, string filterText, DateTime startDate, DateTime endDate);
        Task<OrdersExecLogEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> InsertForm(OrdersExecLogEntity entity);
        Task<int> UpdateForm(OrdersExecLogEntity entity);
        Task<int> SubmitForm(OrdersExecLogEntity entity, string keyValue);
    }

    public class OrdersExecLogApp : IOrdersExecLogApp
    {
        private readonly IRepository<OrdersExecLogEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public OrdersExecLogApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<OrdersExecLogEntity>();
            _httpContext = httpContext;
        }

        /// <summary>
        /// 根据患者ID ，日期段 查询执行医嘱情况
        /// </summary>
        /// <returns></returns>
        public Task<List<OrdersExecLogEntity>> GetList(string pid, DateTime startDate, DateTime endDate)
        {
            //List<OrdersEntity> list = new List<OrdersEntity>();
            var expression = ExtLinq.True<OrdersExecLogEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_NurseOperatorTime >= startDate && t.F_NurseOperatorTime <= endDate);
            expression = expression.And(t => t.F_EnabledMark != false && t.F_DeleteMark != true);
            return _service.IQueryable(expression).OrderBy(t => t.F_NurseOperatorTime).ToListAsync();
        }

        public IQueryable<OrdersExecLogEntity> GetList(string pid, string filterText, DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<OrdersExecLogEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_NurseOperatorTime >= startDate && t.F_NurseOperatorTime <= endDate);
            if (!string.IsNullOrEmpty(filterText)) expression = expression.And(t => t.F_OrderText.Contains(filterText));
            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.IQueryable(expression);
        }



        public Task<OrdersExecLogEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(OrdersExecLogEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> InsertForm(OrdersExecLogEntity entity)
        {
            return _service.InsertAsync(entity);
        }

        public Task<int> SubmitForm(OrdersExecLogEntity entity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.F_LastModifyUserId = claim?.Value;
                return _service.UpdateAsync(entity);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = claim?.Value;
                return _service.InsertAsync(entity);
            }
        }

    }
}
