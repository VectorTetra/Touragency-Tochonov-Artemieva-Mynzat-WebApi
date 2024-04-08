using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetAll();
        Task<ReviewDTO?> GetById(long id);
        Task<IEnumerable<ReviewDTO>> GetByTourId(int tourId);
        Task<IEnumerable<ReviewDTO>> GetByClientId(int clientId);
        Task<IEnumerable<ReviewDTO>> GetByCountryId(int countryId);
        Task<IEnumerable<ReviewDTO>> GetByReviewImageId(long reviewImageId);
        Task<IEnumerable<ReviewDTO>> GetByReviewCaptionSubstring(string reviewCaptionSubstring);
        Task<IEnumerable<ReviewDTO>> GetByReviewTextSubstring(string reviewTextSubstring);
        Task<IEnumerable<ReviewDTO>> GetByRatingDiapazone(short start, short end);
        Task<IEnumerable<ReviewDTO>> GetByCreationDateDiapazone(DateTime start, DateTime end);
        Task Create(ReviewDTO review);
        Task Update(ReviewDTO review);
        Task Delete(long id);
    }
}
