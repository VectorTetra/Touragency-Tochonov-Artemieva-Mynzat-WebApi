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
        Task<IEnumerable<TourImage>> Get200Last();
        Task<TourImage?> GetById(long id);
        Task<IEnumerable<TourImage>> GetByTourId(long tourId);
        Task<IEnumerable<TourImage>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<TourImage>> GetByTourName(string tourName);
        Task<IEnumerable<TourImage>> GetByImageUrlSubstring(string imageUrlSubstring);
        Task<IEnumerable<TourImage>> GetByCountryName(string countryNameSubstring);
        Task<IEnumerable<TourImage>> GetBySettlementName(string settlementNameSubstring);
        Task<IEnumerable<TourImage>> GetByHotelName(string hotelNameSubstring);
        Task<IEnumerable<TourImage>> GetByCompositeSearch(string? tourName,string? imageUrlSubstring, string? countryNameSubstring,
            string? settlementNameSubstring, string? hotelNameSubstring, long? tourId, int? tourNameId);
        Task Create(TourImage tourImage);
        void Update(TourImage tourImage);
        Task Delete(long id);
    }
}
