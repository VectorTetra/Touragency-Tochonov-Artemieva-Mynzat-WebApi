using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetAll();
        Task<IEnumerable<Tour>> Get200Last();
        Task<Tour?> GetById(long id);
        Task<IEnumerable<Tour>> GetByTourName(TourName tourName);
        Task<IEnumerable<Tour>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<Tour>> GetByTourNameStringName(string tourNameSubstring);
        Task<IEnumerable<Tour>> GetByCountry(Country country);
        Task<IEnumerable<Tour>> GetByCountryId(int countryid);
        Task<IEnumerable<Tour>> GetByContinentId(int continentId);
        Task<IEnumerable<Tour>> GetByContinentName(string continentName);
        Task<IEnumerable<Tour>> GetByCountryName(string countryNameSubstring);
        Task<IEnumerable<Tour>> GetByStars(int[] stars);
        Task<IEnumerable<Tour>> GetBySettlement(Settlement settlement);
        Task<IEnumerable<Tour>> GetBySettlementId(int settlementId);
        Task<IEnumerable<Tour>> GetBySettlementName(string settlementNameSubstring);
        Task<IEnumerable<Tour>> GetByHotel(Hotel hotel);
        Task<IEnumerable<Tour>> GetByHotelName(string hotelNameSubstring);
        Task<IEnumerable<Tour>> GetByHotelId(int hotelId);
        Task<IEnumerable<Tour>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Tour>> GetByTourDuration(int[] durationDays);
        Task<IEnumerable<Tour>> GetByHotelServicesIds(int[] hotelServicesIds); 
        Task<IEnumerable<Tour>> GetByTransportType(TransportType transportType);
        Task<IEnumerable<Tour>> GetByTransportTypeId(int id);
        Task<IEnumerable<Tour>> GetByTransportTypeName(string transportTypeNameSubstring);
        Task<IEnumerable<Tour>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<Tour>> GetByClientFirstname(string clientFirstname);
        Task<IEnumerable<Tour>> GetByClientLastname(string clientLastname);
        Task<IEnumerable<Tour>> GetByClientMiddlename(string clientMiddlename);
        Task<IEnumerable<Tour>> GetByClientId(int clientId);
        Task<IEnumerable<Tour>> GetByTourStateId(int tourStateId);
        Task<IEnumerable<Tour>> GetByCompositeSearch(int? tourNameId, int? countryid, int? settlementId, int? hotelId,
            DateTime? startDate, DateTime? endDate, int[]? durationDays, int[]? hotelServicesIds, int? transportTypeId, int? tourStateId,
            string? touristNickname, string? clientFirstname, string? clientLastname, string? clientMiddlename, string? countryName, string? settlementName, 
            string? hotelName, int? continentId, string? continentName, int[]? stars, int? clientId);
        Task Create(Tour tour);
        void Update(Tour tour);
        Task Delete(long id);
    }
}
