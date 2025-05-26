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
                   new User { Id = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"), FirstName = "Max", LastName = "Payne", Email = "max.payne@example.com", PhoneNumber = "1234567890", DateOfBirth = new DateTime(1990, 1, 1) },
                   new User { Id = Guid.Parse("123e4567-e89b-12d3-a456-426614174001"), FirstName = "John", LastName = "Wick", Email = "john.wick@example.com", PhoneNumber = "0987654321", DateOfBirth = new DateTime(1992, 5, 15) }
                });
            }
        }
    }
}