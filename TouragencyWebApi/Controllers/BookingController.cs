using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult> CreateBooking(BookingDTO bookingDTO)
        {
            try
            {
                await _serv.Create(bookingDTO);
                return Ok();
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
        public async Task<ActionResult> UpdateBooking(BookingDTO bookingDTO)
        {
            try
            {
                await _serv.Update(bookingDTO);
                return Ok();
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
        public async Task<ActionResult> DeleteBooking(long id)
        {
            try
            {
                await _serv.Delete(id);
                return Ok();
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
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public int? ClientId { get; set; }
        public int? HotelId { get; set; }
        public long? TourId { get; set; }
        public long? BookingDataId { get; set; }
    }
}
