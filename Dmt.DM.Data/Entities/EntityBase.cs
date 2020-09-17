using System;

namespace Dmt.DM.Data
{
    /// <inheritdoc/>
    [Serializable]
    public abstract class EntityBase : IEntityBase
    {
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}]";
        }

        public abstract object[] GetKeys();
    }

    /// <inheritdoc cref="IEntityBase{TKey}" />
    [Serializable]
    public abstract class EntityBase<TKey> : EntityBase, IEntityBase<TKey>
    {
        /// <inheritdoc/>
        public virtual TKey F_Id { get; set; }

        protected EntityBase()
        {

        }

        protected EntityBase(TKey id)
        {
            F_Id = id;
        }

        public override object[] GetKeys()
        {
            return new object[] { F_Id };
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {F_Id}";
        }
    }
}
