
using System;
using System.ComponentModel.DataAnnotations;
namespace Dmt.DM.Domain.Entity.SystemManage
{
    public class RoleEntity : IEntity<RoleEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(50)]
        public string F_OrganizeId { get; set; }
        public int? F_Category { get; set; }
        [StringLength(50)]
        public string F_EnCode { get; set; }
        [StringLength(50)]
        public string F_FullName { get; set; }
        [StringLength(50)]
        public string F_Type { get; set; }
        public bool? F_AllowEdit { get; set; }
        public bool? F_AllowDelete { get; set; }
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
