using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class QualityItemPartitionEntity : IEntity<QualityItemPartitionEntity>, ICreationAudited
    {
        [StringLength(50)]
        public string F_ParentId { get; set; }

        public int F_OrderNo { get; set; }

        public bool F_LowerCheck { get; set; }

        public float? F_LowerValue { get; set; }

        public bool F_UpperCheck { get; set; }

        public float? F_UpperValue { get; set; }

        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }

    }
}
