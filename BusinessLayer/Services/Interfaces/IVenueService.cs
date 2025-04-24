﻿using BusinessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IVenueService
    {
        bool CreateVenue(Venue venue);

        Task<IEnumerable<Venue>> GetAllVenuesAsync();
    }
}
