using Microsoft.EntityFrameworkCore;
using ServerBaseContract;

namespace MsSqlAdapter.Context
{
    public class MsSqlDbContext : DbContext

    {
        private readonly DatabaseOptions _options;

        public MsSqlDbContext(DatabaseOptions options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.ConnectionString);
        }

    }
}
