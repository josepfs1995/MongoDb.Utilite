using System;
using System.Linq.Expressions;

namespace MongoDb.Util.Interfaces
{
    public interface IKeyConfiguration<T> where T:class
    {
        void HasKey(Expression<Func<T, Guid>> expression);
    }
}
