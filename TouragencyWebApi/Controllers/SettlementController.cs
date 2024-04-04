using Microsoft.AspNetCore.Mvc;
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
                            if (settlementQuery.SettlementId == null)
                            {
                                throw new ValidationException("Не вказано CountryId для пошуку!", nameof(settlementQuery.SettlementId));

                            }
                            var stlmnt = await _serv.GetById(settlementQuery.SettlementId);
                            collection = new List<SettlementDTO?> { stlmnt };
                        }
                        break;
                    case "GetByName":
                        {
                            if (settlementQuery.SettlementName == null)
                            {
                                throw new ValidationException("Не вказано SettlementName для пошуку!", nameof(settlementQuery.SettlementName));
                            }
                            collection = await _serv.GetByName(settlementQuery.SettlementName);
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
                            collection = new List<SettlementDTO>();
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
        public int SettlementId { get; set; }
        public string? SettlementName { get; set; }
        public string? CountryName { get; set; }

    }
}
