using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Continent")]
    [ApiController]
    public class ContinentController : ControllerBase
    {
        private readonly IContinentService _serv;
        public ContinentController(IContinentService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContinentDTO>>> GetContinent([FromQuery] ContinentQuery continentQuery)
        {
            try
            {
                IEnumerable<ContinentDTO> collection = null;
                switch (continentQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (continentQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано ContinentId для пошуку!", nameof(continentQuery.Id));
                            }
                            var acc = await _serv.GetById((int)continentQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<ContinentDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (continentQuery.Name is null)
                            {
                                throw new ValidationException("Не вказано Name для пошуку!", nameof(continentQuery.Name));
                            }
                            collection = await _serv.GetByName(continentQuery.Name);
                        }
                        break;
                    case "GetByCountryId":
                        {
                            if (continentQuery.CountryId is null)
                            {
                                throw new ValidationException("Не вказано CountryId для пошуку!", nameof(continentQuery.CountryId));
                            }
                            collection = await _serv.GetByCountryId((int)continentQuery.CountryId);
                        }
                        break;
                    case "GetByCountryName":
                        {
                            if (continentQuery.CountryName is null)
                            {
                                throw new ValidationException("Не вказано CountryName для пошуку!", nameof(continentQuery.CountryName));
                            }
                            collection = await _serv.GetByCountryName(continentQuery.CountryName);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(continentQuery.Name, continentQuery.CountryName, continentQuery.CountryId);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невідомий параметр пошуку!", nameof(continentQuery.SearchParameter));
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
        public async Task<ActionResult<ContinentDTO>> PostContinent(ContinentDTO continentDTO)
        {
            try
            {
                var result = await _serv.Create(continentDTO);
                return result;
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
        public async Task<ActionResult<ContinentDTO>> PutContinent(ContinentDTO continentDTO)
        {
            try
            {
                var result = await _serv.Update(continentDTO);
                return result;
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
        public async Task<ActionResult<ContinentDTO>> DeleteContinent(int id)
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

    public class ContinentQuery
    {
        public string SearchParameter { get; set; } = "GetAll";
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}
