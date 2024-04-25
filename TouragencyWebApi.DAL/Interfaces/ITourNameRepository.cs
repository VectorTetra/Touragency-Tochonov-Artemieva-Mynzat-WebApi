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
        Task<IEnumerable<TourName>> Get200Last();
        Task<TourName?> GetById(int id);
        Task<IEnumerable<TourName>> GetByName(string tourNameSubstring);
        Task<IEnumerable<TourName>> GetByCountryName(string countryNameSubstring);
        Task<IEnumerable<TourName>> GetBySettlementName(string settlementNameSubstring);
        Task<IEnumerable<TourName>> GetByHotelName(string hotelNameSubstring);
        Task<IEnumerable<TourName>> GetByPageJSONStructureUrlSubstring(string pageJSONStructureUrlSubstring);
        Task<IEnumerable<TourName>> GetByTourId(long tourId);
        Task<IEnumerable<TourName>> GetByTourImageId(long tourImageId);
        Task<IEnumerable<TourName>> GetByCompositeSearch(string? tourNameSubstring, string? countryNameSubstring,
            string? settlementNameSubstring, string? hotelNameSubstring, string? pageJSONStructureUrlSubstring, long? tourId, long? tourImageId);

        Task Create(TourName tourName);
        void Update(TourName tourName);
        Task Delete(int id);
    }
}
