using Dmt.DM.Code;
using Dmt.DM.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain
{
    public class IEntity<TEntity> : EntityBase<string>
    {
        [Key]
        [StringLength(50)]
        public override string F_Id { get; set; }

        public void Create()
        {
            var entity = this as ICreationAudited;
            entity.F_Id = Common.GuId();
            entity.F_CreatorTime = DateTime.Now;
        }

        public override object[] GetKeys()
        {
            return new object[] { F_Id };
        }

        public void Modify(string keyValue)
        {
            var entity = this as IModificationAudited;
            //entity.F_Id = keyValue;
            //var LoginInfo = OperatorProvider.Provider.GetCurrent();
            //if (LoginInfo != null)
            //{
            //    entity.F_LastModifyUserId = LoginInfo.UserId;
            //}
            entity.F_LastModifyTime = DateTime.Now;
        }
        public void Remove()
        {
            var entity = this as IDeleteAudited;
            //var LoginInfo = OperatorProvider.Provider.GetCurrent();
            //if (LoginInfo != null)
            //{
            //    entity.F_DeleteUserId = LoginInfo.UserId;
            //}
            entity.F_DeleteTime = DateTime.Now;
            entity.F_DeleteMark = true;
        }
    }
}
