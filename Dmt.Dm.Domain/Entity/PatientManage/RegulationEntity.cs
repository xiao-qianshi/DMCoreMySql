using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class RegulationEntity : IEntity<RegulationEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [Required]
        [StringLength(50)]
        public string F_FileIndex { get; set; }
        [StringLength(10)]
        public string F_FileType { get; set; }
        [Required]
        [StringLength(200)]
        public string F_RegulationName { get; set; }
        [StringLength(200)]
        public string F_FilePath { get; set; }
        [StringLength(30)]
        public string F_FileSize { get; set; }
        [StringLength(20)]
        public string F_UploadPerson { get; set; }
        public DateTime F_UploadDate { get; set; }
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
