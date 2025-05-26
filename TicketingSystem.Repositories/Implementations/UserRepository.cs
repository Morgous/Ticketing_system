using MongoDB.Driver;
using TicketingSystem.Domain.Models;
using TicketingSystem.Infrastructure.Context;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingSystem.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public UserRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _mongoDbContext.Users.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _mongoDbContext.Users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(User entity)
        {
            await _mongoDbContext.Users.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(User entity)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, entity.Id);
            await _mongoDbContext.Users.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            await _mongoDbContext.Users.DeleteOneAsync(filter);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _mongoDbContext.Users.Find(user => user.Email == email).FirstOrDefaultAsync();
        }
    }
}
