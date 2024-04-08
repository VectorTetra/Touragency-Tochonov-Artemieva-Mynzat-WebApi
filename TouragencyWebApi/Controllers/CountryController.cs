using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Interfaces;

using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _serv;
        public CountryController(ICountryService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<CountryDTO>>> GetCountry([FromQuery]CountryQuery countryQuery)
        {
            try
            {
                IEnumerable<CountryDTO?> collection = null;
                switch (countryQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if(countryQuery.CountryId == null)
                            {
                                throw new ValidationException("Не вказано CountryId для пошуку!", nameof(countryQuery.CountryId));
                            }
                            var cntr = await _serv.GetById((int)countryQuery.CountryId);
                            collection = new List<CountryDTO?> { cntr };
                        }
                        break;
                    case "GetByName":
                        {
                            if (countryQuery.CountryName == null)
                            {
                                throw new ValidationException("Не вказано CountryQuery для пошуку!", nameof(countryQuery.CountryName));
                            }
                            collection = await _serv.GetByName(countryQuery.CountryName);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр countryQuery.SearchParameter!", nameof(countryQuery.SearchParameter));
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
        public async Task<ActionResult> AddCountry(CountryDTO countryDTO)
        {
            try
            {
                await _serv.Add(countryDTO);
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
        public async Task<ActionResult> UpdateCountry(CountryDTO countryDTO)
        {
            try
            {
                await _serv.Update(countryDTO);
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
        public async Task<ActionResult> DeleteCountry(int id)
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

    public class CountryQuery
    {
        public string SearchParameter { get; set; } = "";
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }

    }
}
