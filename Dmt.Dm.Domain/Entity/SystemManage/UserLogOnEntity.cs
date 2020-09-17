﻿
using System;
using System.ComponentModel.DataAnnotations;
namespace Dmt.DM.Domain.Entity.SystemManage
{
    public class UserLogOnEntity : IEntity<UserLogOnEntity>
    {
        [StringLength(50)]
        public string F_UserId { get; set; }
        [StringLength(50)]
        public string F_UserPassword { get; set; }
        [StringLength(50)]
        public string F_UserSecretkey { get; set; }
        public DateTime? F_AllowStartTime { get; set; }
        public DateTime? F_AllowEndTime { get; set; }
        public DateTime? F_LockStartDate { get; set; }
        public DateTime? F_LockEndDate { get; set; }
        public DateTime? F_FirstVisitTime { get; set; }
        public DateTime? F_PreviousVisitTime { get; set; }
        public DateTime? F_LastVisitTime { get; set; }
        public DateTime? F_ChangePasswordDate { get; set; }
        public bool? F_MultiUserLogin { get; set; }
        public int? F_LogOnCount { get; set; }
        public bool? F_UserOnLine { get; set; }
        [StringLength(50)]
        public string F_Question { get; set; }
        [StringLength(500)]
        public string F_AnswerQuestion { get; set; }
        public bool? F_CheckIPAddress { get; set; }
        [StringLength(50)]
        public string F_Language { get; set; }
        [StringLength(50)]
        public string F_Theme { get; set; }
    }
}
