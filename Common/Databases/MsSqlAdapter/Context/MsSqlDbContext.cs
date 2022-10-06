using Microsoft.EntityFrameworkCore;
using ServerBaseContract;

namespace MsSqlAdapter.Context
{
    public abstract class MsSqlDbContext : DbContext

    {
        protected abstract DatabaseOptions Options { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Options.ConnectionString);
        }

    }
}
