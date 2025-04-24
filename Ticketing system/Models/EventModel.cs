using System.ComponentModel.DataAnnotations;

namespace Ticketing_System.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        
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
    }
}
