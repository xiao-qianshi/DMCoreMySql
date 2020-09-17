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
    public interface IOrdersApp : IScopedAppService
    {
        Task<List<OrdersEntity>> GetList(Pagination pagination, string keyword);

        /// <summary>
        /// 根据患者ID、日期查询已执行医嘱
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        Task<List<OrdersExecLogEntity>> GetPerformedOrderListJson(string pid, DateTime visitDate);

        /// <summary>
        /// 查询医嘱开单信息
        /// </summary>
        /// <param name="pid">患者主键</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">截至时间</param>
        /// <param name="showInit">是否显示新开医嘱</param>
        /// <returns></returns>
        Task<List<OrdersEntity>> GetList(string pid, DateTime startDate, DateTime endDate, bool showInit = false);

        Task<List<OrdersEntity>> GetList();
        Task<List<OrdersEntity>> GetChildrens(string parentId);
        Task<OrdersEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(OrdersEntity entity);
        void InsertForm(OrdersEntity entity);
        Task<int> SubmitForm<TDto>(OrdersEntity entity, TDto dto) where TDto:class;
        Task ExecOrder(string keyValue);
        Task<int> InsertBatch(List<OrdersEntity> list);
    }

    public class OrdersApp : IOrdersApp
    {
        private readonly IRepository<OrdersEntity> _service = null;
        private readonly IRepository<OrdersExecLogEntity> _execService = null;
        private IUnitOfWork _uow = null;
        private IUsersService _usersService = null;

        public OrdersApp(IUnitOfWork uow, IUsersService usersService)
        {
            _uow = uow;
            _service = _uow.GetRepository<OrdersEntity>();
            _execService = _uow.GetRepository<OrdersExecLogEntity>();
            _usersService = usersService;
        }

        public Task<List<OrdersEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<OrdersEntity>();
            string pid = string.Empty;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(keyword))
            {
                string[] arr = keyword.Split('^');
                pid = arr[0];
                condition = arr[1];
            }
            if (!string.IsNullOrEmpty(pid))
            {
                expression = expression.And(t => t.F_Pid == pid);
                expression = expression.And(t => t.F_OrderText.Contains(condition));
            }
            else
            {
                expression = expression.And(t => t.F_Id == "");
            }

            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.FindListAsync(expression, pagination);
        }


        /// <summary>
        /// 根据患者ID、日期查询已执行医嘱
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="visitDate"></param>
        /// <returns></returns>
        public Task<List<OrdersExecLogEntity>> GetPerformedOrderListJson(string pid, DateTime visitDate)
        {
            var expression = ExtLinq.True<OrdersExecLogEntity>();
            var endDate = visitDate.AddDays(1);
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_NurseOperatorTime >= visitDate && t.F_NurseOperatorTime <= endDate);
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _execService.IQueryable(expression).OrderBy(t => t.F_NurseOperatorTime).ToListAsync();
        }

        /// <summary>
        /// 查询医嘱开单信息
        /// </summary>
        /// <param name="pid">患者主键</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">截至时间</param>
        /// <param name="showInit">是否显示新开医嘱</param>
        /// <returns></returns>
        public Task<List<OrdersEntity>> GetList(string pid, DateTime startDate, DateTime endDate, bool showInit = false)
        {
            var expression = ExtLinq.True<OrdersEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_EnabledMark == true);
            expression = expression.And(t => t.F_DeleteMark != true);
            if (!showInit) expression = expression.And(t => t.F_OrderStatus >= 1);
            expression = expression.And(t =>
                (t.F_IsTemporary == true && t.F_OrderStartTime < endDate && (t.F_OrderStopTime == null || t.F_OrderStopTime > startDate)) //长期
                || (t.F_IsTemporary == false && t.F_OrderStartTime >= startDate && t.F_OrderStartTime <= endDate) //临时
                );
            return _service.IQueryable(expression).ToListAsync();
        }


        public Task<List<OrdersEntity>> GetList()
        {
            var expression = ExtLinq.True<OrdersEntity>();
            expression = expression.And(t => t.F_OrderStatus >= 1);
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<List<OrdersEntity>> GetChildrens(string parentId)
        {
            var expression = ExtLinq.True<OrdersEntity>();
            expression = expression.And(t => t.F_ParentId == parentId);
            expression = expression.And(t => t.F_EnabledMark == true);
            return _service.IQueryable(expression).ToListAsync();
        }


        public Task<OrdersEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(OrdersEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public void InsertForm(OrdersEntity entity)
        {
            _service.Insert(entity);
        }

        public Task<int> SubmitForm<TDto>(OrdersEntity entity, TDto dto) where TDto:class
        {
            var userId = _usersService.GetCurrentUserId();
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = userId;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = userId;
                return _service.InsertAsync(entity);
            }
        }


        public async Task ExecOrder(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var list = new List<OrdersExecLogEntity>();
                var user = await _usersService.GetCurrentUserAsync();
                //var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
                //claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
                //var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.Name));
                foreach (var item in keyValue.ToJArrayObject())
                {
                    string id = string.Empty;
                    DateTime date;
                    try
                    {
                        id = item.Value<string>("id");
                        if (!DateTime.TryParse(item.Value<string>("date"), out date))
                        {
                            date = DateTime.Now;
                        }
                    }
                    catch
                    {
                        return;
                    }
                    var order = await _service.FindEntityAsync(id);
                    if (order == null) return;
                    order.F_NurseOperatorTime = date;
                    order.F_Nurse = user.F_RealName;//
                    await UpdateForm(order);
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
                    list.Add(logEntity);
                }
                if (list.Count > 0) await _execService.InsertAsync(list);
            }

            return;
        }

        public Task<int> InsertBatch(List<OrdersEntity> list)
        {
            return _service.InsertAsync(list);
        }

    }
}
