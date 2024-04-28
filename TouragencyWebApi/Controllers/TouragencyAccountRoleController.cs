using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/TouragencyAccountRole")]
    [ApiController]
    public class TouragencyAccountRoleController : ControllerBase
    {
        private readonly ITouragencyAccountRoleService _serv;
        public TouragencyAccountRoleController(ITouragencyAccountRoleService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TouragencyAccountRoleDTO>>> GetCountry([FromQuery] RoleQuery roleQuery)
        {
            try
            {
                IEnumerable<TouragencyAccountRoleDTO?> collection = null;
                switch (roleQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (roleQuery.RoleId == null)
                            {
                                throw new ValidationException("Не вказано RoleId для пошуку!", nameof(roleQuery.RoleId));

                            }
                            var cntr = await _serv.GetById(roleQuery.RoleId);
                            collection = new List<TouragencyAccountRoleDTO?> { cntr };
                        }
                        break;
                    case "GetByName":
                        {
                            if (roleQuery.Name == null)
                            {
                                throw new ValidationException("Не вказано RoleName для пошуку!", nameof(roleQuery.Name));
                            }
                            collection = await _serv.GetByName(roleQuery.Name);
                        }
                        break;
                    case "GetByDescription":
                        {
                            if (roleQuery.Description == null)
                            {
                                throw new ValidationException("Не вказано Description для пошуку!", nameof(roleQuery.Description));
                            }
                            collection = await _serv.GetByDescription(roleQuery.Description);
                        }
                        break;
                    case "GetByEmployeeFirstname":
                        {
                            if (roleQuery.EmployeeFirstname == null)
                            {
                                throw new ValidationException("Не вказано EmployeeFirstname для пошуку!", nameof(roleQuery.EmployeeFirstname));
                            }
                            collection = await _serv.GetByEmployeeFirstname(roleQuery.EmployeeFirstname);
                        }
                        break;
                    case "GetByEmployeeLastname":
                        {
                            if (roleQuery.EmployeeLastname == null)
                            {
                                throw new ValidationException("Не вказано EmployeeLastname для пошуку!", nameof(roleQuery.EmployeeLastname));
                            }
                            collection = await _serv.GetByEmployeeLastname(roleQuery.EmployeeLastname);
                        }
                        break;
                    case "GetByEmployeeMiddlename":
                        {
                            if (roleQuery.EmployeeMiddlename == null)
                            {
                                throw new ValidationException("Не вказано EmployeeMiddlename для пошуку!", nameof(roleQuery.EmployeeMiddlename));
                            }
                            collection = await _serv.GetByEmployeeMiddlename(roleQuery.EmployeeMiddlename);
                        }
                        break;
                    case "GetByClientFirstname":
                        {
                            if (roleQuery.ClientFirstname == null)
                            {
                                throw new ValidationException("Не вказано ClientFirstname для пошуку!", nameof(roleQuery.ClientFirstname));
                            }
                            collection = await _serv.GetByClientFirstname(roleQuery.ClientFirstname);
                        }
                        break;
                    case "GetByClientLastname":
                        {
                            if (roleQuery.ClientLastname == null)
                            {
                                throw new ValidationException("Не вказано ClientLastname для пошуку!", nameof(roleQuery.ClientLastname));
                            }
                            collection = await _serv.GetByClientLastname(roleQuery.ClientLastname);
                        }
                        break;
                    case "GetByClientMiddlename":
                        {
                            if (roleQuery.ClientMiddlename == null)
                            {
                                throw new ValidationException("Не вказано ClientMiddlename для пошуку!", nameof(roleQuery.ClientMiddlename));
                            }
                            collection = await _serv.GetByClientMiddlename(roleQuery.ClientMiddlename);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(roleQuery.Name, roleQuery.Description, roleQuery.EmployeeFirstname, roleQuery.EmployeeLastname, roleQuery.EmployeeMiddlename, roleQuery.ClientFirstname, roleQuery.ClientLastname, roleQuery.ClientMiddlename);
                        }
                        break;
                    default:
                        {
                            collection = new List<TouragencyAccountRoleDTO>();
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
        [HttpPost]
        public async Task<ActionResult<TouragencyAccountRoleDTO>> AddRole(TouragencyAccountRoleDTO entity)
        {
            try
            {
                var dto = await _serv.Add(entity);
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
        public async Task<ActionResult<TouragencyAccountRoleDTO>> UpdateRole(TouragencyAccountRoleDTO entity)
        {
            try
            {
                var dto = await _serv.Update(entity);
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
        public async Task<ActionResult<TouragencyAccountRoleDTO>> DeleteRole(int id)
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

    public class RoleQuery
    {
        public string SearchParameter { get; set; } = "";
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? EmployeeFirstname { get; set; }
        public string? EmployeeLastname { get; set; }
        public string? EmployeeMiddlename { get; set; }
        public string? ClientFirstname { get; set; }
        public string? ClientLastname { get; set; }
        public string? ClientMiddlename { get; set; }
    }
}
