using System.Reflection;
using Ticketing_System.BusinessLayer.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ticketing_system.DomainLayer.Entities;

namespace Ticketing_system.DataAccessLayer.Data;

public class TicketingDbContext : DbContext, ITicketingDbContext
{
    public TicketingDbContext() { }
    public TicketingDbContext(DbContextOptions<TicketingDbContext> options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<Row> Rows => Set<Row>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Price> Prices => Set<Price>();
    public DbSet<EventSeat> EventSeats => Set<EventSeat>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Payment> Payment => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
