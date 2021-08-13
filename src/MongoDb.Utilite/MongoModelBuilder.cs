using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MongoDb.Util
{
    public class MongoModelBuilder
    {
        public void ApplyConfigurationFromAssembly(Assembly assembly)
        {
            var mongoConfiguration = typeof(IMongoDbConfiguration<>);
            var @class = assembly.GetTypes().Where(x => x.GetInterfaces().Any(y => y.Name == mongoConfiguration.Name));
            CompileConfigure(@class);
        }
        private void CompileConfigure(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                var @class = GetGenericTypeInType(type);
                Type mongoDbType = typeof(MongoDbTypeConfiguration<>);
                Type constructed = mongoDbType.MakeGenericType(@class);
                object mongoDbTypeInstance = Activator.CreateInstance(constructed);
                type.GetMethod("Configure").Invoke(Activator.CreateInstance(type), new object[] { mongoDbTypeInstance });
            }
        }
        private Type GetGenericTypeInType(Type type)
        {
            var @interface = type.GetInterfaces().FirstOrDefault(x => x.Name == typeof(IMongoDbConfiguration<>).Name);
            return @interface.GenericTypeArguments.FirstOrDefault();
        }
    }
}
