using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Repositories
{
    public class HotelRepository: IHotelRepository
    {
        private readonly TouragencyContext _context;
        public HotelRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Hotel>> GetAll()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel?> GetById(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task<IEnumerable<Hotel>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.Hotels.Where(h => h.Name.Contains(nameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByStars(int[] selectedStarsRatings)
        {
            return await _context.Hotels.Where(h => selectedStarsRatings.Any(sta => sta == h.Stars)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByHotelConfigurationId(int hotelConfigurationId)
        {
            return await _context.Hotels.Where(h => h.HotelConfigurations.Any(bc => bc.Id == hotelConfigurationId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByBedConfigurationId(int bedConfigurationId)
        {
            return await _context.Hotels.Where(h => h.BedConfigurations.Any(bc => bc.Id == bedConfigurationId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetBySettlementId(int settlementId)
        {
            return await _context.Hotels.Where(h => h.Settlement.Id == settlementId).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByTourId(long tourId)
        {
            return await _context.Hotels.Where(h => h.Tours.Any(t => t.Id == tourId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByBookingId(long bookingId)
        {
            return await _context.Hotels.Where(h => h.Bookings.Any(b => b.Id == bookingId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByHotelServiceId(int settlementId)
        {
            return await _context.Hotels.Where(h => h.HotelServices.Any(hs => hs.Id == settlementId)).ToListAsync();
        }

        public async Task Create(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
        }

        public void Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
                _context.Hotels.Remove(hotel);
        }
    }
}
