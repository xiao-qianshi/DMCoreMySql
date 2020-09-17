using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class DrugsEntity : IEntity<DrugsEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(15)]
        public string F_DrugCode { get; set; }
        [StringLength(40)]
        public string F_DrugName { get; set; }
        [StringLength(30)]
        public string F_DrugSpec { get; set; }
        [StringLength(20)]
        public string F_DrugUnit { get; set; }
        public float? F_DrugMiniAmount { get; set; }
        [StringLength(20)]
        public string F_DrugMiniSpec { get; set; }
        public int? F_DrugMiniPackage { get; set; }
        [StringLength(20)]
        public string F_DrugAdministration { get; set; }
        [StringLength(200)]
        public string F_DrugEfficacy { get; set; }
        [StringLength(60)]
        public string F_DrugSupplier { get; set; }
        public float? F_Charges { get; set; }
        [StringLength(15)]
        public string F_DrugSpell { get; set; }
        public bool? F_IsHeparin { get; set; }
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
