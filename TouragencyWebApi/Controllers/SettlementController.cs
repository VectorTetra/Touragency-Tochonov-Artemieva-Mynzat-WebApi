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
                    case "Get200Last":
                        {
                            collection = await _serv.Get200Last();
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
                    case "GetByCountryId":
                        {
                            if (settlementQuery.CountryId == null)
                            {
                                throw new ValidationException("Не вказано CountryId для пошуку!", nameof(settlementQuery.CountryId));
                            }
                            collection = await _serv.GetByCountryId((int)settlementQuery.CountryId);
                        }
                        break;
                    case "GetByCountryIds":
                        {
                            if (settlementQuery.CountryIds == null)
                            {
                                throw new ValidationException("Не вказано CountryId для пошуку!", nameof(settlementQuery.CountryId));
                            }
                            collection = await _serv.GetByCountryIds(((IEnumerable<int>)settlementQuery.CountryIds).ToArray());
                        }
                        break;

                    case "GetByTourNameId":
                        {
                            if (settlementQuery.TourNameId == null)
                            {
                                throw new ValidationException("Не вказано TourNameId для пошуку!", nameof(settlementQuery.TourNameId));
                            }
                            collection = await _serv.GetByTourNameId((int)settlementQuery.TourNameId);
                        }
                        break;
                    case "GetByTourName":
                        {
                            if (settlementQuery.TourName == null)
                            {
                                throw new ValidationException("Не вказано TourName для пошуку!", nameof(settlementQuery.TourName));
                            }
                            collection = await _serv.GetByTourName(settlementQuery.TourName);
                        }
                        break;
                    case "GetByHotelId":
                        {
                            if (settlementQuery.HotelId == null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(settlementQuery.HotelId));
                            }
                            var stlmnt = await _serv.GetByHotelId((int)settlementQuery.HotelId);
                            if (stlmnt != null)
                            {
                                collection = new List<SettlementDTO?> { stlmnt };
                            }
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(settlementQuery.Name, settlementQuery.CountryName, settlementQuery.CountryId, settlementQuery.TourNameId, settlementQuery.TourName);
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
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<SettlementDTO>> AddSettlement(SettlementDTO settlementDTO)
        {
            try
            {
                var dto = await _serv.Add(settlementDTO);
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
        public async Task<ActionResult<SettlementDTO>> UpdateSettlement(SettlementDTO settlementDTO)
        {
            try
            {
                var dto = await _serv.Update(settlementDTO);
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
        public async Task<ActionResult<SettlementDTO>> DeleteSettlement(int id)
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

    public class SettlementQuery
    {
        public string SearchParameter { get; set; } = "";
        public int? Id { get; set; }
        public int? TourNameId { get; set; }
        public string? TourName { get; set; }
        public int? HotelId { get; set; }
        public string? Name { get; set; }
        public string? CountryName { get; set; }
        public int? CountryId { get; set; }
        public IEnumerable<int>? CountryIds { get; set; }

    }
}
