using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Interfaces;

using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;

namespace TouragencyWebApi.Controllers
{
    [Route("api/TourState")]
    [ApiController]
    public class TourStateController : ControllerBase
    {
        private readonly ITourStateService _serv;
        public TourStateController(ITourStateService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourStateDTO>>> GetState([FromQuery] TourStateQuery tourStateQuery)
        {
            try
            {
                IEnumerable<TourStateDTO?> collection = null;
                switch (tourStateQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (tourStateQuery.StateId == null)
                            {
                                throw new ValidationException("Не вказано CountryId для пошуку!", nameof(tourStateQuery.StateId));

                            }
                            var trst = await _serv.GetById(tourStateQuery.StateId);
                            collection = new List<TourStateDTO?> { trst };
                        }
                        break;
                    case "GetByStatus":
                        {
                            if (tourStateQuery.TourStatus == null)
                            {
                                throw new ValidationException("Не вказано CountryQuery для пошуку!", nameof(tourStateQuery.TourStatus));
                            }
                            collection = await _serv.GetByStatus(tourStateQuery.TourStatus);
                        }
                        break;
                    default:
                        {
                            collection = new List<TourStateDTO>();
                        }
                        break;
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
        public async Task<ActionResult> AddState(TourStateDTO stateDTO)
        {
            try
            {
                await _serv.Add(stateDTO);
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
        public async Task<ActionResult> UpdateState(TourStateDTO stateDTO)
        {
            try
            {
                await _serv.Update(stateDTO);
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
        public async Task<ActionResult> DeleteState(int id)
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

    public class TourStateQuery
    {
        public string SearchParameter { get; set; } = "";
        public int StateId { get; set; }
        public string? TourStatus { get; set; }

    }
}
