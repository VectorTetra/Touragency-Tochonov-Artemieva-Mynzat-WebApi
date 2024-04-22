using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/HotelServiceType")]
    [ApiController]
    public class HotelServiceTypeController : ControllerBase
    {
        private readonly IHotelServiceTypeService _serv;
        public HotelServiceTypeController(IHotelServiceTypeService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelServiceTypeDTO>>> GetHotelServiceTypes([FromQuery] HotelServiceTypeQuery hotelServiceTypeQuery)
        {
            try
            {
                IEnumerable<HotelServiceTypeDTO> collection = null;
                switch (hotelServiceTypeQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (hotelServiceTypeQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано HotelServiceTypeId для пошуку!", nameof(hotelServiceTypeQuery.Id));
                            }
                            var acc = await _serv.GetById((int)hotelServiceTypeQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<HotelServiceTypeDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByDescriptionSubstring":
                        {
                            if (hotelServiceTypeQuery.Description is null)
                            {
                                throw new ValidationException("Не вказано Description для пошуку!", nameof(hotelServiceTypeQuery.Description));
                            }
                            collection = await _serv.GetByDescriptionSubstring(hotelServiceTypeQuery.Description);
                        }
                        break;
                    case "GetByHotelServiceId":
                        {
                            if (hotelServiceTypeQuery.HotelServiceId is null)
                            {
                                throw new ValidationException("Не вказано HotelServiceId для пошуку!", nameof(hotelServiceTypeQuery.HotelServiceId));
                            }
                            collection = await _serv.GetByHotelServiceId((int)hotelServiceTypeQuery.HotelServiceId);
                        }
                        break;
                    default:
                        throw new ValidationException("Невідомий параметр пошуку!", nameof(hotelServiceTypeQuery.SearchParameter));
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
        public async Task<ActionResult> CreateHotelServiceType(HotelServiceTypeDTO hotelServiceTypeDTO)
        {
            try
            {
                await _serv.Create(hotelServiceTypeDTO);
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
        public async Task<ActionResult> UpdateHotelServiceType(HotelServiceTypeDTO hotelServiceTypeDTO)
        {
            try
            {
                await _serv.Update(hotelServiceTypeDTO);
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
        public async Task<ActionResult> DeleteHotelServiceType(int id)
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
    public class HotelServiceTypeQuery
    {
        public string SearchParameter { get; set; } = string.Empty;
        public int? Id { get; set; }
        public string? Description { get; set; }
        public int? HotelServiceId { get; set; }
    }
}
