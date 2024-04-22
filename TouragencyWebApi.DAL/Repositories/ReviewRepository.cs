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
        public async Task<IEnumerable<Review>> GetByTourId(long tourId)
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

        public async Task<IEnumerable<Review>> Get200Last()
        {
            return await _context.Reviews
                .OrderByDescending(r => r.CreationDate)
                .Take(200)
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByTourNameSubstring(string tourNameSubstring)
        {
            return await _context.Reviews
                .Where(r => r.Tour.Name.Name.Contains(tourNameSubstring))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByTouristNicknameSubstring(string touristNicknameSubstring)
        {
            return await _context.Reviews
                .Where(r => r.Client.TouristNickname.Contains(touristNicknameSubstring))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByClientFirstnameSubstring(string clientFirstnameSubstring)
        {
            return await _context.Reviews
                .Where(r => r.Client.Person.Firstname.Contains(clientFirstnameSubstring))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByClientLastnameSubstring(string clientLastnameSubstring)
        {
            return await _context.Reviews
                .Where(r => r.Client.Person.Lastname.Contains(clientLastnameSubstring))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByClientMiddlenameSubstring(string clientMiddlenameSubstring)
        {
            return await _context.Reviews
                .Where(r => r.Client.Person.Middlename.Contains(clientMiddlenameSubstring))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByCountryNameSubstring(string countryNameSubstring)
        {
            return await _context.Reviews
                .Where(r => r.Tour.Settlements.Any(st => st.Country.Name.Contains(countryNameSubstring)))
                .ToListAsync();
        }
        public async Task<IEnumerable<Review>> GetByCompositeSearch(long? tourId, int? clientId, int? countryId, long? reviewImageId, string? reviewCaptionSubstring,
                       string? reviewTextSubstring, short? startRating, short? endRating, DateTime? startDate, DateTime? endDate, string? tourNameSubstring,
                                  string? touristNicknameSubstring, string? clientFirstnameSubstring, string? clientLastnameSubstring, string? clientMiddlenameSubstring,
                                             string? countryNameSubstring)
        {
            var reviewCollections = new List<IEnumerable<Review>>();

            if (tourId != null)
            {
                var reviewsByTourId = await GetByTourId(tourId.Value);
                reviewCollections.Add(reviewsByTourId);
            }

            if (clientId != null)
            {
                var reviewsByClientId = await GetByClientId(clientId.Value);
                reviewCollections.Add(reviewsByClientId);
            }

            if (countryId != null)
            {
                var reviewsByCountryId = await GetByCountryId(countryId.Value);
                reviewCollections.Add(reviewsByCountryId);
            }

            if (reviewImageId != null)
            {
                var reviewsByReviewImageId = await GetByReviewImageId(reviewImageId.Value);
                reviewCollections.Add(reviewsByReviewImageId);
            }

            if (!string.IsNullOrEmpty(reviewCaptionSubstring))
            {
                var reviewsByReviewCaptionSubstring = await GetByReviewCaptionSubstring(reviewCaptionSubstring);
                reviewCollections.Add(reviewsByReviewCaptionSubstring);
            }

            if (!string.IsNullOrEmpty(reviewTextSubstring))
            {
                var reviewsByReviewTextSubstring = await GetByReviewTextSubstring(reviewTextSubstring);
                reviewCollections.Add(reviewsByReviewTextSubstring);
            }

            if (startRating != null && endRating != null)
            {
                var reviewsByRatingDiapazone = await GetByRatingDiapazone(startRating.Value, endRating.Value);
                reviewCollections.Add(reviewsByRatingDiapazone);
            }

            if (startDate != null && endDate != null)
            {
                var reviewsByCreationDateDiapazone = await GetByCreationDateDiapazone(startDate.Value, endDate.Value);
                reviewCollections.Add(reviewsByCreationDateDiapazone);
            }

            if (!string.IsNullOrEmpty(tourNameSubstring))
            {
                var reviewsByTourNameSubstring = await GetByTourNameSubstring(tourNameSubstring);
                reviewCollections.Add(reviewsByTourNameSubstring);
            }

            if (!string.IsNullOrEmpty(touristNicknameSubstring))
            {
                var reviewsByTouristNicknameSubstring = await GetByTouristNicknameSubstring(touristNicknameSubstring);
                reviewCollections.Add(reviewsByTouristNicknameSubstring);
            }

            if (!string.IsNullOrEmpty(clientFirstnameSubstring))
            {
                var reviewsByClientFirstnameSubstring = await GetByClientFirstnameSubstring(clientFirstnameSubstring);
                reviewCollections.Add(reviewsByClientFirstnameSubstring);
            }

            if (!string.IsNullOrEmpty(clientLastnameSubstring))
            {
                var reviewsByClientLastnameSubstring = await GetByClientLastnameSubstring(clientLastnameSubstring);
                reviewCollections.Add(reviewsByClientLastnameSubstring);
            }

            if (!string.IsNullOrEmpty(clientMiddlenameSubstring))
            {
                var reviewsByClientMiddlenameSubstring = await GetByClientMiddlenameSubstring(clientMiddlenameSubstring);
                reviewCollections.Add(reviewsByClientMiddlenameSubstring);
            }

            if (!string.IsNullOrEmpty(countryNameSubstring))
            {
                var reviewsByCountryNameSubstring = await GetByCountryNameSubstring(countryNameSubstring);
                reviewCollections.Add(reviewsByCountryNameSubstring);
            }

            if (!reviewCollections.Any())
            {
                return new List<Review>();
            }
            return reviewCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }
    }
}
