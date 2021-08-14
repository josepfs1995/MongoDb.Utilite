using System;
using MongoDb.Util;

namespace MongoDb.Utilite.Tests.Model
{
    public class Person
    {
        public Person()
        {
            UniqueIdentifier = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [UniqueIdentifier]
        public Guid UniqueIdentifier { get; set; }
        public string Name { get; set; }
        public string LastName { get; set;}
        public int Age { get; set; }
        public int Edad { get; set; }
    }
}
