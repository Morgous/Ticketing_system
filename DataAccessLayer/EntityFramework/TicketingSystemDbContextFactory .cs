using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.EntityFramework
{
    public class TicketingSystemDbContextFactory : IDesignTimeDbContextFactory<TicketingSystemDbContext>
    {
        public TicketingSystemDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TicketingSystemDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new TicketingSystemDbContext(optionsBuilder.Options);
        }
    }
}
