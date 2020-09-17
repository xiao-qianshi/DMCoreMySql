using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.MachineManage
{
    public class WaterMBriefEntity : IEntity<WaterMBriefEntity>, ICreationAudited
    {
        [Required]
        public DateTime F_RecordDate { get; set; }
        /// <summary>
        /// 自来水压力（动态）	0.2-0.5Mpa
        /// </summary>
        [StringLength(20)] public string F_Value1 { get; set; }
        /// <summary>
        /// 粗过滤进水端压力	0.2-0.5Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value2 { get; set; }
        /// <summary>
        /// 粗过滤出水端压力	0.2-0.5Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value3 { get; set; }
        /// <summary>
        /// 粗过滤压差       （2-3）	＜0.1Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value4 { get; set; }
        /// <summary>
        /// 多介质过滤器压力（砂滤）	0.45-0.6Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value5 { get; set; }
        /// <summary>
        /// 活性炭过滤器压力（炭滤）	0.45-0.6Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value6 { get; set; }
        /// <summary>
        /// 树脂过滤器压力（软化）	0.45-0.6Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value7 { get; set; }
        /// <summary>
        /// 精过滤进水端压力	0.45-0.6Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value8 { get; set; }
        /// <summary>
        /// 精过滤出水端压力	0.45-0.6Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value9 { get; set; }
        /// <summary>
        /// 精过滤压差        （8-9）	＜0.1Mpa
        /// </summary>
        [StringLength(20)]
        public string F_Value10 { get; set; }
        /// <summary>
        /// 上机前总氯检测结果	＜0.1mg/l
        /// </summary>
        [StringLength(20)]
        public string F_Value11 { get; set; }
        /// <summary>
        /// 下机后总氯检测结果	＜0.1mg/l
        /// </summary>
        [StringLength(20)]
        public string F_Value12 { get; set; }
        /// <summary>
        /// 上机前硬度检测结果	＜17mg/l
        /// </summary>
        [StringLength(20)]
        public string F_Value13 { get; set; }
        /// <summary>
        /// 下机后硬度检测结果	＜17mg/l
        /// </summary>
        [StringLength(20)]
        public string F_Value14 { get; set; }

        /// <summary>
        /// 预处理有无漏水现象
        /// </summary>
        public bool? F_Value15 { get; set; }
        /// <summary>
        /// 确认控制头时间
        /// </summary>
        public bool? F_Value16 { get; set; }
        /// <summary>
        /// 滤芯建议更换周期
        /// </summary>
        public bool? F_Value17 { get; set; }
        /// <summary>
        /// 树脂还原剂添加情况
        /// </summary>
        public bool? F_Value18 { get; set; }
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
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
