using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class IdeaWeightEntity : IEntity<IdeaWeightEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(50)]
        public string F_Pid { get; set; }
        [StringLength(20)]
        public string F_Name { get; set; }
        [Required]
        public int F_DialysisNo { get; set; }
        public float? F_IdealWeight { get; set; }
        public float? F_OldIdealWeight { get; set; }
        public bool? F_EnabledMark { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        [StringLength(50)]
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        [StringLength(50)]
        public string F_DeleteUserId { get; set; }
        public bool? F_DeleteMark { get; set; }
    }
}
