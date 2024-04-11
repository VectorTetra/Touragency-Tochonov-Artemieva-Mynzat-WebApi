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
            return await _context.Bookings.Where(b => b.ClientId == clientId).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByHotelId(int hotelId)
        {
            return await _context.Bookings.Where(b => b.HotelId == hotelId).ToListAsync();
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
