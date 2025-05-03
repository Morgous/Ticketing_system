using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ticketing_system.DataAccessLayer.Data;
using Ticketing_system.DataAccessLayer.Data.Interceptors;
using Ticketing_System.BusinessLayer.Common.Interfaces;

namespace Ticketing_system.DataAccessLayer;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionStringTicket = builder.Configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionStringTicket, message: "Connection string 'DefaultConnection' not found.");

        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddDbContext<TicketingDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionStringTicket);
        });

        builder.Services.AddScoped<ITicketingDbContext>(provider => provider.GetRequiredService<TicketingDbContext>());

        builder.Services.AddSingleton(TimeProvider.System);
    }
}
