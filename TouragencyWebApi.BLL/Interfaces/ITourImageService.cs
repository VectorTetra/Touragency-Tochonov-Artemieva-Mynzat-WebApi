using TouragencyWebApi.BLL.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITourImageService
    {
        Task<IEnumerable<TourImageDTO>> GetAll();
        Task<IEnumerable<TourImageDTO>> Get200Last();
        Task<TourImageDTO?> GetById(long id);
        Task<IEnumerable<TourImageDTO>> GetByTourId(long tourId);
        Task<IEnumerable<TourImageDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<TourImageDTO>> GetByTourName(string tourName);
        Task<IEnumerable<TourImageDTO>> GetByImageUrlSubstring(string imageUrlSubstring);
        Task<IEnumerable<TourImageDTO>> GetByCountryName(string countryNameSubstring);
        Task<IEnumerable<TourImageDTO>> GetBySettlementName(string settlementNameSubstring);
        Task<IEnumerable<TourImageDTO>> GetByHotelName(string hotelNameSubstring);
        Task<IEnumerable<TourImageDTO>> GetByCompositeSearch(string? tourName, string? imageUrlSubstring, string? countryNameSubstring,
            string? settlementNameSubstring, string? hotelNameSubstring, long? tourId, int? tourNameId);
        Task<TourImageDTO> Create(TourImageDTO tourImage);
        Task<TourImageDTO> Update(TourImageDTO tourImage);
        Task<TourImageDTO> Delete(long id);
    }
}
