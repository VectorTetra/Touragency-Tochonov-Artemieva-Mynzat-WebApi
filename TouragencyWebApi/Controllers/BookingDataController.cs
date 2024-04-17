using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.DTO;
using Microsoft.IdentityModel.Tokens;

namespace TouragencyWebApi.Controllers
{
    [Route("api/BookingData")]
    [ApiController]
    public class BookingDataController : ControllerBase
    {
        private readonly IBookingDataService _serv;
        public BookingDataController(IBookingDataService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDataDTO>>> GetBookingDatas([FromQuery] BookingDataQuery bookingDataQuery)
        {
            try
            {
                IEnumerable<BookingDataDTO?> collection = null;
                switch (bookingDataQuery.SearchParameter)
                {
                    case "GetAll":
                        { collection = await _serv.GetAll(); }
                        break;
                    case "GetById":
                        {
                            if (bookingDataQuery.Id == null)
                            {
                                throw new ValidationException("Не вказано BookingDataId для пошуку!", nameof(bookingDataQuery.Id));
                            }
                            var acc = await _serv.GetById((long)bookingDataQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<BookingDataDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByBookingId":
                        {
                            if (bookingDataQuery.BookingId == null)
                            {
                                throw new ValidationException("Не вказано BookingId для пошуку!", nameof(bookingDataQuery.BookingId));
                            }
                            collection = await _serv.GetByBookingId((long)bookingDataQuery.BookingId);
                        }
                        break;
                    case "GetByRoomNumber":
                        {
                            if (bookingDataQuery.RoomNumber == null)
                            {
                                throw new ValidationException("Не вказано RoomNumber для пошуку!", nameof(bookingDataQuery.RoomNumber));
                            }
                            collection = await _serv.GetByRoomNumber((int)bookingDataQuery.RoomNumber);
                        }
                        break;
                    case "GetByDateDiapazon":
                        {
                            if (bookingDataQuery.dateBeginPeriod == null || bookingDataQuery.dateEndPeriod == null)
                            {
                                throw new ValidationException("Не вказано DateBeginPeriod або DateEndPeriod для пошуку!", nameof(bookingDataQuery.dateBeginPeriod));
                            }
                            collection = await _serv.GetByDateDiapazon((DateTime)bookingDataQuery.dateBeginPeriod, (DateTime)bookingDataQuery.dateEndPeriod);
                        }
                        break;
                    case "GetByTotalPriceDiapazon":
                        {
                            if (bookingDataQuery.priceMinValue == null)
                            {
                                throw new ValidationException("Не вказано PriceMinValue для пошуку!", nameof(bookingDataQuery.priceMinValue));
                            }
                            collection = await _serv.GetByTotalPriceDiapazon((int)bookingDataQuery.priceMinValue, (int)bookingDataQuery.priceMaxValue);
                        }
                        break;

                    case "GetByAdultsCount":
                        {
                            if (bookingDataQuery.adultsCount == null)
                            {
                                throw new ValidationException("Не вказано AdultsCount для пошуку!", nameof(bookingDataQuery.adultsCount));
                            }
                            collection = await _serv.GetByAdultsCount((short)bookingDataQuery.adultsCount);
                        }
                        break;
                    case "GetByBookingChildrenId":
                        {
                            if (bookingDataQuery.BookingChildrenId == null)
                            {
                                throw new ValidationException("Не вказано BookingChildrenId для пошуку!", nameof(bookingDataQuery.BookingChildrenId));
                            }
                            collection = await _serv.GetByBookingChildrenId((long)bookingDataQuery.BookingChildrenId);
                        }
                        break;
                    case "GetByBedConfigurationId":
                        {
                            if (bookingDataQuery.BedConfigurationId == null)
                            {
                                throw new ValidationException("Не вказано BedConfigurationId для пошуку!", nameof(bookingDataQuery.BedConfigurationId));
                            }
                            collection = await _serv.GetByBedConfigurationId((int)bookingDataQuery.BedConfigurationId);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невірно вказаний параметр пошуку bookingDataQuery.SearchParameter!", nameof(bookingDataQuery.SearchParameter));
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
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookingDataDTO>> CreateBookingData(BookingDataDTO bookingDataDTO)
        {
            try
            {
                await _serv.Create(bookingDataDTO);
                return Ok(bookingDataDTO);
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<BookingDataDTO>> UpdateBookingData(BookingDataDTO bookingDataDTO)
        {
            try
            {
                await _serv.Update(bookingDataDTO);
                return Ok(bookingDataDTO);
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBookingData(long id)
        {
            try
            {
                await _serv.Delete(id);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }
       
    }
    public class BookingDataQuery
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? BookingId { get; set; }
        public int? RoomNumber { get; set; }
        public DateTime? dateBeginPeriod { get; set; }
        public DateTime? dateEndPeriod { get; set; }
        public int? priceMinValue { get; set; }
        public int? priceMaxValue { get; set; }
        public short? adultsCount { get; set; }
        public long? BookingChildrenId { get; set; }
        public int? BedConfigurationId { get; set; }

    }
}
