using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.MachineManage
{
    public class WaterMDisinfectEntity : IEntity<WaterMDisinfectEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        public DateTime F_DisinfectDate { get; set; }
        /// <summary>
        /// 消毒液名称
        /// </summary>
        [StringLength(60)]
        public string F_DisinfectantName { get; set; }
        public float? F_DisinfectantVolume { get; set; }
        [StringLength(20)]
        public string F_DisinfectantUnit { get; set; }
        [StringLength(60)]
        public string F_DisinfectType { get; set; }
        /// <summary>
        /// 管路末端消毒液是否达到有效浓度
        /// </summary>
        public bool? F_Option1 { get; set; }
        /// <summary>
        /// 消毒液循环时间
        /// </summary>
        public DateTime? F_RecyclingStartTime { get; set; }
        public DateTime? F_RecyclingEndTime { get; set; }
        public float? F_RecyclingMinutes { get; set; }
        /// <summary>
        /// 浸泡时间
        /// </summary>
        public DateTime? F_SoakStartTime { get; set; }
        public DateTime? F_SoakEndTime { get; set; }
        public float? F_SoakMinutes { get; set; }
        /// <summary>
        /// 冲洗时间
        /// </summary>
        public DateTime? F_RinseStartTime { get; set; }
        public DateTime? F_RinseEndTime { get; set; }
        public float? F_RinseMinutes { get; set; }
        /// <summary>
        /// 消毒后监测残余浓度是否合格
        /// </summary>
        public bool? F_Option2 { get; set; }
        /// <summary>
        /// 透析机联机消毒后监测透析机排水残余浓度是否合格
        /// </summary>
        public bool? F_Option3 { get; set; }

        [StringLength(50)]
        public string F_OperatePerson { get; set; }
        [StringLength(50)]
        public string F_CheckPerson { get; set; }
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
