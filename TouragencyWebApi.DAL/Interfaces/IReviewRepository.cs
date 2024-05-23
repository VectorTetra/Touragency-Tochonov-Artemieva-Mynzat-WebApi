using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll();
        Task<Review?> GetById(long id);
        Task<IEnumerable<Review>> GetByTourId(long tourId);
        Task<IEnumerable<Review>> GetByClientId(int clientId);
        Task<IEnumerable<Review>> GetByCountryId(int countryId);
        Task<IEnumerable<Review>> GetByReviewImageId(long reviewImageId);
        Task<IEnumerable<Review>> GetByReviewCaptionSubstring(string reviewCaptionSubstring);
        Task<IEnumerable<Review>> GetByReviewTextSubstring(string reviewTextSubstring);
        Task<IEnumerable<Review>> GetByRatingDiapazone(short start, short end);
        Task<IEnumerable<Review>> GetByCreationDateDiapazone(DateTime start, DateTime end);
        Task Create(Review review);
        void Update(Review review);
        Task Delete(long id);
        Task<IEnumerable<Review>> Get200Last();
        Task<IEnumerable<Review>> GetByTourNameSubstring(string tourNameSubstring);
        Task<IEnumerable<Review>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<Review>> GetByTouristNicknameSubstring(string touristNicknameSubstring);
        Task<IEnumerable<Review>> GetByClientFirstnameSubstring(string clientFirstnameSubstring);
        Task<IEnumerable<Review>> GetByClientLastnameSubstring(string clientLastnameSubstring);
        Task<IEnumerable<Review>> GetByClientMiddlenameSubstring(string clientMiddlenameSubstring);
        Task<IEnumerable<Review>> GetByCountryNameSubstring(string countryNameSubstring);
        Task<IEnumerable<Review>> GetByCompositeSearch(long? tourId, int? clientId, int? countryId, long? reviewImageId, string? reviewCaptionSubstring,
            string? reviewTextSubstring, short? startRating, short? endRating, DateTime? startDate, DateTime? endDate, string? tourNameSubstring,
            string? touristNicknameSubstring, string? clientFirstnameSubstring, string? clientLastnameSubstring, string? clientMiddlenameSubstring,
            string? countryNameSubstring, int? TourNameId);
    }
}
