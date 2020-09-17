using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.MachineManage
{
    public class WaterMLogEntity : IEntity<WaterMLogEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        public DateTime F_LogDate { get; set; }
        /// <summary>
        /// 进水压力
        /// </summary>
        public float? F_Value1 { get; set; }
        /// <summary>
        /// 2级膜产水压
        /// </summary>
        public float? F_Value2 { get; set; }
        /// <summary>
        /// 硬度
        /// </summary>
        public float? F_Value3 { get; set; }
        /// <summary>
        /// 残余氯
        /// </summary>
        public float? F_Value4 { get; set; }
        /// <summary>
        /// 水温
        /// </summary>
        public float? F_Value5 { get; set; }
        /// <summary>
        /// RO水电导
        /// </summary>
        public float? F_Value6 { get; set; }
        /// <summary>
        /// RO水产水压
        /// </summary>
        public float? F_Value7 { get; set; }
        /// <summary>
        /// RO回水压力
        /// </summary>
        public float? F_Value8 { get; set; }
        /// <summary>
        /// PH
        /// </summary>
        public float? F_Value9 { get; set; }
        /// <summary>
        /// 石英砂罐反冲
        /// </summary>
        public bool? F_Option1 { get; set; }
        /// <summary>
        /// 活性炭罐反冲
        /// </summary>
        public bool? F_Option2 { get; set; }
        /// <summary>
        /// 活性炭罐反冲
        /// </summary>
        public bool? F_Option3 { get; set; }
        /// <summary>
        /// 树脂罐反冲
        /// </summary>
        public bool? F_Option4 { get; set; }
        /// <summary>
        /// 系统消毒
        /// </summary>
        public bool? F_Option5 { get; set; }
        /// <summary>
        /// 系统状况
        /// </summary>
        public bool? F_Option6 { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [StringLength(200)]
        public string F_Memo { get; set; }
        [StringLength(50)]
        public string F_OperatePerson { get; set; }
        [StringLength(50)]
        public string F_CheckPerson { get; set; }
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
