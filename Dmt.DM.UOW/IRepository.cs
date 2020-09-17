using Dmt.DM.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dmt.DM.UOW
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Insert(TEntity entity, bool autoSave = true);
        int Insert(List<TEntity> entitys, bool autoSave = true);
        int Update(TEntity entity, bool isPartialUpdate = false, bool autoSave = true);
        int Delete(TEntity entity, bool autoSave = true);
        int Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = true);
        TEntity FindEntity(object keyValue);
        TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> IQueryable();
        IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindList(Pagination pagination);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);

        //异步方法
        Task<int> InsertAsync(List<TEntity> entitys, bool autoSave = true);
        Task<int> InsertAsync(TEntity entity, bool autoSave = true);
        Task<int> UpdatePartialAsync(TEntity entity, bool autoSave = true);
        Task<int> UpdateAsync(TEntity entity, bool isPartialUpdate = false, bool autoSave = true);
        Task<int> UpdateAsync<TDto>(TEntity entity, TDto dto, bool autoSave = true) where TDto : class;
        Task<int> DeleteAsync(TEntity entity, bool autoSave = true);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = true);
        Task<TEntity> FindEntityAsync(object keyValue);
        Task<TEntity> FindEntityAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<IQueryable<TEntity>> IQueryableAsync();
        //Task<IQueryable<TEntity>> IQueryableAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindListAsync(Pagination pagination);
        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, Pagination pagination);

        //事务
        IRepository<TEntity> BeginTrans();
        void CommitTrans();
    }
}
