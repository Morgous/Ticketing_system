﻿using BusinessLayer.Enums;

namespace BusinessLayer.Entities
{
    public class Venue
    {
        int Id { get; set; }

        string Name { get; set; }

        VenueType VenueType { get; set; }

        int TotalNumberOfSeats { get; set; }
    }
}
