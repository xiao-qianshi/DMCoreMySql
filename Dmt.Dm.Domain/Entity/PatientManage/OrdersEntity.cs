using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class OrdersEntity : IEntity<OrdersEntity>, ICreationAudited  , IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [StringLength(50)]
        public string F_ParentId { get; set; }
        /// <summary>
        /// 子医嘱序号
        /// </summary>
        public int? F_SubIndex { get; set; }
        [Required]
        public int F_DialysisNo { get; set; }
        [Required]
        [StringLength(20)]
        public string F_RecordNo { get; set; }
        public DateTime? F_VisitDate { get; set; }
        public int? F_VisitNo { get; set; }
        [StringLength(30)]
        public string F_OrderType { get; set; }
        public DateTime? F_OrderStartTime { get; set; }
        public DateTime? F_OrderStopTime { get; set; }
        [Required]
        [StringLength(50)]
        public string F_OrderCode { get; set; }
        [StringLength(50)]
        public string F_OrderText { get; set; }
        [StringLength(30)]
        public string F_OrderSpec { get; set; }
        [StringLength(20)]
        public string F_OrderUnitAmount { get; set; }
        [StringLength(30)]
        public string F_OrderUnitSpec { get; set; }
        public float? F_OrderAmount { get; set; }
        [StringLength(20)]
        public string F_OrderFrequency { get; set; }
        [StringLength(30)]
        public string F_OrderAdministration { get; set; }
        public bool? F_IsTemporary { get; set; }
        [StringLength(20)]  public string F_Doctor { get; set; }
        public DateTime? F_DoctorOrderTime { get; set; }
        public DateTime? F_DoctorAuditTime { get; set; }
        public int? F_OrderStatus { get; set; }
        [StringLength(20)]  public string F_Nurse { get; set; }
        public DateTime? F_NurseOperatorTime { get; set; }
        [StringLength(150)]
        public string F_Memo { get; set; }
        //public string F_CreatorUserId { get; set; }
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
