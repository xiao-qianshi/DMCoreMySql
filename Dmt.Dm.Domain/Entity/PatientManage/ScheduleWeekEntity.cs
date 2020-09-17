using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class ScheduleWeekEntity : IEntity<ScheduleWeekEntity>
    {
        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime? F_CurrentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string F_BId { get; set; }
        [StringLength(20)]
        public string F_GroupName { get; set; }
        [StringLength(10)]
        public string F_DialysisBedNo { get; set; }

        [StringLength(10)]
        public string F_Monday1 { get; set; }
        [StringLength(10)]
        public string F_Monday2 { get; set; }
        [StringLength(10)]
        public string F_Monday3 { get; set; }
        [StringLength(10)]
        public string F_Tuesday1 { get; set; }
        [StringLength(10)]
        public string F_Tuesday2 { get; set; }
        [StringLength(10)]
        public string F_Tuesday3 { get; set; }

        public string F_Wednesday1 { get; set; }
        [StringLength(10)]
        public string F_Wednesday2 { get; set; }
        [StringLength(10)]
        public string F_Wednesday3 { get; set; }

        public string F_Thursday1 { get; set; }
        [StringLength(10)]
        public string F_Thursday2 { get; set; }
        [StringLength(10)]
        public string F_Thursday3 { get; set; }

        public string F_Friday1 { get; set; }
        [StringLength(10)]
        public string F_Friday2 { get; set; }
        [StringLength(10)]
        public string F_Friday3 { get; set; }

        public string F_Saturday1 { get; set; }
        [StringLength(10)]
        public string F_Saturday2 { get; set; }
        [StringLength(10)]
        public string F_Saturday3 { get; set; }

        public string F_Sunday1 { get; set; }
        [StringLength(10)]
        public string F_Sunday2 { get; set; }
        [StringLength(10)]
        public string F_Sunday3 { get; set; }


        [Required]
        [StringLength(50)]
        public string F_PId1 { get; set; }

        [StringLength(20)]
        public string F_DialysisType1 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId2 { get; set; }

        [StringLength(20)]
        public string F_DialysisType2 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId3 { get; set; }

        [StringLength(20)]
        public string F_DialysisType3 { get; set; }

        [Required]
        [StringLength(50)]
        public string F_PId4 { get; set; }

        [StringLength(20)]
        public string F_DialysisType4 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId5 { get; set; }

        [StringLength(20)]
        public string F_DialysisType5 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId6 { get; set; }

        [StringLength(20)]
        public string F_DialysisType6 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId7 { get; set; }

        [StringLength(20)]
        public string F_DialysisType7 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId8 { get; set; }

        [StringLength(20)]
        public string F_DialysisType8 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId9 { get; set; }

        [StringLength(20)]
        public string F_DialysisType9 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId10 { get; set; }

        [StringLength(20)]
        public string F_DialysisType10 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId11 { get; set; }

        [StringLength(20)]
        public string F_DialysisType11 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId12 { get; set; }

        [StringLength(20)]
        public string F_DialysisType12 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId13 { get; set; }

        [StringLength(20)]
        public string F_DialysisType13 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId14 { get; set; }

        [StringLength(20)]
        public string F_DialysisType14 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId15 { get; set; }

        [StringLength(20)]
        public string F_DialysisType15 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId16 { get; set; }

        [StringLength(20)]
        public string F_DialysisType16 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId17 { get; set; }

        [StringLength(20)]
        public string F_DialysisType17 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId18 { get; set; }

        [StringLength(20)]
        public string F_DialysisType18 { get; set; }

        [Required]
        [StringLength(50)]
        public string F_PId19 { get; set; }

        [StringLength(20)]
        public string F_DialysisType19 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId20 { get; set; }

        [StringLength(20)]
        public string F_DialysisType20 { get; set; }
        [Required]
        [StringLength(50)]
        public string F_PId21 { get; set; }

        [StringLength(20)]
        public string F_DialysisType21 { get; set; }

        //public int? F_DialysisNo { get; set; }
        //public string F_Name { get; set; }
        public int? F_Sort { get; set; }

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
