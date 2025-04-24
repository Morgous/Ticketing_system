﻿using System;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<EventStatus> EventStatusRepository { get; }
        IRepository<Venue> VenueRepository { get; }
        IRepository<VenueType> VenueTypeRepository { get; }
        IRepository<SeatsType> SeatsTypeRepository { get; }
        IRepository<VenueSection> VenueSectionRepository { get; }
        IRepository<VenueRow> VenueRowRepository { get; }
        IRepository<VenueSeat> VenueSeatRepository { get; }
        IRepository<EventSeat> EventSeatRepository { get; }
        IRepository<SeatStatus> SeatStatusRepository { get; }
        IRepository<EventVenue> EventVenueRepository { get; }
        IEventRepository EventRepository { get; }
        IVenueRepository VenueDetailRepository { get; }

        void Save();
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
