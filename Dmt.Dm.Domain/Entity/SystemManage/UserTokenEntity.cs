using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dmt.DM.Domain.Entity.SystemManage
{
    public class UserTokenEntity : IEntity<UserTokenEntity>
    {
        public string F_AccessTokenHash { get; set; }

        public DateTimeOffset F_AccessTokenExpiresDateTime { get; set; }

        public string F_RefreshTokenIdHash { get; set; }

        public string F_RefreshTokenIdHashSource { get; set; }

        public DateTimeOffset F_RefreshTokenExpiresDateTime { get; set; }

        [StringLength(50), Required]
        public string F_UserId { get; set; }
        [ForeignKey("F_UserId")]
        public virtual UserEntity User { get; set; }

    }
}
