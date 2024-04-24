using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _serv;
        public PositionController(IPositionService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDTO>>> GetPositions([FromQuery] PositionQuery positionQuery)
        {
            try
            {
                IEnumerable<PositionDTO> collection = null;
                switch (positionQuery.SearchParameter)
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
                            if (positionQuery.Id == null)
                            {
                                throw new ValidationException("Не вказано PositionId для пошуку!", nameof(PositionQuery.Id));
                            }
                            else
                            {
                                var n = await _serv.GetById((int)positionQuery.Id);
                                if (n != null)
                                {
                                    collection = new List<PositionDTO> { n };
                                }
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (positionQuery.Name == null)
                            {
                                throw new ValidationException("Не вказано Position.Name для пошуку!", nameof(PositionQuery.Name));
                            }
                            else
                            {
                                collection = await _serv.GetByNameSubstring(positionQuery.Name);
                            }
                        }
                        break;
                    case "GetByDescription":
                        {
                            if (positionQuery.Description == null)
                            {
                                throw new ValidationException("Не вказано Position.Name для пошуку!", nameof(PositionQuery.Description));
                            }
                            else
                            {
                                collection = await _serv.GetByDescriptionSubstring(positionQuery.Description);
                            }
                        }
                        break;
                    case "GetByPersonFirstname":
                        {
                            if (positionQuery.PersonFirstname == null)
                            {
                                throw new ValidationException("Не вказано Person.Firstname для пошуку!", nameof(PositionQuery.PersonFirstname));
                            }
                            else
                            {
                                collection = await _serv.GetByPersonFirstnameSubstring(positionQuery.PersonFirstname);
                            }
                        }
                        break;
                    case "GetByPersonLastname":
                        {
                            if (positionQuery.PersonLastname == null)
                            {
                                throw new ValidationException("Не вказано Person.Lastname для пошуку!", nameof(PositionQuery.PersonLastname));
                            }
                            else
                            {
                                collection = await _serv.GetByPersonLastnameSubstring(positionQuery.PersonLastname);
                            }
                        }
                        break;
                    case "GetByPersonMiddlename":
                        {
                            if (positionQuery.PersonMiddlename == null)
                            {
                                throw new ValidationException("Не вказано Person.Middlename для пошуку!", nameof(PositionQuery.PersonMiddlename));
                            }
                            else
                            {
                                collection = await _serv.GetByPersonMiddlenameSubstring(positionQuery.PersonMiddlename);
                            }
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(positionQuery.Name, positionQuery.Description, positionQuery.PersonFirstname, positionQuery.PersonLastname, positionQuery.PersonMiddlename);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невірно вказаний параметр пошуку!", nameof(PositionQuery.SearchParameter));
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
        public async Task<ActionResult<PositionDTO>> CreatePosition(PositionDTO positionDTO)
        {
            try
            {
                var dto = await _serv.Create(positionDTO);
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
        public async Task<ActionResult<PositionDTO>> UpdatePosition(PositionDTO positionDTO)
        {
            try
            {
                var dto = await _serv.Update(positionDTO);
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
        public async Task<ActionResult<PositionDTO>> DeletePosition(int id)
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

    public class PositionQuery
    {
        public string SearchParameter { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PersonFirstname { get; set; }
        public string? PersonLastname { get; set; }
        public string? PersonMiddlename { get; set; }

    }
}
