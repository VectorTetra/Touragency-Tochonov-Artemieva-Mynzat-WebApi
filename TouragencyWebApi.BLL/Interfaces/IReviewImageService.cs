using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;


namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IReviewImageService
    {
        Task<IEnumerable<ReviewImageDTO>> GetAll();
        Task<ReviewImageDTO?> GetById(long id);
        Task<IEnumerable<ReviewImageDTO>> GetByReviewId(long reviewId);
        Task<IEnumerable<ReviewImageDTO>> GetByImagePathSubstring(string imagePathSubstring);
        Task Create(ReviewImageDTO reviewImage);
        Task Update(ReviewImageDTO reviewImage);
        Task Delete(long id);
    }
}
