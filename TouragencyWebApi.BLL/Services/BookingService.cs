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
    public class BookingService : IBookingService
    {
        IUnitOfWork Database;
        public BookingService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration Booking_BookingDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForPath(d => d.ClientId, opt => opt.MapFrom(c => c.Client.Id))
        .ForPath(d => d.HotelId, opt => opt.MapFrom(c => c.Hotel.Id))
        .ForPath(d => d.TourId, opt => opt.MapFrom(c => c.Tour.Id))
        .ForPath(d => d.BookingDataIds, opt => opt.MapFrom(c => c.BookingData.Select(b => b.Id)))

        );
        public async Task<IEnumerable<BookingDTO>> GetAll()
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetAll());
        }

        public async Task<IEnumerable<BookingDTO>> Get200Last()
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.Get200Last());
        }

        public async Task<BookingDTO?> GetById(long id)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<Booking, BookingDTO>(await Database.Bookings.GetById(id));
        }

        public async Task<IEnumerable<BookingDTO>> GetByClientId(int clientId)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByClientId(clientId));
        }

        public async Task<IEnumerable<BookingDTO>> GetByHotelId(int hotelId)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByHotelId(hotelId));
        }

        public async Task<IEnumerable<BookingDTO>> GetByTourId(long tourId)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByTourId(tourId));
        }

        public async Task<IEnumerable<BookingDTO>> GetByBookingDataId(long bookingDataId)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByBookingDataId(bookingDataId));
        }

        public async Task<BookingDTO> Create(BookingDTO bookingDTO)
        {
            var isExist = await Database.Bookings.GetById(bookingDTO.Id);
            if (isExist != null)
            {
                throw new ValidationException($"Бронювання з таким Id вже існує! (bookingDTO.Id : {bookingDTO.Id})", "");
            }
            var booking = new Booking
            {
                BookingData = new List<BookingData>()
            };
            //------------------------------------------------------------------------------------------
            var cl = await Database.Clients.GetById(bookingDTO.ClientId);
            if (cl == null)
            {
                throw new ValidationException($"Такого клієнта з bookingDTO.ClientId не знайдено! (bookingDTO.ClientId : {bookingDTO.ClientId})", "");
            }
            var ht = await Database.Hotels.GetById(bookingDTO.HotelId);
            if (ht == null)
            {
                throw new ValidationException($"Такого готелю з bookingDTO.HotelId не знайдено! (bookingDTO.HotelId : {bookingDTO.HotelId})", "");
            }
            var tr = await Database.Tours.GetById(bookingDTO.TourId);
            if (tr == null)
            {
                throw new ValidationException($"Такого туру з bookingDTO.TourId не знайдено! (bookingDTO.TourId : {bookingDTO.TourId})", "");
            }
            //------------------------------------------------------------------------------------------
            if (bookingDTO.BookingDataIds != null)
            {
                foreach (var id in bookingDTO.BookingDataIds)
                {
                    var bookingData = await Database.BookingDatas.GetById(id);
                    if (bookingData == null)
                    {
                        throw new ValidationException($"Такий bookingData у bookingDTO.BookingDataIds не знайдено! (bookingDataId : {id})", "");
                    }
                    booking.BookingData.Add(bookingData);
                }
            }
            //------------------------------------------------------------------------------------------
            booking.Client = cl;
            booking.Hotel = ht;
            booking.Tour = tr;
            await Database.Bookings.Create(booking);
            await Database.Save();
            bookingDTO.Id = booking.Id;
            return bookingDTO;
        }

        public async Task<BookingDTO> Update(BookingDTO bookingDTO)
        {
            var booking = await Database.Bookings.GetById(bookingDTO.Id);
            if (booking == null)
            {
                throw new ValidationException($"Бронювання з таким Id не знайдено! (bookingDTO.Id : {bookingDTO.Id})", "");
            }
            //------------------------------------------------------------------------------------------
            var cl = await Database.Clients.GetById(bookingDTO.ClientId);
            if (cl == null)
            {
                throw new ValidationException($"Такого клієнта з bookingDTO.ClientId не знайдено! (bookingDTO.ClientId : {bookingDTO.ClientId})", "");
            }
            var ht = await Database.Hotels.GetById(bookingDTO.HotelId);
            if (ht == null)
            {
                throw new ValidationException($"Такого готелю з bookingDTO.HotelId не знайдено! (bookingDTO.HotelId : {bookingDTO.HotelId})", "");
            }
            var tr = await Database.Tours.GetById(bookingDTO.TourId);
            if (tr == null)
            {
                throw new ValidationException($"Такого туру з bookingDTO.TourId не знайдено! (bookingDTO.TourId : {bookingDTO.TourId})", "");
            }
            //------------------------------------------------------------------------------------------
            booking.Client = cl;
            booking.Hotel = ht;
            booking.Tour = tr;
            booking.BookingData.Clear();
            if (bookingDTO.BookingDataIds != null)
            {
                foreach (var id in bookingDTO.BookingDataIds)
                {
                    var bookingData = await Database.BookingDatas.GetById(id);
                    if (bookingData == null)
                    {
                        throw new ValidationException($"Такий bookingData у bookingDTO.BookingDataIds не знайдено! (bookingDataId : {id})", "");
                    }
                    booking.BookingData.Add(bookingData);
                }
            }
            Database.Bookings.Update(booking);
            await Database.Save();
            return bookingDTO;
        }

        public async Task<BookingDTO> Delete(long id)
        {
            var booking = await Database.Bookings.GetById(id);
            if (booking == null)
            {
                throw new ValidationException($"Бронювання з таким Id не знайдено! (id : {id})", "");
            }
            var dto = await GetById(id);
            await Database.Bookings.Delete(id);
            await Database.Save();
            return dto;
        }

        public async Task<IEnumerable<BookingDTO>> GetByTourNameId(int tourNameId)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByTourNameId(tourNameId));
        }

        public async Task<IEnumerable<BookingDTO>> GetByTourNameSubstring(string tourNameSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByTourNameSubstring(tourNameSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetByClientFirstnameSubstring(string clientFirstnameSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByClientFirstnameSubstring(clientFirstnameSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetByClientLastnameSubstring(string clientLastnameSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByClientLastnameSubstring(clientLastnameSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetByClientMiddlenameSubstring(string clientMiddlenameSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByClientMiddlenameSubstring(clientMiddlenameSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetByClientPhoneNumberSubstring(string clientPhoneNumberSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByClientPhoneNumberSubstring(clientPhoneNumberSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetByClientEmailAddressSubstring(string clientEmailAddressSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByClientEmailAddressSubstring(clientEmailAddressSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetByHotelNameSubstring(string hotelNameSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByHotelNameSubstring(hotelNameSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetBySettlementNameSubstring(string settlementNameSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetBySettlementNameSubstring(settlementNameSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetByCountryNameSubstring(string countryNameSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByCountryNameSubstring(countryNameSubstring));
        }

        public async Task<IEnumerable<BookingDTO>> GetByCompositeSearch(long? tourId, int? clientId, int? hotelId, long? bookingDataId, int? tourNameId, string? tourNameSubstring, string? clientFirstnameSubstring, string? clientLastnameSubstring, string? clientMiddlenameSubstring, string? clientPhoneNumberSubstring, string? clientEmailAddressSubstring, string? hotelNameSubstring, string? settlementNameSubstring, string? countryNameSubstring)
        {
            var mapper = new Mapper(Booking_BookingDTOMapConfig);
            return mapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(await Database.Bookings.GetByCompositeSearch(tourId, clientId, hotelId, bookingDataId, tourNameId, tourNameSubstring, clientFirstnameSubstring, clientLastnameSubstring, clientMiddlenameSubstring, clientPhoneNumberSubstring, clientEmailAddressSubstring, hotelNameSubstring, settlementNameSubstring, countryNameSubstring));
        }

    }
}
