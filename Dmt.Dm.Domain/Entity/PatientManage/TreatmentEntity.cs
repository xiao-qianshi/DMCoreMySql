using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class TreatmentEntity : IEntity<TreatmentEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(15)]
        public string F_TreatmentCode { get; set; }
        [StringLength(40)]
        public string F_TreatmentName { get; set; }
        [StringLength(30)]
        public string F_TreatmentSpec { get; set; }
        [StringLength(20)]
        public string F_TreatmentUnit { get; set; }
        public float? F_Charges { get; set; }
        [StringLength(15)]
        public string F_TreatmentSpell { get; set; }
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
