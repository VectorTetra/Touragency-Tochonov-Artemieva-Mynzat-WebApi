using Microsoft.EntityFrameworkCore;
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
    public class HotelConfigurationRepository : IHotelConfigurationRepository
    {
        private readonly TouragencyContext _context;
        public HotelConfigurationRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HotelConfiguration>> GetAll()
        {
            return await _context.HotelConfigurations.ToListAsync();
        }

        public async Task<HotelConfiguration?> GetById(int id)
        {
            return await _context.HotelConfigurations.FindAsync(id);
        }

        public async Task<IEnumerable<HotelConfiguration>> GetByHotelId(int hotelId)
        {
            return await _context.HotelConfigurations.Where(h => h.Hotels.Any(hh=> hh.Id == hotelId) ).ToListAsync();
        }

        public async Task<IEnumerable<HotelConfiguration>> GetByCompassSideSubstring(string compassSideSubstring)
        {
            return await _context.HotelConfigurations.Where(h => h.CompassSide.Contains(compassSideSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<HotelConfiguration>> GetByWindowViewSubstring(string WindowViewSubstring)
        {
            return await _context.HotelConfigurations.Where(h => h.WindowView.Contains(WindowViewSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<HotelConfiguration>> GetByIsAllowChildren(bool isAllowChildren)
        {
            return await _context.HotelConfigurations.Where(h => h.IsAllowChildren == isAllowChildren).ToListAsync();
        }

        public async Task<IEnumerable<HotelConfiguration>> GetByIsAllowPets(bool isAllowPets)
        {
            return await _context.HotelConfigurations.Where(h => h.IsAllowPets == isAllowPets).ToListAsync();
        }

        public async Task Create(HotelConfiguration hotelConfiguration)
        {
            await _context.HotelConfigurations.AddAsync(hotelConfiguration);
        }

        public void Update(HotelConfiguration hotelConfiguration)
        {
            _context.Entry(hotelConfiguration).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var hotelConfiguration = await _context.HotelConfigurations.FindAsync(id);
            if (hotelConfiguration != null)
                _context.HotelConfigurations.Remove(hotelConfiguration);
        }
    }
}
