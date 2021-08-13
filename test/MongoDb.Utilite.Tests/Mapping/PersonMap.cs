using MongoDb.Util;
using MongoDb.Utilite.Tests.Model;

namespace MongoDb.Utilite.Tests.Mapping
{
    public class PersonMap : IMongoDbConfiguration<Person>
    {
        public void Configure(MongoDbTypeConfiguration<Person> builder)
        {
            builder.HasKey(x=>x.Id);

            /*Configure columns*/
            builder.Property(x => x.Age)
                .Order(1)
                .DefaultValue(20);

            builder.Property(x => x.Name)
                .DefaultValue("Anonimo");
        }
    }
}
