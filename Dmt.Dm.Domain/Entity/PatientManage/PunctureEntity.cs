using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class PunctureEntity: IEntity<PunctureEntity>, ICreationAudited , IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [StringLength(50)]
        public string F_Nurse { get; set; }        
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime? F_OperateTime { get; set; }
        /// <summary>
        /// 动脉点
        /// </summary>
        [StringLength(20)]
        public string  F_Point1 { get; set; }
        /// <summary>
        /// 静脉点
        /// </summary>
        [StringLength(20)]
        public string F_Point2 { get; set; }
        /// <summary>
        /// 动脉点与吻口距离
        /// </summary>
        public float? F_Distance1 { get; set; }
        /// <summary>
        /// 两点距离
        /// </summary>
        public float? F_Distance2 { get; set; }
        public bool? F_IsSuccess { get; set; } = true;
        public bool? F_EnabledMark { get; set; } = true;
        [StringLength(250)]
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
