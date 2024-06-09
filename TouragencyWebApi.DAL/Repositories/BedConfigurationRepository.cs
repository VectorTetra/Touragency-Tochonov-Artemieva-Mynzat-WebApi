using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Repositories
{
    public class BedConfigurationRepository : IBedConfigurationRepository
    {
        private readonly TouragencyContext _context;

        public BedConfigurationRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<BedConfiguration?> GetById(int id)
        {
            return await _context.BedConfigurations.FindAsync(id);
        }
        public async Task<IEnumerable<BedConfiguration>> GetAll()
        {
            return await _context.BedConfigurations.OrderBy(b => b.Capacity).ToListAsync();
        }

        public async Task<IEnumerable<BedConfiguration>> Get200Last()
        {
            return await _context.BedConfigurations.OrderByDescending(b => b.Id).Take(200).ToListAsync();
        }

        public async Task<IEnumerable<BedConfiguration>> GetByHotelId(int hotelId){
            
            return await _context.BedConfigurations.Where(b => b.Hotels.Any(h => h.Id == hotelId) ).ToListAsync();
        }
        public async Task<IEnumerable<BedConfiguration>> GetByBookingDataId(long bookingDataId)
        {
            return await _context.BedConfigurations.Where(b => b.BookingDatas.Any(bd => bd.Id == bookingDataId)).ToListAsync();
        }
        public async Task<IEnumerable<BedConfiguration>> GetByCapacity(short capacity)
        {
            return await _context.BedConfigurations.Where(b => b.Capacity == capacity).ToListAsync();
        }
        public async Task<IEnumerable<BedConfiguration>> GetByLabelSubstring(string labelSubstring)
        {
            return await _context.BedConfigurations.Where(b => b.Label.Contains(labelSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<BedConfiguration>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.BedConfigurations.Where(b => b.Description.Contains(descriptionSubstring)).ToListAsync();
        }
        public async Task Create(BedConfiguration bedConfiguration)
        {
            await _context.BedConfigurations.AddAsync(bedConfiguration);
        }
        public void Update(BedConfiguration bedConfiguration)
        {
            _context.Entry(bedConfiguration).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            var bedConfiguration = await _context.BedConfigurations.FindAsync(id);
            if (bedConfiguration != null)
            {
                _context.BedConfigurations.Remove(bedConfiguration);
            }
        }
    }
}
