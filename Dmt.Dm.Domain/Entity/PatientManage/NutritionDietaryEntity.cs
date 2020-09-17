using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class NutritionDietaryEntity : IEntity<NutritionDietaryEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(30)]
        public string F_Type { get; set; }
        [StringLength(50)]
        public string F_Name { get; set; }
        [StringLength(50)]
        public string F_Py { get; set; }
        [StringLength(20)]
        public string F_Col1 { get; set; }
        [StringLength(20)]
        public string F_Col2 { get; set; }
        [StringLength(20)]
        public string F_Col3 { get; set; }
        [StringLength(20)]
        public string F_Col4 { get; set; }
        [StringLength(20)]
        public string F_Col5 { get; set; }
        [StringLength(20)]
        public string F_Col6 { get; set; }
        [StringLength(20)]
        public string F_Col7 { get; set; }
        [StringLength(20)]
        public string F_Col8 { get; set; }
        [StringLength(20)]
        public string F_Col9 { get; set; }
        [StringLength(20)]
        public string F_Col10 { get; set; }
        [StringLength(20)]
        public string F_Col11 { get; set; }
        [StringLength(200)]
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
