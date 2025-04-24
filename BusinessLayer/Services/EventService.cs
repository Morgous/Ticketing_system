using AutoMapper;
using BusinessLayer.Entities;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(ILogger<EventService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public bool CreateEvent(Event obj)
        {
            var result = false;
            try
            {
                if (obj is null)
                {
                    throw new ArgumentNullException($"Invalid event.{obj}");
                }
                _unitOfWork.BeginTransaction();

                _unitOfWork.CommitTransaction();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not create event - {ex.Message}");
                _unitOfWork.RollbackTransaction();
            }

            return result;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            try
            {
                var result = await _unitOfWork.EventRepository.GetAllAsync().ConfigureAwait(false);
                if (result != null && result.Any())
                {
                    return result.Select(x => _mapper.Map<Event>(x));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get Events - {ex.Message}.");
            }
            
            return Enumerable.Empty<Event>(); 
        }
    }
}
