using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TicketingService.Application.Data;
using TicketingService.Application.Interfaces;
using TicketingService.Application.Services;

namespace TicketingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;

        public EventController(EventService eventService, ICacheService cache, IMapper mapper)
        {
            _eventService = eventService;
            _cache = cache;
            _mapper = mapper;
        }

        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]

        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var cacheKey = $"Event_{id}";
            var cachedEvent = await _cache.GetStringAsync(cacheKey);
            Event eventItem;

            if (string.IsNullOrEmpty(cachedEvent))
            {
                eventItem = await _eventService.GetEventByIdAsync(id);
                if (eventItem == null)
                {
                    return NotFound();
                }

                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(eventItem), cacheOptions);
            }
            else
            {
                eventItem = JsonConvert.DeserializeObject<Event>(cachedEvent);
            }

            return Ok(eventItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto newEventDTO)
        {
            var newEvent = _mapper.Map<Event>(newEventDTO);
            await _eventService.AddEventAsync(newEvent);
            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] Event updatedEventDTO)
        {
            if (id != updatedEventDTO.Id)
            {
                return BadRequest();
            }

            //var updatedEvent = _mapper.Map<Event>(updatedEventDTO);
            await _eventService.UpdateEventAsync(updatedEventDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }

        [HttpGet("{eventId}/sections/{sectionId}/seats")]
        public async Task<IActionResult> GetSeatsByEventAndSection(Guid eventId, Guid sectionId)
        {
            var seats = await _eventService.GetSeatsByEventAndSectionAsync(eventId, sectionId);
            if (seats == null || !seats.Any())
            {
                return NotFound();
            }
            return Ok(seats);
        }
    }
}