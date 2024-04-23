using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using Microsoft.IdentityModel.Tokens;
namespace TouragencyWebApi.Controllers
{
    [Route("api/Email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _serv;
        public EmailController(IEmailService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailDTO>>> GetEmails([FromQuery] EmailQuery emailQuery)
        {
            try
            {
                IEnumerable<EmailDTO?> collection = null;
                switch (emailQuery.SearchParameter)
                {
                    case "GetAll":
                        { collection = await _serv.GetAll(); }
                        break;
                        case "Get200Last":
                        { collection = await _serv.Get200Last(); }
                        break;
                    case "GetById":
                        {
                            if (emailQuery.EmailId == null)
                            {
                                throw new ValidationException("Не вказано EmailId для пошуку!", nameof(emailQuery.EmailId));
                            }
                            var acc = await _serv.GetById((long)emailQuery.EmailId);
                            if (acc != null)
                            {
                                collection = new List<EmailDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByPersonId":
                        {
                            if (emailQuery.PersonId == null)
                            {
                                throw new ValidationException("Не вказано PersonId для пошуку!", nameof(emailQuery.PersonId));
                            }
                            collection = await _serv.GetByPersonId((int)emailQuery.PersonId);
                        }
                        break;
                    case "GetByClientId":
                        {
                            if (emailQuery.ClientId == null)
                            {
                                throw new ValidationException("Не вказано ClientId для пошуку!", nameof(emailQuery.ClientId));
                            }
                            collection = await _serv.GetByClientId((int)emailQuery.ClientId);
                        }
                        break;
                    case "GetByTouragencyEmployeeId":
                        {
                            if (emailQuery.TouragencyEmployeeId == null)
                            {
                                throw new ValidationException("Не вказано TouragencyEmployeeId для пошуку!", nameof(emailQuery.TouragencyEmployeeId));
                            }
                            collection = await _serv.GetByTouragencyEmployeeId((int)emailQuery.TouragencyEmployeeId);
                        }
                        break;
                    case "GetByContactTypeId":
                        {
                            if (emailQuery.ContactTypeId == null)
                            {
                                throw new ValidationException("Не вказано ContactTypeId для пошуку!", nameof(emailQuery.ContactTypeId));
                            }
                            collection = await _serv.GetByContactTypeId((int)emailQuery.ContactTypeId);
                        }
                        break;
                    case "GetByEmailAddress":
                        {
                            if (emailQuery.EmailAddress == null)
                            {
                                throw new ValidationException("Не вказано EmailAddress для пошуку!", nameof(emailQuery.EmailAddress));
                            }
                            collection = await _serv.GetByEmailAddress(emailQuery?.EmailAddress);
                        }
                        break;
                        case "GetByTouristNickname":
                        {
                            if (emailQuery.TouristNickname == null)
                            {
                                throw new ValidationException("Не вказано TouristNickname для пошуку!", nameof(emailQuery.TouristNickname));
                            }
                            collection = await _serv.GetByTouristNickname(emailQuery?.TouristNickname);
                        }
                        break;
                        case "GetByFirstname":
                        {
                            if (emailQuery.Firstname == null)
                            {
                                throw new ValidationException("Не вказано Firstname для пошуку!", nameof(emailQuery.Firstname));
                            }
                            collection = await _serv.GetByFirstname(emailQuery?.Firstname);
                        }
                        break;
                        case "GetByLastname":
                        {
                            if (emailQuery.Lastname == null)
                            {
                                throw new ValidationException("Не вказано Lastname для пошуку!", nameof(emailQuery.Lastname));
                            }
                            collection = await _serv.GetByLastname(emailQuery?.Lastname);
                        }
                        break;
                        case "GetByMiddlename":
                        {
                            if (emailQuery.Middlename == null)
                            {
                                throw new ValidationException("Не вказано Middlename для пошуку!", nameof(emailQuery.Middlename));
                            }
                            collection = await _serv.GetByMiddlename(emailQuery?.Middlename);
                        }
                        break;
                        case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(emailQuery?.ClientId, emailQuery?.PersonId, emailQuery?.TouragencyEmployeeId, emailQuery?.TouristNickname, emailQuery?.ContactTypeId, emailQuery?.EmailAddress, emailQuery?.Firstname, emailQuery?.Lastname, emailQuery?.Middlename);
                        }break;
                    default:
                        { // Якщо немає правильного варіанту - кинути виключення
                            throw new ValidationException("Вказано неправильний параметр emailQuery.SearchParameter!", nameof(emailQuery.SearchParameter));
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
        public async Task<ActionResult> AddEmail(EmailDTO emailDTO)
        {
            try
            {
                var dto = await _serv.TryToAddNewEmail(emailDTO);
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
        public async Task<ActionResult> UpdateEmail(EmailDTO emailDTO)
        {
            try
            {
                var dto = await _serv.Update(emailDTO);
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

        [HttpDelete]
        public async Task<ActionResult> DeleteEmail(long id)
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

    public class EmailQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? EmailId { get; set; }
        public int? PersonId { get; set; }
        public int? ClientId { get; set; }
        public int? TouragencyEmployeeId { get; set; }
        public int? ContactTypeId { get; set; }
        public string? EmailAddress { get; set; }
        public string? TouristNickname { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Middlename { get; set; }
    }

}
