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
    public class TourImageRepository: ITourImageRepository
    {
        private readonly TouragencyContext _context;
        public TourImageRepository(TouragencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourImage>> GetAll()
        {
            return await _context.TourImages.ToListAsync();
        }

        public async Task<TourImage?> GetById(long id)
        {
            return await _context.TourImages.FindAsync(id);
        }

        public async Task<IEnumerable<TourImage>> GetByTourId(long tourId)
        {
            return await _context.TourImages.Where(ti => ti.TourName.Tours.Any(tt => tt.Id == tourId)).ToListAsync();
        }

        public async Task<IEnumerable<TourImage>> GetByTourNameId(int tourNameId)
        {
            return await _context.TourImages.Where(ti => ti.TourName.Id == tourNameId).ToListAsync();
        }

        public async Task<IEnumerable<TourImage>> GetByImageUrlSubstring(string imageUrlSubstring)
        {
            return await _context.TourImages.Where(ti => ti.ImageUrl.Contains(imageUrlSubstring)).ToListAsync();
        }   

        public async Task Create(TourImage tourImage)
        {
            await _context.TourImages.AddAsync(tourImage);
        }

        public void Update(TourImage tourImage)
        {
            _context.Entry(tourImage).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            TourImage tourImage = await GetById(id);
            if (tourImage != null)
            {
                _context.TourImages.Remove(tourImage);
            }
        }
    }
}
