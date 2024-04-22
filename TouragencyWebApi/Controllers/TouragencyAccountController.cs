using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/TouragencyAccount")]
    [ApiController]
    public class TouragencyAccountController : ControllerBase
    {
        private readonly ITouragencyAccountService _serv;
        public TouragencyAccountController(ITouragencyAccountService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TouragencyEmployeeAccountDTO>>> GetCountry([FromQuery] AccountQuery accountQuery)
        {
            try
            {
                IEnumerable<TouragencyEmployeeAccountDTO?> collection = null;
                switch (accountQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (accountQuery.AccountId == null)
                            {
                                throw new ValidationException("Не вказано AccountId для пошуку!", nameof(accountQuery.AccountId));

                            }
                            var cntr = await _serv.GetById(accountQuery.AccountId);
                            collection = new List<TouragencyEmployeeAccountDTO?> { cntr };
                        }
                        break;
                    case "GetByLogin":
                        {
                            if (accountQuery.Login == null)
                            {
                                throw new ValidationException("Не вказано accountQuery для пошуку!", nameof(accountQuery.Login));
                            }
                            collection = await _serv.GetByLogin(accountQuery.Login);
                        }
                        break;
                    case "GetByRole":
                        {
                            if (accountQuery.Role == null)
                            {
                                throw new ValidationException("Не вказано accountQuery для пошуку!", nameof(accountQuery.Role));
                            }
                            collection = await _serv.GetByRole(accountQuery.Role);
                        }
                        break;
                    case "GetByEmployeeName":
                        {
                            if (accountQuery.EmployeeName == null)
                            {
                                throw new ValidationException("Не вказано accountQuery для пошуку!", nameof(accountQuery.EmployeeName));
                            }
                            collection = await _serv.GetByRole(accountQuery.EmployeeName);
                        }
                        break;
                    default:
                        {
                            collection = new List<TouragencyEmployeeAccountDTO>();
                        }
                        break;
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


        [HttpPut]
        public async Task<ActionResult> UpdateAccount(TouragencyEmployeeAccountDTO accountDTO)
        {
            try
            {
                await _serv.Update(accountDTO);
                return Ok();
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
        public async Task<ActionResult> DeleteAccount(int id)
        {
            try
            {
                await _serv.Delete(id);
                return Ok();
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

    public class AccountQuery
    {
        public string SearchParameter { get; set; } = "";
        public int AccountId { get; set; }
        public string? Login { get; set; }
        public string? Role { get; set; }
        public string? EmployeeName { get; set; }
    }
}
