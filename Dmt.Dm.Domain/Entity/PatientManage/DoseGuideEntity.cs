using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class DoseGuideEntity : IEntity<DoseGuideEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
     
        [StringLength(50)]
        public string F_DrugId { get; set; }
        [StringLength(100)]
        public string F_DrugEnName { get; set; }
        [StringLength(100)]
        public string F_DrugName { get; set; }
        [StringLength(100)]
        public string F_Py { get; set; }
        [StringLength(20)]
        public string F_Method { get; set; }
        [StringLength(60)]
        public string F_Range1 { get; set; }
        [StringLength(60)]
        public string F_Range2 { get; set; }
        [StringLength(60)]
        public string F_Range3 { get; set; }
        [StringLength(60)]
        public string F_Range4 { get; set; }
        [StringLength(80)]
        public string F_Symptom1 { get; set; }
        [StringLength(80)]
        public string F_Symptom2 { get; set; }
        [StringLength(80)]
        public string F_Symptom3 { get; set; }
        [StringLength(80)]
        public string F_Symptom4 { get; set; }
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
