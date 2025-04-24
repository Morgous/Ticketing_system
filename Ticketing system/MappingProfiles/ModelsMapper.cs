using AutoMapper;
using Ticketing_System.Models;
using BusinessLayer.Entities;

namespace Ticketing_System.MappingProfiles
{
    public class ModelsMapper : Profile
    {
        public ModelsMapper()
        { 
            CreateMap<EventModel, Event>();
            CreateMap<Event, EventModel>();

            CreateMap<Event, EventModel>();
            CreateMap<Event, EventModel>();
        }
    }
}
