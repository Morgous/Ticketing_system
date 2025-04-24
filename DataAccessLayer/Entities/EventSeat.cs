using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class EventSeat
    {
        [Key]
        [Column(Order = 0)]
        public int SeatId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int EventId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("SeatId")]
        public virtual VenueSeat Seat { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        [ForeignKey("StatusId")]
        public virtual EventSeatStatus Status { get; set; }
    }
}
