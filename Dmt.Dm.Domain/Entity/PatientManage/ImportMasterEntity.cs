using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    /// <summary>
    /// 入库主记录
    /// </summary>
    public class ImportMasterEntity : IEntity<ImportMasterEntity>, ICreationAudited  , IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 入库单号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string F_ImpNo { get; set; }
        /// <summary>
        /// 入库日期
        /// </summary>
        [Required]
        public DateTime? F_ImpDate { get; set; }
        /// <summary>
        /// 入库类型 采购入库 
        /// </summary>
        [StringLength(30)]
        public string F_ImpClass { get; set; }
        /// <summary>
        /// 入库类别 药品 耗材
        /// </summary>
        [StringLength(30)]
        public string F_ImpType { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        [Required]
        [StringLength(50)]
        public string F_Storage { get; set; }
        /// <summary>
        /// 是否记账
        /// </summary>
        public bool? F_IsAcct { get; set; }
        /// <summary>
        /// 记账人
        /// </summary>
        [StringLength(20)]
        public string F_AcctPerson { get; set; }
        /// <summary>
        /// 供货商
        /// </summary>
        [StringLength(60)]
        public string F_Supplier { get; set; }
        /// <summary>
        /// 供货商电话
        /// </summary>
        [StringLength(20)]
        public string F_SupplierPhone { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [StringLength(20)]
        public string F_Contacts { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public float? F_Costs { get; set; }
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
