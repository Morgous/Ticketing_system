using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class EventSeatStatus : BaseEntity
    {
        public EventSeatStatus()
        {
            EventSeats = new HashSet<EventSeat>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<EventSeat> EventSeats { get; set; }
    }
}
