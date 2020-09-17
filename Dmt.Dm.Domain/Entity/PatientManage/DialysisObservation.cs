using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class DialysisObservationEntity : IEntity<DialysisObservationEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
       
        [StringLength(50)]
        public string F_Pid { get; set; }
        public DateTime? F_VisitDate { get; set; }
        public int? F_VisitNo { get; set; }
        public float? F_SSY { get; set; }
        public float? F_SZY { get; set; }
        public float? F_HR { get; set; }
        public float? F_A { get; set; }
        public float? F_BF { get; set; }
        public float? F_UFR { get; set; }
        public float? F_V { get; set; }
        public float? F_C { get; set; }
        public float? F_T { get; set; }
        public float? F_UFV { get; set; }
        public float? F_TMP { get; set; }
        public float? F_GSL { get; set; }
        [StringLength(200)]
        public string F_MEMO { get; set; }
        [StringLength(50)]
        public string F_Nurse { get; set; }
        [StringLength(30)]
        public string F_NurseName { get; set; }
        public DateTime? F_NurseOperatorTime { get; set; }
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
