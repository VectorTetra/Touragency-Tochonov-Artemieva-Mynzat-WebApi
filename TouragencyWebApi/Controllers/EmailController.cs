using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
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
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailDTO>>> GetEmails(string searchParameter, string stringParameter, int intParameter, long longParameter)
        {
            try
            {
                IEnumerable<EmailDTO?> collec = null;
                switch (searchParameter)
                {
                    case "GetAll":
                        { collec = await _serv.GetAll(); }
                        break;
                    case "GetById":
                        {
                            var acc = await _serv.GetById(longParameter);
                            collec = new List<EmailDTO?> { acc };
                        }
                        break;
                    case "GetByPersonId":
                        { collec = await _serv.GetByPersonId(intParameter); }
                        break;
                    case "GetByClientId":
                        { collec = await _serv.GetByClientId(intParameter); }
                        break;
                    case "GetByTouragencyEmployeeId":
                        { collec = await _serv.GetByTouragencyEmployeeId(intParameter); }
                        break;
                    case "GetByContactTypeId":
                        { collec = await _serv.GetByContactTypeId(intParameter); }
                        break;
                    case "GetByEmailAddress":
                        {
                            var acc = await _serv.GetByEmailAddress(stringParameter);
                            collec = new List<EmailDTO?> { acc };
                        }
                        break;
                    default:
                        { // Якщо немає правильного варіанту - повернути пусту колекцію
                            collec = new List<EmailDTO>();
                        }
                        break;
                }

                return collec?.ToList();
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
        
         */


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailDTO>>> GetEmails([FromQuery]EmailQuery emailQuery)
        {
            try
            {
                IEnumerable<EmailDTO?> collec = null;
                switch (emailQuery.SearchParameter)
                {
                    case "GetAll":
                        { collec = await _serv.GetAll(); }
                        break;
                    case "GetByEmailId":
                        {
                            if (emailQuery.EmailId == null)
                            {
                                throw new ValidationException("Не вказано EmailId для пошуку!", nameof(emailQuery.EmailId));
                            }
                            var acc = await _serv.GetById((long)emailQuery.EmailId);
                            collec = new List<EmailDTO?> { acc };
                        }
                        break;
                    case "GetByPersonId":
                        {
                            if (emailQuery.PersonId == null)
                            {
                                throw new ValidationException("Не вказано PersonId для пошуку!", nameof(emailQuery.PersonId));
                            }
                            collec = await _serv.GetByPersonId((int)emailQuery.PersonId);
                        }
                        break;
                    case "GetByClientId":
                        {
                            if (emailQuery.ClientId == null)
                            {
                                throw new ValidationException("Не вказано ClientId для пошуку!", nameof(emailQuery.ClientId));
                            }
                            collec = await _serv.GetByClientId((int)emailQuery.ClientId);
                        }
                        break;
                    case "GetByTouragencyEmployeeId":
                        {
                            if (emailQuery.TouragencyEmployeeId == null)
                            {
                                throw new ValidationException("Не вказано TouragencyEmployeeId для пошуку!", nameof(emailQuery.TouragencyEmployeeId));
                            }
                            collec = await _serv.GetByTouragencyEmployeeId((int)emailQuery.TouragencyEmployeeId);
                        }
                        break;
                    case "GetByContactTypeId":
                        {
                            if (emailQuery.ContactTypeId == null)
                            {
                                throw new ValidationException("Не вказано ContactTypeId для пошуку!", nameof(emailQuery.ContactTypeId));
                            }
                            collec = await _serv.GetByContactTypeId((int)emailQuery.ContactTypeId);
                        }
                        break;
                    case "GetByEmailAddress":
                        {
                            if (emailQuery.EmailAddress == null)
                            {
                                throw new ValidationException("Не вказано EmailAddress для пошуку!", nameof(emailQuery.EmailAddress));
                            }
                            collec = await _serv.GetByEmailAddress(emailQuery?.EmailAddress);
                        }
                        break;
                    default:
                        { // Якщо немає правильного варіанту - повернути пусту колекцію
                            collec = new List<EmailDTO>();
                        }
                        break;
                }

                return collec?.ToList();
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
        public async Task<ActionResult> AddEmail(EmailDTO emailDTO)
        {
            try
            {
                await _serv.TryToAddNewEmail(emailDTO);
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
        public async Task<ActionResult> UpdateEmail(EmailDTO emailDTO)
        {
            try
            {
                await _serv.Update(emailDTO);
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
        public async Task<ActionResult> DeleteEmail(long id)
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

    public class EmailQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? EmailId { get; set; }
        public int? PersonId { get; set; }
        public int? ClientId { get; set; }
        public int? TouragencyEmployeeId { get; set; }
        public int? ContactTypeId { get; set; }
        public string? EmailAddress { get; set; }
    }

}
