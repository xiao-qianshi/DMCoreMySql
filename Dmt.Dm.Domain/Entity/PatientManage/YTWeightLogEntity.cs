using System;
using System.ComponentModel.DataAnnotations;
using Dmt.DM.Domain;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    /// <summary>
    /// 预脱体重修改日志
    /// </summary>
    public class YTWeightLogEntity : IEntity<YTWeightLogEntity>, ICreationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Vid { get; set; }
        public float? F_Value { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
    }
}
