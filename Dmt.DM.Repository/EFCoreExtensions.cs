using Dmt.DM.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dmt.DM.EntityFrameWork
{
    public static class EfCoreExtensions
    {
        /// <summary>
        /// 注册某个程序集中所有<typeparamref name="TEntityBase"/>的非抽象子类为实体
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assembly">注册程序集</param>
        /// <param name="entityTypePredicate"></param>
        public static void RegisterEntitiesFromAssembly(this ModelBuilder modelBuilder, Assembly assembly, Func<Type, bool> entityTypePredicate)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            //反射得到ModelBuilder的ApplyConfiguration<TEntity>(...)方法
            //var applyConfigurationMethod = modelBuilder.GetType().GetMethods()
            //    .FirstOrDefault(t =>
            //        t.Name == "ApplyConfiguration" && t.ContainsGenericParameters && t.IsGenericMethod
            //        && t.GetParameters().Any(p => p.ParameterType.ToString() == typeof(IEntityTypeConfiguration<>).ToString())
            //        );
            //.GetMethod("ApplyConfiguration", new [] { typeof(IEntityTypeConfiguration<>) });//"ApplyConfiguration"

            //所有fluent api配置类
            var configTypes = assembly
                               .DefinedTypes
                               .Where(t =>
                                 !t.IsAbstract && t.BaseType != null && t.IsClass
                                && (t.BaseType.BaseType == typeof(EntityBase<string>) || t.BaseType == typeof(EntityBase<long>))
                               ).ToList();

            HashSet<Type> registedTypes = new HashSet<Type>();
            //存在fluent api配置的类,必须在Entity方法之前调用
            //configTypes.ForEach(mappingType =>
            //{
            //    var entityType = mappingType.GetTypeInfo().ImplementedInterfaces.First().GetGenericArguments().Single();

            //    //如果不满足条件的实体,不注册
            //    if (!entityTypePredicate(entityType))
            //        return;

            //    var map = Activator.CreateInstance(mappingType);
            //    applyConfigurationMethod.MakeGenericMethod(entityType)
            //         .Invoke(modelBuilder, new object[] { map });

            //    registedTypes.Add(entityType);
            //});

            foreach (var r in configTypes// assembly.DefinedTypes
                .Where(r => !registedTypes.Contains(r))
                .Where(entityTypePredicate))
            {
                modelBuilder.Entity(r);
            }
        }
    }
}
