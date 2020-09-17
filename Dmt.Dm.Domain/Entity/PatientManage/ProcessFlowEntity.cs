using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class ProcessFlowEntity : IEntity<ProcessFlowEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        public int? F_DialylisNo { get; set; }
        [StringLength(20)]
        public string F_PName { get; set; }
        [StringLength(10)]
        public string F_PGender { get; set; }
        [Required]
        [StringLength(50)]
        public string F_Vid { get; set; }
        public DateTime? F_VisitDate { get; set; }
        public int? F_VisitNo { get; set; }
        public float? F_TotalHours { get; set; }
        public float? F_PreUrea { get; set; }
        public float? F_PostUrea { get; set; }
        public float? F_PreWeight { get; set; }
        public float? F_PostWeight { get; set; }
        public float? F_Result { get; set; }
        public bool? F_Step_1 { get; set; }
        public bool? F_Step_2 { get; set; }
        public bool? F_Step_3 { get; set; }
        public bool? F_Step_4 { get; set; }
        public bool? F_Step_5 { get; set; }
        public bool? F_Step_1_Reason1 { get; set; }
        public bool? F_Step_1_Reason2 { get; set; }
        public bool? F_Step_1_Reason3 { get; set; }
        [StringLength(500)]
        public string F_Step_1_Measures { get; set; }
        public bool? F_Step_2_Reason1 { get; set; }
        public bool? F_Step_2_Reason2 { get; set; }
        public bool? F_Step_2_Reason3 { get; set; }
        public bool? F_Step_2_Reason4 { get; set; }
        [StringLength(500)]
        public string F_Step_2_Measures { get; set; }
        [StringLength(500)]
        public string F_Step_3_Measures { get; set; }
        [StringLength(500)]
        public string F_Step_4_Measures { get; set; }
        [StringLength(500)]
        public string F_Step_5_Measures { get; set; }

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
