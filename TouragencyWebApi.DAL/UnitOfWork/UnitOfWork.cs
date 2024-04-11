﻿using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Repositories;

namespace TouragencyWebApi.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TouragencyContext _context;
        private IClientRepository _clients;
        private IEmailRepository _emails;
        private IPhoneRepository _phones;
        private IPersonRepository _persons;
        private ICountriesRepository _countries;
        private ISettlementsRepository _settlements;
        private ITourStateRepository _statuses;
        private ITourNameRepository _tourNames;
        private ITourRepository _tours;
        private IPositionRepository _positions;
        private IReviewRepository _reviews;
        private IReviewImageRepository _reviewImages;
        private ITransportTypeRepository _transportTypes;
        private IBookingRepository _bookings;
        private IBookingDataRepository _bookingDatas;
        private IBedConfigurationRepository _bedConfigurations;
        private IBookingChildrenRepository _bookingChildrens;

        public UnitOfWork(TouragencyContext context)
        {
            _context = context;
        }
        public IBookingChildrenRepository BookingChildrens
        {
            get
            {
                if (_bookingChildrens == null)
                    _bookingChildrens = new BookingChildrenRepository(_context);
                return _bookingChildrens;
            }
        }
        public IBookingDataRepository BookingDatas
        {
            get
            {
                if (_bookingDatas == null)
                    _bookingDatas = new BookingDataRepository(_context);
                return _bookingDatas;
            }
        }
        public IBookingRepository Bookings
        {
            get
            {
                if (_bookings == null)
                    _bookings = new BookingRepository(_context);
                return _bookings;
            }
        }
        public IBedConfigurationRepository BedConfigurations
        {
            get
            {
                if (_bedConfigurations == null)
                    _bedConfigurations = new BedConfigurationRepository(_context);
                return _bedConfigurations;
            }
        }
        public ITransportTypeRepository TransportTypes
        {
            get
            {
                if (_transportTypes == null)
                    _transportTypes = new TransportTypeRepository(_context);
                return _transportTypes;
            }
        }
        public IReviewImageRepository ReviewImages
        {
            get
            {
                if (_reviewImages == null)
                    _reviewImages = new ReviewImageRepository(_context);
                return _reviewImages;
            }
        }
        public IPositionRepository Positions
        {
            get
            {
                if (_positions == null)
                    _positions = new PositionRepository(_context);
                return _positions;
            }
        }
        public ITourNameRepository TourNames
        {
            get
            {
                if (_tourNames == null)
                    _tourNames = new TourNameRepository(_context);
                return _tourNames;
            }
        }
        public ITourRepository Tours
        {
            get
            {
                if (_tours == null)
                    _tours = new TourRepository(_context);
                return _tours;
            }
        }
        public IPersonRepository Persons
        {
            get
            {
                if (_persons == null)
                    _persons = new PersonRepository(_context);
                return _persons;
            }
        }
        public IClientRepository Clients
        {
            get
            {
                if (_clients == null)
                    _clients = new ClientRepository(_context);
                return _clients;
            }
        }
        public IEmailRepository Emails
        {
            get
            {
                if (_emails == null)
                    _emails = new EmailRepository(_context);
                return _emails;
            }
        }
        public IPhoneRepository Phones
        {
            get
            {
                if (_phones == null)
                    _phones = new PhoneRepository(_context);
                return _phones;
            }
        }
        public ICountriesRepository Countries
        {
            get
            {
                if (_countries == null)
                    _countries = new CountriesRepository(_context);
                return _countries;
            }
        }
        public ISettlementsRepository Settlements
        {
            get
            {
                if (_settlements == null)
                    _settlements = new SettlementsRepository(_context);
                return _settlements;
            }
        }
        public ITourStateRepository TourStates
        {
            get
            {
                if (_statuses == null)
                    _statuses = new TourStateRepository(_context);
                return _statuses;
            }
        }
        public IReviewRepository Reviews
        {
            get
            {
                if (_reviews == null)
                    _reviews = new ReviewRepository(_context);
                return _reviews;
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
