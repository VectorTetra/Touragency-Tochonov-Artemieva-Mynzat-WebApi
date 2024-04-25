using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAll();
        Task<IEnumerable<BookingDTO>> Get200Last();
        Task<BookingDTO?> GetById(long id);
        Task<IEnumerable<BookingDTO>> GetByClientId(int clientId);
        Task<IEnumerable<BookingDTO>> GetByHotelId(int hotelId);
        Task<IEnumerable<BookingDTO>> GetByTourId(long tourId); 
        Task<IEnumerable<BookingDTO>> GetByBookingDataId(long bookingDataId);
        Task<BookingDTO> Create(BookingDTO bookingDTO);
        Task<BookingDTO> Update(BookingDTO bookingDTO);
        Task<BookingDTO> Delete(long id);

        Task<IEnumerable<BookingDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<BookingDTO>> GetByTourNameSubstring(string tourNameSubstring);
        Task<IEnumerable<BookingDTO>> GetByClientFirstnameSubstring(string clientFirstnameSubstring);
        Task<IEnumerable<BookingDTO>> GetByClientLastnameSubstring(string clientLastnameSubstring);
        Task<IEnumerable<BookingDTO>> GetByClientMiddlenameSubstring(string clientMiddlenameSubstring);
        Task<IEnumerable<BookingDTO>> GetByClientPhoneNumberSubstring(string clientPhoneNumberSubstring);
        Task<IEnumerable<BookingDTO>> GetByClientEmailAddressSubstring(string clientEmailAddressSubstring);
        Task<IEnumerable<BookingDTO>> GetByHotelNameSubstring(string hotelNameSubstring);
        Task<IEnumerable<BookingDTO>> GetBySettlementNameSubstring(string settlementNameSubstring);
        Task<IEnumerable<BookingDTO>> GetByCountryNameSubstring(string countryNameSubstring);
        Task<IEnumerable<BookingDTO>> GetByCompositeSearch(long? tourId, int? clientId, int? hotelId, long? bookingDataId, int? tourNameId,
            string? tourNameSubstring, string? clientFirstnameSubstring, string? clientLastnameSubstring, string? clientMiddlenameSubstring, string? clientPhoneNumberSubstring,
            string? clientEmailAddressSubstring, string? hotelNameSubstring, string? settlementNameSubstring, string? countryNameSubstring);
    }
}
