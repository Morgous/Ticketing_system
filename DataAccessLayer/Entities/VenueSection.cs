using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class VenueSection : BaseEntity
    {
        public VenueSection ()
        {
            VenueRows = new HashSet<VenueRow>();
        }

        [Required]
        public int VenueId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Venue Venue { get; set; }

        public virtual ICollection<VenueRow> VenueRows { get; set; }
    }
}
