using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.JSReporting
{
    public class DataReportJSEntity : IEntity<DataReportJSEntity>, ICreationAudited
    {
        [StringLength(50)]
        public string F_PId { get; set; }
        [StringLength(10)]
        public string F_MonthDesc { get; set; }
        [StringLength(30)]
        public string F_FileName { get; set; }
        [StringLength(30)]
        public string F_FileSize { get; set; }
        [StringLength(150)]
        public string F_FilePath { get; set; }
        public bool F_IsCompleted { get; set; } = false;
        public bool? F_HasDownload { get; set; } = false;
        public DateTime? F_DownloadTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
    }
}
