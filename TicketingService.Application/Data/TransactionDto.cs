namespace TicketingSystem.Application.Data
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
