using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/HotelService")]
    [ApiController]
    public class HotelServiceController : ControllerBase
    {
        private readonly IHotelServiceService _serv;
        public HotelServiceController(IHotelServiceService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelServiceDTO>>> GetHotelServices([FromQuery] HotelServiceQuery hotelServiceQuery)
        {
            try
            {
                IEnumerable<HotelServiceDTO> collection = null;
                switch (hotelServiceQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (hotelServiceQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано HotelServiceId для пошуку!", nameof(hotelServiceQuery.Id));
                            }
                            var acc = await _serv.GetById((int)hotelServiceQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<HotelServiceDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByDescriptionSubstring":
                        {
                            if (hotelServiceQuery.Description is null)
                            {
                                throw new ValidationException("Не вказано Description для пошуку!", nameof(hotelServiceQuery.Description));
                            }
                            collection = await _serv.GetByDescriptionSubstring(hotelServiceQuery.Description);
                        }
                        break;
                    case "GetByNameSubstring":
                        {
                            if (hotelServiceQuery.Name is null)
                            {
                                throw new ValidationException("Не вказано Name для пошуку!", nameof(hotelServiceQuery.Name));
                            }
                            collection = await _serv.GetByNameSubstring(hotelServiceQuery.Name);
                        }
                        break;
                    case "GetByHotelServiceTypeId":
                        {
                            if (hotelServiceQuery.HotelServiceTypeId is null)
                            {
                                throw new ValidationException("Не вказано HotelServiceTypeId для пошуку!", nameof(hotelServiceQuery.HotelServiceTypeId));
                            }
                            collection = await _serv.GetByHotelServiceTypeId((int)hotelServiceQuery.HotelServiceTypeId);
                        }
                        break;
                    case "GetByHotelId":
                        {
                            if (hotelServiceQuery.HotelId is null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(hotelServiceQuery.HotelId));
                            }
                            collection = await _serv.GetByHotelId((int)hotelServiceQuery.HotelId);
                        }
                        break;
                    default:
                        {

                            throw new ValidationException("Невідомий параметр пошуку!", nameof(hotelServiceQuery.SearchParameter));
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
        public async Task<ActionResult<HotelServiceDTO>> CreateHotelService(HotelServiceDTO hotelServiceDTO)
        {
            try
            {
                await _serv.Create(hotelServiceDTO);
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

        [HttpPut]
        public async Task<ActionResult<HotelServiceDTO>> UpdateHotelService(HotelServiceDTO hotelServiceDTO)
        {
            try
            {
                await _serv.Update(hotelServiceDTO);
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

        [HttpDelete]
        public async Task<ActionResult> DeleteHotelService([FromQuery] int id)
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

    public class HotelServiceQuery
    {
        public string SearchParameter { get; set; }
        public int? Id { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public int? HotelServiceTypeId { get; set; }
        public int? HotelId { get; set; }
    }
}
