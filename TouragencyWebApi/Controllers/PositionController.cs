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
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PositionDTO>> CreatePosition(PositionDTO positionDTO)
        {
            try
            {
                await _serv.Create(positionDTO);
                return new ObjectResult(positionDTO);
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
        public async Task<ActionResult<PositionDTO>> UpdatePosition(PositionDTO positionDTO)
        {
            try
            {
                await _serv.Update(positionDTO);
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
        public async Task<ActionResult<PositionDTO>> DeletePosition(int id)
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

    public class PositionQuery
    {
        public string SearchParameter { get; set; }
        public int? Id { get; set; }
        public string? Description { get; set; }
    }
}
