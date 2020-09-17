using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dmt.DM.EntityFrameWork
{
    public class HsDbContext : DbContext
    {
        public HsDbContext(DbContextOptions<HsDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.Load("Dmt.DM.Domain");
            modelBuilder.RegisterEntitiesFromAssembly(assembly,(type => true));
            
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = "Sys_" + entityType.Relational().TableName.Replace("Entity", "");
            }
        }
    }   
}

