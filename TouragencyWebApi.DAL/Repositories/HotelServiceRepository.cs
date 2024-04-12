using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.DAL.Repositories
{
    public class HotelServiceRepository : IHotelServiceRepository
    {
        private readonly TouragencyContext _context;
        public HotelServiceRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HotelService>> GetAll()
        {
            return await _context.HotelServices.ToListAsync();
        }

        public async Task<HotelService?> GetById(int id)
        {
            return await _context.HotelServices.FindAsync(id);
        }

        public async Task<IEnumerable<HotelService>> GetByHotelId(int hotelId)
        {
            return await _context.HotelServices.Where(h => h.Hotels.Any(h => h.Id == hotelId)).ToListAsync();
        }

        public async Task<IEnumerable<HotelService>> GetByHotelServiceTypeId(int hotelServiceTypeId)
        {
            return await _context.HotelServices.Where(h => h.HotelServiceType.Id == hotelServiceTypeId).ToListAsync();
        }

        public async Task<IEnumerable<HotelService>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.HotelServices.Where(h => h.Name.Contains(nameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<HotelService>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.HotelServices.Where(h => h.Description.Contains(descriptionSubstring)).ToListAsync();
        }

        public async Task Create(HotelService hotelService)
        {
            await _context.HotelServices.AddAsync(hotelService);
        }

        public void Update(HotelService hotelService)
        {
            _context.Entry(hotelService).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var hotelService = await _context.HotelServices.FindAsync(id);
            if (hotelService != null)
                _context.HotelServices.Remove(hotelService);
        }
    }
}
