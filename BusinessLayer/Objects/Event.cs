using BusinessLayer.Enums;

namespace BusinessLayer.Objects
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
