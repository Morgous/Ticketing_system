using AutoMapper;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mapper
{
    public class BusinessLayerMapper : Profile
    {
        public BusinessLayerMapper()
        {
            CreateMap<Objects.Event, Event>();
            CreateMap<Event, Objects.Event>();

            CreateMap<Objects.Venue, Venue>();
            CreateMap<Venue, Objects.Venue>();
        }
    }
}
