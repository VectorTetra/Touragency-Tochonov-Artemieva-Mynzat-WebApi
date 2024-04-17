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

        public async Task<BookingDTO> GetById(long id)
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

        public async Task Create(BookingDTO bookingDTO)
        {
            var booking = new Booking
            { 
                BookingData = new List<BookingData>()
            };
            //------------------------------------------------------------------------------------------
            var cl = await Database.Clients.GetById(bookingDTO.ClientId);
            if (cl == null)
            {
                throw new ValidationException("Такого клієнта з bookingDTO.ClientId не знайдено!", "");
            }
            var ht = await Database.Hotels.GetById(bookingDTO.HotelId);
            if (ht == null)
            {
                throw new ValidationException("Такого готелю з bookingDTO.HotelId не знайдено!", "");
            }
            var tr = await Database.Tours.GetById(bookingDTO.TourId);
            if (tr == null)
            {
                throw new ValidationException("Такого туру з bookingDTO.TourId не знайдено!", "");
            }
            //------------------------------------------------------------------------------------------
            if (bookingDTO.BookingDataIds != null)
            {
                foreach (var id in bookingDTO.BookingDataIds)
                {
                    var bookingData = await Database.BookingDatas.GetById(id);
                    if (bookingData == null)
                    {
                        throw new ValidationException("Такий bookingData у bookingDTO.BookingDataIds не знайдено!","");   
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
        }

        public async Task Update(BookingDTO bookingDTO)
        {
            var booking = await Database.Bookings.GetById(bookingDTO.Id);
            if (booking == null)
            {
                throw new ValidationException("Бронювання не знайдено", "");
            }
            //------------------------------------------------------------------------------------------
            var cl = await Database.Clients.GetById(bookingDTO.ClientId);
            if (cl == null)
            {
                throw new ValidationException("Такого клієнта з bookingDTO.ClientId не знайдено!", "");
            }
            var ht = await Database.Hotels.GetById(bookingDTO.HotelId);
            if (ht == null)
            {
                throw new ValidationException("Такого готелю з bookingDTO.HotelId не знайдено!", "");
            }
            var tr = await Database.Tours.GetById(bookingDTO.TourId);
            if (tr == null)
            {
                throw new ValidationException("Такого туру з bookingDTO.TourId не знайдено!", "");
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
                        throw new ValidationException("Такий bookingData у bookingDTO.BookingDataIds не знайдено!", "");
                    }
                    booking.BookingData.Add(bookingData);
                }
            }
            Database.Bookings.Update(booking);
            await Database.Save();
        }

        public async Task Delete(long id)
        {
            var booking = await Database.Bookings.GetById(id);
            if (booking == null)
            {
                throw new ValidationException("Бронювання не знайдено", "");
            }
            await Database.Bookings.Delete(id);
            await Database.Save();
        }
    }
}
