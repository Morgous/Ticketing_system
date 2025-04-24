using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Objects;
using DataAccessLayer.UnitOfWork.Interfaces;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
    public class VenueService : IVenueService
    {
        private readonly ILogger<VenueService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public VenueService(ILogger<VenueService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public bool CreateVenue(Venue venue)
        {
            var result = false;
            try
            {
                if (venue is null)
                {
                    throw new ArgumentNullException($"Invalid venue.{venue}");
                }
                _unitOfWork.BeginTransaction();

                _unitOfWork.CommitTransaction();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not create venue - {ex.Message}");
                _unitOfWork.RollbackTransaction();
            }

            return result;
        }

        public async Task<IEnumerable<Venue>> GetAllVenuesAsync()
        {
            try
            {
                var result = await _unitOfWork.VenueRepository.GetAllAsync().ConfigureAwait(false);
                if (result != null && result.Any())
                {
                    return result.Select(x => _mapper.Map<Venue>(x));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get Venues - {ex.Message}.");
            }

            return Enumerable.Empty<Venue>();
        }
    }
}
