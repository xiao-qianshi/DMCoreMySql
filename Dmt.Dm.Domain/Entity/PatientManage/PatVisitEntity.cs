using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class PatVisitEntity : IEntity<PatVisitEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(50)]
        [Required]
        public string F_Pid { get; set; }
        [StringLength(20)]
        public string F_Name { get; set; }
        [StringLength(8)]
        public string F_Gender { get; set; }
        public int? F_DialysisNo { get; set; }
        [StringLength(20)]
        public string F_RecordNo { get; set; }
        public DateTime? F_BirthDay { get; set; }
        public DateTime? F_VisitDate { get; set; }
        public int? F_VisitNo { get; set; }
        [StringLength(20)]
        public string F_PatientSourse { get; set; }
        [StringLength(10)]
        public string F_BedNo { get; set; }
        [StringLength(20)]
        public string F_InpNo { get; set; }
        [StringLength(20)]
        public string F_GroupName { get; set; }  
        [StringLength(10)]
        public string F_DialysisBedNo { get; set; }
        public float? F_WeightSXTH { get; set; }
        public DateTime? F_FirstWeightTime { get; set; }
        public float? F_LastWeightValue { get; set; }
        public float? F_WeightTQ { get; set; }
        public float? F_WeightYT { get; set; }
        public float? F_WeightTH { get; set; }
        public float? F_WeightST { get; set; }
        [StringLength(30)]
        public string F_DialysisType { get; set; }
        public float? F_ExchangeAmount { get; set; }
        public float? F_ExchangeSpeed { get; set; }
        [StringLength(30)]
        public string F_Machine { get; set; }
        public DateTime? F_DisinfectTime { get; set; }
        [StringLength(10)]
        public string F_DisinfectPerson { get; set; }
        [StringLength(50)]
        public string F_DialyzerType1 { get; set; }
        [StringLength(50)]
        public string F_DialyzerType2 { get; set; }
        public float? F_EstimateHours { get; set; }
        [StringLength(30)]
        public string F_VascularAccess { get; set; }
        [StringLength(30)]
        public string F_AccessName { get; set; }
        public DateTime? F_DialysisStartTime { get; set; }
        public DateTime? F_DialysisEndTime { get; set; }
        [StringLength(10)]
        public string F_DialysisHours { get; set; }
        /// <summary>
        /// 穿刺者
        /// </summary>
        [StringLength(50)]
        public string F_PuncturePerson { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        [StringLength(50)]
        public string F_StartPerson { get; set; }
        /// <summary>
        /// 核对者
        /// </summary>
        [StringLength(50)]
        public string F_CheckPerson { get; set; }
        /// <summary>
        /// 下机者
        /// </summary>
        [StringLength(50)]
        public string F_EndPerson { get; set; }
        [StringLength(50)]
        public string F_HeparinType { get; set; }
        public float? F_HeparinAmount { get; set; }
        [StringLength(15)]
        public string F_HeparinUnit { get; set; }
        public float? F_HeparinAddAmount { get; set; }
        [StringLength(20)]
        public string F_HeparinAddSpeedUnit { get; set; }
        public bool? F_LowCa { get; set; }
        public float? F_Ca { get; set; }
        public float? F_K { get; set; }
        public float? F_Na { get; set; }
        public float? F_Hco3 { get; set; }
        public float? F_BloodSpeed { get; set; }
        public float? F_DialysateTemperature { get; set; }     
        [StringLength(50)]
        public string F_Nurse { get; set; }
        [StringLength(50)]
        public string F_Doctor { get; set; }
        public bool? F_IsAcct { get; set; }
        public bool? F_IsCritical { get; set; }
        public bool? F_IsPrint { get; set; }
        public float? F_MachineShowAmount { get; set; }
        public float? F_RealExchangeAmount { get; set; }
        [StringLength(50)]
        public string F_DJMH { get; set; }
        [StringLength(50)]
        public string F_DialyzerStatus { get; set; }  
        [StringLength(50)]
        public string F_FistulaStatus { get; set; }
        [StringLength(50)]
        public string F_DuctOpeningStatus { get; set; }
        [StringLength(200)]
        public string F_PatientStatus { get; set; }
        [StringLength(255)]
        public string F_Reason { get; set; }
        public float? F_SystolicPressure { get; set; }
        public float? F_DiastolicPressure { get; set; }
        public float? F_Pulse { get; set; }
        public float? F_Temperature { get; set; }
        [StringLength(50)]
        public string F_RecordDoctor { get; set; }
        [StringLength(10)]
        public string F_DilutionType { get; set; }
        public string F_Memo { get; set; }
        [StringLength(50)]
        public string F_PGwzsz { get; set; }
        [StringLength(50)]
        public string F_PGxftz1 { get; set; }
        [StringLength(50)]
        public string F_PGxftz2 { get; set; }
        [StringLength(50)]
        public string F_PGxftz3 { get; set; }
        [StringLength(50)]
        public string F_PGwzcx1 { get; set; }
        [StringLength(50)]
        public string F_PGwzcx2 { get; set; }
        [StringLength(50)]
        public string F_PGwzcx3 { get; set; }
        [StringLength(150)]
        public string F_PGother { get; set; }
        /// <summary>
        /// 是否已归档
        /// </summary>
        public bool? F_IsArchive { get; set; }
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
