using Microsoft.EntityFrameworkCore;
using TicketingSystem.Infrastructure.Context;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingSystem.Repositories.Implementations
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByOrderIdAsync(Guid orderId)
        {
            return await _context.Transactions
                                 .Where(t => t.OrderId == orderId)
                                 .ToListAsync();
        }
    }
}
