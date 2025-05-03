using AutoMapper;
using Ticketing_system.DataAccessLayer;
using Ticketing_system.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();
#if DEBUG
builder.Services.AddLogging(builder =>
{
	builder.AddConsole();
	builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information);
});
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.Services.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi(settings =>
{
	settings.Path = "/api";
	settings.DocumentPath = "/api/specification.json";
});

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.UseExceptionHandler(options => { });


app.MapEndpoints();

app.Run();

public partial class Program { }
