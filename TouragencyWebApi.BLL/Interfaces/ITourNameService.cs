using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITourNameService
    {
        Task<IEnumerable<TourNameDTO>> GetAll();
        Task<IEnumerable<TourNameDTO>> Get200Last();
        Task<TourNameDTO?> GetById(int id);
        Task<IEnumerable<TourNameDTO>> GetByName(string tourNameSubstring);
        Task<IEnumerable<TourNameDTO>> GetByContinentName(string continentNameSubstring);  
        Task<IEnumerable<TourNameDTO>> GetByCountryName(string countryNameSubstring);
        Task<IEnumerable<TourNameDTO>> GetBySettlementName(string settlementNameSubstring);
        Task<IEnumerable<TourNameDTO>> GetByHotelName(string hotelNameSubstring);
        Task<IEnumerable<TourNameDTO>> GetByTourId(long tourId);
        Task<IEnumerable<TourNameDTO>> GetByTourImageId(long tourImageId);
        Task<IEnumerable<TourNameDTO>> GetByPageJSONStructureUrlSubstring(string pageJSONStructureUrlSubstring);
        Task<IEnumerable<TourNameDTO>> GetByCompositeSearch(string? tourNameSubstring, string? continentNameSubstring, string? countryNameSubstring,
             string? settlementNameSubstring, string? hotelNameSubstring, string? pageJSONStructureUrlSubstring, long? tourId, long? tourImageId);
        Task<TourNameDTO> Create(TourNameDTO tourNameDTO);
        Task<TourNameDTO> Update(TourNameDTO tourNameDTO);
        Task<TourNameDTO> Delete(int id);
    }
}
