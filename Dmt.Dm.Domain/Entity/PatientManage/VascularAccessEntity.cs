using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class VascularAccessEntity: IEntity<VascularAccessEntity>, ICreationAudited , IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [StringLength(30)]
        public string F_Type { get; set; }
        [StringLength(30)]
        public string F_VascularAccess { get; set; }
        
        /// <summary>
        /// 手术日期
        /// </summary>
        public DateTime F_OperateTime { get; set; }
        /// <summary>
        /// 血流速
        /// </summary>
        public float? F_BloodSpeed_Idea { get; set; }
        /// <summary>
        /// 血流速
        /// </summary>
        public float? F_BloodSpeed { get; set; }
        /// <summary>
        /// 首次使用时间
        /// </summary>
        public DateTime? F_FirstUseTime { get; set; }
        /// <summary>
        /// 停用时间
        /// </summary>
        public DateTime? F_DiscardTime { get; set; }
        /// <summary>
        /// 部位
        /// </summary>
        [StringLength(30)]
        public string F_BodyPart { get; set; }

        /// <summary>
        /// 左右
        /// </summary>
        [StringLength(30)]
        public string F_BodyPosition { get; set; }


        public bool? F_EnabledMark { get; set; }
        /// <summary>
        /// 停用原因
        /// </summary>
        [StringLength(200)]
        public string F_DisabledRemark { get; set; }

        //public DateTime? F_CreatorTime { get; set; }
        [StringLength(500)]
        public string F_Memo { get; set; }
        [StringLength(500)]
        public string F_Complication { get; set; }
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
        [StringLength(100)]
        public string F_PicPath { get; set; }

        public VascularAccessEntity()
        {
            F_EnabledMark = true;
        }
    }
}
