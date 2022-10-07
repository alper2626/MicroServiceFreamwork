using EntityBase.Enum;
using Microsoft.EntityFrameworkCore;

namespace PostgresAdapter.Helper
{
    public static class OperationTypeExtensions
    {
        public static OperationType ToOperationType(this EntityState state)
        {
            switch (state)
            {
                case EntityState.Added: return OperationType.Create;
                case EntityState.Modified: return OperationType.Update;
                case EntityState.Deleted: return OperationType.Delete;
                default: return OperationType.Read;
            }
        }

        public static EntityState ToEntityState(this OperationType operation)
        {
            switch (operation)
            {
                case OperationType.Create: return EntityState.Added;
                case OperationType.Update: case OperationType.Remove: return EntityState.Modified;
                case OperationType.Delete: return EntityState.Deleted;
                default: return EntityState.Unchanged;
            }
        }
    }
}
