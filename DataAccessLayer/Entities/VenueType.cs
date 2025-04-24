using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class VenueType : BaseEntity
    {
        public VenueType()
        {
            Venues = new HashSet<Venue>();   
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int SeatsTypeId { get; set; }
        
        [ForeignKey("SeatsTypeId")]
        public virtual SeatsType SeatsType { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }
    }
}
