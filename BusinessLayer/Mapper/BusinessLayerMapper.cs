using AutoMapper;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mapper
{
    public class BusinessLayerMapper : Profile
    {
        public BusinessLayerMapper()
        {
            CreateMap<Entities.Event, Event>();
            CreateMap<Event, Entities.Event>();

            CreateMap<Entities.Venue, Venue>();
            CreateMap<Venue, Entities.Venue>();
        }
    }
}
