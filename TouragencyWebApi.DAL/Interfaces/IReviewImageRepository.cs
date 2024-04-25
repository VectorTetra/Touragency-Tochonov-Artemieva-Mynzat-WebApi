using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IReviewImageRepository
    {
        Task<IEnumerable<ReviewImage>> GetAll();
        Task<IEnumerable<ReviewImage>> Get200Last();
        Task<ReviewImage?> GetById(long id);
        Task<IEnumerable<ReviewImage>> GetByReviewId(long reviewId);
        Task<IEnumerable<ReviewImage>> GetByReviewRatingDiapazone(short minRating, short maxRating);
        Task<IEnumerable<ReviewImage>> GetByImagePathSubstring(string imagePathSubstring);
        Task<IEnumerable<ReviewImage>> GetByTourId(long tourId);
        Task<IEnumerable<ReviewImage>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<ReviewImage>> GetByTourName(string tourName);
        Task<IEnumerable<ReviewImage>> GetByClientFirstname(string clientFirstname);
        Task<IEnumerable<ReviewImage>> GetByClientLastname(string clientLastname);
        Task<IEnumerable<ReviewImage>> GetByClientMiddlename(string clientMiddlename);
        Task<IEnumerable<ReviewImage>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<ReviewImage>> GetByCountryName(string countryName);
        Task<IEnumerable<ReviewImage>> GetBySettlementName(string settlementName);
        Task<IEnumerable<ReviewImage>> GetByHotelName(string hotelName);
        Task<IEnumerable<ReviewImage>> GetByCompositeSearch(long? reviewId, short? minRating, short? maxRating,
            string? imagePathSubstring, long? tourId, int? tourNameId, string? tourName, string? clientFirstname, string? clientLastname,
            string? clientMiddlename, string? touristNickname, string? countryName, string? settlementName, string? hotelName);
        Task Create(ReviewImage reviewImage);
        void Update(ReviewImage reviewImage);
        Task Delete(long id);
    }
}
