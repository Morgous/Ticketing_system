using Ticketing_system.BusinessLayer.Enums;

namespace Ticketing_system.BusinessLayer.Entities
{
    public class Event
    {
        int Id { get; set; }

        string Name { get; set; }

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        EventStatus EventStatus { get; set; }
    }
}
