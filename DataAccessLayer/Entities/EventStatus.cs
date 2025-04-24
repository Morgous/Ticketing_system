using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class EventStatus : BaseEntity
    {
        public EventStatus()
        {
            Events = new HashSet<Event>();   
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
