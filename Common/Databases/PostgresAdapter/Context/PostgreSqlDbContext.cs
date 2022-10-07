using Microsoft.EntityFrameworkCore;
using ServerBaseContract;

namespace PostgresAdapter.Context
{
    public abstract class PostgreSqlDbContext : DbContext
    {
        protected abstract DatabaseOptions Options { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Options.ConnectionString);
        }

    }
}
