using AutoMapper;
using TicketingService.Application.Data;
using TicketingSystem.Application.Data;
using TicketingSystem.Domain.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<Seat, SeatDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Transaction, TransactionDto>().ReverseMap();
        CreateMap<Order, CreateOrderDto>().ReverseMap();
        CreateMap<Order, UpdateOrderDto>().ReverseMap();
        CreateMap<CreateEventDto, Event>();
        CreateMap<CreateSeatDto, Seat>();
    }
}
