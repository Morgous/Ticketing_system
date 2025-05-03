using Ticketing_system.DomainLayer.Entities;

namespace Ticketing_System.BusinessLayer.Common.Interfaces;
public interface ITicketingDbContext
{
    public DbSet<Event> Events { get; }
    public DbSet<Venue> Venues { get; }
    public DbSet<Section> Sections { get; }
    public DbSet<Row> Rows { get; }
    public DbSet<Seat> Seats { get; }
    public DbSet<Price> Prices { get; }
    public DbSet<EventSeat> EventSeats { get; }
    public DbSet<Cart> Carts { get; }
    public DbSet<Payment> Payment { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
