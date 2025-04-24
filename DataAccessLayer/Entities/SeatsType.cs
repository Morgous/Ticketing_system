using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class SeatsType : BaseEntity
    {
        public SeatsType()
        {
            VenueTypes = new HashSet<VenueType>();
            VenueSeats = new HashSet<VenueSeat>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<VenueType> VenueTypes { get; set; }

        public virtual ICollection<VenueSeat> VenueSeats { get; set; }
    }
}
