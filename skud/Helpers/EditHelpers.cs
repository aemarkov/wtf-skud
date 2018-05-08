using System.Data.Entity;
using System.Linq;
using skud.Data;

namespace skud.Helpers
{
    public static class EditHelpers
    {
        public static void DetachAllEntities<T>(SkudContext ctx)
        {
            var changedEntriesCopy = ctx.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entity in changedEntriesCopy)            
                ctx.Entry(entity.Entity).State = EntityState.Detached;            
        }
    }
}