using Ticketing_system.DomainLayer.Entities;

namespace Ticketing_System.BusinessLayer.Prices.Queries.GetPrices;

public class PriceDto
{
    public required int Id { get; set; }

    public required decimal AmountToPay { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Price, PriceDto>();
        }
    }
}
