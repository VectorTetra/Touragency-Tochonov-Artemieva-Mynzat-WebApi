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
        public async Task<IEnumerable<ReviewImage>> Get200Last()
        {
            return await _context.ReviewImages.OrderByDescending(p => p.Id).Take(200).ToListAsync();
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

        public async Task<IEnumerable<ReviewImage>> GetByTourId(long tourId)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.TourId == tourId)
                .ToListAsync();
        }
        public async Task<IEnumerable<ReviewImage>> GetByTourNameId(int tourNameId)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Tour.Name.Id == tourNameId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByTourName(string tourName)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Tour.Name.Name.Contains(tourName))
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByClientFirstname(string clientFirstname)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Client.Person.Firstname.Contains(clientFirstname))
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByClientLastname(string clientLastname)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Client.Person.Lastname.Contains(clientLastname))
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByClientMiddlename(string clientMiddlename)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Client.Person.Middlename.Contains(clientMiddlename))
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByTouristNickname(string touristNickname)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Client.TouristNickname.Contains(touristNickname))
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByCountryName(string countryName)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Tour.Name.Settlements.Any(sss=> sss.Country.Name.Contains(countryName)))
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetBySettlementName(string settlementName)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Tour.Name.Settlements.Any(sss => sss.Name.Contains(settlementName)))
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByHotelName(string hotelName)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Tour.Name.Hotels.Any(h => h.Name.Contains(hotelName)))
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByReviewRatingDiapazone(short minRating, short maxRating)
        {
            return await _context.ReviewImages
                .Where(p => p.Review.Rating >= minRating && p.Review.Rating <= maxRating)
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewImage>> GetByCompositeSearch(long? reviewId, short? minRating, short? maxRating, string? imagePathSubstring, long? tourId, int? tourNameId, string? tourName, string? clientFirstname, string? clientLastname, string? clientMiddlename, string? touristNickname, string? countryName, string? settlementName, string? hotelName)
        {
            var collections = new List<IEnumerable<ReviewImage>>();
            if (reviewId != null)
            {
                collections.Add(await GetByReviewId((long)reviewId));
            }
            if (minRating != null && maxRating != null)
            {
                collections.Add(await GetByReviewRatingDiapazone((short)minRating, (short)maxRating));
            }
            if (imagePathSubstring != null)
            {
                collections.Add(await GetByImagePathSubstring(imagePathSubstring));
            }
            if (tourId != null)
            {
                collections.Add(await GetByTourId((long)tourId));
            }
            if (tourNameId != null)
            {
                collections.Add(await GetByTourNameId((int)tourNameId));
            }
            if (tourName != null)
            {
                collections.Add(await GetByTourName(tourName));
            }
            if (clientFirstname != null)
            {
                collections.Add(await GetByClientFirstname(clientFirstname));
            }
            if (clientLastname != null)
            {
                collections.Add(await GetByClientLastname(clientLastname));
            }
            if (clientMiddlename != null)
            {
                collections.Add(await GetByClientMiddlename(clientMiddlename));
            }
            if (touristNickname != null)
            {
                collections.Add(await GetByTouristNickname(touristNickname));
            }
            if (countryName != null)
            {
                collections.Add(await GetByCountryName(countryName));
            }
            if (settlementName != null)
            {
                collections.Add(await GetBySettlementName(settlementName));
            }
            if (hotelName != null)
            {
                collections.Add(await GetByHotelName(hotelName));
            }
            if (!collections.Any())
            {
                return new List<ReviewImage>();
            }
            return collections.Aggregate((a, b) => a.Intersect(b));
        }
    }
}
