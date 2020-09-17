using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class QualityItemEntity : IEntity<QualityItemEntity>, ICreationAudited , IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 类别
        /// </summary>
        [StringLength(20)]
        public string F_ItemType { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? F_OrderNo { get; set; }
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
        public bool? F_EnabledMark { get; set; }
        //public DateTime? F_CreatorTime { get; set; }
        [StringLength(500)]
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

        public QualityItemEntity()
        {
            F_EnabledMark = true;
        }
    }
}
