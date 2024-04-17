using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<TourDTO>> GetAll(TourDTO searchTourDTO);
        Task<TourDTO?> GetById(long id);
        Task<IEnumerable<TourDTO>> GetByExtendedSearch(TourDTO searchTourDTO);
        Task<IEnumerable<TourDTO>> GetByTourName(TourName tourName);
        Task<IEnumerable<TourDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<TourDTO>> GetByTourNameStringName(string tourNameSubstring);
        Task<IEnumerable<TourDTO>> GetByCountry(Country country);
        Task<IEnumerable<TourDTO>> GetByCountryId(int countryid);
        Task<IEnumerable<TourDTO>> GetByCountryName(string countryNameSubstring);
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
        Task Create(TourDTO tour);
        void Update(TourDTO tour);
        Task Delete(int id);
    }
}
