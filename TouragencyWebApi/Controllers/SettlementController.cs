using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Settlement")]
    [ApiController]
    public class SettlementController : ControllerBase
    {
        private readonly ISettlementService _serv;
        public SettlementController(ISettlementService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SettlementDTO>>> GetSettlement([FromQuery] SettlementQuery settlementQuery)
        {
            try
            {
                IEnumerable<SettlementDTO?> collection = null;
                switch (settlementQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (settlementQuery.Id == null)
                            {
                                throw new ValidationException("Не вказано CountryId для пошуку!", nameof(settlementQuery.Id));
                            }
                            var stlmnt = await _serv.GetById((int)settlementQuery.Id);
                            if (stlmnt != null)
                            {
                                collection = new List<SettlementDTO?> { stlmnt };
                            }
                        }
                        break;
                    case "GetByName":
                        {
                            if (settlementQuery.Name == null)
                            {
                                throw new ValidationException("Не вказано SettlementName для пошуку!", nameof(settlementQuery.Name));
                            }
                            collection = await _serv.GetByName(settlementQuery.Name);
                        }
                        break;
                    case "GetByCountryName":
                        {
                            if (settlementQuery.CountryName == null)
                            {
                                throw new ValidationException("Не вказано CountryName для пошуку!", nameof(settlementQuery.CountryName));
                            }
                            collection = await _serv.GetByCountryName(settlementQuery.CountryName);
                        }
                        break;

                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр settlementQuery.SearchParameter!", nameof(settlementQuery.SearchParameter));
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
        public async Task<ActionResult> AddSettlement(SettlementDTO settlementDTO)
        {
            try
            {
                await _serv.Add(settlementDTO);
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
        public async Task<ActionResult> UpdateSettlement(SettlementDTO settlementDTO)
        {
            try
            {
                await _serv.Update(settlementDTO);
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
        public async Task<ActionResult> DeleteSettlement(int id)
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

    public class SettlementQuery
    {
        public string SearchParameter { get; set; } = "";
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? CountryName { get; set; }

    }
}
