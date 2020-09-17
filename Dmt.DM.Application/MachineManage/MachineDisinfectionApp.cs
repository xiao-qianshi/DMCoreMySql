using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.MachineManage;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint.MachineDisinfection;
using Dmt.DM.UOW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.MachineManage
{
    public interface IMachineDisinfectionApp : IScopedAppService
    {
        Task<List<MachineDisinfectionEntity>> GetList(Pagination pagination, string keyword);

        /// <summary>
        /// 根据治疗单ID生成记录
        /// </summary>
        /// <param name="vid"></param>
        /// <param name="userId">API调用时需传值</param>
        /// <returns></returns>
        string CreateSingleData(string vid, string userId = null);

        IQueryable<MachineDisinfectionEntity> GetList();
        IQueryable<MachineDisinfectionEntity> GetListByDate(DateTime startDate, DateTime endDate);
        Task<MachineDisinfectionEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(MachineDisinfectionEntity entity);
        Task<int> SubmitForm<TDto>(MachineDisinfectionEntity entity, TDto dto) where TDto:class;

        /// <summary>
        /// 生成FastReport报告
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        string GetImageReport(MachineDisinfectionCategory category);

        string GetImageReport(List<MachineInfo> machineInfos);
        Task<int> SubmitFormBatch(List<MachineDisinfectionEntity> list);
    }

    public class MachineDisinfectionApp : IMachineDisinfectionApp
    {
        private readonly IRepository<MachineDisinfectionEntity> _service;
        private readonly IRepository<PatVisitEntity> _patVisitService;
        private readonly IRepository<DialysisMachineEntity> _dialysisMachineService;
        private readonly IUsersService _usersService;

        public MachineDisinfectionApp(IUnitOfWork uow, IUsersService usersService)
        {
            _service = uow.GetRepository<MachineDisinfectionEntity>();
            _patVisitService = uow.GetRepository<PatVisitEntity>();
            _dialysisMachineService = uow.GetRepository<DialysisMachineEntity>();
            _usersService = usersService;
        }

        public async Task<List<MachineDisinfectionEntity>> GetList(Pagination pagination, string keyword)
        {
            var json = keyword.ToJObject();
            var startDateStr = json.Value<string>("startDate");
            var endDateStr = json.Value<string>("endDate");
            var groupName = json.Value<string>("condition");
            if (!DateTime.TryParse(startDateStr, out var startDate)) startDate = DateTime.Today;
            if (!DateTime.TryParse(endDateStr, out var endDate)) endDate = DateTime.Today;

            var list = CreateData(startDate, endDate);
            if (list.Any()) await SubmitFormBatch(list);

            var expression = ExtLinq.True<MachineDisinfectionEntity>();
            if (!string.IsNullOrEmpty(groupName))
            {
                expression = expression.And(t => t.F_GroupName == groupName);
            }
            expression = expression.And(t => t.F_VisitDate >= startDate);
            expression = expression.And(t => t.F_VisitDate <= endDate);
            expression = expression.And(t => t.F_DeleteMark != false);
            return await _service.FindListAsync(expression, pagination);
        }
        /// <summary>
        /// 根据治疗单ID生成记录
        /// </summary>
        /// <param name="vid"></param>
        /// <param name="userId">API调用时需传值</param>
        /// <returns></returns>
        public string CreateSingleData(string vid, string userId = null)
        {
            var msg = string.Empty;
            var patVisit = _patVisitService.IQueryable()
                .Where(t => t.F_Id == vid)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_Pid,
                    t.F_Name,
                    t.F_Gender,
                    t.F_DialysisNo,
                    t.F_GroupName,
                    t.F_DialysisBedNo,
                    t.F_VisitDate,
                    t.F_VisitNo
                })
                .FirstOrDefault();
            if (patVisit == null) return "治疗单主键有误!";
            var machine = _dialysisMachineService.IQueryable()
                .Where(t => t.F_GroupName == patVisit.F_GroupName && t.F_DialylisBedNo == patVisit.F_DialysisBedNo)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_MachineName,
                    t.F_MachineNo,
                    t.F_ShowOrder
                })
                .FirstOrDefault();
            if (machine == null) return "床位分区或编号有变动，未生成消毒记录!";
            var expression = ExtLinq.True<MachineDisinfectionEntity>();
            expression = expression.And(t => t.F_Vid == vid);
            var count = _service.IQueryable(expression).Count();
            if (count == 0)
            {
                var entity = new MachineDisinfectionEntity
                {
                    F_Mid = machine.F_Id,
                    F_Pid = patVisit.F_Pid,
                    F_PName = patVisit.F_Name,
                    F_PGender = patVisit.F_Gender,
                    F_Vid = patVisit.F_Id,
                    F_VisitDate = patVisit.F_VisitDate,
                    F_VisitNo = patVisit.F_VisitNo,
                    F_GroupName = patVisit.F_GroupName,
                    F_DialylisBedNo = patVisit.F_DialysisBedNo,
                    F_ShowOrder = machine.F_ShowOrder,
                    F_MachineNo = machine.F_MachineNo,
                    F_MachineName = machine.F_MachineName,
                    F_EnabledMark = true
                };
                if (string.IsNullOrEmpty(userId))
                {
                    entity.Create();
                }
                else
                {
                    entity.F_Id = Common.GuId();
                    entity.F_CreatorTime = DateTime.Now;
                    entity.F_CreatorUserId = userId;
                }
                _service.Insert(entity);
            }
            return msg;
        }
        /// <summary>
        /// 生成记录 根据已完成的透析记录单
        /// </summary>
        /// <param name="dateRange"></param>
        private void CreateData(string dateRange)
        {
            var expression = ExtLinq.True<PatVisitEntity>();
            if (!string.IsNullOrEmpty(dateRange))
            {
                var json = dateRange.ToJObject();
                var startDateStr = json.Value<string>("startDate");
                var endDateStr = json.Value<string>("endDate");
                if (!string.IsNullOrEmpty(startDateStr) && !string.IsNullOrEmpty(endDateStr))
                {
                    if (DateTime.TryParse(startDateStr, out var startDate) && DateTime.TryParse(endDateStr, out var endDate))
                    {
                        expression = expression.And(t => t.F_VisitDate >= startDate);
                        expression = expression.And(t => t.F_VisitDate <= endDate);
                        expression = expression.And(t => t.F_DialysisEndTime != null);
                        expression = expression.And(t => t.F_EnabledMark != false);
                        var existsIds = GetListByDate(startDate, endDate).Select((t) => new { t.F_Vid }).ToList();
                        var findRows = _patVisitService.IQueryable(expression).ToList().Where(t => existsIds.All(g => g.F_Vid != t.F_Id));
                        var list = (from item in from r in findRows
                            join m in _dialysisMachineService.IQueryable() on new {r.F_GroupName, bedNo = r.F_DialysisBedNo} equals new {m.F_GroupName, bedNo = m.F_DialylisBedNo}
                            select new
                            {
                                Mid = m.F_Id,
                                Vid = r.F_Id,
                                r.F_VisitDate,
                                r.F_VisitNo,
                                Pid = r.F_Pid,
                                r.F_Name,
                                r.F_Gender,
                                r.F_GroupName,
                                r.F_DialysisBedNo,
                                m.F_ShowOrder,
                                m.F_MachineNo,
                                m.F_MachineName
                            }
                            select new MachineDisinfectionEntity
                            {
                                F_Mid = item.Mid,
                                F_Pid = item.Pid,
                                F_PName = item.F_Name,
                                F_PGender = item.F_Gender,
                                F_Vid = item.Vid,
                                F_VisitDate = item.F_VisitDate,
                                F_VisitNo = item.F_VisitNo,
                                F_ShowOrder = item.F_ShowOrder,
                                F_MachineName = item.F_MachineName,
                                F_MachineNo = item.F_MachineNo,
                                F_GroupName = item.F_GroupName,
                                F_DialylisBedNo = item.F_DialysisBedNo
                            }).ToList();

                        if (list.Count > 0)
                        {
                            SubmitFormBatch(list);
                        }
                    }

                }
            }
        }

        private List<MachineDisinfectionEntity> CreateData(DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<PatVisitEntity>();
            expression = expression.And(t => t.F_VisitDate >= startDate);
            expression = expression.And(t => t.F_VisitDate <= endDate);
            expression = expression.And(t => t.F_DialysisEndTime != null);
            expression = expression.And(t => t.F_EnabledMark != false);
            var existsIds = GetListByDate(startDate, endDate).Select((t) => new { t.F_Vid }).ToList();
            var findRows = _patVisitService.IQueryable(expression).ToList().Where(t => existsIds.All(g => g.F_Vid != t.F_Id));
            return (from item in from r in findRows
                                     join m in _dialysisMachineService.IQueryable() on new { r.F_GroupName, bedNo = r.F_DialysisBedNo } equals new { m.F_GroupName, bedNo = m.F_DialylisBedNo }
                                     select new
                                     {
                                         Mid = m.F_Id,
                                         Vid = r.F_Id,
                                         r.F_VisitDate,
                                         r.F_VisitNo,
                                         Pid = r.F_Pid,
                                         r.F_Name,
                                         r.F_Gender,
                                         r.F_GroupName,
                                         r.F_DialysisBedNo,
                                         m.F_ShowOrder,
                                         m.F_MachineNo,
                                         m.F_MachineName
                                     }
                        select new MachineDisinfectionEntity
                        {
                            F_Mid = item.Mid,
                            F_Pid = item.Pid,
                            F_PName = item.F_Name,
                            F_PGender = item.F_Gender,
                            F_Vid = item.Vid,
                            F_VisitDate = item.F_VisitDate,
                            F_VisitNo = item.F_VisitNo,
                            F_ShowOrder = item.F_ShowOrder,
                            F_MachineName = item.F_MachineName,
                            F_MachineNo = item.F_MachineNo,
                            F_GroupName = item.F_GroupName,
                            F_DialylisBedNo = item.F_DialysisBedNo
                        }).ToList();
        }

        public IQueryable<MachineDisinfectionEntity> GetList()
        {
            return _service.IQueryable();
        }

        public IQueryable<MachineDisinfectionEntity> GetListByDate(DateTime startDate, DateTime endDate)
        {
            var expression = ExtLinq.True<MachineDisinfectionEntity>();
            expression = expression.And(t => t.F_VisitDate >= startDate).And(t => t.F_VisitDate <= endDate);
            return _service.IQueryable(expression);
        }

        public Task<MachineDisinfectionEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public Task<int> UpdateForm(MachineDisinfectionEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(MachineDisinfectionEntity entity, TDto dto) where TDto:class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                return _service.UpdateAsync(entity,dto);
            }
            else
            {
                entity.Create();
                return _service.InsertAsync(entity);
            }
        }
        /// <summary>
        /// 生成FastReport报告
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public string GetImageReport(MachineDisinfectionCategory category)
        {
            var reportFilePath =
                Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "MachineDisinfection.frx");//FileHelper.MapPath("\\ReportFiles\\MachineDisinfection.frx");
            var dataSetName = "Categories";
            var exportFormat = "html";
            return FastReportHelper.GetReportString(reportFilePath, dataSetName, category.MachineInfos, exportFormat);
        }

        public string GetImageReport(List<MachineInfo> machineInfos)
        {
            var reportFilePath = Path.Combine(AppConsts.AppRootPath, "upload", "reportfiles", "MachineDisinfection.frx");
            var dataSetName = "MachineInfo";
            var exportFormat = "html";
            return FastReportHelper.GetReportString(reportFilePath, dataSetName, machineInfos, exportFormat);
        }

        public Task<int> SubmitFormBatch(List<MachineDisinfectionEntity> list)
        {
            foreach (var entity in list)
            {
                entity.Create();
                entity.F_EnabledMark = true;
            }
            return _service.InsertAsync(list);
        }
    }
}
