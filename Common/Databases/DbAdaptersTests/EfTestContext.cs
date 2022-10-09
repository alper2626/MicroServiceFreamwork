using EntityBase.Concrete;
using Microsoft.EntityFrameworkCore;
using MongoDbExtender.Models;

namespace DbAdaptersTests
{
    public class EfTestContext : DbContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName:"testDb");
        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }

    public class TestEntity : RemovableEntity
    {
        public string Name { get; set; }
    }

    public class TestMongoEntity : RemovebleMongoEntity
    {
        public string Name { get; set; }
    }
}
