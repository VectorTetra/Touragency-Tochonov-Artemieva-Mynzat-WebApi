using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITourNameRepository
    {
        Task<IEnumerable<TourName>> GetAll();
        Task<TourName?> GetById(int id);
        Task<IEnumerable<TourName>> GetByName(string tourNameSubstring);
        Task<IEnumerable<TourName>> GetByPageJSONStructureUrlSubstring(string pageJSONStructureUrlSubstring);
        Task<IEnumerable<TourName>> GetByTourId(long tourId);
        Task<IEnumerable<TourName>> GetByTourImageId(long tourImageId);
        Task Create(TourName tourName);
        void Update(TourName tourName);
        Task Delete(int id);
    }
}
