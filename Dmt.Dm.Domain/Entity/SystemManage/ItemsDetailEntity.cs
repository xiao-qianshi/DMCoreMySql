
using System;
using System.ComponentModel.DataAnnotations;
namespace Dmt.DM.Domain.Entity.SystemManage
{
    public class ItemsDetailEntity : IEntity<ItemsDetailEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(50)]
        public string F_ItemId { get; set; }
        [StringLength(50)]
        public string F_ParentId { get; set; }
        [StringLength(50)]
        public string F_ItemCode { get; set; }
        [StringLength(50)]
        public string F_ItemName { get; set; }
        [StringLength(50)]
        public string F_SimpleSpelling { get; set; }
        public bool? F_IsDefault { get; set; }
        public int? F_Layers { get; set; }
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
