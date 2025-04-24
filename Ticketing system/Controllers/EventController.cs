using AutoMapper;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ticketing_System.Models;

namespace Ticketing_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public EventController(ILogger<EventController> logger, IMapper mapper, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EventModel>> GetAllEventsAsync()
        {
            try
            {
                var events = await _eventService.GetAllEventsAsync().ConfigureAwait(false);
                
                return events.Select(e => _mapper.Map<EventModel>(e));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}.");
                return Enumerable.Empty<EventModel>();
            }
        }
    }
}
