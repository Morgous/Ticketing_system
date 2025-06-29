using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using TicketingService.Application.Data;
using TicketingSystem.Infrastructure.Context;
using Web.IntegrationTests;
using Xunit;
using Assert = Xunit.Assert;

namespace TicketingSystem.IntegrationTests
{
	public class OrderTests : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly WebApplicationFactory<Program> _factory;
		private readonly HttpClient _client;

		public class TestDbContext : AppDbContext
		{
			public TestDbContext(DbContextOptions<AppDbContext> options)
				: base(options) { }

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				base.OnModelCreating(modelBuilder);
			}
		}

		public OrderTests(CustomWebApplicationFactory factory)
		{
			_factory = factory.WithWebHostBuilder(builder =>
			{
				builder.ConfigureServices(services =>
				{
					var descriptor = services.SingleOrDefault(
						d => d.ServiceType == typeof(DbContextOptions<TestDbContext>));
					if (descriptor != null)
					{
						services.Remove(descriptor);
					}

					services.AddDbContext<TestDbContext>(options =>
					{
						options.UseInMemoryDatabase("TestDatabase");
					});

					var sp = services.BuildServiceProvider();
					using var scope = sp.CreateScope();
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<TestDbContext>();
				});
			});

			_client = _factory.CreateClient();
		}

		[Fact]
		public async Task GetCartItem_CartExist_ReturnsCartItem()
		{
			// Arrange
			var cartId = "00000000-0000-0000-0000-000000000000";

			// Act
			var response = await _client.GetAsync($"api/Order/carts/{cartId}");

			// Assert
			response.EnsureSuccessStatusCode();
			var cart = await response.Content.ReadFromJsonAsync<List<Order>>();
			Assert.NotNull(cart);
			Assert.Equal(Guid.Parse(cartId), cart.First().CartId);
		}

		[Fact]
		public async Task GetCart_UseInvalidId_ThrowsBadRequest()
		{
			// Arrange
			var invalidCartId = "something";

			// Act
			var response = await _client.GetAsync($"api/Order/carts/{invalidCartId}");

			// Assert
			Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task AddToCart_WithInvalidId_ThrowsBadRequest()
		{
			// Arrange
			var cartId = "something";
			var invalidCartItem = new CreateOrderDto
			{
				EventId = Guid.NewGuid(),
				SeatId = Guid.NewGuid(),
				PriceId = Guid.NewGuid()
			};

			// Act
			var response = await _client.PostAsJsonAsync($"api/Order/carts/{cartId}", invalidCartItem);

			// Assert
			Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task BookCart_CartExist_ReturnsPayment()
		{
			// Arrange
			var cartId = "4b7e0735-71cd-4c2e-83b5-d495a21655a7";

			// Act
			var response = await _client.PutAsync($"api/Order/carts/{cartId}/book", null);

			// Assert
			response.EnsureSuccessStatusCode();
			var paymentId = await response.Content.ReadFromJsonAsync<Guid>();
			Assert.NotEqual(Guid.Empty, paymentId);
		}

		[Fact]
		public async Task BookCart_CartIsEmpty_ThrowsNotFound()
		{
			// Arrange
			var emptyCartId = Guid.NewGuid().ToString();

			// Act
			var response = await _client.PutAsync($"api/Order/carts/{emptyCartId}/book", null);

			// Assert
			Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}
