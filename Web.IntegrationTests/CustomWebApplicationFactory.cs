using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Domain.Models;
using TicketingSystem.Infrastructure.Context;

namespace Web.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment("Test");

		builder.ConfigureServices(services =>
		{
			var descriptor = services.SingleOrDefault(
				d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
			if (descriptor != null)
				services.Remove(descriptor);

			services.AddDbContext<AppDbContext>(options =>
				options.UseInMemoryDatabase("TestDb"));

			var sp = services.BuildServiceProvider();
			using var scope = sp.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			db.Database.EnsureCreated();
			FillTestData(db);
		});
	}

	private void FillTestData(AppDbContext dbContext)
	{
		var eventId = Guid.NewGuid();
		var venueId = Guid.NewGuid();
		var sectionId = Guid.NewGuid();
		var seatId = Guid.NewGuid();
		var orderId = Guid.NewGuid();
		var paymentId = Guid.NewGuid();
		var transactionId = Guid.NewGuid();
		var userId = Guid.NewGuid();

		// User
		var user = new User
		{
			Id = userId,
			FirstName = "John",
			LastName = "Wick",
			Email = "John.Wick@user.com",
			PhoneNumber = "82109840948",
			DateOfBirth = new DateTime(1990, 1, 1)
		};
		dbContext.Users.Add(user);

		// Venue
		var venue = new Venue
		{
			Id = venueId,
			Name = "Some Venue",
			Address = "Some Street"
		};
		dbContext.Venues.Add(venue);

		// Section
		var section = new Section
		{
			Id = sectionId,
			Name = "Some Section",
			VenueId = venueId
		};
		dbContext.Sections.Add(section);

		// Event
		var @event = new Event
		{
			Id = eventId,
			Title = "Awesome Event",
			Location = "Some Venue",
			EventDate = new DateTime(2025, 12, 25)
		};
		dbContext.Events.Add(@event);

		// Seat
		var seat = new Seat
		{
			Id = seatId,
			Row = "A",
			SeatNumber = "1",
			IsAvailable = true,
			EventId = eventId,
			SectionId = sectionId,
			Status = SeatStatus.Available
		};
		dbContext.Seats.Add(seat);

		// User Seat Reservation
		var reservation = new UserSeatReservation
		{
			UserId = userId,
			SeatId = seatId,
			ReservedAt = DateTime.Now,
			ExpiresAt = DateTime.Now.AddHours(4)
		};
		dbContext.UserSeatReservations.Add(reservation);

		// Order
		var order = new Order
		{
			Id = orderId,
			CartId = Guid.Parse("4b7e0735-71cd-4c2e-83b5-d495a21655a7"),
			UserId = userId,
			EventId = eventId,
			SeatId = seatId,
			Status = OrderStatus.Booked,
			CreatedAt = DateTime.Now
		};
		dbContext.Orders.Add(order);

		// Transaction
		var transaction = new Transaction
		{
			Id = transactionId,
			OrderId = orderId,
			Status = "Pending",
			Amount = 100.00m,
			DateCreated = DateTime.Now
		};
		dbContext.Transactions.Add(transaction);

		// Payment
		var payment = new Payment
		{
			Id = paymentId,
			OrderId = orderId,
			Status = PaymentStatus.Pending,
			PaymentDate = DateTime.Now
		};
		dbContext.Payments.Add(payment);

		dbContext.SaveChanges();
	}
}
