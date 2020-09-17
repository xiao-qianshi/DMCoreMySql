using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.SystemManage
{
    /// <summary>
    /// 审计日志
    /// </summary>
    public class AuditLogEntity : IEntity<AuditLogEntity>, ICreationAudited
    {
        [StringLength(30)]
        public string F_ControllName { get; set; }
        [StringLength(30)]
        public string F_MethodName { get; set; }
        public string F_Parameters { get; set; }
        public string F_Exception { get; set; }
        public string F_Result { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }

    }
}
