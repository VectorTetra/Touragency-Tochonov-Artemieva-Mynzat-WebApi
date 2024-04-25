using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Repositories
{
    public class BookingRepository: IBookingRepository
    {
        private readonly TouragencyContext _context;

        public BookingRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _context.Bookings.ToListAsync();
        }

public async Task<IEnumerable<Booking>> Get200Last()
        {
            return await _context.Bookings.OrderByDescending(b => b.Id).Take(200).ToListAsync();
        }

        public async Task<Booking?> GetById(long id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetByTourId(long tourId)
        {
            return await _context.Bookings.Where(b => b.Tour.Id == tourId).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByClientId(int clientId)
        {
            return await _context.Bookings.Where(b => b.Client.Id == clientId).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByHotelId(int hotelId)
        {
            return await _context.Bookings.Where(b => b.Hotel.Id == hotelId).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByBookingDataId(long bookingDataId)
        {
            return await _context.Bookings.Where(b => b.BookingData.Any(bd=>bd.Id == bookingDataId)).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByTourNameId(int tourNameId)
        {
            return await _context.Bookings.Where(b => b.Tour.Name.Id == tourNameId).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByTourNameSubstring(string tourNameSubstring)
        {
            return await _context.Bookings.Where(b => b.Tour.Name.Name.Contains(tourNameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByClientFirstnameSubstring(string clientFirstnameSubstring)
        {
            return await _context.Bookings.Where(b => b.Client.Person.Firstname.Contains(clientFirstnameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByClientLastnameSubstring(string clientLastnameSubstring)
        {
            return await _context.Bookings.Where(b => b.Client.Person.Lastname.Contains(clientLastnameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByClientMiddlenameSubstring(string clientMiddlenameSubstring)
        {
            return await _context.Bookings.Where(b => b.Client.Person.Middlename.Contains(clientMiddlenameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByClientPhoneNumberSubstring(string clientPhoneNumberSubstring)
        {
            return await _context.Bookings.Where(b => b.Client.Person.Phones.Any(ph=> ph.PhoneNumber.Contains(clientPhoneNumberSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByClientEmailAddressSubstring(string clientEmailAddressSubstring)
        {
            return await _context.Bookings.Where(b => b.Client.Person.Emails.Any(em=> em.EmailAddress.Contains(clientEmailAddressSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByHotelNameSubstring(string hotelNameSubstring)
        {
            return await _context.Bookings.Where(b => b.Hotel.Name.Contains(hotelNameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetBySettlementNameSubstring(string settlementNameSubstring)
        {
            return await _context.Bookings.Where(b => b.Hotel.Settlement.Name.Contains(settlementNameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByCountryNameSubstring(string countryNameSubstring)
        {
            return await _context.Bookings.Where(b => b.Hotel.Settlement.Country.Name.Contains(countryNameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Booking>> GetByCompositeSearch(long? tourId, int? clientId, int? hotelId, long? bookingDataId, int? tourNameId,
            string? tourNameSubstring, string? clientFirstnameSubstring, string? clientLastnameSubstring, string? clientMiddlenameSubstring, string? clientPhoneNumberSubstring,
            string? clientEmailAddressSubstring, string? hotelNameSubstring, string? settlementNameSubstring, string? countryNameSubstring)
        {
            var bookingCollections = new List<IEnumerable<Booking>>();

            if (tourId != null)
            {
                var bookingsByTourId = await GetByTourId(tourId.Value);
                bookingCollections.Add(bookingsByTourId);
            }

            if (clientId != null)
            {
                var bookingsByClientId = await GetByClientId(clientId.Value);
                bookingCollections.Add(bookingsByClientId);
            }

            if (hotelId != null)
            {
                var bookingsByHotelId = await GetByHotelId(hotelId.Value);
                bookingCollections.Add(bookingsByHotelId);
            }

            if (bookingDataId != null)
            {
                var bookingsByBookingDataId = await GetByBookingDataId(bookingDataId.Value);
                bookingCollections.Add(bookingsByBookingDataId);
            }

            if (tourNameId != null)
            {
                var bookingsByTourNameId = await GetByTourNameId(tourNameId.Value);
                bookingCollections.Add(bookingsByTourNameId);
            }

            if (tourNameSubstring != null)
            {
                var bookingsByTourNameSubstring = await GetByTourNameSubstring(tourNameSubstring);
                bookingCollections.Add(bookingsByTourNameSubstring);
            }

            if (clientFirstnameSubstring != null)
            {
                var bookingsByClientFirstnameSubstring = await GetByClientFirstnameSubstring(clientFirstnameSubstring);
                bookingCollections.Add(bookingsByClientFirstnameSubstring);
            }

            if (clientLastnameSubstring != null)
            {
                var bookingsByClientLastnameSubstring = await GetByClientLastnameSubstring(clientLastnameSubstring);
                bookingCollections.Add(bookingsByClientLastnameSubstring);
            }

            if (clientMiddlenameSubstring != null)
            {
                var bookingsByClientMiddlenameSubstring = await GetByClientMiddlenameSubstring(clientMiddlenameSubstring);
                bookingCollections.Add(bookingsByClientMiddlenameSubstring);
            }

            if (clientPhoneNumberSubstring != null)
            {
                var bookingsByClientPhoneNumberSubstring = await GetByClientPhoneNumberSubstring(clientPhoneNumberSubstring);
                bookingCollections.Add(bookingsByClientPhoneNumberSubstring);
            }

            if (clientEmailAddressSubstring != null)
            {
                var bookingsByClientEmailAddressSubstring = await GetByClientEmailAddressSubstring(clientEmailAddressSubstring);
                bookingCollections.Add(bookingsByClientEmailAddressSubstring);
            }

            if (hotelNameSubstring != null)
            {
                var bookingsByHotelNameSubstring = await GetByHotelNameSubstring(hotelNameSubstring);
                bookingCollections.Add(bookingsByHotelNameSubstring);
            }

            if (settlementNameSubstring != null)
            {
                var bookingsBySettlementNameSubstring = await GetBySettlementNameSubstring(settlementNameSubstring);
                bookingCollections.Add(bookingsBySettlementNameSubstring);
            }

            if (countryNameSubstring != null)
            {
                var bookingsByCountryNameSubstring = await GetByCountryNameSubstring(countryNameSubstring);
                bookingCollections.Add(bookingsByCountryNameSubstring);
            }

            if (!bookingCollections.Any())
            {
                return new List<Booking>();
            }
            return bookingCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }
        
        public async Task Create(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }

        public void Update(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
        }
    }
}
