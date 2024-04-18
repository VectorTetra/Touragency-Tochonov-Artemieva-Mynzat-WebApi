using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITourImageRepository
    {
        Task<IEnumerable<TourImage>> GetAll();
        Task<TourImage?> GetById(long id);
        Task<IEnumerable<TourImage>> GetByTourId(long tourId);
        Task<IEnumerable<TourImage>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<TourImage>> GetByImageUrlSubstring(string imageUrlSubstring);
        Task Create(TourImage tourImage);
        void Update(TourImage tourImage);
        Task Delete(long id);
    }
}
