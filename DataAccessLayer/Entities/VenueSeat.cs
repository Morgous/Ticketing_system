using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class VenueSeat : BaseEntity
    {
        public VenueSeat()
        {
            EventSeats = new HashSet<EventSeat>();
        }

        [Required]
        public int RowId { get; set; }

        [Required]
        public int Number { get; set; }

        public int SeatsTypeId { get; set; }

        [ForeignKey("RowId")]
        public virtual VenueRow Row { get; set; }

        [ForeignKey("SeatsTypeId")]
        public virtual SeatsType SeatsType { get; set; }

        public virtual ICollection<EventSeat> EventSeats { get; set; }
    }
}
