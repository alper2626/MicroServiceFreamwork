using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PostgresAdapter.Helper
{
    public static partial class FilterBuilder
    {
        static readonly MethodInfo SetMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set), new Type[] { });

        public static IQueryable Query(this DbContext context, Type entityType) =>
            (IQueryable)SetMethod.MakeGenericMethod(entityType).Invoke(context, null);
    }
}
