using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabDecodeEngineEntity : IEntity<LabDecodeEngineEntity>, ICreationAudited
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(30)]
        public string F_Name { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        [StringLength(30)]
        public string F_Alias { get; set; }
        /// <summary>
        /// 处理请求的类名
        /// </summary>
        [StringLength(120)]
        public string F_HandleProcess { get; set; }
        /// <summary>
        /// 可解码的仪器类型
        /// </summary>
        [StringLength(30)]
        public string F_Desc { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
