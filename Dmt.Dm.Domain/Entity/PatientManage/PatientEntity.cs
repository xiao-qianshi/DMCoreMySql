using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class PatientEntity : IEntity<PatientEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(20)]
        public string F_Name { get; set; }
        [Required]
        public int F_DialysisNo { get; set; }
        //[Required]
        [StringLength(20)]
        public string F_RecordNo { get; set; }
        //[Required]
        [StringLength(20)]
        public string F_PatientNo { get; set; }
        [StringLength(8)]
        public string F_Gender { get; set; }
        public DateTime? F_BirthDay { get; set; }
        public float? F_Charges { get; set; }
        [StringLength(20)]
        public string F_InsuranceNo { get; set; }
        [StringLength(20)]
        public string F_IdNo { get; set; }
        [StringLength(8)]
        public string F_MaritalStatus { get; set; }
        public float? F_IdealWeight { get; set; }
        public float? F_Height { get; set; }
        public DateTime? F_DialysisStartTime { get; set; }
        [StringLength(80)]
        public string F_PrimaryDisease { get; set; }
        [StringLength(80)]
        public string F_Diagnosis { get; set; }
        [StringLength(80)]
        public string F_Address { get; set; }
        [StringLength(30)]
        public string F_InsuranceType { get; set; }
        [StringLength(20)]
        public string F_Contacts { get; set; }
        [StringLength(20)]
        public string F_PhoneNo { get; set; }
        [StringLength(20)]
        public string F_Contacts2 { get; set; }
        [StringLength(20)]
        public string F_PhoneNo2 { get; set; }
        [StringLength(20)]
        public string F_Trasfer { get; set; }
        [StringLength(6)]
        public string F_BloodAbo { get; set; }
        [StringLength(6)]
        public string F_BloodRh { get; set; }
        [StringLength(6)]
        public string F_Tp { get; set; }
        [StringLength(6)]
        public string F_Hiv { get; set; }
        [StringLength(6)]
        public string F_HBsAg { get; set; }
        [StringLength(6)]
        public string F_HBsAb { get; set; }
        [StringLength(6)]
        public string F_HBcAb { get; set; }
        [StringLength(6)]
        public string F_HBeAg { get; set; }
        [StringLength(6)]
        public string F_HBeAb { get; set; }
        [StringLength(6)]
        public string F_HCVAb { get; set; }
        [StringLength(300)]
        public string F_MedicalHistory { get; set; }
        [StringLength(20)]
        public string F_CardNo { get; set; }
        [StringLength(20)]
        public string F_PY { get; set; }
        [StringLength(100)]
        public string F_HeadIcon { get; set; }
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
