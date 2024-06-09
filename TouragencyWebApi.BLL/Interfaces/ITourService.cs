using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<TourDTO>> GetAll();
        Task<IEnumerable<TourDTO>> Get200Last();
        Task<TourDTO?> GetById(long id);
        Task<IEnumerable<TourDTO>> GetByTourName(TourName tourName);
        Task<IEnumerable<TourDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<TourDTO>> GetByTourNameStringName(string tourNameSubstring);
        Task<IEnumerable<TourDTO>> GetByCountry(Country country);
        Task<IEnumerable<TourDTO>> GetByCountryId(int countryid);
        Task<IEnumerable<TourDTO>> GetByCountryName(string countryNameSubstring);
        Task<IEnumerable<TourDTO>> GetByContinentId(int continentId);
        Task<IEnumerable<TourDTO>> GetByContinentName(string continentName);
        Task<IEnumerable<TourDTO>> GetByStars(int[] stars);
        Task<IEnumerable<TourDTO>> GetBySettlement(Settlement settlement);
        Task<IEnumerable<TourDTO>> GetBySettlementId(int settlementId);
        Task<IEnumerable<TourDTO>> GetBySettlementName(string settlementNameSubstring);
        Task<IEnumerable<TourDTO>> GetByHotel(Hotel hotel);
        Task<IEnumerable<TourDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<TourDTO>> GetByHotelName(string hotelNameSubstring);
        Task<IEnumerable<TourDTO>> GetByTransportType(TransportType transportType);
        Task<IEnumerable<TourDTO>> GetByTransportTypeId(int id);
        Task<IEnumerable<TourDTO>> GetByTransportTypeName(string transportTypeName);
        Task<IEnumerable<TourDTO>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TourDTO>> GetByTourDuration(params int[] durationDays);
        Task<IEnumerable<TourDTO>> GetByHotelServicesIds(params int[] hotelServicesIds);
        Task<IEnumerable<TourDTO>> GetByTourStateId(int tourStateId);
        Task<IEnumerable<TourDTO>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<TourDTO>> GetByClientFirstname(string clientFirstname);
        Task<IEnumerable<TourDTO>> GetByClientLastname(string clientLastname);
        Task<IEnumerable<TourDTO>> GetByClientMiddlename(string clientMiddlename);
        Task<IEnumerable<TourDTO>> GetByClientId(int clientId);
        Task<IEnumerable<TourDTO>> GetByCompositeSearch(int? tourNameId, int? countryid, int? settlementId, int? hotelId,
            DateTime? startDate, DateTime? endDate, int[]? durationDays, int[]? hotelServicesIds, int? transportTypeId, int? tourStateId,
            string? touristNickname, string? clientFirstname, string? clientLastname, string? clientMiddlename, string? countryName, string? settlementName, 
            string? hotelName, int? continentId, string? continentName, int[]? stars, int? clientId);
        Task<TourDTO> Create(TourDTO tour);
        Task<TourDTO> Update(TourDTO tour);
        Task<TourDTO> Delete(long id);
    }
}
