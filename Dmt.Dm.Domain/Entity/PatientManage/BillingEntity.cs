using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class BillingEntity : IEntity<BillingEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        [Required]
        public int F_DialylisNo { get; set; }
        [StringLength(20)]
        public string F_PName { get; set; }
        [StringLength(10)]
        public string F_PGender { get; set; }
        public DateTime F_BillingDateTime { get; set; }
        public string F_BillingPersonId { get; set; }
        public string F_BillingPerson { get; set; }
        [Required]
        [StringLength(50)]
        public string F_ItemId { get; set; }
        [StringLength(15)]
        public string F_ItemClass { get; set; }
        [StringLength(15)]
        public string F_ItemCode { get; set; }
        [StringLength(40)]
        public string F_ItemName { get; set; }
        [StringLength(30)]
        public string F_ItemSpec { get; set; }
        [StringLength(20)]
        public string F_ItemUnit { get; set; }
        public float? F_Amount { get; set; }
        [StringLength(60)]
        public string F_Supplier { get; set; }
        public float? F_Charges { get; set; }
        public float? F_Costs { get; set; }
        [StringLength(15)]
        public string F_ItemSpell { get; set; }
        public bool? F_IsAcct { get; set; }
        [StringLength(20)]
        public string F_DisabledPerson { get; set; }
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
