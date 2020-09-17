using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class SettingEntity : IEntity<SettingEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(50)]
        [Required]
        public string F_Pid { get; set; }
        [StringLength(30)]
        public string F_DialysisType { get; set; }
        public bool F_IsDefault { get; set; }
        [StringLength(30)]
        public string F_DilutionType { get; set; }
        public float? F_ExchangeAmount { get; set; }
        public float? F_ExchangeSpeed { get; set; }
        //[StringLength(30)]
        //public string F_Machine { get; set; }
        //public DateTime? F_DisinfectTime { get; set; }
        //[StringLength(10)]
        //public string F_DisinfectPerson { get; set; }
        [StringLength(50)]
        public string F_DialyzerType1 { get; set; }
        [StringLength(50)]
        public string F_DialyzerType2 { get; set; }
        public float? F_EstimateHours { get; set; }
        [StringLength(30)]
        public string F_VascularAccess { get; set; }
        [StringLength(30)]
        public string F_AccessName { get; set; }
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
        public SettingEntity()
        {
            F_IsDefault = false;
            F_EnabledMark = true;
        }
    }
}
