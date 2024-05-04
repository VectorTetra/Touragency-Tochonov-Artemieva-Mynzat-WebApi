using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/TourName")]
    [ApiController]
    public class TourNameController : ControllerBase
    {
        private readonly ITourNameService _serv;

        public TourNameController(ITourNameService serv)
        {
            _serv = serv;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourNameDTO>>> GetTourNames([FromQuery] TourNameQuery tourNameQuery)
        {
            try
            {
                IEnumerable<TourNameDTO> collection = null;
                switch (tourNameQuery.SearchParameter)
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
                            if (tourNameQuery.Id == null)
                            {
                                throw new ValidationException("Не вказано TourNameId для пошуку!", nameof(TourNameQuery.Id));
                            }
                            else
                            {
                                var n = await _serv.GetById((int)tourNameQuery.Id);
                                if (n != null)
                                {
                                    collection = new List<TourNameDTO> { n };
                                }
                            }
                        }
                        break;
                    case "GetByCountryName":
                        {
                            if (tourNameQuery.CountryName == null)
                            {
                                throw new ValidationException("Не вказано TourName.CountryName для пошуку!", nameof(TourNameQuery.CountryName));
                            }
                            else
                            {
                                collection = await _serv.GetByCountryName(tourNameQuery.CountryName);
                            }
                        }
                        break;
                    case "GetBySettlementName":
                        {
                            if (tourNameQuery.SettlementName == null)
                            {
                                throw new ValidationException("Не вказано TourName.SettlementName для пошуку!", nameof(TourNameQuery.SettlementName));
                            }
                            else
                            {
                                collection = await _serv.GetBySettlementName(tourNameQuery.SettlementName);
                            }
                        }
                        break;
                    case "GetByHotelName":
                        {
                            if (tourNameQuery.HotelName == null)
                            {
                                throw new ValidationException("Не вказано TourName.HotelName для пошуку!", nameof(TourNameQuery.HotelName));
                            }
                            else
                            {
                                collection = await _serv.GetByHotelName(tourNameQuery.HotelName);
                            }
                        }
                        break;
                    case "GetByTourId":
                        {
                            if (tourNameQuery.TourId == null)
                            {
                                throw new ValidationException("Не вказано TourName.TourId для пошуку!", nameof(TourNameQuery.TourId));
                            }
                            else
                            {
                                collection = await _serv.GetByTourId((long)tourNameQuery.TourId);
                            }
                        }
                        break;
                    case "GetByTourImageId":
                        {
                            if (tourNameQuery.TourImageId == null)
                            {
                                throw new ValidationException("Не вказано TourName.TourImageId для пошуку!", nameof(TourNameQuery.TourImageId));
                            }
                            else
                            {
                                collection = await _serv.GetByTourImageId((long)tourNameQuery.TourImageId);
                            }
                        }
                        break;
                    case "GetByPageJSONStructureUrl":
                        {
                            if (tourNameQuery.PageJSONStructureUrl == null)
                            {
                                throw new ValidationException("Не вказано TourName.PageJSONStructureUrl для пошуку!", nameof(TourNameQuery.PageJSONStructureUrl));
                            }
                            else
                            {
                                collection = await _serv.GetByPageJSONStructureUrlSubstring(tourNameQuery.PageJSONStructureUrl);
                            }
                        }
                        break;

                    case "GetByName":
                        {
                            if (tourNameQuery.Name == null)
                            {
                                throw new ValidationException("Не вказано TourName.Name для пошуку!", nameof(TourNameQuery.Name));
                            }
                            else
                            {
                                collection = await _serv.GetByName(tourNameQuery.Name);
                            }
                        }
                        break;
                        case "GetByContinentName":
                        {
                            if (tourNameQuery.ContinentName == null)
                            {
                                throw new ValidationException("Не вказано TourName.ContinentName для пошуку!", nameof(TourNameQuery.ContinentName));
                            }
                            else
                            {
                                collection = await _serv.GetByContinentName(tourNameQuery.ContinentName);
                            }
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(tourNameQuery.Name, tourNameQuery.ContinentName, tourNameQuery.CountryName, tourNameQuery.SettlementName, tourNameQuery.HotelName, tourNameQuery.PageJSONStructureUrl, tourNameQuery.TourId, tourNameQuery.TourImageId);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр tourNameQuery.SearchParameter!", nameof(tourNameQuery.SearchParameter));
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


        [HttpPut]
        public async Task<ActionResult<TourNameDTO>> UpdateTourName(TourNameDTO tourNameDTO)
        {
            try
            {
                var dto = await _serv.Update(tourNameDTO);
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

        [HttpPost]
        public async Task<ActionResult<TourNameDTO>> AddTourName(TourNameDTO tourNameDTO)
        {
            try
            {
                var dto = await _serv.Create(tourNameDTO);
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
        public async Task<ActionResult<TourNameDTO>> DeleteTourName(int id)
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

    public class TourNameQuery
    {
        public string SearchParameter { get; set; } = "";
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? ContinentName { get; set; }
        public string? CountryName { get; set; }
        public string? SettlementName { get; set; }
        public string? HotelName { get; set; }
        public string? PageJSONStructureUrl { get; set; }
        public long? TourId { get; set; }
        public long? TourImageId { get; set; }
    }
}
