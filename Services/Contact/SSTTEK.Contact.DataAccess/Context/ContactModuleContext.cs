using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MsSqlAdapter.Context;
using RestHelpers.DIHelpers;
using ServerBaseContract;
using SSTTEK.Contact.Entities.Db;

namespace SSTTEK.Contact.DataAccess.Context
{
    public class ContactModuleContext : MsSqlDbContext
    {
        protected override DatabaseOptions Options { get; set; }

        public ContactModuleContext()
        {
            Options = ServiceTool.ServiceProvider.GetService<DatabaseOptions>();
        }

        /// <summary>
        /// For Migrate
        /// </summary>
        /// <param name="options"></param>
        public ContactModuleContext(DatabaseOptions options)
        {
            Options = options;
        }

        public virtual DbSet<ContactEntity> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
