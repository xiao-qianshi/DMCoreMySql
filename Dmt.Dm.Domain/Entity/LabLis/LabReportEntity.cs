using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabReportEntity : IEntity<LabReportEntity>, ICreationAudited
    {
        /// <summary>
        /// 检验序号
        /// </summary>
        [Required]
        public long F_TestId { get; set; }

        [Required]
        [StringLength(50)]
        public string F_ItemId { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string F_Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary> 
        [StringLength(40)]
        public string F_Name { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(20)]
        public string F_ShortName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(40)]
        public string F_Unit { get; set; }
        /// <summary>
        /// 结果（定量）
        /// </summary>
        public float? F_Result { get; set; }
        /// <summary>
        /// 结果（定性）
        /// </summary>
        [StringLength(200)]
        public string F_ResultText { get; set; }
        /// <summary>
        /// 是否危急值
        /// </summary>
        public bool? F_IsCritical { get; set; } = false;
        /// <summary>
        /// 高低标识
        /// </summary>
        [StringLength(5)]
        public string F_Flag { get; set; }
        /// <summary>
        /// 参考下限
        /// </summary>
        public float? F_LowRef { get; set; }
        /// <summary>
        /// 参考上限
        /// </summary>
        public float? F_UpperRef { get; set; }
        /// <summary>
        /// 检验方法
        /// </summary>
        [StringLength(60)]
        public string F_MethodName { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int? F_Sorter { get; set; }
        public bool? F_EnabledMark { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
