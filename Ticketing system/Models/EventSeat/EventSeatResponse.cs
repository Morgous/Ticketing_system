using AutoMapper;
using System.Text.Json.Serialization;
using Ticketing_system.DomainLayer.Enums;
using Ticketing_System.BusinessLayer.Events.Queries.GetSeatsOnEventBySection;

namespace Ticketing_system.Models.EventSeat;

public class EventSeatResponse
{
    public required int Id { get; set; }

    public required int SectionId { get; set; }

    public required int RowId { get; set; }

    public required int PriceId { get; set; }

    public required int SeatId { get; set; }

    public required string SeatName { get; set; }

    public required decimal AmountToPay { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required EventSeatState State { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<EventSeatDto, EventSeatResponse>(MemberList.Destination);
        }
    }
}
