
using System;
using System.ComponentModel.DataAnnotations;
namespace Dmt.DM.Domain.Entity.SystemSecurity
{
    public class LogEntity : IEntity<LogEntity>, ICreationAudited
    {
        public DateTime? F_Date { get; set; }
        [StringLength(50)]
        public string F_Account { get; set; }
        [StringLength(50)]
        public string F_NickName { get; set; }
        [StringLength(50)]
        public string F_Type { get; set; }
        [StringLength(50)]
        public string F_IPAddress { get; set; }
        [StringLength(50)]
        public string F_IPAddressName { get; set; }
        [StringLength(50)]
        public string F_ModuleId { get; set; }
        [StringLength(50)]
        public string F_ModuleName { get; set; }
        public bool? F_Result { get; set; }
        [StringLength(50)]
        public string F_Description { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
