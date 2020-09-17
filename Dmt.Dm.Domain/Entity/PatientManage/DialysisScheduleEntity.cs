using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class DialysisScheduleEntity : IEntity<DialysisScheduleEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
     
        [StringLength(20)]
        public string F_GroupName { get; set; }
        [StringLength(10)]
        public string F_DialysisBedNo { get; set; }
        
        [StringLength(50)]
        public string F_BId { get; set; }
        [StringLength(20)]
        public string F_DialysisType { get; set; }
        public DateTime? F_VisitDate { get; set; }
        public int? F_VisitNo { get; set; }
        [StringLength(50)]
        public string F_PId { get; set; }
        public int? F_DialysisNo { get; set; }
        [StringLength(20)]
        public string F_Name { get; set; }
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
