using Dmt.DM.Code;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dmt.DM.UOW
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        private IDbContextTransaction _dbTransaction { get; set; }
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public int Insert(TEntity entity, bool autoSave = true)
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Added;
            return autoSave ? _dbContext.SaveChanges() : 0;
        }

        public int Insert(List<TEntity> entitys, bool autoSave = true)
        {
            foreach (var entity in entitys)
            {
                _dbContext.Entry<TEntity>(entity).State = EntityState.Added;
            }
            return autoSave ? _dbContext.SaveChanges() : 0;
        }

        public int Update(TEntity entity, bool isPartialUpdate = false, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!isPartialUpdate)
                {
                    if (prop.GetValue(entity, null) != null)
                    {
                        if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                            _dbContext.Entry(entity).Property(prop.Name).CurrentValue = null;
                        _dbContext.Entry(entity).Property(prop.Name).IsModified = !prop.Name.Equals("F_Id", StringComparison.InvariantCultureIgnoreCase);
                    }
                }
                else
                {
                    var currentValue = _dbContext.Entry(entity).Property(prop.Name).CurrentValue;
                    var originalValue = _dbContext.Entry(entity).Property(prop.Name).OriginalValue;
                    if (originalValue != null && currentValue != null)
                    {
                        if (!originalValue.Equals(currentValue)) _dbContext.Entry(entity).Property(prop.Name).IsModified = true;
                    }
                    else if (originalValue != null || currentValue != null)
                    {
                        _dbContext.Entry(entity).Property(prop.Name).IsModified = true;
                    }
                    _dbContext.Entry(entity).Property(prop.Name).IsModified = !prop.Name.Equals("F_Id", StringComparison.InvariantCultureIgnoreCase);
                }
            }
            return autoSave ? _dbContext.SaveChanges() : 0;
        }

        public int Delete(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return autoSave ? _dbContext.SaveChanges() : 0;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = true)
        {
            var entitys = _dbContext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => _dbContext.Entry<TEntity>(m).State = EntityState.Deleted);
            return autoSave ? _dbContext.SaveChanges() : 0;
        }

        public TEntity FindEntity(object keyValue)
        {
            return _dbContext.Set<TEntity>().Find(keyValue);
        }

        public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> IQueryable()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public List<TEntity> FindList(Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = _dbContext.Set<TEntity>().AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToList();
        }

        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = _dbContext.Set<TEntity>().Where(predicate);
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToList();
        }

        public Task<int> InsertAsync(List<TEntity> entitys, bool autoSave = true)
        {
            foreach (var entity in entitys)
            {
                _dbContext.Entry<TEntity>(entity).State = EntityState.Added;
            }
            return autoSave ? _dbContext.SaveChangesAsync() : Task.FromResult(0);
        }

        public Task<int> InsertAsync(TEntity entity, bool autoSave = true)
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Added;
            return autoSave ? _dbContext.SaveChangesAsync() : Task.FromResult(0);
        }

        public Task<int> UpdatePartialAsync(TEntity entity, bool autoSave = true)
        {
            return autoSave ? _dbContext.SaveChangesAsync() : Task.FromResult(0);
        }

        public Task<int> UpdateAsync(TEntity entity, bool isPartialUpdate = false, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!isPartialUpdate)
                {
                    if (prop.GetValue(entity, null) != null)
                    {
                        if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                            _dbContext.Entry(entity).Property(prop.Name).CurrentValue = null;
                        _dbContext.Entry(entity).Property(prop.Name).IsModified = !prop.Name.Equals("F_Id", StringComparison.InvariantCultureIgnoreCase);
                    }
                }
                else
                {
                    
                    var currentValue = _dbContext.Entry(entity).Property(prop.Name).CurrentValue;
                    var originalValue = _dbContext.Entry(entity).Property(prop.Name).OriginalValue;
                    if (originalValue != null && currentValue != null)
                    {
                        if (!originalValue.Equals(currentValue)) _dbContext.Entry(entity).Property(prop.Name).IsModified = true;
                    }
                    else if (originalValue != null || currentValue != null)
                    {
                        _dbContext.Entry(entity).Property(prop.Name).IsModified = true;
                    }
                    _dbContext.Entry(entity).Property(prop.Name).IsModified = !prop.Name.Equals("F_Id", StringComparison.InvariantCultureIgnoreCase);
                }
            }
            return autoSave ? _dbContext.SaveChangesAsync() : Task.FromResult(0);
        }

        public Task<int> UpdateAsync<TDto>(TEntity entity, TDto dto, bool autoSave = true) where TDto: class
        {
            _dbContext.Set<TEntity>().Attach(entity);
            var targetProps = entity.GetType().GetProperties();
            var sourceProps = dto.GetType().GetProperties();
            foreach (var prop in sourceProps)
            {
                var value = prop.GetValue(dto, null);
                if (value==null) continue;
                var entityItem = _dbContext.Entry(entity).Property(prop.Name);
                if (entityItem == null) continue;
                if (value.ToString() == "&nbsp;")
                {
                    entityItem.CurrentValue = null;
                }
                else
                {
                    var find = targetProps.First(p => p.Name.Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase));
                    var sourceValue = value.ToString();
                    if (find.PropertyType == typeof(string))
                    {
                        entityItem.CurrentValue = sourceValue;
                    }
                    else if (find.PropertyType == typeof(DateTime?) || find.PropertyType == typeof(DateTime))
                    {
                        if (DateTime.TryParse(sourceValue, out var date))
                        {
                            entityItem.CurrentValue = date;
                        }
                        else
                        {
                            entityItem.CurrentValue = null;
                        }
                    }
                    else if (find.PropertyType == typeof(float?) || find.PropertyType == typeof(float))
                    {
                        if (float.TryParse(sourceValue, out var f))
                        {
                            entityItem.CurrentValue = f;
                        }
                    }
                    else if (find.PropertyType == typeof(bool?) || find.PropertyType == typeof(bool))
                    {
                        if (bool.TryParse(sourceValue, out var b))
                        {
                            entityItem.CurrentValue = b;
                        }
                    }
                    else if (find.PropertyType == typeof(int?) || find.PropertyType == typeof(int))
                    {
                        if (int.TryParse(sourceValue, out var i))
                        {
                            entityItem.CurrentValue = i;
                        }
                    }
                    else if (find.PropertyType == typeof(double?) || find.PropertyType == typeof(double))
                    {
                        if (double.TryParse(sourceValue, out var temp))
                        {
                            entityItem.CurrentValue = temp;
                        }
                    }
                }
                
                entityItem.IsModified = true;
            }
            return autoSave ? _dbContext.SaveChangesAsync() : Task.FromResult(0);
        }

        public Task<int> DeleteAsync(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return autoSave ? _dbContext.SaveChangesAsync() : Task.FromResult(0);
        }

        public Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = true)
        {
            var entitys = _dbContext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => _dbContext.Entry<TEntity>(m).State = EntityState.Deleted);
            return autoSave ? _dbContext.SaveChangesAsync() : Task.FromResult(0);
        }

        public Task<TEntity> FindEntityAsync(object keyValue)
        {
            return _dbContext.Set<TEntity>().FindAsync(keyValue);
        }

        public Task<TEntity> FindEntityAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> FindListAsync(Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = _dbContext.Set<TEntity>().AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToListAsync();
        }

        public Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = _dbContext.Set<TEntity>().Where(predicate);
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToListAsync();
        }

        public IRepository<TEntity> BeginTrans()
        {
            //DbConnection dbConnection = ((IObjectContextAdapter)_dbContext).ObjectContext.Connection;
            //if (dbConnection.State == ConnectionState.Closed)
            //{
            //    dbConnection.Open();
            //}
            //dbTransaction = dbConnection.BeginTransaction();DbContext.Database.CreateExecutionStrategy()
            _dbTransaction = _dbContext.Database.BeginTransaction();
            return this;
        }

        public void CommitTrans()
        {
            try
            {
                _dbContext.SaveChanges();
                _dbTransaction.Commit();
            }
            catch
            {
                _dbTransaction.Rollback();
            }
        }
    }
}
