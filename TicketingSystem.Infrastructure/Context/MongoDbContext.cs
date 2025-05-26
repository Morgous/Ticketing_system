using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TicketingSystem.Domain.Models;

namespace TicketingSystem.Infrastructure.Context
{
    public class MongoDbContext:IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {

            //var config = new ConfigurationBuilder()
            //  .AddUserSecrets<MongoDbContext>()
            //  .Build();


            var connectionString = configuration["MongoDbSettings:ConnectionString"];
            var databaseName = configuration["MongoDbSettings:DatabaseName"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public void SeedData()
        {
            if (!Users.AsQueryable().Any())
            {
                Users.InsertMany(new List<User>
                {
                   new User { Id = Guid.Parse("b0b79e20-0e5f-41b7-adc9-957847f06fe6"), FirstName = "Max", LastName = "Payne", Email = "max.payne@example.com", PhoneNumber = "1234567890", DateOfBirth = new DateTime(1990, 1, 1) },
                   new User { Id = Guid.Parse("061734a3-57c6-443b-a454-bc442c6feb34"), FirstName = "Jardani", LastName = "Wick", Email = "Jardani.wick@example.com", PhoneNumber = "0987654321", DateOfBirth = new DateTime(1992, 5, 15) }
                });
            }
        }
    }
}