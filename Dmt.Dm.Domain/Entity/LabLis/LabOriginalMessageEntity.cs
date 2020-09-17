using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabOriginalMessageEntity : IEntity<LabOriginalMessageEntity>, ICreationAudited
    {
        /// <summary>
        /// 检验仪器
        /// </summary>
        [Required]
        [StringLength(50)]
        public string F_InstrumentId { get; set; }
        /// <summary>
        /// 条码号
        /// </summary>
        [StringLength(20)]
        public string F_Barcode { get; set; }
        /// <summary>
        /// 样本编号日期
        /// </summary>
        public DateTime F_TestDate { get; set; }
        /// <summary>
        /// 样本编号序号
        /// </summary>
        public int F_TestNo { get; set; }
        /// <summary>
        /// 消息正文
        /// </summary>
        public string F_MessageContent { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
