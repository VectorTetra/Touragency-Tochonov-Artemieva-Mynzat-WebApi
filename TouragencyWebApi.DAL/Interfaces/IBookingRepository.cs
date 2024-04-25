using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAll();
        Task<IEnumerable<Booking>> Get200Last();
        Task<Booking?> GetById(long id);
        Task<IEnumerable<Booking>> GetByTourId(long tourId);
        Task<IEnumerable<Booking>> GetByClientId(int clientId);
        Task<IEnumerable<Booking>> GetByHotelId(int hotelId);
        Task<IEnumerable<Booking>> GetByBookingDataId(long bookingDataId);
        Task Create(Booking booking);
        void Update(Booking booking);
        Task Delete(long id);
        Task<IEnumerable<Booking>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<Booking>> GetByTourNameSubstring(string tourNameSubstring);
        Task<IEnumerable<Booking>> GetByClientFirstnameSubstring(string clientFirstnameSubstring);
        Task<IEnumerable<Booking>> GetByClientLastnameSubstring(string clientLastnameSubstring);
        Task<IEnumerable<Booking>> GetByClientMiddlenameSubstring(string clientMiddlenameSubstring);
        Task<IEnumerable<Booking>> GetByClientPhoneNumberSubstring(string clientPhoneNumberSubstring);
        Task<IEnumerable<Booking>> GetByClientEmailAddressSubstring(string clientEmailAddressSubstring);
        Task<IEnumerable<Booking>> GetByHotelNameSubstring(string hotelNameSubstring);
        Task<IEnumerable<Booking>> GetBySettlementNameSubstring(string settlementNameSubstring);
        Task<IEnumerable<Booking>> GetByCountryNameSubstring(string countryNameSubstring);
        Task<IEnumerable<Booking>> GetByCompositeSearch(long? tourId, int? clientId, int? hotelId, long? bookingDataId, int? tourNameId,
            string? tourNameSubstring, string? clientFirstnameSubstring, string? clientLastnameSubstring, string? clientMiddlenameSubstring, string? clientPhoneNumberSubstring,
            string? clientEmailAddressSubstring, string? hotelNameSubstring, string? settlementNameSubstring, string? countryNameSubstring);
    }
}
