using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDTO>> GetAll();
        Task<IEnumerable<HotelDTO>> Get200Last();
        Task<HotelDTO?> GetById(int id);
        Task<IEnumerable<HotelDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<HotelDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<HotelDTO>> GetByStars(int[] selectedStarsRatings);
        Task<IEnumerable<HotelDTO>> GetByHotelConfigurationId(int hotelConfigurationId);
        Task<IEnumerable<HotelDTO>> GetByBedConfigurationId(int bedConfigurationId);
        Task<IEnumerable<HotelDTO>> GetBySettlementId(int settlementId);
        Task<IEnumerable<HotelDTO>> GetBySettlementIds(int[] settlementIds);
        Task<IEnumerable<HotelDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<HotelDTO>> GetByTourName(string tourName);
        Task<IEnumerable<HotelDTO>> GetByBookingId(long bookingId);
        Task<IEnumerable<HotelDTO>> GetByHotelServiceId(int hotelServiceId);
        Task<IEnumerable<HotelDTO>> GetByHotelImageId(long hotelImageId);
        Task<IEnumerable<HotelDTO>> GetByCountryNameSubstring(string countryNameSubstring);
        Task<IEnumerable<HotelDTO>> GetBySettlementNameSubstring(string settlementNameSubstring);
        Task<IEnumerable<HotelDTO>> GetByCompositeSearch(string? nameSubstring, string? countryNameSubstring, string? settlementNameSubstring, string? descriptionSubstring,
            int[]? stars, int? hotelConfigurationId, int? bedConfigurationId, int? settlementId, int? tourNameId, string? tourName,
            long? bookingId, int? hotelServiceId, long? hotelImageId, int[]? settlementIds);
        Task<HotelDTO> Create(HotelDTO hotel);
        Task<HotelDTO> Update(HotelDTO hotel);
        Task<HotelDTO> Delete(int id);
    }
}
