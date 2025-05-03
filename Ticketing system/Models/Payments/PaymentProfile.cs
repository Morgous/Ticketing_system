using AutoMapper;
using Ticketing_system.DomainLayer.Enums;

namespace Ticketing_system.Models.Payments;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<PaymentState, PaymentStateResponse>(MemberList.Destination);
    }
}
