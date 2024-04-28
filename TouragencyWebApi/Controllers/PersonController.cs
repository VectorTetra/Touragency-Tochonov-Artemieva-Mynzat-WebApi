using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get([FromQuery] PersonQuery personQuery)
        {
            try
            {
                IEnumerable<PersonDTO>? collection = null;
                switch (personQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _personService.GetAll();
                        }
                        break;
                    case "Get200Last":
                        {
                            collection = await _personService.Get200Last();
                        }
                        break;
                    case "GetById":
                        {
                            if (personQuery.Id == null)
                            {
                                throw new ValidationException("Не вказано Id для пошуку!", nameof(personQuery.Id));
                            }
                            var person = await _personService.GetById((int)personQuery.Id);
                            if (person != null)
                            {
                                collection = new List<PersonDTO> { person };
                            }
                        }
                        break;
                    case "GetByClientId":
                        {
                            if (personQuery.ClientId == null)
                            {
                                throw new ValidationException("Не вказано ClientId для пошуку!", nameof(personQuery.ClientId));
                            }
                            var person = await _personService.GetByClientId((int)personQuery.ClientId);
                            if (person != null)
                            {
                                collection = new List<PersonDTO> { person };
                            }
                        }
                        break;
                    case "GetByTouragencyEmployeeId":
                        {
                            if (personQuery.TouragencyEmployeeId == null)
                            {
                                throw new ValidationException("Не вказано TouragencyEmployeeId для пошуку!", nameof(personQuery.TouragencyEmployeeId));
                            }
                            var person = await _personService.GetByTouragencyEmployeeId((int)personQuery.TouragencyEmployeeId);
                            if (person != null)
                            {
                                collection = new List<PersonDTO> { person };
                            }
                        }
                        break;
                    case "GetByFirstname":
                        {
                            if (personQuery.Firstname == null)
                            {
                                throw new ValidationException("Не вказано Firstname для пошуку!", nameof(personQuery.Firstname));
                            }
                            collection = await _personService.GetByFirstnameSubstring(personQuery.Firstname);
                        }
                        break;
                    case "GetByLastname":
                        {
                            if (personQuery.Lastname == null)
                            {
                                throw new ValidationException("Не вказано Lastname для пошуку!", nameof(personQuery.Lastname));
                            }
                            collection = await _personService.GetByLastnameSubstring(personQuery.Lastname);
                        }
                        break;
                    case "GetByMiddlename":
                        {
                            if (personQuery.Middlename == null)
                            {
                                throw new ValidationException("Не вказано Middlename для пошуку!", nameof(personQuery.Middlename));
                            }
                            collection = await _personService.GetByMiddlenameSubstring(personQuery.Middlename);
                        }
                        break;
                    case "GetByPhoneNumber":
                        {
                            if (personQuery.PhoneNumber == null)
                            {
                                throw new ValidationException("Не вказано PhoneNumber для пошуку!", nameof(personQuery.PhoneNumber));
                            }
                            collection = await _personService.GetByPhoneNumberSubstring(personQuery.PhoneNumber);
                        }
                        break;
                    case "GetByEmailAddress":
                        {
                            if (personQuery.EmailAddress == null)
                            {
                                throw new ValidationException("Не вказано EmailAddress для пошуку!", nameof(personQuery.EmailAddress));
                            }
                            collection = await _personService.GetByEmailAddressSubstring(personQuery.EmailAddress);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _personService.GetByCompositeSearch(personQuery.Ids, personQuery.Firstname, personQuery.Lastname, personQuery.Middlename, personQuery.PhoneNumber, personQuery.EmailAddress);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр personQuery.SearchParameter!", nameof(personQuery.SearchParameter));
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
        public async Task<ActionResult<PersonDTO>> AddPerson(PersonDTO personDTO)
        {
            try
            {
                var dto = await _personService.Create(personDTO);
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
        public async Task<ActionResult<PersonDTO>> UpdatePerson(PersonDTO personDTO)
        {
            try
            {
                var dto = await _personService.Update(personDTO);
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
        public async Task<ActionResult<PersonDTO>> DeletePerson(int id)
        {
            try
            {
                var dto = await _personService.Delete(id);
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

    public class PersonQuery
    {
        public string SearchParameter { get; set; }
        public int? Id { get; set; }
        public int? ClientId { get; set; }
        public int? TouragencyEmployeeId { get; set; }
        public ICollection<int>? Ids { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Middlename { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
    }
}
