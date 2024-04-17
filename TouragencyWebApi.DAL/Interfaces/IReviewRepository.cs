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
        Task<IEnumerable<Review>> GetByTourId(int tourId);
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
    }
}
