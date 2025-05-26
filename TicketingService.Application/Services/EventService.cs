using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TicketingService.Application.Interfaces;
using TicketingSystem.Repositories.Interfaces;

namespace TicketingService.Application.Services
{
    public class EventService
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<Seat> _seatRepository;
        private readonly ICacheService _cacheService;

        public EventService(IRepository<Event> eventRepository, IRepository<Seat> seatRepository, ICacheService cache)
        {
            _eventRepository = eventRepository;
            _seatRepository = seatRepository;
            _cacheService = cache;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            var cacheKey = "events";
            string cachedEvents = null;

			try
            {
				cachedEvents = await _cacheService.GetStringAsync(cacheKey);
			}
            catch(Exception ex)
            {
                Console.WriteLine("Oh no");
			}

			IEnumerable<Event> events;

            if (string.IsNullOrEmpty(cachedEvents))
            {
                events = await _eventRepository.GetAllAsync();
                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));
                //await _cacheService.SetStringAsync(cacheKey, JsonConvert.SerializeObject(events), cacheOptions);
            }
            else
            {
                events = JsonConvert.DeserializeObject<IEnumerable<Event>>(cachedEvents);
            }

            return events;
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            var cacheKey = $"Event_{id}";
            var cachedEvent = await _cacheService.GetStringAsync(cacheKey);
            Event eventItem;

            if (string.IsNullOrEmpty(cachedEvent))
            {
                eventItem = await _eventRepository.GetByIdAsync(id);
                if (eventItem != null)
                {
                    var cacheOptions = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30));
                    await _cacheService.SetStringAsync(cacheKey, JsonConvert.SerializeObject(eventItem), cacheOptions);
                }
            }
            else
            {
                eventItem = JsonConvert.DeserializeObject<Event>(cachedEvent);
            }

            return eventItem;
        }

        public async Task AddEventAsync(Event newEvent)
        {
            await _eventRepository.AddAsync(newEvent);
            await InvalidateCache(newEvent.Id);
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            await _eventRepository.UpdateAsync(updatedEvent);
            await InvalidateCache(updatedEvent.Id);
        }

        public async Task DeleteEventAsync(Guid id)
        {
            await _eventRepository.DeleteAsync(id);
            await InvalidateCache(id);
        }

        public async Task<IEnumerable<Seat>> GetSeatsByEventAndSectionAsync(Guid eventId, Guid sectionId)
        {
            return await _seatRepository.FindAsync(seat => seat.EventId == eventId && seat.SectionId == sectionId);
        }

        private async Task InvalidateCache(Guid eventId)
        {
            await _cacheService.RemoveAsync($"Event_{eventId}");
        }
    }
}