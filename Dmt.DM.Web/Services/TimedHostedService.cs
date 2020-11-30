using Dmt.DM.Web.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.EntityFrameWork;

namespace Dmt.DM.Web.Services
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IHubContext<HubTChat> _hubContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TimedHostedService(ILogger<TimedHostedService> logger, IHubContext<HubTChat> hubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _hubContext = hubContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<HsDbContext>();
                var visitRecords = db.Set<PatVisitEntity>()
                    .Where(t => t.F_VisitDate == DateTime.Today && t.F_EnabledMark != false && t.F_DeleteMark != true);
                var query = from v in visitRecords
                    join d in db.Set<DrugsEntity>() on v.F_HeparinType equals d.F_Id into temp
                    from dt in temp.DefaultIfEmpty()
                    join b in db.Set<DialysisMachineEntity>() on new
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
                var data = query.ToList().GroupJoin(db.Set<DialysisObservationEntity>().Where(o => o.F_DeleteMark != true),
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
                        }).OrderByDescending(r => r.OperatorTime)
                    }).OrderBy(n => n.Record.VisitNo).ThenBy(n => n.Record.GroupName).ThenBy(n => n.Record.ShowNo);

                _hubContext.Clients.Group("TV").SendCoreAsync("RefreshList", new object[] {data.ToJson()});
            }
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
