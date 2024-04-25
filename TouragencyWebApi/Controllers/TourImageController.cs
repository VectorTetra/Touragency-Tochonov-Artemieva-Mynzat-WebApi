using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/TourImage")]
    [ApiController]
    public class TourImageController : ControllerBase
    {
        private readonly ITourImageService _serv;
        public TourImageController(ITourImageService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourImageDTO>>> GetTourImages([FromQuery] TourImageQuery tourImageQuery)
        {
            try
            {
                IEnumerable<TourImageDTO> collection = null;
                switch (tourImageQuery.SearchParameter)
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
                            if (tourImageQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано TourImageId для пошуку!", nameof(tourImageQuery.Id));
                            }
                            var acc = await _serv.GetById((long)tourImageQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<TourImageDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByImageUrl":
                        {
                            if (tourImageQuery.ImageUrl is null)
                            {
                                throw new ValidationException("Не вказано ImageUrl для пошуку!", nameof(tourImageQuery.ImageUrl));
                            }
                            collection = await _serv.GetByImageUrlSubstring(tourImageQuery.ImageUrl);
                        }
                        break;
                    case "GetByTourNameId":
                        {
                            if (tourImageQuery.TourNameId is null)
                            {
                                throw new ValidationException("Не вказано TourNameId для пошуку!", nameof(tourImageQuery.TourNameId));
                            }
                            collection = await _serv.GetByTourNameId((int)tourImageQuery.TourNameId);
                        }
                        break;
                    case "GetByTourName":
                        {
                            if (tourImageQuery.TourName is null)
                            {
                                throw new ValidationException("Не вказано TourName для пошуку!", nameof(tourImageQuery.TourName));
                            }
                            collection = await _serv.GetByTourName(tourImageQuery.TourName);
                        }
                        break;
                    case "GetByCountryName":
                        {
                            if (tourImageQuery.CountryName is null)
                            {
                                throw new ValidationException("Не вказано CountryName для пошуку!", nameof(tourImageQuery.CountryName));
                            }
                            collection = await _serv.GetByCountryName(tourImageQuery.CountryName);
                        }
                        break;
                    case "GetBySettlementName":
                        {
                            if (tourImageQuery.SettlementName is null)
                            {
                                throw new ValidationException("Не вказано SettlementName для пошуку!", nameof(tourImageQuery.SettlementName));
                            }
                            collection = await _serv.GetBySettlementName(tourImageQuery.SettlementName);
                        }
                        break;
                    case "GetByHotelName":
                        {
                            if (tourImageQuery.HotelName is null)
                            {
                                throw new ValidationException("Не вказано HotelName для пошуку!", nameof(tourImageQuery.HotelName));
                            }
                            collection = await _serv.GetByHotelName(tourImageQuery.HotelName);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(tourImageQuery.TourName, tourImageQuery.ImageUrl, tourImageQuery.CountryName, tourImageQuery.SettlementName, tourImageQuery.HotelName, tourImageQuery.TourId, tourImageQuery.TourNameId);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невідомий параметр пошуку!", nameof(tourImageQuery.SearchParameter));
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
        public async Task<ActionResult<TourImageDTO>> CreateTourImage(TourImageDTO tourImageDTO)
        {
            try
            {
                var dto = await _serv.Create(tourImageDTO);
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
        public async Task<ActionResult<TourImageDTO>> UpdateTourImage(TourImageDTO tourImageDTO)
        {
            try
            {
                var dto = await _serv.Update(tourImageDTO);
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
        public async Task<ActionResult<TourImageDTO>> DeleteTourImage(long id)
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

    public class TourImageQuery
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public long? TourId { get; set; }
        public string? ImageUrl { get; set; }
        public int? TourNameId { get; set; }
        public string? TourName { get; set; }
        public string? CountryName { get; set; }
        public string? SettlementName { get; set; }
        public string? HotelName { get; set; }
    }
}
