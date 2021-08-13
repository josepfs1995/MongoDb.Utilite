using System;
using System.Linq.Expressions;
using MongoDb.Util.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MongoDb.Util
{
    public class MongoDbTypeConfiguration<T>: IKeyConfiguration<T>, 
                                            IPropertyConfiguration where T:class{
        private BsonMemberMap bsonMemberMap;
        private readonly BsonClassMap<T> bsonClassMap;
        public MongoDbTypeConfiguration()
        {
            bsonClassMap = BsonClassMap.RegisterClassMap<T>();
        }
        public void HasKey(Expression<Func<T, Guid>> expression){
            bsonClassMap.MapProperty(expression)
            .SetIdGenerator(CombGuidGenerator.Instance);
        }
        public IPropertyConfiguration HasKey()
        {
            bsonMemberMap.SetIdGenerator(CombGuidGenerator.Instance);
            return this;
        }
        public IPropertyConfiguration Property(Expression<Func<T, object>> expression){
            bsonMemberMap = bsonClassMap.MapProperty(expression);
            return this;
        }
        public IPropertyConfiguration Name(string name)
        {
            bsonMemberMap.SetElementName(name);
            return this;
        }
        public IPropertyConfiguration Order(int order)
        {
            bsonMemberMap.SetOrder(order);
            return this;
        }
        public IPropertyConfiguration DefaultValue(object defaultValue)
        {
            bsonMemberMap.SetDefaultValue(defaultValue);
            return this;
        }

        
    }
}
