using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class DialysisMachineEntity : IEntity<DialysisMachineEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
   
        [Required]
        [StringLength(20)]
        public string F_GroupName { get; set; }
        [Required]
        public int? F_ShowOrder { get; set; }
        [Required]
        [StringLength(10)]
        public string F_DialylisBedNo { get; set; }
        [Required]
        [StringLength(20)]
        public string F_MachineNo { get; set; }
        [Required]
        [StringLength(20)]
        public string F_MachineName { get; set; }
        [Required]
        [StringLength(20)]
        public string F_DefaultType { get; set; }     
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
