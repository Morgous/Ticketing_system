namespace TicketingSystem.Repositories.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactionsByOrderIdAsync(Guid orderId);
    }
}
