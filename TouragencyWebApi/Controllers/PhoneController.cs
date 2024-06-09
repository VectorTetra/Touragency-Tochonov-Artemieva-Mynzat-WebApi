using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Phone")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneService _serv;
        public PhoneController(IPhoneService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneDTO>>> GetPhones([FromQuery] PhoneQuery phoneQuery)
        {
            try
            {
                IEnumerable<PhoneDTO?> collection = null;
                switch (phoneQuery.SearchParameter)
                {
                    case "GetAll":
                        { collection = await _serv.GetAll(); }
                        break;
                    case "GetById":
                        {
                            if (phoneQuery.PhoneId == null)
                            {
                                throw new ValidationException("Не вказано PhoneId для пошуку!", nameof(phoneQuery.PhoneId));
                            }
                            var acc = await _serv.GetById((long)phoneQuery.PhoneId);
                            if (acc != null)
                            {
                                collection = new List<PhoneDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByPersonId":
                        {
                            if (phoneQuery.PersonId == null)
                            {
                                throw new ValidationException("Не вказано PersonId для пошуку!", nameof(phoneQuery.PersonId));
                            }
                            collection = await _serv.GetByPersonId((int)phoneQuery.PersonId);
                        }
                        break;
                    case "GetByClientId":
                        {
                            if (phoneQuery.ClientId == null)
                            {
                                throw new ValidationException("Не вказано ClientId для пошуку!", nameof(phoneQuery.ClientId));
                            }
                            collection = await _serv.GetByClientId((int)phoneQuery.ClientId);
                        }
                        break;
                    case "GetByTouragencyEmployeeId":
                        {
                            if (phoneQuery.TouragencyEmployeeId == null)
                            {
                                throw new ValidationException("Не вказано TouragencyEmployeeId для пошуку!", nameof(phoneQuery.TouragencyEmployeeId));
                            }
                            collection = await _serv.GetByTouragencyEmployeeId((int)phoneQuery.TouragencyEmployeeId);
                        }
                        break;
                    case "GetByContactTypeId":
                        {
                            if (phoneQuery.ContactTypeId == null)
                            {
                                throw new ValidationException("Не вказано ContactTypeId для пошуку!", nameof(phoneQuery.ContactTypeId));
                            }
                            collection = await _serv.GetByContactTypeId((int)phoneQuery.ContactTypeId);
                        }
                        break;
                    case "GetByPhoneNumber":
                        {
                            if (phoneQuery.PhoneNumber == null)
                            {
                                throw new ValidationException("Не вказано PhoneAddress для пошуку!", nameof(phoneQuery.PhoneNumber));
                            }
                            collection = await _serv.GetByPhoneNumber(phoneQuery?.PhoneNumber);
                        }
                        break;
                    case "GetByTouristNickname":
                        {
                            if (phoneQuery.TouristNickname == null)
                            {
                                throw new ValidationException("Не вказано TouristNickname для пошуку!", nameof(phoneQuery.TouristNickname));
                            }
                            collection = await _serv.GetByTouristNickname(phoneQuery?.TouristNickname);
                        }
                        break;
                    case "GetByFirstname":
                        {
                            if (phoneQuery.Firstname == null)
                            {
                                throw new ValidationException("Не вказано Firstname для пошуку!", nameof(phoneQuery.Firstname));
                            }
                            collection = await _serv.GetByFirstname(phoneQuery?.Firstname);
                        }
                        break;
                    case "GetByLastname":
                        {
                            if (phoneQuery.Lastname == null)
                            {
                                throw new ValidationException("Не вказано Lastname для пошуку!", nameof(phoneQuery.Lastname));
                            }
                            collection = await _serv.GetByLastname(phoneQuery?.Lastname);
                        }
                        break;
                    case "GetByMiddlename":
                        {
                            if (phoneQuery.Middlename == null)
                            {
                                throw new ValidationException("Не вказано Middlename для пошуку!", nameof(phoneQuery.Middlename));
                            }
                            collection = await _serv.GetByMiddlename(phoneQuery?.Middlename);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(phoneQuery.ClientId, phoneQuery.PersonId, phoneQuery.TouragencyEmployeeId,
                                                               phoneQuery.TouristNickname, phoneQuery.ContactTypeId, phoneQuery.PhoneNumber, phoneQuery.Firstname, phoneQuery.Lastname, phoneQuery.Middlename);
                        }
                        break;
                    default:
                        { // Якщо немає правильного варіанту - кинути виключення
                            throw new ValidationException("Вказано неправильний параметр phoneQuery.SearchParameter!", nameof(phoneQuery.SearchParameter));
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
        public async Task<ActionResult> AddPhone(PhoneDTO phoneDTO)
        {
            try
            {
                var dto = await _serv.TryToAddNewPhone(phoneDTO);
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
        public async Task<ActionResult> UpdatePhone(PhoneDTO phoneDTO)
        {
            try
            {
                var dto = await _serv.Update(phoneDTO);
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
        public async Task<ActionResult> DeletePhone(long id)
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

    public class PhoneQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? PhoneId { get; set; }
        public int? PersonId { get; set; }
        public int? ClientId { get; set; }
        public int? TouragencyEmployeeId { get; set; }
        public int? ContactTypeId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TouristNickname { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Middlename { get; set; }
    }
}

