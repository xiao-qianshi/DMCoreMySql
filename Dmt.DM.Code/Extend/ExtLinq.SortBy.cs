﻿
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Dmt.DM.Code
{
    public static partial class ExtLinq
    {
        public static IOrderedQueryable<TEntity> SortBy<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, dynamic>> sortPredicate)
            where TEntity : class, new()
        {
            return InvokeSortBy(query, sortPredicate, SortOrder.Ascending);
        }

        public static IOrderedQueryable<TEntity> SortByDescending<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, dynamic>> sortPredicate)
            where TEntity : class, new()
        {
            return InvokeSortBy(query, sortPredicate, SortOrder.Descending);
        }

        private static IOrderedQueryable<TEntity> InvokeSortBy<TEntity>(IQueryable<TEntity> query,
            Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
            where TEntity : class, new()
        {
            var param = sortPredicate.Parameters[0];
            string propertyName = null;
            Type propertyType = null;
            Expression bodyExpression = null;
            if (sortPredicate.Body is UnaryExpression)
            {
                var unaryExpression = sortPredicate.Body as UnaryExpression;
                bodyExpression = unaryExpression.Operand;
            }
            else if (sortPredicate.Body is MemberExpression)
            {
                bodyExpression = sortPredicate.Body;
            }
            else
                throw new ArgumentException(@"The body of the sort predicate expression should be 
                either UnaryExpression or MemberExpression.", "sortPredicate");
            var memberExpression = (MemberExpression)bodyExpression;
            propertyName = memberExpression.Member.Name;
            if (memberExpression.Member.MemberType == MemberTypes.Property)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;
                if (propertyInfo != null) propertyType = propertyInfo.PropertyType;
            }
            else
                throw new InvalidOperationException(@"Cannot evaluate the type of property since the member expression 
                represented by the sort predicate expression does not contain a PropertyInfo object.");

            var funcType = typeof(Func<,>).MakeGenericType(typeof(TEntity), propertyType);
            var convertedExpression = Expression.Lambda(funcType,
                Expression.Convert(Expression.Property(param, propertyName), propertyType), param);

            var sortingMethods = typeof(Queryable).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var sortingMethodName = GetSortingMethodName(sortOrder);
            var sortingMethod = sortingMethods.First(sm => sm.Name == sortingMethodName &&
                                                           sm.GetParameters().Length == 2);
            return (IOrderedQueryable<TEntity>)sortingMethod
                .MakeGenericMethod(typeof(TEntity), propertyType)
                .Invoke(null, new object[] { query, convertedExpression });
        }

        private static string GetSortingMethodName(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    return "OrderBy";
                case SortOrder.Descending:
                    return "OrderByDescending";
                default:
                    throw new ArgumentException("Sort Order must be specified as either Ascending or Descending.",
            "sortOrder");
            }
        }
    }

    //
    // 摘要:
    //     指定的数据行的排序方式。
    public enum SortOrder
    {
        //
        // 摘要:
        //     默认值。 不指定任何排序顺序。
        Unspecified = -1,
        //
        // 摘要:
        //     行按升序排序。
        Ascending = 0,
        //
        // 摘要:
        //     行按降序排序。
        Descending = 1
    }
}
