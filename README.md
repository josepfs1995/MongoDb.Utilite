# Introduction

This is a framework from MongoDb orientaded in EntityFramework Core, but is basic.

## Installation

Use the package manager [MongoDb.Utilite](https://www.nuget.org/packages/MongoDb.Utilite/) to install MongoDb.Utilite.

```bash
Install-Package MongoDb.Utilite
```

## Step by Step

1.- Create your model 
```csharp
public class Person
{
   //Id is necessary
   public Guid Id { get; set; }
   public string Name { get; set; }
   public int Age { get; set; }
}
```
2.- Create your Mapper (Implement IMongoDbConfiguration<T> is this example T is Person)
```csharp
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
```
3.- Create your Context (Inheriting of MongoDbContext and passing their parameters in the constructor)

```csharp
 public class PersonDbContext : MongoDbContext
    {
        //DbSet of your MongoCollections *this is neccesary
        public DbSet<Person> Personas { get; set; }
        //Passing their parameters
        public PersonDbContext(string connectionString, string database) : base(connectionString, database)
        {

        }
        public override void OnModalCreating(MongoModelBuilder modelBuilder)
        {
           //ApplyConfigurationFromAssembly to assigns all configures by assembly *this is neccesary
            modelBuilder.ApplyConfigurationFromAssembly(GetType().Assembly);
        }
    }
```
4.- Inject MongoDbContext in your Startup (ConfigureService) 
```csharp
  service.AddMongoDbContext<PersonDbContext>("mongodb://server", "database");
```
5.- In the constructor of your repositories inject your Context 
```csharp
  private readonly PersonDbContext _context;
  public PersonRepository(PersonDbContext context)
  {
     _context = context;
  }

  //Create
  public async Task Create(Person person){
      await _context.Person.Add(person);  
  }
  //List
  public async Task<IEnumerable<Person>> GetAll(){
      await _context.Person.ToListAsync();  
  }
  //Get
  public async Task<IEnumerable<Person>> Get(string name){
      await _context.Person.FirstOrDefault(x=> x.Name == name);  
  }
  //Edit
  public async Task<IEnumerable<Person>> Get(Person person){
      await _context.Person.Update(x=> x.Name == name, person);  
  }
  //Delete
 public async Task<IEnumerable<Person>> Delete(string name){
      await _context.Person.Delete(x=> x.Name == name);  
  }
```
## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)