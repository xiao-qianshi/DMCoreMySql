using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    /// <summary>
    /// 入库明细记录
    /// </summary>
    public class StorageLogEntity : IEntity<StorageLogEntity>
    {
        [Required]
        [StringLength(50)]
        public string F_ItemId { get; set; }
        [StringLength(30)]
        public string F_ImpClass { get; set; }
        [StringLength(50)]
        public string F_Storage { get; set; }
        [StringLength(20)]
        public string F_Code { get; set; }
        [StringLength(50)]
        public string F_Name { get; set; }
        [StringLength(80)]
        public string F_Spec { get; set; }
        [StringLength(50)]
        public string F_Unit { get; set; }
        public float? F_Charges { get; set; }
        /// <summary>
        /// 盘盈  盘亏
        /// </summary>
        [StringLength(20)]
        public string F_CheckType { get; set; }
        public int? F_Amount { get; set; }
        /// <summary>
        /// 实际数量
        /// </summary>
        public int? F_RealAmount { get; set; }
        public float? F_TotalCharges { get; set; }
        public DateTime? F_CheckTime { get; set; }
    }
}
