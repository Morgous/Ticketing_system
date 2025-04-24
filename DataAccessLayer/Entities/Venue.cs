namespace DataAccessLayer.Entities
{
    public class Venue : BaseEntity
    {
        public string Name { get; set; }

        public int VenueTypeId { get; set; }

        public int TotalNumberOfSeats { get; set; }

        public virtual ICollection<EventVenue> EventVenues { get; set; }

        public virtual ICollection<VenueSection> VenueSections { get; set; }

        public virtual VenueType VenueType { get; set; }
    }
}
