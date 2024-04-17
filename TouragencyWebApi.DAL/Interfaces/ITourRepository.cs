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
        Task<Tour?> GetById(long id);
        Task<IEnumerable<Tour>> GetByTourName(TourName tourName);
        Task<IEnumerable<Tour>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<Tour>> GetByTourNameStringName(string tourNameSubstring);
        Task<IEnumerable<Tour>> GetByCountry(Country country);
        Task<IEnumerable<Tour>> GetByCountryId(int countryid);
        Task<IEnumerable<Tour>> GetByCountryName(string countryNameSubstring);
        Task<IEnumerable<Tour>> GetBySettlement(Settlement settlement);
        Task<IEnumerable<Tour>> GetBySettlementId(int settlementId);
        Task<IEnumerable<Tour>> GetBySettlementName(string settlementNameSubstring);
        Task<IEnumerable<Tour>> GetByHotel(Hotel hotel);
        Task<IEnumerable<Tour>> GetByHotelName(string hotelNameSubstring);
        Task<IEnumerable<Tour>> GetByHotelId(int hotelId);
        Task<IEnumerable<Tour>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Tour>> GetByTourDuration(params int[] durationDays);
        Task<IEnumerable<Tour>> GetByHotelServicesIds(params int[] hotelServicesIds); 
        Task<IEnumerable<Tour>> GetByTransportType(TransportType transportType);
        Task<IEnumerable<Tour>> GetByTransportTypeId(int id);
        Task<IEnumerable<Tour>> GetByTransportTypeName(string transportTypeNameSubstring);
        Task Create(Tour tour);
        void Update(Tour tour);
        Task Delete(int id);
    }
}
