using MongoDB.Driver;
using TicketingSystem.Domain.Models;

namespace TicketingSystem.Infrastructure.Context
{
    public interface IMongoDbContext
    {
        IMongoCollection<User> Users { get; }
    }
}
