﻿
using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.SystemManage
{
    public class OrganizeEntity : IEntity<OrganizeEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(50)]
        public string F_ParentId { get; set; }
        public int? F_Layers { get; set; }
        [StringLength(50)]
        public string F_EnCode { get; set; }
        [StringLength(50)]
        public string F_FullName { get; set; }
        [StringLength(50)]
        public string F_ShortName { get; set; }
        [StringLength(50)]
        public string F_CategoryId { get; set; }
        [StringLength(50)]
        public string F_ManagerId { get; set; }
        [StringLength(20)]
        public string F_TelePhone { get; set; }
        [StringLength(20)]
        public string F_MobilePhone { get; set; }
        [StringLength(50)]
        public string F_WeChat { get; set; }
        [StringLength(20)]
        public string F_Fax { get; set; }
        [StringLength(50)]
        public string F_Email { get; set; }
        [StringLength(50)]
        public string F_AreaId { get; set; }
        [StringLength(500)]
        public string F_Address { get; set; }
        public bool? F_AllowEdit { get; set; }
        public bool? F_AllowDelete { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_DeleteMark { get; set; }
        public bool? F_EnabledMark { get; set; }
        [StringLength(200)]
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
