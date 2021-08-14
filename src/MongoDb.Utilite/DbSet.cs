using MongoDb.Util.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDb.Util
{
    public class DbSet<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        private IMongoCollection<TEntity> mongoCollection;
        public DbSet()
        {

        }
        public DbSet(IMongoDatabase database, string name)
        {
            mongoCollection = database.GetCollection<TEntity>(name);
        }
        public IEnumerable<TEntity> ToList()
        {
            return mongoCollection.Find(filter => true).ToList();
        }
        public IEnumerable<TEntity> ToList(Expression<Func<TEntity, bool>> filter)
        {
            return mongoCollection.Find(filter).ToList();
        }
        public async Task<IEnumerable<TEntity>> ToListAsync()
        {
            return await mongoCollection.Find(filter => true).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await mongoCollection.Find(filter).ToListAsync();
        }
        public TEntity FirstOrDefault()
        {
            return mongoCollection.Find(filter => true).FirstOrDefault();
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return mongoCollection.Find(filter).FirstOrDefault();
        }
        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await mongoCollection.Find(filter => true).FirstOrDefaultAsync();
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await mongoCollection.Find(filter).FirstOrDefaultAsync();
        }
        public void Add(TEntity model)
        {
            mongoCollection.InsertOne(model);
        }
        public async Task AddAsync(TEntity model)
        {
            await mongoCollection.InsertOneAsync(model);
        }
        public void Update(TEntity model)
        {  
            var filter = GenerateExpression(model);
            var modelInDatabase = FirstOrDefault(filter);
            model.GetType().GetProperty("Id").SetValue(model, modelInDatabase.GetType().GetProperty("Id").GetValue(modelInDatabase));
            mongoCollection.ReplaceOne(filter, model);
        }
        public void Update(Expression<Func<TEntity, bool>> filter, TEntity model)
        {
            mongoCollection.ReplaceOne(filter, model);
        }
        public async Task UpdateAsync(TEntity model)
        {
            var filter = GenerateExpression(model);
            var modelInDatabase = FirstOrDefault(filter);
            model.GetType().GetProperty("Id").SetValue(model, modelInDatabase.GetType().GetProperty("Id").GetValue(modelInDatabase));
            await mongoCollection.ReplaceOneAsync(filter, model);
        }

        public async Task UpdateAsync(Expression<Func<TEntity, bool>> filter, TEntity model)
        {
            await mongoCollection.ReplaceOneAsync(filter, model);
        }
        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            mongoCollection.DeleteOne(filter);
        }
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            await mongoCollection.DeleteOneAsync(filter);
        }
        private Expression<Func<TEntity, bool>> GenerateExpression(TEntity model)
        {
            Type type = model.GetType();
            Expression<Func<TEntity, bool>> expression = null;
            var property = type.GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(UniqueIdentifierAttribute), true).Any());
            if (property == null) throw new ArgumentException("Entity must have a property with the [UniqueIdentifier] attribute");
            ConstantExpression constant = Expression.Constant(type.GetProperty(property.Name).GetValue(model), typeof(Guid));


            var parameterInLambda = Expression.Parameter(typeof(TEntity));
            var propertyofParameter = Expression.Property(parameterInLambda, property);
            var lambda = Expression.Equal(
                Expression.Convert(propertyofParameter, property.PropertyType), 
                Expression.Constant(property.GetValue(model), typeof(Guid)));
            expression = Expression.Lambda<Func<TEntity, bool>>(lambda, parameterInLambda);
            return expression;
        }
    }
}