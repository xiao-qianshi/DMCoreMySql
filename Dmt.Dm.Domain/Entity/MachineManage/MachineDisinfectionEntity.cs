using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.MachineManage
{
    public class MachineDisinfectionEntity : IEntity<MachineDisinfectionEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Mid { get; set; }
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [StringLength(20)]
        public string F_PName { get; set; }
        [StringLength(10)]
        public string F_PGender { get; set; }
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
        public DateTime? F_StartTime { get; set; }
        public DateTime? F_EndTime { get; set; }
        public DateTime? F_WipeStartTime { get; set; }
        public DateTime? F_WipeEndTime { get; set; }
        /// <summary>
        /// 热化学消毒
        /// </summary>
        public bool? F_Option1 { get; set; }
        [StringLength(80)]
        public string F_Option1Value { get; set; }
        /// <summary>
        /// 化学消毒
        /// </summary>
        public bool? F_Option2 { get; set; }
        [StringLength(80)]
        public string F_Option2Value { get; set; }
        /// <summary>
        /// 热消毒
        /// </summary>
        public bool? F_Option3 { get; set; }
        /// <summary>
        /// 是否除钙
        /// </summary>
        public bool? F_Option4 { get; set; }
        /// <summary>
        /// 是否除杂质
        /// </summary>
        public bool? F_Option5 { get; set; }
        /// <summary>
        /// 外表擦拭
        /// </summary>
        public bool? F_Option6 { get; set; }
        [StringLength(80)]
        public string F_Option6Value { get; set; }
        [StringLength(50)]
        public string F_OperatePerson { get; set; }
        [StringLength(50)]
        public string F_CheckPerson { get; set; }
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
