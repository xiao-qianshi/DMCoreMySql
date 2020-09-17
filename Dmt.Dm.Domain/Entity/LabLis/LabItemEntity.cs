using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabItemEntity : IEntity<LabItemEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 代码 
        /// </summary>
        [StringLength(20)]
        public string F_Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(60)]
        public string F_Name { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        [StringLength(40)]
        public string F_EnName { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(30)]
        public string F_ShortName { get; set; }
        /// <summary>
        /// 样本类型 血清 
        /// </summary>
        [StringLength(20)]
        public string F_SampleType { get; set; }
        /// <summary>
        /// 采样容器
        /// </summary>
        [StringLength(20)]
        public string F_Container { get; set; }
        /// <summary>
        /// 合管编号
        /// </summary>
        public int F_CuvetteNo { get; set; }
        /// <summary>
        /// 检验类别 临检、生化等
        /// </summary>
        [StringLength(20)]
        public string F_Type { get; set; }
        /// <summary>
        /// 第三方检验代码
        /// </summary>
        [StringLength(30)]
        public string F_ThirdPartyCode { get; set; }
        /// <summary>
        /// 是否外送项目
        /// </summary>
        public bool F_IsExternal { get; set; } = true;
        /// <summary>
        ///  项目排序
        /// </summary>
        public int? F_Sorter { get; set; } = 0;
        /// <summary>
        ///  是否定期检查项目
        /// </summary>
        public bool? F_IsPeriodic { get; set; } = false;
        /// <summary>
        ///  时间间隔，用于提醒医生开单(以天为单位)
        /// </summary>
        public float? F_TimeInterval { get; set; }
        /// <summary>
        /// 检验备注
        /// </summary>
        [StringLength(500)]
        public string F_Memo { get; set; }

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
