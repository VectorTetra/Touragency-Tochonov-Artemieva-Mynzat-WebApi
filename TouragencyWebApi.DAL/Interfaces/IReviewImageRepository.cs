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
        Task<ReviewImage?> GetById(long id);
        Task<IEnumerable<ReviewImage>> GetByReviewId(long reviewId);
        Task<IEnumerable<ReviewImage>> GetByImagePathSubstring(string imagePathSubstring);
        Task Create(ReviewImage reviewImage);
        void Update(ReviewImage reviewImage);
        Task Delete(long id);
    }
}
