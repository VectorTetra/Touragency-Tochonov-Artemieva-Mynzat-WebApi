using Microsoft.EntityFrameworkCore;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TouragencyContext _context;
        public ReviewRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews.ToListAsync();
        }
        public async Task<Review?> GetById(long id)
        {
            return await _context.Reviews.FindAsync(id);
        }
        public async Task<IEnumerable<Review>> GetByTourId(int tourId)
        {
            return await _context.Reviews
                .Where(r => r.TourId == tourId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByClientId(int clientId)
        {
            return await _context.Reviews
                .Where(r => r.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByCountryId(int countryId)
        {
            return await _context.Reviews
                .Where(r => r.Tour.Settlements.Any(st => st.Country.Id == countryId))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByReviewImageId(long reviewImageId)
        {
            return await _context.Reviews
                .Where(r => r.ReviewImages.Any(im => im.Id == reviewImageId))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByReviewCaptionSubstring(string reviewCaptionSubstring)
        {
            return await _context.Reviews
                .Where(r => r.ReviewCaption.Contains(reviewCaptionSubstring))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByReviewTextSubstring(string reviewTextSubstring)
        {
            return await _context.Reviews
                .Where(r => r.ReviewText.Contains(reviewTextSubstring))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByRatingDiapazone(short start, short end)
        {
            if (start == end)
            {
                return await _context.Reviews
                .Where(r => r.Rating == start)
                .ToListAsync();
            }
            if (start > end)
            {
                return await _context.Reviews
                .Where(r => r.Rating >= end && r.Rating <= start)
                .ToListAsync();
            }
            return await _context.Reviews
                .Where(r => r.Rating >= start && r.Rating <= end)
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByCreationDateDiapazone(DateTime start, DateTime end)
        {
            if (start == end)
            {
                return await _context.Reviews
                .Where(r => r.CreationDate == start)
                .ToListAsync();
            }
            if (start > end)
            {
                return await _context.Reviews
                .Where(r => r.CreationDate >= end && r.CreationDate <= start)
                .ToListAsync();
            }
            return await _context.Reviews
                .Where(r => r.CreationDate >= start && r.CreationDate <= end)
                .ToListAsync();
        }
        public async Task Create(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }
        public void Update(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            Review? review = await _context.Reviews.FindAsync(id);
            if (review != null)
                _context.Reviews.Remove(review);
        }
    }
}
