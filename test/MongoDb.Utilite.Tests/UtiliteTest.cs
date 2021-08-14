using Xunit;
using MongoDb.Utilite.Tests.Data;
using Microsoft.Extensions.DependencyInjection;
using MongoDb.Utilite.Tests.Model;
using MongoDb.Utilite.Tests.Fixture;
using System.Threading.Tasks;
using FluentAssertions;

namespace MongoDb.Utilite.Tests
{
    public class UtiliteTest : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;
        public UtiliteTest(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }
        [Fact]
        public void MongoUtilite_UpdatePersonaPorModelo_DebeDevolverSuccess()
        {
            var person = CreatePerson();
            person.UniqueIdentifier = new System.Guid("d2c54351-3a16-48f0-87d0-7ccba5b16fc9");
            person.Name = "Josep Fuentes";
            var context = _serviceProvider.GetService<PersonDbContext>();
            context.Personas.Update(person);
            Assert.True(true);
        }
        [Trait("Categoria", "POST")]
        [Fact]
        public async Task MongoUtilite_CrearPersona_DebeDevolverSuccess()
        {
            var person = CreatePerson();
            var context = _serviceProvider.GetService<PersonDbContext>();
            await context.Personas.AddAsync(person);
            person.Id.Should().NotBeEmpty("Al crear una persona el deberia autogenerar el Id");
        }
        [Trait("Categoria", "GET")]
        [Fact]
        public async Task MongoUtilite_ListarPersona_DebeDevolverSuccess()
        {
            var context = _serviceProvider.GetService<PersonDbContext>();
            var personas = await context.Personas.ToListAsync();
            personas.Should().HaveCountGreaterThan(0,"Listar todas las personas");
        }
        [Trait("Categoria", "GET")]
        [Theory]
        [InlineData("Carlos")]
        [InlineData("Debra")]
        [InlineData("Gina")]
        public async Task MongoUtilite_ObtenerPersonaPorNombre_DebeDevolverSuccess(string nombre)
        {
            var context = _serviceProvider.GetService<PersonDbContext>();
            var personas = await context.Personas.FirstOrDefaultAsync(x=> x.Name == nombre);
            personas.Should().NotBeNull("Obtener a Persona por Nombre");
        }
        [Trait("Categoria", "PUT")]
        [Theory]
        [InlineData("Carlos")]
        public async Task MongoUtilite_ModificarPersonaPorNombre_DebeDevolverSuccess(string nombre)
        {
            var context = _serviceProvider.GetService<PersonDbContext>();
            var persona = await context.Personas.FirstOrDefaultAsync(x => x.Name == nombre);
            persona.Age = 100;
            await context.Personas.UpdateAsync(x => x.Name == nombre, persona);
            persona.Age.Should().BeGreaterOrEqualTo(100, "Deberia obtener a Carlos con 100 a�os");
        }
        [Trait("Categoria", "DELETE")]
        [Theory]
        [InlineData("Emily")]
        public async Task MongoUtilite_EliminamosPersonaPorNombre_DebeDevolverSuccess(string nombre)
        {
            var context = _serviceProvider.GetService<PersonDbContext>();
            await context.Personas.DeleteAsync(x => x.Name == nombre);

            var persona = await context.Personas.FirstOrDefaultAsync(x => x.Name == nombre);
            persona.Should().BeNull("Deberia devolver Nulo ya que fue eliminada");
        }
        private Person CreatePerson()
        {
            return new Bogus.Faker<Person>()
                 .RuleFor(x => x.Name, f => f.Person.FirstName)
                 .RuleFor(x => x.LastName, f => f.Person.LastName)
                 .RuleFor(x => x.Age, f => f.Random.Number(10, 40))
                 .Generate();
        }
    }
}
