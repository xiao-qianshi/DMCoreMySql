using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.MachineManage
{
    public class MachineProcessEntity : IEntity<MachineProcessEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Mid { get; set; }
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        public int? F_DialylisNo { get; set; }
        [StringLength(20)]
        public string F_PName { get; set; }
        [StringLength(10)]
        public string F_PGender { get; set; }
        [Required]
        [StringLength(50)]
        public string F_Vid { get; set; }
        public DateTime? F_VisitDate { get; set; }
        public int? F_VisitNo { get; set; }

        [StringLength(20)]
        public string F_GroupName { get; set; }

        public int? F_ShowOrder { get; set; }
  
        [StringLength(10)]
        public string F_DialylisBedNo { get; set; }

        [StringLength(20)]
        public string F_MachineNo { get; set; }

        [StringLength(20)]
        public string F_MachineName { get; set; }

        /// <summary>
        /// 机器自检
        /// </summary>
        public bool? F_Option1 { get; set; }
        /// <summary>
        /// 机器运行
        /// </summary>
        public bool? F_Option2 { get; set; }
        /// <summary>
        /// 外部擦拭
        /// </summary>
        public bool? F_Option3 { get; set; }
        /// <summary>
        /// 是否更换耗材
        /// </summary>
        public bool? F_Option4 { get; set; }
        /// <summary>
        /// 耗材名称
        /// </summary>
        public string F_Option5 { get; set; }
        /// <summary>
        /// 耗材数量
        /// </summary>
        public string F_Option6 { get; set; }

        public DateTime? F_OperateTime { get; set; }
        [StringLength(50)]
        public string F_OperatePerson { get; set; }
        [StringLength(200)]
        public string F_Memo { get; set; }

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
