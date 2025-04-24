using BusinessLayer.Interfaces;
using BusinessLayer.Mapper;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using DataAccessLayer.UnitOfWork;
using DataAccessLayer.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ticketing_System.MappingProfiles;

namespace Ticketing_System
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ticketing_System", Version = "v1" });
            });

            services.AddDbContext<TicketingSystemDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<IEventRepository, EventRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(ModelsMapper), typeof(BusinessLayerMapper));
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IVenueService, VenueService>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketingSystem"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TicketingSystemDbContext>();
                dbContext.Database.Migrate();
                dbContext.Database.EnsureCreated();
                CreateData(dbContext);
            }
        }

        private static void CreateData(TicketingSystemDbContext dbContext)
        {
            PopulateReferenceData(dbContext);
            GenerateSampleEvents(dbContext);
            SetupVenuesAndLayout(dbContext);

            dbContext.SaveChanges();
        }

        private static void PopulateReferenceData(TicketingSystemDbContext db)
        {
            AddEntities(db.EventStatuses, ["Created", "Approved", "Ended"], name => new EventStatus { Name = name });
            AddEntities(db.VenueTypes, ["VenueOne", "VenueTwo"], name => new VenueType { Name = name });
            AddEntities(db.EventSeatStatuses, ["Available", "Booked", "Assigned"], name => new EventSeatStatus { Name = name });
            AddEntities(db.SeatsTypes, ["Regular", "Discounted", "Premium"], name => new SeatsType { Name = name });
        
            db.SaveChanges();
        }

        private static void AddEntities<T>(DbSet<T> dbSet, IEnumerable<string> names, Func<string, T> creator) where T : class
        {
            var entities = names.Select(creator).ToList();
            dbSet.AddRange(entities);
        }

        private static void GenerateSampleEvents(TicketingSystemDbContext db)
        {
            var now = DateTime.Now;

            var events = new[]
            {
            new Event
            {
                Name = "Tournament",
                Description = "yes",
                StartDate = now,
                EndDate = now.AddDays(1),
                SetupTime = 2400,
                TeardownTime = 2400,
                StatusId = 1
            },
            new Event
            {
                Name = "Another tournament",
                Description = "no",
                StartDate = now.AddMonths(1),
                EndDate = now.AddMonths(1).AddHours(1),
                SetupTime = 4800,
                TeardownTime = 4800,
                StatusId = 2
            },
            new Event
            {
                Name = "Concert",
                Description = "maybe",
                StartDate = now.AddDays(2),
                EndDate = now.AddDays(3).AddHours(2),
                SetupTime = 4800,
                TeardownTime = 4800,
                StatusId = 3
            }
            };
            db.Events.AddRange(events);
        }

        private static void SetupVenuesAndLayout(TicketingSystemDbContext db)
        {
            var venues = new List<Venue>
            {
                new Venue { Name = "VenueOne", VenueTypeId = 1, TotalNumberOfSeats = 100 },
                new Venue { Name = "VenueTwo", VenueTypeId = 2, TotalNumberOfSeats = 1200 }
            };

            db.Venues.AddRange(venues);
            db.SaveChanges();

            var firstVenue = db.Venues.FirstOrDefault(v => v.VenueTypeId == 1);
            if (firstVenue != null)
            {
                BuildVenueLayout(firstVenue, db);
            }
        }

        private static void BuildVenueLayout(Venue venue, TicketingSystemDbContext db)
        {
            int seatCounter = 0;

            for (int section = 0; section < 36; section++)
            {
                var newSection = new VenueSection
                {
                    VenueId = venue.Id,
                    Name = $"Section {section}"
                };
                db.VenueSections.Add(newSection);
                db.SaveChanges();

                for (int row = 0; row < 50; row++)
                {
                    var newRow = new VenueRow
                    {
                        SectionId = newSection.Id,
                        Name = $"Row {row}"
                    };
                    db.VenueRows.Add(newRow);
                    db.SaveChanges();

                    for (int seat = 0; seat <= 46; seat++)
                    {
                        db.VenueSeats.Add(new VenueSeat
                        {
                            RowId = newRow.Id,
                            Number = seat,
                            SeatsTypeId = 1
                        });
                        seatCounter++;
                    }
                }
            }

            venue.TotalNumberOfSeats = seatCounter;
            db.SaveChanges();
        }
    }
}
