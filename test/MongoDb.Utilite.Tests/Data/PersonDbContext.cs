using MongoDb.Util;
using MongoDb.Utilite.Tests.Model;

namespace MongoDb.Utilite.Tests.Data
{
    public class PersonDbContext : MongoDbContext
    {
        public DbSet<Person> Personas { get; set; }
        public PersonDbContext(string connectionString, string database) : base(connectionString, database)
        {

        }
        public override void OnModalCreating(MongoModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationFromAssembly(GetType().Assembly);
        }
    }
}
