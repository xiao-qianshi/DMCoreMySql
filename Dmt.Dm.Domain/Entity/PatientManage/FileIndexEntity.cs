using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    /// <summary>
    /// 文书索引 视图
    /// </summary>
    public class FileIndexEntity : IEntity<FileIndexEntity>
    {
        /// <summary>
        /// 患者ID
        /// </summary>
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string F_Name { get; set; }
        /// <summary>
        /// 透析号
        /// </summary>
        public int? F_DialysisNo { get; set; }
        public string F_CardNo { get; set; }
        public string F_RecordNo { get; set; }
        public string F_PatientNo { get; set; }
        public string F_Gender { get; set; }
        public string F_FileType { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_RealName { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
    }
}
