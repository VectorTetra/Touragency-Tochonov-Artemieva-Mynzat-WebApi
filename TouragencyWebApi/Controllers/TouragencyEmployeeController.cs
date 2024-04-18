using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/TouragencyEmployee")]
    [ApiController]
    public class TouragencyEmployeeController : ControllerBase
    {
        private readonly ITouragencyEmployeeService _serv;
        public TouragencyEmployeeController(ITouragencyEmployeeService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TouragencyEmployeeDTO>>> GetCountry([FromQuery] EmployeeQuery employeeQuery)
        {
            try
            {
                IEnumerable<TouragencyEmployeeDTO?> collection = null;
                switch (employeeQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (employeeQuery.EmployeeId == null)
                            {
                                throw new ValidationException("Не вказано EmployeeId для пошуку!", nameof(employeeQuery.EmployeeId));

                            }
                            var cntr = await _serv.GetById(employeeQuery.EmployeeId);
                            collection = new List<TouragencyEmployeeDTO?> { cntr };
                        }
                        break;
                    case "GetByName":
                        {
                            if (employeeQuery.EmployeeName == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery для пошуку!", nameof(employeeQuery.EmployeeName));
                            }
                            collection = await _serv.GetByName(employeeQuery.EmployeeName);
                        }
                        break;
                    case "GetByPosition":
                        {
                            if (employeeQuery.Position == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery для пошуку!", nameof(employeeQuery.Position));
                            }
                            collection = await _serv.GetByPosition(employeeQuery.Position);
                        }
                        break;
                    default:
                        {
                            collection = new List<TouragencyEmployeeDTO>();
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
        public async Task<ActionResult> AddEmployee(TouragencyEmployeeDTO employeeDTO)
        {
            try
            {
                await _serv.Add(employeeDTO);
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
        public async Task<ActionResult> UpdateEmployee(TouragencyEmployeeDTO employeeDTO)
        {
            try
            {
                await _serv.Update(employeeDTO);
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
        public async Task<ActionResult> DeleteEmployee(int id)
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

    public class EmployeeQuery
    {
        public string SearchParameter { get; set; } = "";
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Position { get; set; }
    }
}
