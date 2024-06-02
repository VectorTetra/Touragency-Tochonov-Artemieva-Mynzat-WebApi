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
    public class HotelServiceTypeRepository: IHotelServiceTypeRepository
    {
        private readonly TouragencyContext _context;

        public HotelServiceTypeRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HotelServiceType>> GetAll()
        {
            return await _context.HotelServiceTypes.ToListAsync();
        }

        public async Task<IEnumerable<HotelServiceType>> Get200Last()
        {
            return await _context.HotelServiceTypes.OrderByDescending(hst => hst.Id).Take(200).ToListAsync();
        }
        public async Task<HotelServiceType?> GetById(int id)
        {
            return await _context.HotelServiceTypes.FindAsync(id);
        }

        public async Task<IEnumerable<HotelServiceType>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.HotelServiceTypes.Where(hst => hst.Description.Contains(descriptionSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<HotelServiceType>> GetByHotelServiceId(int hotelServiceId)
        {
            return await _context.HotelServiceTypes.Where(hst => hst.HotelServices.Any(hs => hs.Id == hotelServiceId)).ToListAsync();
        }

        public async Task Create(HotelServiceType hotelServiceType)
        {
            await _context.HotelServiceTypes.AddAsync(hotelServiceType);
        }

        public void Update(HotelServiceType hotelServiceType)
        {
            _context.Entry(hotelServiceType).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var hotelServiceType = await _context.HotelServiceTypes.FindAsync(id);
            if (hotelServiceType != null)
            {
                _context.HotelServiceTypes.Remove(hotelServiceType);
            }
        }
    }
}
