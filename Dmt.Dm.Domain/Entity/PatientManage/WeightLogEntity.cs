using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class WeightLogEntity : IEntity<WeightLogEntity>
    {
        [Required]
        [StringLength(50)]
        public string F_Vid { get; set; }
        public float? F_Value { get; set; }
        public DateTime F_CreatorTime { get; set; } = DateTime.Now;
    }
}
