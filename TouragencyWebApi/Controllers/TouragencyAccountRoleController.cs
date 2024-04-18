using Microsoft.AspNetCore.Mvc;
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
                                throw new ValidationException("Не вказано EmployeeId для пошуку!", nameof(roleQuery.RoleId));

                            }
                            var cntr = await _serv.GetById(roleQuery.RoleId);
                            collection = new List<TouragencyAccountRoleDTO?> { cntr };
                        }
                        break;
                    case "GetByName":
                        {
                            if (roleQuery.Name == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery для пошуку!", nameof(roleQuery.Name));
                            }
                            collection = await _serv.GetByName(roleQuery.Name);
                        }
                        break;
                    case "GetByEmployeeName":
                        {
                            if (roleQuery.EmployeeName == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery для пошуку!", nameof(roleQuery.EmployeeName));
                            }
                            collection = await _serv.GetByEmployeeName(roleQuery.EmployeeName);
                        }
                        break;
                    case "GetByClientName":
                        {
                            if (roleQuery.ClientName == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery для пошуку!", nameof(roleQuery.ClientName));
                            }
                            collection = await _serv.GetByClientName(roleQuery.ClientName);
                        }
                        break;
                    default:
                        {
                            collection = new List<TouragencyAccountRoleDTO>();
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
        public async Task<ActionResult> AddRole(TouragencyAccountRoleDTO entity)
        {
            try
            {
                await _serv.Add(entity);
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
        public async Task<ActionResult> UpdateRole(TouragencyAccountRoleDTO entity)
        {
            try
            {
                await _serv.Update(entity);
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
        public async Task<ActionResult> DeleteRole(int id)
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

    public class RoleQuery
    {
        public string SearchParameter { get; set; } = "";
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? EmployeeName { get; set; }
        public string? ClientName { get; set; }
    }
}
