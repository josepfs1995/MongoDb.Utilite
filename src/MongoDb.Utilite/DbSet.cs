using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDb.Util
{
    public class DbSet<TEntity> where TEntity : class
    {
        private IMongoCollection<TEntity> mongoCollection;
        public DbSet()
        {
            
        }
        public DbSet(IMongoDatabase database, string name)
        {
            mongoCollection = database.GetCollection<TEntity>(name);
        }
        public async Task<IEnumerable<TEntity>> ToListAsync()
        {
            return await mongoCollection.Find(filter => true).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await mongoCollection.Find(filter).ToListAsync();
        }
        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await mongoCollection.Find(filter => true).FirstOrDefaultAsync();
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await mongoCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task Add(TEntity model)
        {
            await mongoCollection.InsertOneAsync(model);
        }
        public async Task Update(Expression<Func<TEntity, bool>> filter,TEntity model)
        {
            await mongoCollection.ReplaceOneAsync(filter, model);
        }
        public async Task Delete(Expression<Func<TEntity, bool>> filter)
        {
            await mongoCollection.DeleteOneAsync(filter);
        }
    }
}
