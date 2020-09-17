using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class LabSheetEntity : IEntity<LabSheetEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public DateTime F_RequestDate { get; set; } = DateTime.Today;
        public long F_RequestId { get; set; }
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [StringLength(20)]
        public string F_Name { get; set; }
        public bool? F_BeInfected { get; set; } = false;
        public int? F_DialysisNo { get; set; }
        [StringLength(20)]
        public string F_RecordNo { get; set; }
        [StringLength(20)]
        public string F_PatientNo { get; set; }
        [StringLength(8)]
        public string F_Gender { get; set; }
        public DateTime? F_BirthDay { get; set; }
        [StringLength(20)]
        public string F_InsuranceNo { get; set; }
        [StringLength(20)]
        public string F_IdNo { get; set; }
        [StringLength(8)]
        public string F_MaritalStatus { get; set; }
        public float? F_IdealWeight { get; set; }
        public float? F_Height { get; set; }
        [StringLength(80)]
        public string F_PrimaryDisease { get; set; }
        [StringLength(80)]
        public string F_Diagnosis { get; set; }
        #region 样本信息
        [StringLength(20)]
        public string F_Barcode { get; set; }
        /// <summary>
        /// 样本类型 血清 
        /// </summary>
        [StringLength(20)]
        public string F_SampleType { get; set; }
        /// <summary>
        /// 采样容器 
        /// </summary>
        [StringLength(20)]
        public string F_Container { get; set; }
        #endregion
        /// <summary>
        /// 申请单状态 1 新申请；2 已提交；3 已采样 ；4 正在检验 ；5 已审核 ；6 已打印
        /// </summary>
        public int F_Status { get; set; } = 1;
        /// <summary>
        /// 开单
        /// </summary>
        [StringLength(50)]
        public string F_DoctorId { get; set; }
        [StringLength(20)]
        public string F_DoctorName { get; set; }
        public DateTime? F_OrderTime { get; set; }
        /// <summary>
        /// 采样
        /// </summary>
        [StringLength(50)]
        public string F_NurseId { get; set; }
        [StringLength(20)]
        public string F_NurseName { get; set; }
        public DateTime? F_SamplingTime { get; set; }
        /// <summary>
        /// 上机
        /// </summary>
        [StringLength(50)]
        public string F_TestUserId { get; set; }
        [StringLength(20)]
        public string F_TestUserName { get; set; }
        public DateTime? F_TestTime { get; set; }
        /// <summary>
        /// 审核
        /// </summary>
        [StringLength(50)]
        public string F_AuditUserId { get; set; }
        [StringLength(20)]
        public string F_AuditUserName { get; set; }
        public DateTime? F_AuditTime { get; set; }
        /// <summary>
        /// 核对
        /// </summary>
        [StringLength(50)]
        public string F_CheckUserId { get; set; }
        [StringLength(20)]
        public string F_CheckUserName { get; set; }
        public DateTime? F_CheckTime { get; set; }
        /// <summary>
        /// 发布报告
        /// </summary>
        [StringLength(50)]
        public string F_ReportUserId { get; set; }
        [StringLength(20)]
        public string F_ReportUserName { get; set; }
        public DateTime? F_ReportTime { get; set; }
        public DateTime? F_PrintTime { get; set; }
        /// <summary>
        /// 检验备注
        /// </summary>
        [StringLength(200)]
        public string F_Memo { get; set; }
        //public virtual ICollection<LabSheetItemsEntity> Items { get; set; }
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
