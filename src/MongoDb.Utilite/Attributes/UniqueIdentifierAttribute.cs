using System;

namespace MongoDb.Util{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    ///<summary>
    /// Indicates that the property is a unique identifier. (Guid)
    ///</summary>
    public class UniqueIdentifierAttribute:Attribute{

    }
}