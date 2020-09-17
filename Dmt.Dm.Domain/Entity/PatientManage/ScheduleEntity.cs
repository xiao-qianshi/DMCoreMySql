using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class ScheduleEntity : IEntity<DialysisScheduleEntity>
    {
        public DateTime? F_StartDate { get; set; }
        public DateTime? F_EndDate { get; set; }
        public int? F_VisitNo { get; set; }
        [StringLength(20)]
        public string F_GroupName { get; set; }
    }
}
