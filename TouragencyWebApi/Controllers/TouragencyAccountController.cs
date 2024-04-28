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
                    case "Get200Last":
                        {
                            collection = await _serv.Get200Last();
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

                    case "GetByRoleName":
                        {
                            if (accountQuery.RoleName == null)
                            {
                                throw new ValidationException("Не вказано RoleName для пошуку!", nameof(accountQuery.RoleName));
                            }
                            collection = await _serv.GetByRoleName(accountQuery.RoleName);
                        }
                        break;
                    case "GetByRoleDescription":
                        {
                            if (accountQuery.RoleDescription == null)
                            {
                                throw new ValidationException("Не вказано RoleDescription для пошуку!", nameof(accountQuery.RoleDescription));
                            }
                            collection = await _serv.GetByRoleDescription(accountQuery.RoleDescription);
                        }
                        break;
                    case "GetByEmployeeFirstname":
                        {
                            if (accountQuery.EmployeeFirstname == null)
                            {
                                throw new ValidationException("Не вказано EmployeeFirstname для пошуку!", nameof(accountQuery.EmployeeFirstname));
                            }
                            collection = await _serv.GetByEmployeeFirstname(accountQuery.EmployeeFirstname);
                        }
                        break;
                    case "GetByEmployeeLastname":
                        {
                            if (accountQuery.EmployeeLastname == null)
                            {
                                throw new ValidationException("Не вказано EmployeeLastname для пошуку!", nameof(accountQuery.EmployeeLastname));
                            }
                            collection = await _serv.GetByEmployeeLastname(accountQuery.EmployeeLastname);
                        }
                        break;
                    case "GetByEmployeeMiddlename":
                        {
                            if (accountQuery.EmployeeMiddlename == null)
                            {
                                throw new ValidationException("Не вказано EmployeeMiddlename для пошуку!", nameof(accountQuery.EmployeeMiddlename));
                            }
                            collection = await _serv.GetByEmployeeMiddlename(accountQuery.EmployeeMiddlename);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(accountQuery.Login, accountQuery.RoleName, accountQuery.RoleDescription, accountQuery.EmployeeFirstname, accountQuery.EmployeeLastname, accountQuery.EmployeeMiddlename);
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
        public async Task<ActionResult<TouragencyEmployeeAccountDTO>> UpdateAccount(TouragencyEmployeeAccountDTO accountDTO)
        {
            try
            {
                var dto = await _serv.Update(accountDTO);
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
        public async Task<ActionResult<TouragencyEmployeeAccountDTO>> DeleteAccount(int id)
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

    public class AccountQuery
    {
        public string SearchParameter { get; set; } = "Get200Last";
        public int AccountId { get; set; }
        public string? Login { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public string? EmployeeFirstname { get; set; }
        public string? EmployeeLastname { get; set; }
        public string? EmployeeMiddlename { get; set; }
    }
}
