using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/HotelConfiguration")]
    [ApiController]
    public class HotelConfigurationController : ControllerBase
    {
        private readonly IHotelConfigurationService _serv;
        public HotelConfigurationController(IHotelConfigurationService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelConfigurationDTO>>> GetHotelConfigurations([FromQuery] HotelConfigurationQuery hotelConfigurationQuery)
        {
            try
            {
                IEnumerable<HotelConfigurationDTO> collection = null;
                switch (hotelConfigurationQuery.SearchParameter)
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
                            if (hotelConfigurationQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано HotelConfigurationId для пошуку!", nameof(hotelConfigurationQuery.Id));
                            }
                            var acc = await _serv.GetById((int)hotelConfigurationQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<HotelConfigurationDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByHotelId":
                        {
                            if (hotelConfigurationQuery.HotelId is null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(hotelConfigurationQuery.HotelId));
                            }
                            collection = await _serv.GetByHotelId((int)hotelConfigurationQuery.HotelId);
                        }
                        break;
                    case "GetByCompassSide":
                        {
                            if (hotelConfigurationQuery.CompassSide is null)
                            {
                                throw new ValidationException("Не вказано CompassSide для пошуку!", nameof(hotelConfigurationQuery.CompassSide));
                            }
                            collection = await _serv.GetByCompassSideSubstring(hotelConfigurationQuery.CompassSide);
                        }
                        break;
                    case "GetByWindowView":
                        {
                            if (hotelConfigurationQuery.WindowView is null)
                            {
                                throw new ValidationException("Не вказано WindowView для пошуку!", nameof(hotelConfigurationQuery.WindowView));
                            }
                            collection = await _serv.GetByWindowViewSubstring(hotelConfigurationQuery.WindowView);
                        }
                        break;
                    case "GetByIsAllowChildren":
                        {
                            if (hotelConfigurationQuery.IsAllowChildren is null)
                            {
                                throw new ValidationException("Не вказано IsAllowChildren для пошуку!", nameof(hotelConfigurationQuery.IsAllowChildren));
                            }
                            collection = await _serv.GetByIsAllowChildren((bool)hotelConfigurationQuery.IsAllowChildren);
                        }
                        break;
                    case "GetByIsAllowPets":
                        {
                            if (hotelConfigurationQuery.IsAllowPets is null)
                            {
                                throw new ValidationException("Не вказано IsAllowPets для пошуку!", nameof(hotelConfigurationQuery.IsAllowPets));
                            }
                            collection = await _serv.GetByIsAllowPets((bool)hotelConfigurationQuery.IsAllowPets);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(hotelConfigurationQuery.HotelId, hotelConfigurationQuery.CompassSide, hotelConfigurationQuery.WindowView, hotelConfigurationQuery.IsAllowChildren, hotelConfigurationQuery.IsAllowPets);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Неправильно вказаний параметр пошуку!", nameof(hotelConfigurationQuery.SearchParameter));
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
        public async Task<ActionResult<HotelConfigurationDTO>> CreateHotelConfiguration(HotelConfigurationDTO hotelConfigurationDTO)
        {
            try
            {
                var dto = await _serv.Create(hotelConfigurationDTO);
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
        public async Task<ActionResult<HotelConfigurationDTO>> UpdateHotelConfiguration(HotelConfigurationDTO hotelConfigurationDTO)
        {
            try
            {
                var dto = await _serv.Update(hotelConfigurationDTO);
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
        public async Task<ActionResult<HotelConfigurationDTO>> DeleteHotelConfiguration(int id)
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

    public class HotelConfigurationQuery
    {
        public string SearchParameter { get; set; }
        public int? Id { get; set; }
        public string? CompassSide { get; set; }
        public string? WindowView { get; set; }
        public int? HotelId { get; set; }
        public bool? IsAllowChildren { get; set; }
        public bool? IsAllowPets { get; set; }
    }
}
