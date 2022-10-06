using Microsoft.EntityFrameworkCore;
using MsSqlAdapter.Context;
using ServerBaseContract;
using SSTTEK.Contacts.Entities.Db;

namespace SSTTEK.Contact.DataAccess.Context
{
    public class ContactModuleContext : MsSqlDbContext
    {
        public ContactModuleContext(DatabaseOptions options) : base(options)
        {

        }

        public virtual DbSet<ContactEntity> Contacts { get; set; }

        public virtual DbSet<ContactInformationEntity> ContactInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactInformationEntity>().HasIndex(w => w.ContentIndex);

            base.OnModelCreating(modelBuilder);
        }
    }
}
