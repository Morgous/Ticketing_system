using BusinessLayer.Objects;

namespace BusinessLayer.Interfaces
{
    public interface IVenueService
    {
        bool CreateVenue(Venue venue);

        Task<IEnumerable<Venue>> GetAllVenuesAsync();
    }
}
