using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _serv;
        public BookingController(IBookingService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings([FromQuery] BookingQuery bookingQuery)
        {
            try
            {
                IEnumerable<BookingDTO> collection = null;
                switch (bookingQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "Get200Last":
                        {
                            collection = await _serv.Get200Last();
                        }
                        break;
                    case "GetById":
                        {
                            if (bookingQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано BookingId для пошуку!", nameof(bookingQuery.Id));
                            }
                            var acc = await _serv.GetById((long)bookingQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<BookingDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByClientId":
                        {
                            if (bookingQuery.ClientId is null)
                            {
                                throw new ValidationException("Не вказано ClientId для пошуку!", nameof(bookingQuery.ClientId));
                            }
                            collection = await _serv.GetByClientId((int)bookingQuery.ClientId);
                        }
                        break;
                    case "GetByHotelId":
                        {
                            if (bookingQuery.HotelId is null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(bookingQuery.HotelId));
                            }
                            collection = await _serv.GetByHotelId((int)bookingQuery.HotelId);
                        }
                        break;
                    case "GetByTourId":
                        {
                            if (bookingQuery.TourId is null)
                            {
                                throw new ValidationException("Не вказано TourId для пошуку!", nameof(bookingQuery.TourId));
                            }
                            collection = await _serv.GetByTourId((long)bookingQuery.TourId);
                        }
                        break;
                    case "GetByBookingDataId":
                        {
                            if (bookingQuery.BookingDataId is null)
                            {
                                throw new ValidationException("Не вказано BookingDataId для пошуку!", nameof(bookingQuery.BookingDataId));
                            }
                            collection = await _serv.GetByBookingDataId((long)bookingQuery.BookingDataId);
                        }
                        break;
                    case "GetByTourNameId":
                        {
                            if (bookingQuery.TourNameId is null)
                            {
                                throw new ValidationException("Не вказано TourNameId для пошуку!", nameof(bookingQuery.TourNameId));
                            }
                            collection = await _serv.GetByTourNameId((int)bookingQuery.TourNameId);
                        }
                        break;
                    case "GetByTourNameSubstring":
                        {
                            if (bookingQuery.TourNameSubstring is null)
                            {
                                throw new ValidationException("Не вказано TourNameSubstring для пошуку!", nameof(bookingQuery.TourNameSubstring));
                            }
                            collection = await _serv.GetByTourNameSubstring(bookingQuery.TourNameSubstring);
                        }
                        break;
                    case "GetByClientFirstnameSubstring":
                        {
                            if (bookingQuery.ClientFirstnameSubstring is null)
                            {
                                throw new ValidationException("Не вказано ClientFirstnameSubstring для пошуку!", nameof(bookingQuery.ClientFirstnameSubstring));
                            }
                            collection = await _serv.GetByClientFirstnameSubstring(bookingQuery.ClientFirstnameSubstring);
                        }
                        break;
                    case "GetByClientLastnameSubstring":
                        {
                            if (bookingQuery.ClientLastnameSubstring is null)
                            {
                                throw new ValidationException("Не вказано ClientLastnameSubstring для пошуку!", nameof(bookingQuery.ClientLastnameSubstring));
                            }
                            collection = await _serv.GetByClientLastnameSubstring(bookingQuery.ClientLastnameSubstring);
                        }
                        break;
                    case "GetByClientMiddlenameSubstring":
                        {
                            if (bookingQuery.ClientMiddlenameSubstring is null)
                            {
                                throw new ValidationException("Не вказано ClientMiddlenameSubstring для пошуку!", nameof(bookingQuery.ClientMiddlenameSubstring));
                            }
                            collection = await _serv.GetByClientMiddlenameSubstring(bookingQuery.ClientMiddlenameSubstring);
                        }
                        break;
                    case "GetByClientPhoneNumberSubstring":
                        {
                            if (bookingQuery.ClientPhoneNumberSubstring is null)
                            {
                                throw new ValidationException("Не вказано ClientPhoneNumberSubstring для пошуку!", nameof(bookingQuery.ClientPhoneNumberSubstring));
                            }
                            collection = await _serv.GetByClientPhoneNumberSubstring(bookingQuery.ClientPhoneNumberSubstring);
                        }
                        break;
                    case "GetByClientEmailAddressSubstring":
                        {
                            if (bookingQuery.ClientEmailAddressSubstring is null)
                            {
                                throw new ValidationException("Не вказано ClientEmailAddressSubstring для пошуку!", nameof(bookingQuery.ClientEmailAddressSubstring));
                            }
                            collection = await _serv.GetByClientEmailAddressSubstring(bookingQuery.ClientEmailAddressSubstring);
                        }
                        break;
                    case "GetByHotelNameSubstring":
                        {
                            if (bookingQuery.HotelNameSubstring is null)
                            {
                                throw new ValidationException("Не вказано HotelNameSubstring для пошуку!", nameof(bookingQuery.HotelNameSubstring));
                            }
                            collection = await _serv.GetByHotelNameSubstring(bookingQuery.HotelNameSubstring);
                        }
                        break;
                    case "GetBySettlementNameSubstring":
                        {
                            if (bookingQuery.SettlementNameSubstring is null)
                            {
                                throw new ValidationException("Не вказано SettlementNameSubstring для пошуку!", nameof(bookingQuery.SettlementNameSubstring));
                            }
                            collection = await _serv.GetBySettlementNameSubstring(bookingQuery.SettlementNameSubstring);
                        }
                        break;
                    case "GetByCountryNameSubstring":
                        {
                            if (bookingQuery.CountryNameSubstring is null)
                            {
                                throw new ValidationException("Не вказано CountryNameSubstring для пошуку!", nameof(bookingQuery.CountryNameSubstring));
                            }
                            collection = await _serv.GetByCountryNameSubstring(bookingQuery.CountryNameSubstring);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(bookingQuery.TourId, bookingQuery.ClientId, bookingQuery.HotelId, bookingQuery.BookingDataId, bookingQuery.TourNameId,
                                                               bookingQuery.TourNameSubstring, bookingQuery.ClientFirstnameSubstring, bookingQuery.ClientLastnameSubstring, bookingQuery.ClientMiddlenameSubstring, bookingQuery.ClientPhoneNumberSubstring,
                                                                                              bookingQuery.ClientEmailAddressSubstring, bookingQuery.HotelNameSubstring, bookingQuery.SettlementNameSubstring, bookingQuery.CountryNameSubstring);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невірно вказаний параметр пошуку!", nameof(bookingQuery.SearchParameter));
                        }
                }
                if (collection.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return collection?.ToList();
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookingDTO>> CreateBooking(BookingDTO bookingDTO)
        {
            try
            {
                var dto = await _serv.Create(bookingDTO);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<BookingDTO>> UpdateBooking(BookingDTO bookingDTO)
        {
            try
            {
                var dto = await _serv.Update(bookingDTO);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BookingDTO>> DeleteBooking(long id)
        {
            try
            {
                var dto = await _serv.Delete(id);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
    public class BookingQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? Id { get; set; }
        public int? ClientId { get; set; }
        public int? HotelId { get; set; }
        public long? TourId { get; set; }
        public long? BookingDataId { get; set; }
        public int? TourNameId { get; set; }
        public string? TourNameSubstring { get; set; }
        public string? ClientFirstnameSubstring { get; set; }
        public string? ClientLastnameSubstring { get; set; }
        public string? ClientMiddlenameSubstring { get; set; }
        public string? ClientPhoneNumberSubstring { get; set; }
        public string? ClientEmailAddressSubstring { get; set; }
        public string? HotelNameSubstring { get; set; }
        public string? SettlementNameSubstring { get; set; }
        public string? CountryNameSubstring { get; set; }
    }

}
