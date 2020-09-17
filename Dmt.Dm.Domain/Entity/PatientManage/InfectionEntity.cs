using System;
using System.ComponentModel.DataAnnotations;
using Dmt.DM.Domain;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class InfectionEntity : IEntity<InfectionEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public DateTime F_ReportDate { get; set; }
        /// <summary>
        /// 空气细菌培养（CFU/皿）
        /// </summary>
        public float? F_Item1 { get; set; }
        /// <summary>
        /// 物表细菌培养（cfu/cm²）
        /// </summary>
        public float? F_Item2 { get; set; }
        /// <summary>
        /// 手卫生细菌培养（cfu/cm²）
        /// </summary>
        public float? F_Item3 { get; set; }
        /// <summary>
        /// 透析用水细菌培养（cfu/ml)
        /// </summary>
        public float? F_Item4 { get; set; }
        /// <summary>
        /// 透析用水内毒素检测(EU/ml)
        /// </summary>
        public float? F_Item5 { get; set; }
        /// <summary>
        /// 透析液细菌培养（cfu/ml）
        /// </summary>
        public float? F_Item6 { get; set; }
        /// <summary>
        /// 透析液内毒素检测(EU/ml) 
        /// </summary>
        public float? F_Item7 { get; set; }
        [StringLength(50)]
        public string F_RecordPerson { get; set; }
        [StringLength(200)]
        public string F_Memo { get; set; }
        [StringLength(100)]
        public string F_ImangePath { get; set; }
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
