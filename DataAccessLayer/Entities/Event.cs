using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Event : BaseEntity
    {
        public Event()
        {
            EventVenues = new HashSet<EventVenue>();
            EventSeats = new HashSet<EventSeat>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int SetupTime { get; set; }

        [Required]
        public int TeardownTime { get; set; }

        public string Description { get; set; }

        public virtual ICollection<EventVenue> EventVenues { get; set; }

        public virtual ICollection<EventSeat> EventSeats { get; set; }

        [ForeignKey("StatusId")]
        public virtual EventStatus Status { get; set; }
    }
}
