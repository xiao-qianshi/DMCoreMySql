
using System;
using System.ComponentModel.DataAnnotations;
namespace Dmt.DM.Domain.Entity.SystemManage
{
    public class RoleAuthorizeEntity : IEntity<RoleAuthorizeEntity>, ICreationAudited
    {
        public int? F_ItemType { get; set; }
        [StringLength(50)]
        public string F_ItemId { get; set; }
        public int? F_ObjectType { get; set; }
        [StringLength(50)]
        public string F_ObjectId { get; set; }
        public int? F_SortCode { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
    }
}
