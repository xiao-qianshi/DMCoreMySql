using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class TransferLogEntity : IEntity<TransferLogEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [Required]
        public DateTime F_TransferDate { get; set; } = DateTime.Today;
        /// <summary>
        /// 状态变更  转入,转出,退出,短期
        /// </summary>
        [StringLength(20)]
        public string F_Status { get; set; } 
        /// <summary>
        /// 转出地点
        /// </summary>
        [StringLength(80)]
        public string F_LocationOut { get; set; }
        /// <summary>
        /// 转出原因
        /// </summary>
        [StringLength(100)]
        public string F_TransferReason { get; set; }
        /// <summary>
        /// 退出说明 0:死亡,1:好转脱离透析,2:肾移植,3:转为腹膜透析,4:放弃治疗,5:其它
        /// </summary>
        [StringLength(20)]
        public string F_ExitType { get; set; }
        /// <summary>
        /// 退出原因
        /// </summary>
        [StringLength(150)]
        public string F_ExitReason { get; set; }
        [StringLength(150)]
        public string F_Memo { get; set; }
        
        public bool? F_EnabledMark { get; set; } = true;
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
