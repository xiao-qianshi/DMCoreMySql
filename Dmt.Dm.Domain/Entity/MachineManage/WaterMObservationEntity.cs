using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.MachineManage
{
    public class WaterMObservationEntity : IEntity<WaterMObservationEntity>, ICreationAudited
    {
        [Required]
        public DateTime F_RecordDate { get; set; }
        /// <summary>
        /// 一级系统压力	0.7-1.0Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value1 { get; set; }
        /// <summary>
        /// 一级纯水压力	0.25-0.6Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value2 { get; set; }
        /// <summary>
        /// 二级系统压力	0.7-1.0Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value3 { get; set; }
        /// <summary>
        /// 二级纯水压力	0.25-0.6Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value4 { get; set; }
        /// <summary>
        /// 管路末端压力	0.25-0.6Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value5 { get; set; }
        /// <summary>
        /// 原水电导率	/
        /// </summary>
        [StringLength(20)]
        public string F_Value6 { get; set; }
        /// <summary>
        /// 一级电导率 	/
        /// </summary>
        [StringLength(20)]
        public string F_Value7 { get; set; }
        /// <summary>
        /// 二级电导率 	≤10 μs/cm
        /// </summary>
        [StringLength(20)]
        public string F_Value8 { get; set; }
        /// <summary>
        /// 原水水温 ℃	/
        /// </summary>
        [StringLength(20)]
        public string F_Value9 { get; set; }
        /// <summary>
        /// 产水量   L/H	/
        /// </summary>
        [StringLength(20)]
        public string F_Value10 { get; set; }
        /// <summary>
        /// 设备运行状态	/
        /// </summary>
        public bool? F_Value11 { get; set; }
        /// <summary>
        /// 细菌内毒素检测	≤0.25EU/mL
        /// </summary>
        [StringLength(20)]
        public string F_Value12 { get; set; }
        /// <summary>
        /// 微生物检测结果 	≤100CFU/mL
        /// </summary>
        [StringLength(20)]
        public string F_Value13 { get; set; }
        /// <summary>
        /// 化学消毒 	无残留
        /// </summary>
        public bool? F_Value14 { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [StringLength(200)]
        public string F_Memo { get; set; }
        [StringLength(50)]
        public string F_OperatePerson { get; set; }
        [StringLength(30)]
        public string F_OperatePersonName { get; set; }
        [StringLength(50)]
        public string F_CheckPerson { get; set; }
        [StringLength(30)]
        public string F_CheckPersonName { get; set; }
        public bool? F_EnabledMark { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
    }
}
