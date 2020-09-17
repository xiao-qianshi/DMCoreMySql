using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    /// <summary>
    /// 入库明细记录
    /// </summary>
    public class ImportDetailEntity : IEntity<ImportDetailEntity>, ICreationAudited  , IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 关联的主记录ID
        /// </summary>
        [Required]
        [StringLength(50)]
        public string F_ImpId { get; set; }
        [StringLength(50)]
        public string F_ItemId { get; set; }
        [Required]
        [StringLength(20)]
        public string F_Code { get; set; }
        [StringLength(50)]
        public string F_Name { get; set; }
        public string F_Spec { get; set; }
        public string F_Unit { get; set; }
        public float? F_Charges { get; set; }
        public int? F_Amount { get; set; }
        public float? F_TotalCharges { get; set; }
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
