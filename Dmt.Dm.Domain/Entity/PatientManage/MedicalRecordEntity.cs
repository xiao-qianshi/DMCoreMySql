using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class MedicalRecordEntity : IEntity<MedicalRecordEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [StringLength(80)]
        public string F_Title { get; set; }
        //[StringLength(4000)]
        public string F_Content { get; set; }
        public bool? F_AuditFlag { get; set; }
        public DateTime? F_ContentTime { get; set; }
        public DateTime? F_AuditTime { get; set; }  
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
