namespace MongoDb.Util.Interfaces{
    public interface IPropertyConfiguration{
        IPropertyConfiguration HasKey();
        IPropertyConfiguration Name(string name);
        IPropertyConfiguration Order(int order);
        IPropertyConfiguration DefaultValue(object defaultValue);
    }
}