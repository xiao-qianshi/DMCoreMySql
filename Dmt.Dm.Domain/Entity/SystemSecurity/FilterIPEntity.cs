
using System;
using System.ComponentModel.DataAnnotations;
namespace Dmt.DM.Domain.Entity.SystemSecurity
{
    public class DbBackupEntity : IEntity<FilterIPEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(50)]
        public string F_BackupType { get; set; }
        [StringLength(50)]
        public string F_DbName { get; set; }
        [StringLength(50)]
        public string F_FileName { get; set; }
        [StringLength(50)]
        public string F_FileSize { get; set; }
        [StringLength(500)]
        public string F_FilePath { get; set; }
        public DateTime? F_BackupTime { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_DeleteMark { get; set; }
        public bool? F_EnabledMark { get; set; }
        [StringLength(500)]
        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        [StringLength(50)]
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        [StringLength(50)]
        public string F_DeleteUserId { get; set; }
    }
}
