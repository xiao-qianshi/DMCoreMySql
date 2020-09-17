using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabTestEntity : IEntity<LabItemEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 检验仪器
        /// </summary>
        [Required]
        [StringLength(50)]
        public string F_InstrumentId { get; set; }
        /// <summary>
        /// 样本编号日期
        /// </summary>
        [Required]
        public DateTime F_TestDate { get; set; }
        /// <summary>
        /// 样本编号序号
        /// </summary>
        [Required]
        public int F_TestNo { get; set; }
        /// <summary>
        /// 检验序号 自动生成 关联检验报告记录
        /// </summary>
        [Required]
        public long F_TestId { get; set; }
        /// <summary>
        /// 条码号
        /// </summary>
        [StringLength(20)]
        public string F_Barcode { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        public long F_RequestId { get; set; }
        /// <summary>
        /// 患者主键
        /// </summary>
        [StringLength(50)]
        public string F_PatientId { get; set; }
        /// <summary>
        /// 样本类型
        /// </summary>
        [StringLength(20)]
        public string F_SampleType { get; set; }
        /// <summary>
        /// 检验状态 0 已编号 1 上机  2 检验完成 3 已审核
        /// </summary>
        public int F_Status { get; set; }
        /// <summary>
        /// 编号时间
        /// </summary>
        public DateTime? F_SignInTime { get; set; }
        /// <summary>
        /// 编号人员工号
        /// </summary>
        [StringLength(50)]
        public string F_SignInPerson { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? F_TestTime { get; set; }
        /// <summary>
        /// 检验人员工号
        /// </summary>
        [StringLength(50)]
        public string F_TestPerson { get; set; }
        /// <summary>
        /// 获取检验结果时间
        /// </summary>
        public DateTime? F_RecieveResultTime { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? F_AuditTime { get; set; }   
        /// <summary>
        /// 审核人员工号
        /// </summary>
        [StringLength(50)]
        public string F_AuditPerson { get; set; }

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
