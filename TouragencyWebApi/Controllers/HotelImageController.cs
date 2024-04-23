using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/HotelImage")]
    [ApiController]
    public class HotelImageController : ControllerBase
    {
        private readonly IHotelImageService _serv;
        public HotelImageController(IHotelImageService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelImageDTO>>> GetHotelImages([FromQuery] HotelImageQuery hotelImageQuery)
        {
            try
            {
                IEnumerable<HotelImageDTO> collection = null;
                switch (hotelImageQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (hotelImageQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано Id для пошуку!", nameof(hotelImageQuery.Id));
                            }
                            var acc = await _serv.GetById((long)hotelImageQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<HotelImageDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByHotelId":
                        {
                            if (hotelImageQuery.HotelId is null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(hotelImageQuery.HotelId));
                            }
                            collection = await _serv.GetByHotelId((int)hotelImageQuery.HotelId);
                        }
                        break;
                    case "GetByImageUrl":
                        {
                            if (hotelImageQuery.ImageUrl is null)
                            {
                                throw new ValidationException("Не вказано ImageUrl для пошуку!", nameof(hotelImageQuery.ImageUrl));
                            }
                            collection = await _serv.GetByImageUrlSubstring(hotelImageQuery.ImageUrl);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невідомий параметр пошуку!", nameof(hotelImageQuery.SearchParameter));
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
        public async Task<ActionResult<HotelImageDTO>> CreateHotelImage(HotelImageDTO hotelImageDTO)
        {
            try
            {
                var dto = await _serv.Create(hotelImageDTO);
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
        public async Task<ActionResult<HotelImageDTO>> UpdateHotelImage(HotelImageDTO hotelImageDTO)
        {
            try
            {
                var dto = await _serv.Update(hotelImageDTO);
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
        public async Task<ActionResult<HotelImageDTO>> DeleteHotelImage(long id)
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

    public class HotelImageQuery
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? ImageUrl { get; set; }
        public int? HotelId { get; set; }
    }
}
