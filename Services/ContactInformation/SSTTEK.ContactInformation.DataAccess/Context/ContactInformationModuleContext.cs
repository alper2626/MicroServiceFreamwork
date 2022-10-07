using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PostgresAdapter.Context;
using RestHelpers.DIHelpers;
using ServerBaseContract;
using SSTTEK.ContactInformation.Entities.Db;

namespace SSTTEK.ContactInformation.DataAccess.Context
{
    public class ContactInformationModuleContext : PostgreSqlDbContext
    {
        protected override DatabaseOptions Options { get; set; }

        public ContactInformationModuleContext()
        {
            Options = ServiceTool.ServiceProvider.GetService<DatabaseOptions>();
        }

        /// <summary>
        /// For Migrate
        /// </summary>
        /// <param name="options"></param>
        public ContactInformationModuleContext(DatabaseOptions options)
        {
            Options = options;
        }

        public virtual DbSet<ContactInformationEntity> ContactInformations { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactInformationEntity>().HasIndex(w => w.ContentIndex);

            base.OnModelCreating(modelBuilder);
        }
    }
}
