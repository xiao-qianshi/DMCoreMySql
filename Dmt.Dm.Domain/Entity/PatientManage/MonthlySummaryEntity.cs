using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    /// <summary>
    /// 透析月小结
    /// </summary>
    public class MonthlySummaryEntity : IEntity<MonthlySummaryEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [Required]
        [StringLength(20)]
        public string F_Month { get; set; }
        public int? F_HDTimes { get; set; }
        [StringLength(50)]
        public string F_HDDialyzerType { get; set; }
        public int? F_HFTimes { get; set; }
        [StringLength(50)]
        public string F_HFDialyzerType { get; set; }
        public int? F_HDFTimes { get; set; }
        [StringLength(50)]
        public string F_HDFDialyzerType { get; set; }
        public int? F_HDHPTimes { get; set; }
        [StringLength(50)]
        public string F_HDHPDialyzerType { get; set; }
        public int? F_TotalCount { get; set; }
        public float? F_IdeaWeight { get; set; }
        public float? F_UrineVolume { get; set; }
        [StringLength(30)]
        public string F_VascularAccess { get; set; }
        [StringLength(30)]
        public string F_AccessName { get; set; }
        [StringLength(50)]
        public string F_HeparinType { get; set; }
        public float? F_HeparinAmount { get; set; }
        [StringLength(20)]
        public string F_HeparinUnit { get; set; }
        public float? F_BloodSpeed { get; set; }
        public float? F_TXYCa { get; set; }
        public float? F_TXYK { get; set; }
        public float? F_TXYHco3 { get; set; }
        public float? F_EstimateHours { get; set; }
        public int? F_WeekTimes { get; set; }
        [StringLength(300)]
        public string F_Complication { get; set; }
        public float? F_Hb { get; set; }
        public float? F_RBC { get; set; }
        public float? F_HCT { get; set; }
        public float? F_WBC { get; set; }
        public float? F_PLT { get; set; }
        public float? F_Scr { get; set; }
        public float? F_Urea { get; set; }
        public float? F_UA { get; set; }
        public float? F_K { get; set; }
        public float? F_Na { get; set; }
        public float? F_Cl { get; set; }
        public float? F_HCO3 { get; set; }
        public float? F_Ca { get; set; }
        public float? F_P { get; set; }
        public float? F_Scr1 { get; set; }
        public float? F_Urea1 { get; set; }
        public float? F_UA1 { get; set; }
        public float? F_K1 { get; set; }
        public float? F_Na1 { get; set; }
        public float? F_Cl1 { get; set; }
        public float? F_HCO31 { get; set; }
        public float? F_Ca1 { get; set; }
        public float? F_P1 { get; set; }
        public float? F_URR { get; set; }
        public float? F_spKtV { get; set; }
        public float? F_FER { get; set; }
        public float? F_TS { get; set; }
        public float? F_iPTH { get; set; }
        public float? F_CRP { get; set; }
        public float? F_ALT { get; set; }
        public float? F_AST { get; set; }
        public float? F_TP { get; set; }
        public float? F_ALB { get; set; }
        public float? F_Glu { get; set; }
        public bool? F_HCV { get; set; }
        public bool? F_HIV { get; set; }
        public bool? F_RPR { get; set; }
        public bool? F_HBsAg { get; set; }
        public bool? F_HBsAb { get; set; }
        public bool? F_HBeAg { get; set; }
        public bool? F_HBeAb { get; set; }
        public bool? F_HBcAb { get; set; }
        [StringLength(300)]
        public string F_ExamXP { get; set; }
        [StringLength(300)]
        public string F_ExamXDT { get; set; }
        [StringLength(300)]
        public string F_ExamCC { get; set; }

        [StringLength(1000)]
        public string F_DrugSummary { get; set; }
        //[StringLength(150)]
        //public string F_DrugJYY { get; set; }
        //[StringLength(150)]
        //public string F_DrugTJ { get; set; }
        //[StringLength(150)]
        //public string F_DrugEPO { get; set; }
        //[StringLength(150)]
        //public string F_DrugLJHJ { get; set; }
        //[StringLength(150)]
        //public string F_DrugVitD { get; set; }
        //[StringLength(150)]
        //public string F_DrugOther { get; set; }
        [StringLength(500)]
        public string F_Content { get; set; }
        [StringLength(500)]
        public string F_Suggest { get; set; }
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
