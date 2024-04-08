using AutoMapper;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class TourService : ITourService
    {
        IUnitOfWork Database;
        public TourService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration Tour_TourDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Tour, TourDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("ArrivalDate", opt => opt.MapFrom(c => c.ArrivalDate))
        .ForMember("DepartureDate", opt => opt.MapFrom(c => c.DepartureDate))
        .ForPath(d => d.TourNameId, opt => opt.MapFrom(c => c.Name.Id))
        .ForMember("IsHaveNightRides", opt => opt.MapFrom(c => c.IsHaveNightRides))
        .ForMember("NightRidesCount", opt => opt.MapFrom(c => c.NightRidesCount))
        .ForMember("FreeSeats", opt => opt.MapFrom(c => c.FreeSeats))
        .ForPath(d => d.TourStateId, opt => opt.MapFrom(c => c.TourState.Id))
        .ForMember("Route", opt => opt.MapFrom(c => c.Route))
        .ForPath(d => d.SettlementIds, opt => opt.MapFrom(c => c.Settlements.Select(s => s.Id)))
        .ForPath(d => d.HotelIds, opt => opt.MapFrom(c => c.Hotels.Select(h => h.Id)))
        .ForPath(d => d.ReviewIds, opt => opt.MapFrom(c => c.Reviews.Select(r => r.Id)))
        .ForPath(d => d.TransportTypeIds, opt => opt.MapFrom(c => c.TransportTypes.Select(t => t.Id)))
        .ForPath(d => d.BookingIds, opt => opt.MapFrom(c => c.Bookings.Select(b => b.Id)))
        .ForPath(d => d.ClientIds, opt => opt.MapFrom(c => c.Clients.Select(cl => cl.Id)))
        );
        public async Task<IEnumerable<TourDTO>> GetAll(TourDTO searchTourDTO)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tours = await Database.Tours.GetAll();
            var toursDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tours);
            return toursDTO;
        }
        public async Task<TourDTO?> GetById(long id)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tour = await Database.Tours.GetById(id);
            var tourDTO = mapper.Map<Tour, TourDTO>(tour);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByExtendedSearch(TourDTO searchTourDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<TourDTO>> GetByTourName(TourName tourName)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByTourName(tourName);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByTourNameId(int tourNameId)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByTourNameId(tourNameId);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByTourNameStringName(string tourNameSubstring)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByTourNameStringName(tourNameSubstring);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByCountry(Country country)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByCountry(country);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByCountryId(int countryid)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByCountryId(countryid);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByCountryName(string countryNameSubstring)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByCountryName(countryNameSubstring);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetBySettlement(Settlement settlement)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetBySettlement(settlement);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetBySettlementId(int settlementId)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetBySettlementId(settlementId);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetBySettlementName(string settlementNameSubstring)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetBySettlementName(settlementNameSubstring);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByHotel(Hotel hotel)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByHotel(hotel);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByHotelId(int hotelId)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByHotelId(hotelId);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByHotelName(string hotelNameSubstring)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByHotelName(hotelNameSubstring);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByTransportType(TransportType transportType)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByTransportType(transportType);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByTransportTypeId(int id)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByTransportTypeId(id);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByTransportTypeName(string transportTypeName)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByTransportTypeName(transportTypeName);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByDateRange(startDate, endDate);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByTourDuration(params int[] durationDays)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByTourDuration(durationDays);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }
        public async Task<IEnumerable<TourDTO>> GetByHotelServicesIds(params int[] hotelServicesIds)
        {
            var mapper = new Mapper(Tour_TourDTOMapConfig);
            var tourCollection = await Database.Tours.GetByHotelServicesIds(hotelServicesIds);
            var tourDTO = mapper.Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(tourCollection);
            return tourDTO;
        }

        // Для повної реалізації цього сервісу (TourService) потрібно додати створити репозиторій для:
        // ? Review,
        // ? TransportType,
        // ? Booking,
        // ? Hotel,
        // ---- Client,
        // ---- Settlement,
        // ---- TourState,
        // ---- TourName
        
        public async Task Create(TourDTO tourDTO)
        {
            //Намагаємось визначити, чи ще не існує тур з таким tourId
            var BusyTourId = await Database.Tours.GetById(tourDTO.Id);
            //Якщо такий tourId вже зайнято, кидаємо виключення
            if (BusyTourId != null)
            {
                throw new ValidationException("Такий tourId вже зайнято!", "");
            }
            //-----------------------------------------------------------------------------------------------------
            var SettlementCollection = new List<Settlement>();
            var ClientCollection = new List<Client>();
            var HotelCollection = new List<Hotel>();
            var ReviewCollection = new List<Review>();
            var TransportTypeCollection = new List<TransportType>();
            var BookingCollection = new List<Booking>();
            //-----------------------------------------------------------------------------------------------------

            foreach(var settlementId in tourDTO.SettlementIds)
            {
                var settlement = await Database.Settlements.GetById(settlementId);
                if (settlement == null)
                {
                    throw new ValidationException("Неможливо знайти поселення з таким settlementId!", "");
                }
                SettlementCollection.Add(settlement);
            }
            foreach(var clientId in tourDTO.ClientIds)
            {
                var client = await Database.Clients.GetByClientId(clientId);
                if (client == null)
                {
                    throw new ValidationException("Неможливо знайти клієнта з таким clientId!", "");
                }
                ClientCollection.Add(client);
            }
            //-----------------------------------------------------------------------------------------------------
            var tour = new Tour
            {
                Id = tourDTO.Id,
                ArrivalDate = tourDTO.ArrivalDate,
                DepartureDate = tourDTO.DepartureDate,
                Name = await Database.TourNames.GetById(tourDTO.TourNameId),
                IsHaveNightRides = tourDTO.IsHaveNightRides,
                NightRidesCount = tourDTO.NightRidesCount,
                FreeSeats = tourDTO.FreeSeats,
                TourState = await Database.TourStates.GetById(tourDTO.TourStateId),
                Route = tourDTO.Route,
                Settlements = SettlementCollection,
                Hotels = HotelCollection,
                Reviews = ReviewCollection,
                TransportTypes = TransportTypeCollection,
                Bookings = BookingCollection,
                Clients = ClientCollection
            };
            await Database.Tours.Create(tour);
            await Database.Save();
        }
        public async void Update(TourDTO tourDTO) 
        { 
            var tour = await Database.Tours.GetById(tourDTO.Id);
            if (tour == null)
            {
                throw new ValidationException("Тур не знайдено!", "");
            }
            //-----------------------------------------------------------------------------------------------------
            var SettlementCollection = new List<Settlement>();
            var ClientCollection = new List<Client>();
            var HotelCollection = new List<Hotel>();
            var ReviewCollection = new List<Review>();
            var TransportTypeCollection = new List<TransportType>();
            var BookingCollection = new List<Booking>();
            //-----------------------------------------------------------------------------------------------------
            tour.ArrivalDate = tourDTO.ArrivalDate;
            tour.DepartureDate = tourDTO.DepartureDate;
            tour.Name = await Database.TourNames.GetById(tourDTO.TourNameId);
            tour.IsHaveNightRides = tourDTO.IsHaveNightRides;
            tour.NightRidesCount = tourDTO.NightRidesCount;
            tour.FreeSeats = tourDTO.FreeSeats;
            tour.TourState = await Database.TourStates.GetById(tourDTO.TourStateId);
            tour.Route = tourDTO.Route;
            tour.Settlements = SettlementCollection;
            tour.Hotels = HotelCollection;
            tour.Reviews = ReviewCollection;
            tour.TransportTypes = TransportTypeCollection;
            tour.Bookings = BookingCollection;
            tour.Clients = ClientCollection;
            Database.Tours.Update(tour);
            await Database.Save();
        }
        public async Task Delete(int id) 
        {
            Tour tour = await Database.Tours.GetById(id);
            if (tour == null)
            {
                throw new ValidationException("Тур не знайдено!", "");
            }
            await Database.Tours.Delete(id);
            await Database.Save();
        }
    }
}