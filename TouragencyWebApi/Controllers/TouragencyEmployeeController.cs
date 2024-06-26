﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                            var cntr = await _serv.GetById(employeeQuery.EmployeeId.Value);
                            collection = new List<TouragencyEmployeeDTO?> { cntr };
                        }
                        break;
                    case "GetByFirstname":
                        {
                            if (employeeQuery.EmployeeFirstname == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery.EmployeeFirstname для пошуку!", nameof(employeeQuery.EmployeeFirstname));
                            }
                            collection = await _serv.GetByFirstname(employeeQuery.EmployeeFirstname);
                        }
                        break;
                    case "GetByLastname":
                        {
                            if (employeeQuery.EmployeeLastname == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery.EmployeeLastname для пошуку!", nameof(employeeQuery.EmployeeLastname));
                            }
                            collection = await _serv.GetByLastname(employeeQuery.EmployeeLastname);
                        }
                        break;
                    case "GetByMiddlename":
                        {
                            if (employeeQuery.EmployeeMiddlename == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery.EmployeeMiddlename для пошуку!", nameof(employeeQuery.EmployeeMiddlename));
                            }
                            collection = await _serv.GetByMiddlename(employeeQuery.EmployeeMiddlename);
                        }
                        break;
                    case "GetByPositionName":
                        {
                            if (employeeQuery.PositionName == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery.PositionName для пошуку!", nameof(employeeQuery.PositionName));
                            }
                            collection = await _serv.GetByPositionName(employeeQuery.PositionName);
                        }
                        break;
                    case "GetByPositionDescription":
                        {
                            if (employeeQuery.PositionDescription == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery.PositionDescription для пошуку!", nameof(employeeQuery.PositionDescription));
                            }
                            collection = await _serv.GetByPositionDescription(employeeQuery.PositionDescription);
                        }
                        break;
                        case "GetByAccountLogin":
                        {
                            if (employeeQuery.EmployeeAccountLogin == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery.EmployeeAccountLogin для пошуку!", nameof(employeeQuery.EmployeeAccountLogin));
                            }
                            collection = await _serv.GetByAccountLogin(employeeQuery.EmployeeAccountLogin);
                        }
                        break;
                        case "GetByAccountRoleId":
                        {
                            if (employeeQuery.EmployeeAccountRoleId == null)
                            {
                                throw new ValidationException("Не вказано employeeQuery.EmployeeAccountRoleId для пошуку!", nameof(employeeQuery.EmployeeAccountRoleId));
                            }
                            collection = await _serv.GetByAccountRoleId(employeeQuery.EmployeeAccountRoleId.Value);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(employeeQuery.EmployeeFirstname, employeeQuery.EmployeeLastname, employeeQuery.EmployeeMiddlename, employeeQuery.PositionName, employeeQuery.PositionDescription, employeeQuery.EmployeeAccountLogin, employeeQuery.EmployeeAccountRoleId, employeeQuery.EmployeeEmail, employeeQuery.EmployeePhone);
                        }
                        break;
                    default:
                        {
                            collection = new List<TouragencyEmployeeDTO>();
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
        public async Task<ActionResult<TouragencyEmployeeDTO>> AddEmployee(TouragencyEmployeeDTO employeeDTO)
        {
            try
            {
                var dto = await _serv.Add(employeeDTO);
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
        public async Task<ActionResult<TouragencyEmployeeDTO>> UpdateEmployee(TouragencyEmployeeDTO employeeDTO)
        {
            try
            {
                var dto = await _serv.Update(employeeDTO);
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
        public async Task<ActionResult<TouragencyEmployeeDTO>> DeleteEmployee(int id)
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

    public class EmployeeQuery
    {
        public string SearchParameter { get; set; } = "";
        public int? EmployeeId { get; set; }
        public int? EmployeeAccountRoleId { get; set; }
        public string? EmployeeAccountLogin { get; set; }
        public string? EmployeeFirstname { get; set; }
        public string? EmployeeLastname { get; set; }
        public string? EmployeeMiddlename { get; set; }
        public string? PositionName { get; set; }
        public string? PositionDescription { get; set; }
        public string? EmployeePhone { get; set;}
        public string? EmployeeEmail { get; set; }
    }
}
