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
                                    collection= new List<TourNameDTO> { n };
                                }
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
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateTourName(TourNameDTO tourNameDTO)
        {
            try
            {
                await _serv.Update(tourNameDTO);
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

        [HttpPost]
        public async Task<ActionResult> AddTourName(TourNameDTO tourNameDTO)
        {
            try
            {
                await _serv.Create(tourNameDTO);
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
        public async Task<ActionResult> DeleteTourName(int id)
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

    public class TourNameQuery
    {
        public string SearchParameter { get; set; } = "";
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
