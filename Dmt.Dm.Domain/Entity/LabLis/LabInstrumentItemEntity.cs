using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabInstrumentItemEntity : IEntity<LabInstrumentItemEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 仪器Id
        /// </summary>
        [Required]
        [StringLength(50)]
        public string F_MachineId { get; set; }
        /// <summary>
        /// 项目代码 
        /// </summary>
        [StringLength(20)]
        public string F_Code { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(60)]
        public string F_Name { get; set; }
        /// <summary>
        /// 中文文名
        /// </summary>
        [StringLength(60)]
        public string F_CnName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        [StringLength(60)]
        public string F_EnName { get; set; }
        /// <summary>
        /// 结果类型 ： true 定量  ；false 定性
        /// </summary>
        public bool? F_ResultType { get; set; } = true;
        /// <summary>
        ///  排序
        /// </summary>
        public int? F_Sorter { get; set; } = 0;
        /// <summary>
        ///  保留小数位数
        /// </summary>
        public int? F_KeepDecimal { get; set; } = 2;
        /// <summary>
        /// 项目单位
        /// </summary>
        [StringLength(30)]
        public string F_ValueUnit { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public float? F_DefaultValue { get; set; }
        /// <summary>
        /// 参考值下限
        /// </summary>
        public float? F_ReferenceMinValue { get; set; }
        /// <summary>
        /// 参考值上限
        /// </summary>
        public float? F_ReferenceMaxValue { get; set; }
        /// <summary>
        /// 危机值下限
        /// </summary>
        public float? F_CriticalMinValue { get; set; }
        /// <summary>
        /// 危机值上限
        /// </summary>
        public float? F_CriticalMaxValue { get; set; }
        /// <summary>
        /// 参考值文本
        /// </summary>
        [StringLength(100)]
        public string F_ReferenceTextValue { get; set; }
        /// <summary>
        /// 是否质控指标
        /// </summary>
        public bool? F_IsQualityItem { get; set; } = false;
        /// <summary>
        /// 质控项目ID
        /// </summary>
        [StringLength(50)]
        public string F_QualityItemId { get; set; }
        /// <summary>
        /// 质控项目名称
        /// </summary>
        [StringLength(30)]
        public string F_QualityItemName { get; set; }
        /// <summary>
        /// 转换系数
        /// </summary>
        public float? F_ConvertCoefficient { get; set; } = 1;
        /// <summary>
        /// 通道号描述 ，用逗号分隔 
        /// </summary>
        public string F_ChannelValue { get; set; }
        /// <summary>
        /// 是否隐藏该项
        /// </summary>
        public bool IsHiddenItem { get; set; } = false;
        
        [StringLength(20)]
        public string F_PY { get; set; }
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
