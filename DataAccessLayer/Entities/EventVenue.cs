using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class EventVenue
    {
        public int EventId { get; set; }

        public int VenueId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        [ForeignKey("VenueId")]
        public virtual Venue Venue { get; set; }
    }
}
