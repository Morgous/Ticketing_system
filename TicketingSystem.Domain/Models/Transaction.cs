public class Transaction
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
