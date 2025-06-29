using Microsoft.EntityFrameworkCore;
using TicketingService.Application.Interfaces;
using TicketingService.Application.Services;
using TicketingSystem.Application.Services;
using TicketingSystem.Infrastructure;
using TicketingSystem.Infrastructure.Context;
using TicketingSystem.Repositories.Implementations;
using TicketingSystem.Repositories.Interfaces;

public class Program
{
	public static void Main(string[] args)
	{
		var app = CreateApp(args);
		if (!app.Environment.IsEnvironment("Test"))
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetRequiredService<AppDbContext>();
				dbContext.Database.Migrate();
			}
		}
		else
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				var dbContext = services.GetRequiredService<AppDbContext>();
				dbContext.Database.EnsureCreated();
			}
		}

		app.Run();
	}

	public static WebApplication CreateApp(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);


		builder.Configuration
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddUserSecrets<Program>()
			.AddEnvironmentVariables();
		builder.Services.AddDbContext<AppDbContext>(options =>
		{
			if (!builder.Environment.IsEnvironment("Test"))
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			}
		});

		builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
		builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

		builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		builder.Services.AddScoped<IUserRepository, UserRepository>();
		builder.Services.AddScoped<IOrderRepository, OrderRepository>();
		builder.Services.AddScoped<ISeatRepository, SeatRepository>();

		builder.Services.AddScoped<UserService>();
		builder.Services.AddScoped<EventService>();
		builder.Services.AddScoped<VenueService>();
		builder.Services.AddScoped<OrderService>();
		builder.Services.AddScoped<PaymentService>();

		builder.Services.AddSingleton<ICacheService, CacheService>();

		builder.Services.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
			options.InstanceName = "SomeInstance";
		});

		builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.MapControllers();

		return app;
	}
}
