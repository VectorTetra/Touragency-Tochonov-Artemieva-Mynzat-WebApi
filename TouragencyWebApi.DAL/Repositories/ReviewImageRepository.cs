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
    public class ReviewImageRepository : IReviewImageRepository
    {
        private readonly TouragencyContext _context;
        public ReviewImageRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task Create(ReviewImage reviewImage)
        {
            await _context.ReviewImages.AddAsync(reviewImage);
        }
        public void Update(ReviewImage reviewImage)
        {
            _context.Entry(reviewImage).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            ReviewImage? reviewImage = await _context.ReviewImages.FindAsync(id);
            if (reviewImage != null)
                _context.ReviewImages.Remove(reviewImage);
        }

        public async Task<IEnumerable<ReviewImage>> GetAll()
        {
            return await _context.ReviewImages.ToListAsync();
        }

        public async Task<ReviewImage?> GetById(long id)
        {
            return await _context.ReviewImages.FindAsync(id);
        }

        public async Task<IEnumerable<ReviewImage>> GetByReviewId(long reviewId)
        {
            return await _context.ReviewImages
                .Where(p => p.ReviewId == reviewId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByImagePathSubstring(string imagePathSubstring)
        {
            return await _context.ReviewImages
                .Where(p => p.ImagePath.Contains(imagePathSubstring))
                .ToListAsync();
        }
        
    }
}
