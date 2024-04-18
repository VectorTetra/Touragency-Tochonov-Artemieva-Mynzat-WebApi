using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITourImageService
    {
        Task<IEnumerable<TourImageDTO>> GetAll();
        Task<TourImageDTO?> GetById(long id);
        Task<IEnumerable<TourImageDTO>> GetByTourId(long tourId);
        Task<IEnumerable<TourImageDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<TourImageDTO>> GetByImageUrlSubstring(string imageUrlSubstring);
        Task Create(TourImageDTO tourImage);
        Task Update(TourImageDTO tourImage);
        Task Delete(long id);
    }
}
