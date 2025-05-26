using TicketingSystem.Repositories.Interfaces;

namespace TicketingSystem.Application.Services
{
    public class VenueService
    {
        private readonly IRepository<Venue> _venueRepository;

        public VenueService(IRepository<Venue> venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<IEnumerable<Venue>> GetAllVenuesAsync()
        {
            return await _venueRepository.GetAllAsync();
        }

        public async Task<Venue> GetVenueByIdAsync(Guid id)
        {
            return await _venueRepository.GetByIdAsync(id);
        }

        public async Task AddVenueAsync(Venue newVenue)
        {
            await _venueRepository.AddAsync(newVenue);
        }

        public async Task UpdateVenueAsync(Venue updatedVenue)
        {
            await _venueRepository.UpdateAsync(updatedVenue);
        }

        public async Task DeleteVenueAsync(Guid id)
        {
            await _venueRepository.DeleteAsync(id);
        }
    }
}