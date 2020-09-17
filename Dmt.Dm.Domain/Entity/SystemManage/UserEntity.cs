
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dmt.DM.Domain.Entity.SystemManage
{
    public class UserEntity : IEntity<UserEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [StringLength(50)]
        public string F_Account { get; set; }
        [StringLength(50)]
        public string F_RealName { get; set; }
        [StringLength(50)]
        public string F_NickName { get; set; }
        [StringLength(500)]
        public string F_Password { get; set; }
        [StringLength(50)]
        public string F_HeadIcon { get; set; }
        public bool? F_Gender { get; set; }
        public DateTime? F_Birthday { get; set; }
        [StringLength(20)]
        public string F_MobilePhone { get; set; }
        [StringLength(50)]
        public string F_Email { get; set; }
        [StringLength(50)]
        public string F_WeChat { get; set; }
        [StringLength(50)]
        public string F_ManagerId { get; set; }
        public int? F_SecurityLevel { get; set; }
        [StringLength(500)]
        public string F_Signature { get; set; }
        [StringLength(50)]
        public string F_OrganizeId { get; set; }
        [StringLength(500)]
        public string F_DepartmentId { get; set; }
        [StringLength(500)]
        public string F_RoleId { get; set; }
        [StringLength(500)]
        public string F_DutyId { get; set; }
        public bool? F_IsAdministrator { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_DeleteMark { get; set; }
        public bool? F_EnabledMark { get; set; }
        public DateTimeOffset? F_LastLoggedIn { get; set; }
        [StringLength(100)]
        public string F_SerialNumber { get; set; }
        public bool F_IsActive { get; set; } = true;
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
