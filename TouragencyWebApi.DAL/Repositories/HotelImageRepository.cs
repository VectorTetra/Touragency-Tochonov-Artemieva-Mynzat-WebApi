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
    public class HotelImageRepository: IHotelImageRepository
    {
        private readonly TouragencyContext _context;
        public HotelImageRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HotelImage>> GetAll()
        {
            return await _context.HotelImages.ToListAsync();
        }

        public async Task<HotelImage?> GetById(long id)
        {
            return await _context.HotelImages.FindAsync(id);
        }

        public async Task<IEnumerable<HotelImage>> GetByHotelId(int hotelId)
        {
            return await _context.HotelImages.Where(hi => hi.Hotel.Id == hotelId).ToListAsync();
        }

        public async Task<IEnumerable<HotelImage>> GetByImageUrlSubstring(string imageUrlSubstring)
        {
            return await _context.HotelImages.Where(hi => hi.ImageUrl.Contains(imageUrlSubstring)).ToListAsync();
        }

        public async Task Create(HotelImage hotelImage)
        {
            await _context.HotelImages.AddAsync(hotelImage);
        }

        public void Update(HotelImage hotelImage)
        {
            _context.Entry(hotelImage).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            HotelImage hotelImage = await GetById(id);
            if (hotelImage != null)
            {
                _context.HotelImages.Remove(hotelImage);
            }
        }
    }
}
