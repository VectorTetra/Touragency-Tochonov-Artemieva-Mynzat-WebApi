using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TouragencyWebApi.Controllers
{
    [Route("api/TransportType")]
    [ApiController]
    public class TransportTypeController : ControllerBase
    {
        private readonly ITransportTypeService _serv;
        public TransportTypeController(ITransportTypeService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportTypeDTO>>> GetTransportTypes([FromQuery] TransportTypeQuery transportTypeQuery)
        {
            try
            {
                IEnumerable<TransportTypeDTO> collection = null;
                switch (transportTypeQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (transportTypeQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано TransportTypeId для пошуку!", nameof(transportTypeQuery.Id));
                            }
                            var acc = await _serv.GetById((int)transportTypeQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<TransportTypeDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByNameSubstring":
                        {
                            if (transportTypeQuery.Name is null)
                            {
                                throw new ValidationException("Не вказано Name для пошуку!", nameof(transportTypeQuery.Name));
                            }
                            collection = await _serv.GetByNameSubstring(transportTypeQuery.Name);
                        }
                        break;
                    case "GetByDescriptionSubstring":
                        {
                            if (transportTypeQuery.Description is null)
                            {
                                throw new ValidationException("Не вказано Description для пошуку!", nameof(transportTypeQuery.Description));
                            }
                            collection = await _serv.GetByDescriptionSubstring(transportTypeQuery.Description);
                        }
                        break;
                    case "GetByTourId":
                        {
                            if (transportTypeQuery.TourId is null)
                            {
                                throw new ValidationException("Не вказано TourId для пошуку!", nameof(transportTypeQuery.TourId));
                            }
                            collection = await _serv.GetByTourId((long)transportTypeQuery.TourId);
                        }
                        break;
                    case "GetByTourName":
                        {
                            if (transportTypeQuery.TourName is null)
                            {
                                throw new ValidationException("Не вказано TourName для пошуку!", nameof(transportTypeQuery.TourName));
                            }
                            collection = await _serv.GetByTourName(transportTypeQuery.TourName);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(transportTypeQuery.Name, transportTypeQuery.Description, transportTypeQuery.TourId, transportTypeQuery.TourName);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невідомий параметр пошуку!", nameof(transportTypeQuery.SearchParameter));
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
        public async Task<ActionResult<TransportTypeDTO>> PostTransportType(TransportTypeDTO transportType)
        {
            try
            {
                var dto = await _serv.Create(transportType);
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
        public async Task<ActionResult<TransportTypeDTO>> PutTransportType(TransportTypeDTO transportType)
        {
            try
            {
                var dto = await _serv.Update(transportType);
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
        public async Task<ActionResult> DeleteTransportType(int id)
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

    public class TransportTypeQuery
    {
        public string SearchParameter { get; set; } = "";
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? TourName { get; set; }
        public string? Description { get; set; }
        public long? TourId { get; set; }
    }
}
