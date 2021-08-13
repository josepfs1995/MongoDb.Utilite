using Microsoft.Extensions.DependencyInjection;
using MongoDb.Util;
using MongoDb.Utilite.Tests.Data;

namespace MongoDb.Utilite.Tests.Fixture
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMongoDbContext<PersonDbContext>("mongodb://localhost:27017/", "cqrs");
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public ServiceProvider ServiceProvider { get; private set; }
    }
}
