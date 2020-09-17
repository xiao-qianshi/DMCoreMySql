using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabInstrumentEntity : IEntity<LabInstrumentEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 仪器代码 
        /// </summary>
        [Required]
        [StringLength(20)]
        public string F_Code { get; set; }
        /// <summary>
        /// 仪器名称
        /// </summary>
        [StringLength(60)]
        public string F_Name { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        [StringLength(40)]
        public string F_SerialNo { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(30)]
        public string F_ShortName { get; set; }
        /// <summary>
        /// 检验类别 
        /// </summary>
        [StringLength(20)]
        public string F_TestType { get; set; }
        /// <summary>
        /// 联机类型 Comm、Socket等
        /// </summary>
        [StringLength(20)]
        public string F_CommunicateMode { get; set; }
        /// <summary>
        /// 联机配置 
        /// </summary>
        [StringLength(2000)]
        public string F_CommunicateConfig { get; set; }
        /// <summary>
        ///  是否双工传输
        /// </summary>
        public bool? F_IsDuplex { get; set; } = false;
        /// <summary>
        /// 解码引擎
        /// </summary>
        [StringLength(30)]
        public string F_DecodeEngine { get; set; }
        /// <summary>
        /// 是否外送
        /// </summary>
        public bool F_IsExternal { get; set; } = false;
        /// <summary>
        ///  排序
        /// </summary>
        public int? F_Sorter { get; set; } = 0;
        /// <summary>
        ///  工作站Ip
        /// </summary>
        [StringLength(30)]
        public string F_WorkStationIp { get; set; }
        /// <summary>
        ///  工作站Ip
        /// </summary>
        public int? F_WorkStationPort { get; set; }
        /// <summary>
        /// 设备厂商
        /// </summary>
        [StringLength(60)]
        public string F_Supplier { get; set; }
        /// <summary>
        /// 是否已注册
        /// </summary>
        public bool? F_IsRegistered { get; set; }
        /// <summary>
        /// 注册密钥
        /// </summary>
        [StringLength(200)]
        public string F_RegistKey { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        [StringLength(200)]
        public string F_PicPath { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
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
