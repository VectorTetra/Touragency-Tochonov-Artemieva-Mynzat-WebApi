using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class HotelService : IHotelService
    {
        IUnitOfWork Database;
        public HotelService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration Hotel_HotelDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("Description", opt => opt.MapFrom(c => c.Description))
        .ForMember("Stars", opt => opt.MapFrom(c => c.Stars))
        .ForPath(d => d.HotelConfigurationIds, opt => opt.MapFrom(c => c.HotelConfigurations.Select(b => b.Id)))
        .ForPath(d => d.BedConfigurationIds, opt => opt.MapFrom(c => c.BedConfigurations.Select(b => b.Id)))
        .ForPath(d => d.SettlementId, opt => opt.MapFrom(c => c.Settlement.Id))
        .ForPath(d => d.TourIds, opt => opt.MapFrom(c => c.Tours.Select(b => b.Id)))
        .ForPath(d => d.BookingIds, opt => opt.MapFrom(c => c.Bookings.Select(b => b.Id)))
        .ForPath(d => d.HotelServiceIds, opt => opt.MapFrom(c => c.HotelServices.Select(b => b.Id)))
        .ForPath(d => d.HotelImageIds, opt => opt.MapFrom(c => c.HotelImages.Select(b => b.Id)))
        );
        public async Task<IEnumerable<HotelDTO>> GetAll()
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetAll());
        }

        public async Task<HotelDTO?> GetById(int id)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<Hotel, HotelDTO>(await Database.Hotels.GetById(id));
        }

        public async Task<IEnumerable<HotelDTO>> GetByNameSubstring(string nameSubstring)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByNameSubstring(nameSubstring));
        }

        public async Task<IEnumerable<HotelDTO>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByDescriptionSubstring(descriptionSubstring));
        }

        public async Task<IEnumerable<HotelDTO>> GetByStars(int[] selectedStarsRatings)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByStars(selectedStarsRatings));
        }

        public async Task<IEnumerable<HotelDTO>> GetByHotelConfigurationId(int hotelConfigurationId)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByHotelConfigurationId(hotelConfigurationId));
        }

        public async Task<IEnumerable<HotelDTO>> GetByBedConfigurationId(int bedConfigurationId)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByBedConfigurationId(bedConfigurationId));
        }

        public async Task<IEnumerable<HotelDTO>> GetBySettlementId(int settlementId)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetBySettlementId(settlementId));
        }

        public async Task<IEnumerable<HotelDTO>> GetByTourId(long tourId)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByTourId(tourId));
        }

        public async Task<IEnumerable<HotelDTO>> GetByBookingId(long bookingId)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByBookingId(bookingId));
        }

        public async Task<IEnumerable<HotelDTO>> GetByHotelServiceId(int hotelServiceId)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByHotelServiceId(hotelServiceId));
        }

        public async Task<IEnumerable<HotelDTO>> GetByHotelImageId(long hotelImageId)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByHotelImageId(hotelImageId));
        }

        public async Task<IEnumerable<HotelDTO>> GetByCompositeSearch(string? nameSubstring, string? descriptionSubstring, int[]? stars, int? hotelConfigurationId, int? bedConfigurationId, int? settlementId, long? tourId, long? bookingId, int? hotelServiceId, long? hotelImageId)
        {
            var mapper = new Mapper(Hotel_HotelDTOMapConfig);
            return mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(await Database.Hotels.GetByCompositeSearch(nameSubstring, descriptionSubstring, stars, hotelConfigurationId, bedConfigurationId, settlementId, tourId, bookingId, hotelServiceId, hotelImageId));
        }

        public async Task Create(HotelDTO hotelDTO)
        {
            var PreExistedHotelId = await Database.Hotels.GetById(hotelDTO.Id);
            if (PreExistedHotelId != null)
            {
                throw new ValidationException($"Готель з таким Id вже існує (Id : {PreExistedHotelId.Id})", "");
            }
            //======================================================================
            var HotelConfigurations = new List<HotelConfiguration>();
            foreach (var id in hotelDTO.HotelConfigurationIds)
            {
                var hotelConfiguration = await Database.HotelConfigurations.GetById(id);
                if (hotelConfiguration == null)
                {
                    throw new ValidationException($"Такої конфігурації готелю, вказаної в hotel.HotelConfigurationIds, не існує! (HotelConfigurationId : {id})", "");
                }
                HotelConfigurations.Add(hotelConfiguration);
            }
            //======================================================================
            var BedConfigurations = new List<BedConfiguration>();
            foreach (var id in hotelDTO.BedConfigurationIds)
            {
                var bedConfiguration = await Database.BedConfigurations.GetById(id);
                if (bedConfiguration == null)
                {
                    throw new ValidationException($"Такої конфігурації ліжка, вказаної в hotel.BedConfigurationIds, не існує! (BedConfigurationId : {id})", "");
                }
                BedConfigurations.Add(bedConfiguration);
            }
            //======================================================================
            var Settlement = await Database.Settlements.GetById(hotelDTO.SettlementId);
            if (Settlement == null)
            {
                throw new ValidationException($"Такого поселення, вказаного в hotel.SettlementId, не існує! (SettlementId : {hotelDTO.SettlementId})", "");
            }
            //======================================================================
            var Tours = new List<Tour>();
            foreach (var id in hotelDTO.TourIds)
            {
                var tour = await Database.Tours.GetById(id);
                if (tour == null)
                {
                    throw new ValidationException($"Такого туру, вказаного в hotel.TourIds, не існує! (TourId : {id})", "");
                }
                Tours.Add(tour);
            }
            //======================================================================
            var Bookings = new List<Booking>();
            foreach (var id in hotelDTO.BookingIds)
            {
                var booking = await Database.Bookings.GetById(id);
                if (booking == null)
                {
                    throw new ValidationException($"Такого бронювання, вказаного в hotel.BookingIds, не існує! (BookingId : {id})", "");
                }
                Bookings.Add(booking);
            }
            //======================================================================
            var HotelServices = new List<TouragencyWebApi.DAL.Entities.HotelService>();
            foreach (var id in hotelDTO.HotelServiceIds)
            {
                var hotelService = await Database.HotelServices.GetById(id);
                if (hotelService == null)
                {
                    throw new ValidationException($"Такої послуги готелю, вказаної в hotel.HotelServiceIds, не існує! (HotelServiceId : {id})", "");
                }
                HotelServices.Add(hotelService);
            }
            //======================================================================
            Hotel newHotel = new Hotel
            {
                Id = hotelDTO.Id,
                Name = hotelDTO.Name,
                Stars = hotelDTO.Stars,
                HotelConfigurations = HotelConfigurations,
                BedConfigurations = BedConfigurations,
                Settlement = Settlement,
                Tours = Tours,
                Bookings = Bookings,
                HotelServices = HotelServices
            };

            await Database.Hotels.Create(newHotel);
            await Database.Save();
        }

        public async Task Update(HotelDTO hotelDTO)
        {
            Hotel hotelEntity = await Database.Hotels.GetById(hotelDTO.Id);
            if (hotelEntity == null)
            {
                throw new ValidationException($"Готель з таким Id не знайдено (Id : {hotelDTO.Id})", "");
            }
            //======================================================================
            hotelEntity.HotelConfigurations.Clear();
            foreach (var id in hotelDTO.HotelConfigurationIds)
            {
                var hotelConfiguration = await Database.HotelConfigurations.GetById(id);
                if (hotelConfiguration == null)
                {
                    throw new ValidationException($"Такої конфігурації готелю, вказаної в hotel.HotelConfigurationIds, не існує! (HotelConfigurationId : {id})", "");
                }
                hotelEntity.HotelConfigurations.Add(hotelConfiguration);
            }
            //======================================================================
            hotelEntity.BedConfigurations.Clear();
            foreach (var id in hotelDTO.BedConfigurationIds)
            {
                var bedConfiguration = await Database.BedConfigurations.GetById(id);
                if (bedConfiguration == null)
                {
                    throw new ValidationException($"Такої конфігурації ліжка, вказаної в hotel.BedConfigurationIds, не існує! (BedConfigurationId : {id})", "");
                }
                hotelEntity.BedConfigurations.Add(bedConfiguration);
            }
            //======================================================================
            var Settlement = await Database.Settlements.GetById(hotelDTO.SettlementId);
            if (Settlement == null)
            {
                throw new ValidationException($"Такого поселення, вказаного в hotel.SettlementId, не існує! (SettlementId : {hotelDTO.SettlementId})", "");
            }
            //======================================================================
            hotelEntity.Tours.Clear();
            foreach (var id in hotelDTO.TourIds)
            {
                var tour = await Database.Tours.GetById(id);
                if (tour == null)
                {
                    throw new ValidationException($"Такого туру, вказаного в hotel.TourIds, не існує! (TourId : {id})", "");
                }
                hotelEntity.Tours.Add(tour);
            }
            //======================================================================
            hotelEntity.Bookings.Clear();
            foreach (var id in hotelDTO.BookingIds)
            {
                var booking = await Database.Bookings.GetById(id);
                if (booking == null)
                {
                    throw new ValidationException($"Такого бронювання, вказаного в hotel.BookingIds, не існує! (BookingId : {id})", "");
                }
                hotelEntity.Bookings.Add(booking);
            }
            //======================================================================
            hotelEntity.HotelServices.Clear();
            foreach (var id in hotelDTO.HotelServiceIds)
            {
                var hotelService = await Database.HotelServices.GetById(id);
                if (hotelService == null)
                {
                    throw new ValidationException($"Такої послуги готелю, вказаної в hotel.HotelServiceIds, не існує! (HotelServiceId : {id})", "");
                }
                hotelEntity.HotelServices.Add(hotelService);
            }
            //======================================================================
            hotelEntity.Name = hotelDTO.Name;
            hotelEntity.Stars = hotelDTO.Stars;
            hotelEntity.Settlement = Settlement;

            Database.Hotels.Update(hotelEntity);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await Database.Hotels.GetById(id);
            if (hotel == null)
            {
                throw new ValidationException("Готель не знайдено", "");
            }
            await Database.Hotels.Delete(id);
            await Database.Save();
        }
    }
}
