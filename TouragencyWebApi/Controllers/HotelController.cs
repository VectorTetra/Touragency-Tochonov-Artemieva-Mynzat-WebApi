using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _serv;
        public HotelController(IHotelService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels([FromQuery] HotelQuery hotelQuery)
        {
            try
            {
                IEnumerable<HotelDTO> collection = null;
                switch (hotelQuery.SearchParameter)
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
                            if (hotelQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(hotelQuery.Id));
                            }
                            var acc = await _serv.GetById((int)hotelQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<HotelDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (hotelQuery.Name is null)
                            {
                                throw new ValidationException("Не вказано Name для пошуку!", nameof(hotelQuery.Name));
                            }
                            collection = await _serv.GetByNameSubstring(hotelQuery.Name);
                        }
                        break;
                    case "GetByCountryName":
                        {
                            if (hotelQuery.CountryName is null)
                            {
                                throw new ValidationException("Не вказано CountryName для пошуку!", nameof(hotelQuery.CountryName));
                            }
                            collection = await _serv.GetByCountryNameSubstring(hotelQuery.CountryName);
                        }
                        break;
                    case "GetBySettlementName":
                        {
                            if (hotelQuery.SettlementName is null)
                            {
                                throw new ValidationException("Не вказано SettlementName для пошуку!", nameof(hotelQuery.SettlementName));
                            }
                            collection = await _serv.GetBySettlementNameSubstring(hotelQuery.SettlementName);
                        }
                        break;
                    case "GetByDescription":
                        {
                            if (hotelQuery.Description is null)
                            {
                                throw new ValidationException("Не вказано Description для пошуку!", nameof(hotelQuery.Description));
                            }
                            collection = await _serv.GetByDescriptionSubstring(hotelQuery.Description);
                        }
                        break;
                    case "GetByStars":
                        {
                            if (hotelQuery.Stars is null)
                            {
                                throw new ValidationException("Не вказано Stars для пошуку!", nameof(hotelQuery.Stars));
                            }
                            collection = await _serv.GetByStars((int[])hotelQuery.Stars);
                        }
                        break;

                    case "GetByHotelConfigurationId":
                        {
                            if (hotelQuery.HotelConfigurationId is null)
                            {
                                throw new ValidationException("Не вказано HotelConfigurationId для пошуку!", nameof(hotelQuery.HotelConfigurationId));
                            }
                            collection = await _serv.GetByHotelConfigurationId((int)hotelQuery.HotelConfigurationId);
                        }
                        break;
                    case "GetByBedConfigurationId":
                        {
                            if (hotelQuery.BedConfigurationId is null)
                            {
                                throw new ValidationException("Не вказано BedConfigurationId для пошуку!", nameof(hotelQuery.BedConfigurationId));
                            }
                            collection = await _serv.GetByBedConfigurationId((int)hotelQuery.BedConfigurationId);
                        }
                        break;
                    case "GetBySettlementId":
                        {
                            if (hotelQuery.SettlementId is null)
                            {
                                throw new ValidationException("Не вказано SettlementId для пошуку!", nameof(hotelQuery.SettlementId));
                            }
                            collection = await _serv.GetBySettlementId((int)hotelQuery.SettlementId);
                        }
                        break;
                        case "GetByTourNameId":
                        {
                            if (hotelQuery.TourNameId is null)
                            {
                                throw new ValidationException("Не вказано TourNameId для пошуку!", nameof(hotelQuery.TourNameId));
                            }
                            collection = await _serv.GetByTourNameId((int)hotelQuery.TourNameId);
                        }
                        break;
                        case "GetByTourName":
                        {
                            if (hotelQuery.TourName is null)
                            {
                                throw new ValidationException("Не вказано TourName для пошуку!", nameof(hotelQuery.TourName));
                            }
                            collection = await _serv.GetByTourName(hotelQuery.TourName);
                        }
                        break;
                    case "GetByBookingId":
                        {
                            if (hotelQuery.BookingId is null)
                            {
                                throw new ValidationException("Не вказано BookingId для пошуку!", nameof(hotelQuery.BookingId));
                            }
                            collection = await _serv.GetByBookingId((long)hotelQuery.BookingId);
                        }
                        break;
                    case "GetByHotelServiceId":
                        {
                            if (hotelQuery.HotelServiceId is null)
                            {
                                throw new ValidationException("Не вказано HotelServiceId для пошуку!", nameof(hotelQuery.HotelServiceId));
                            }
                            collection = await _serv.GetByHotelServiceId((int)hotelQuery.HotelServiceId);
                        }
                        break;
                    case "GetByHotelImageId":
                        {
                            if (hotelQuery.HotelImageId is null)
                            {
                                throw new ValidationException("Не вказано HotelImageId для пошуку!", nameof(hotelQuery.HotelImageId));
                            }
                            collection = await _serv.GetByHotelImageId((long)hotelQuery.HotelImageId);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(hotelQuery.Name, hotelQuery.CountryName, hotelQuery.SettlementName, hotelQuery.Description,
                                                               hotelQuery.Stars, hotelQuery.HotelConfigurationId, hotelQuery.BedConfigurationId, hotelQuery.SettlementId, hotelQuery.TourNameId, hotelQuery.TourName,
                                                                                              hotelQuery.BookingId, hotelQuery.HotelServiceId, hotelQuery.HotelImageId);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невірно вказаний параметр пошуку!", nameof(hotelQuery.SearchParameter));
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
        public async Task<ActionResult<HotelDTO>> CreateHotel(HotelDTO hotelDTO)
        {
            try
            {
                var dto = await _serv.Create(hotelDTO);
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
        public async Task<ActionResult<HotelDTO>> UpdateHotel(HotelDTO hotelDTO)
        {
            try
            {
                var dto = await _serv.Update(hotelDTO);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<HotelDTO>> DeleteHotel(int id)
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

    public class HotelQuery
    {
        public string SearchParameter { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? SettlementName { get; set; }
        public string? CountryName { get; set; }
        public string? Description { get; set; }
        public int[]? Stars { get; set; }
        //public virtual Resort Resort { get; set; }
        public int? HotelConfigurationId { get; set; }
        public int? BedConfigurationId { get; set; }
        public int? SettlementId { get; set; }
        // Many-to-many relationship between Hotel and Tour
        public int? TourNameId { get; set; }
        public string? TourName { get; set; }
        // One-to-many relationship between Hotel and Booking
        public long? BookingId { get; set; }
        // В цьому полі можуть зберігатися дані про послуги готелю (наприклад, Wi-Fi, сніданок, басейн, парковка, трансфер)
        // А також дані про модель харчування (наприклад, BB, HB, FB, AI)
        public int? HotelServiceId { get; set; }
        public long? HotelImageId { get; set; }
    }
}
