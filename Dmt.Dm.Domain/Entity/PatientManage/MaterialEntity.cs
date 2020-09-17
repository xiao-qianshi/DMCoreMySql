using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class MaterialEntity : IEntity<MaterialEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(15)]
        public string F_MaterialType { get; set; }
        [StringLength(15)]
        public string F_MaterialCode { get; set; }
        [StringLength(40)]
        public string F_MaterialName { get; set; }
        [StringLength(30)]
        public string F_MaterialSpec { get; set; }
        [StringLength(20)]
        public string F_MaterialUnit { get; set; }
        [StringLength(80)]
        public string F_MaterialSupplier { get; set; }
        public float? F_Charges { get; set; }
        [StringLength(15)]
        public string F_MaterialSpell { get; set; }
        public bool? F_IsDialyzer { get; set; }
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
