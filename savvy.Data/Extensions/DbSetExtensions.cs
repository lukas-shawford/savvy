using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace savvy.Data.Extensions
{
    public static class DbSetExtensions
    {
        public static void AttachAsModified<T>(this DbSet<T> dbSet, T entity, DbContext ctx) where T : class
        {
            DbEntityEntry<T> entityEntry = ctx.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                // attach the entity
                dbSet.Attach(entity);
            }

            // transition the entity to the modified state
            entityEntry.State = EntityState.Modified;
        }
    }
}
