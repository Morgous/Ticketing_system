using Microsoft.EntityFrameworkCore;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Domain.Models;

namespace TicketingSystem.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserSeatReservation> UserSeatReservations { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));

            modelBuilder.Entity<Seat>()
                .Property(s => s.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (SeatStatus)Enum.Parse(typeof(SeatStatus), v));

            modelBuilder.Entity<Order>()
               .Property(s => s.Status)
               .HasConversion(
                   v => v.ToString(),
                   v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));

            // --------------------- SEED DATA ----------------------

            // Venues
            var mainVenueId = Guid.Parse("bb578f5f-836d-4767-8e98-58d0afbc3ff8");
            modelBuilder.Entity<Venue>().HasData(
                new Venue { Id = mainVenueId, Name = "My awesome Venue", Address = "Centralna Street 57" }
            );

            // Sections
            var sectionAId = Guid.Parse("03dbdd25-660b-4980-a918-c8d918594d8e");
            var sectionBId = Guid.Parse("ba39cf73-57c4-4f2e-a180-9d6c4ecb03bb");
            modelBuilder.Entity<Section>().HasData(
                new Section { Id = sectionAId, Name = "Some Section", VenueId = mainVenueId },
                new Section { Id = sectionBId, Name = "Some Other Section", VenueId = mainVenueId }
            );

            // Events
            var eventId = Guid.Parse("b1e8c82c-736f-4a6b-9f10-15d562ee5692");
            modelBuilder.Entity<Event>().HasData(
                new Event { Id = eventId, Title = "Awesome Rock Concert", Location = "My awesome Venue", EventDate = new DateTime(2025, 1, 1) }
            );

            // Seats
            var seats = new List<Seat>
            {
                new Seat { Id = Guid.Parse("a87980ba-e793-4d76-8b67-45c5273a2dde"), Row = "B", SeatNumber = "3", IsAvailable = false, EventId = eventId, SectionId = sectionAId, Status = SeatStatus.Available },
                new Seat { Id = Guid.Parse("31f614f0-938a-4d0f-8945-ec288558e420"), Row = "B", SeatNumber = "4", IsAvailable = false, EventId = eventId, SectionId = sectionAId, Status = SeatStatus.Available }
            };

            for (int i = 1; i < 9; i++)
            {
                seats.Add(new Seat
                {
                    Id = Guid.Parse($"00000000-0000-0000-0000-00000000000{i}"),
                    Row = "A",
                    SeatNumber = i.ToString(),
                    IsAvailable = true,
                    EventId = eventId,
                    SectionId = sectionAId,
                    Status = SeatStatus.Available
                });
            }

            modelBuilder.Entity<Seat>().HasData(seats);

            // Users
            var userId1 = Guid.Parse("b0b79e20-0e5f-41b7-adc9-957847f06fe6");
            var userId2 = Guid.Parse("061734a3-57c6-443b-a454-bc442c6feb34");
            modelBuilder.Entity<User>().HasData(
                new User { Id = userId1, FirstName = "Jardani", LastName = "Jovonovich", Email = "Jardani.Jovonovich@example.com", PhoneNumber = "1234567890", DateOfBirth = new DateTime(1990, 1, 1) },
                new User { Id = userId2, FirstName = "Max", LastName = "Payne", Email = "Max.Payne@example.com", PhoneNumber = "0987654321", DateOfBirth = new DateTime(1992, 5, 15) }
            );

            // UserSeatReservations
            modelBuilder.Entity<UserSeatReservation>()
                .HasKey(usr => new { usr.UserId, usr.SeatId });

            modelBuilder.Entity<UserSeatReservation>().HasData(
                new UserSeatReservation
                {
                    UserId = userId1,
                    SeatId = Guid.Parse("a87980ba-e793-4d76-8b67-45c5273a2dde"),
                    ReservedAt = new DateTime(2025, 6, 1, 14, 30, 0),
                    ExpiresAt = new DateTime(2025, 6, 1, 16, 30, 0)
                },
                new UserSeatReservation
                {
                    UserId = userId2,
                    SeatId = Guid.Parse("31f614f0-938a-4d0f-8945-ec288558e420"),
                    ReservedAt = new DateTime(2025, 6, 2, 14, 30, 0),
                    ExpiresAt = new DateTime(2025, 6, 2, 16, 30, 0)
                }
            );

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);

            // Orders
            var orderId1 = Guid.Parse("9e5a8b14-42bf-4b1c-9242-3fc0f57d1738");
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = orderId1,
                    UserId = userId1,
                    Status = OrderStatus.Booked,
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0),
                    PaymentId = null,
                    EventId= eventId,
                    SeatId = Guid.Parse("6d762474-a6c5-49a1-8461-4a93b2fe4c82"),
                }
            );

            // Transactions
            var transactionId1 = Guid.Parse("982ec780-25b9-481d-bbc5-bd5075ff5b7e");
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = transactionId1,
                    OrderId = orderId1,
                    Status = "Pending",
                    Amount = 150.00m,
                    DateCreated = new DateTime(2025, 1, 1, 0, 0, 0)
                }
            );

            // Payments
            var paymentId1 = Guid.Parse("efb2fbdc-5eb8-4390-ac51-225c30ac0b36");
            modelBuilder.Entity<Payment>().HasData(
               new Payment
               {
                   Id = paymentId1,
                   OrderId = orderId1,
                   Status = PaymentStatus.Pending,
                   PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0)
               }
            );
        }
    }
}