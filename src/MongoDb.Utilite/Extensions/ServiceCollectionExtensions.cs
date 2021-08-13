using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MongoDb.Util
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbContext<TContext>(this IServiceCollection services, string connectionString, string database, ServiceLifetime serviceLifeTime = ServiceLifetime.Scoped)
            where TContext:MongoDbContext
        {
            services.AddScoped(_ =>
            {
                TContext @object = (TContext)Activator.CreateInstance(typeof(TContext), connectionString, database);
                @object.OnModalCreating(new MongoModelBuilder());
                SetMongoCollection(@object);
                return @object;
            });
            return services;
        }

        private static void SetMongoCollection<TContext>(TContext context) where TContext : MongoDbContext
        {
            var properties = context.GetType().GetProperties().Where(x=> x.PropertyType.Name == typeof(DbSet<>).Name);
            foreach(var property in properties)
            {
                Type mongoDbType = typeof(DbSet<>);
                Type constructed = mongoDbType.MakeGenericType(property.PropertyType.GenericTypeArguments);
                object mongoDbTypeInstance = Activator.CreateInstance(constructed, context.Database, property.Name);
                property.SetValue(context, mongoDbTypeInstance);
            }
        }
    }
}
