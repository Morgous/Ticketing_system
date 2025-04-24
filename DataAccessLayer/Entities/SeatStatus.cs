using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class SeatStatus : BaseEntity
    {
        public SeatStatus() => VenueSeats = new HashSet<VenueSeat>();

        [Required]
        public string Name { get; set; }

        public virtual ICollection<VenueSeat> VenueSeats { get; set; }
    }
}
