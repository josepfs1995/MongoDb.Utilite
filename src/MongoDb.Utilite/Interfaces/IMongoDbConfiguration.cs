using System;

namespace MongoDb.Util
{
    public interface IMongoDbConfiguration<TEntity> where TEntity: class{
        void Configure(MongoDbTypeConfiguration<TEntity> builder);
    }
}
