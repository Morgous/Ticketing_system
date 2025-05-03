using System.Reflection;
using Azure.Identity;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Ticketing_system.DataAccessLayer;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<TicketingSystemDbContext>();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddRazorPages();

        // Customise default API behaviour
        builder.Services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "Ticketing API";
        });
    }

    //public static void AddKeyVaultIfConfigured(this IHostApplicationBuilder builder)
    //{
    //    var keyVaultUri = builder.Configuration["AZURE_KEY_VAULT_ENDPOINT"];
    //    if (!string.IsNullOrWhiteSpace(keyVaultUri))
    //    {
    //        builder.Configuration.AddAzureKeyVault(
    //            new Uri(keyVaultUri),
    //            new DefaultAzureCredential());
    //    }
    //}
}
