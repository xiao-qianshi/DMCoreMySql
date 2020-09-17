using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class CriticalValueEntity : IEntity<CriticalValueEntity>, ICreationAudited , IDeleteAudited, IModificationAudited
    {
    
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [StringLength(20)]
        public string F_Name { get; set; }
        [Required]
        public int F_DialysisNo { get; set; }
        [Required]
        [StringLength(20)]
        public string F_RecordNo { get; set; }
        [Required]
        [StringLength(20)]
        public string F_PatientNo { get; set; }
        [StringLength(8)]
        public string F_Gender { get; set; }
        public DateTime? F_BirthDay { get; set; }
        [StringLength(20)]
        public string F_IdNo { get; set; }

        public DateTime? F_ReportTime { get; set; }
        [Required]
        [StringLength(50)]
        public string F_ItemId { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [StringLength(20)]
        public string F_ItemType { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(20)]
        public string F_ItemCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)]
        public string F_ItemName { get; set; }

        /// <summary>
        /// 数据导入His 编码
        /// </summary>
        [StringLength(20)]
        public string F_HisItemCode { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(20)]
        public string F_Unit { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        [StringLength(20)]
        public string F_Result { get; set; }
        /// <summary>
        /// 参考下限
        /// </summary>
        public float? F_LowerValue { get; set; }
        /// <summary>
        /// 参考上线
        /// </summary>
        public float? F_UpperValue { get; set; }
        /// <summary>
        /// 危急值下限
        /// </summary>
        public float? F_LowerCriticalValue { get; set; }
        /// <summary>
        /// 危急值上限
        /// </summary>
        public float? F_UpperCriticalValue { get; set; }
        /// <summary>
        /// 结果类型：定量 true ；定性 false
        /// </summary>
        public bool? F_ResultType { get; set; }
        
        public bool? F_EnabledMark { get; set; } = true;

        [StringLength(200)]
        public string F_Memo { get; set; }
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
