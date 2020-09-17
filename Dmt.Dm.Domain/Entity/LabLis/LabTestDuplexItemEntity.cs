using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabTestDuplexItemEntity : IEntity<LabTestDuplexItemEntity>, ICreationAudited
    {
        /// <summary>
        /// 检验序号 关联检验主记录
        /// </summary>
        [Required]
        public long F_TestId { get; set; }
        /// <summary>
        /// 检验仪器通道号代码
        /// </summary>
        public string F_ItemCode { get; set; }
        /// <summary>
        /// 仪器检验模式 
        /// </summary>
        public string F_TestMode { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
    }
}
