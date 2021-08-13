using MongoDB.Driver;

namespace MongoDb.Util
{
    public class MongoDbContext
    {
        private readonly MongoClient _mongoClient;
        public IMongoDatabase Database { get; private set; }
        public MongoDbContext(string connectionString, string database)
        {
            _mongoClient = new MongoClient(connectionString);
            Database = _mongoClient.GetDatabase(database);
        }
        public virtual void OnModalCreating(MongoModelBuilder modelBuilder)
        {

        }
    }
}
