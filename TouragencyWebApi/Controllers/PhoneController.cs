using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Phone")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneService  _serv;
        public PhoneController(IPhoneService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneDTO>>> GetPhones([FromQuery] PhoneQuery phoneQuery)
        {
            try
            {
                IEnumerable<PhoneDTO?> collec = null;
                switch (phoneQuery.SearchParameter)
                {
                    case "GetAll":
                        { collec = await _serv.GetAll(); }
                        break;
                    case "GetByPhoneId":
                        {
                            if (phoneQuery.PhoneId == null)
                            {
                                throw new ValidationException("Не вказано PhoneId для пошуку!", nameof(phoneQuery.PhoneId));
                            }
                            var acc = await _serv.GetById((long)phoneQuery.PhoneId);
                            collec = new List<PhoneDTO?> { acc };
                        }
                        break;
                    case "GetByPersonId":
                        {
                            if (phoneQuery.PersonId == null)
                            {
                                throw new ValidationException("Не вказано PersonId для пошуку!", nameof(phoneQuery.PersonId));
                            }
                            collec = await _serv.GetByPersonId((int)phoneQuery.PersonId);
                        }
                        break;
                    case "GetByClientId":
                        {
                            if (phoneQuery.ClientId == null)
                            {
                                throw new ValidationException("Не вказано ClientId для пошуку!", nameof(phoneQuery.ClientId));
                            }
                            collec = await _serv.GetByClientId((int)phoneQuery.ClientId);
                        }
                        break;
                    case "GetByTouragencyEmployeeId":
                        {
                            if (phoneQuery.TouragencyEmployeeId == null)
                            {
                                throw new ValidationException("Не вказано TouragencyEmployeeId для пошуку!", nameof(phoneQuery.TouragencyEmployeeId));
                            }
                            collec = await _serv.GetByTouragencyEmployeeId((int)phoneQuery.TouragencyEmployeeId);
                        }
                        break;
                    case "GetByContactTypeId":
                        {
                            if (phoneQuery.ContactTypeId == null)
                            {
                                throw new ValidationException("Не вказано ContactTypeId для пошуку!", nameof(phoneQuery.ContactTypeId));
                            }
                            collec = await _serv.GetByContactTypeId((int)phoneQuery.ContactTypeId);
                        }
                        break;
                    case "GetByPhoneNumber":
                        {
                            if (phoneQuery.PhoneNumber == null)
                            {
                                throw new ValidationException("Не вказано PhoneAddress для пошуку!", nameof(phoneQuery.PhoneNumber));
                            }
                            collec = await _serv.GetByPhoneNumber(phoneQuery?.PhoneNumber);
                        }
                        break;
                    default:
                        { // Якщо немає правильного варіанту - повернути пусту колекцію
                            collec = new List<PhoneDTO>();
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
        public async Task<ActionResult> AddPhone(PhoneDTO phoneDTO)
        {
            try
            {
                await _serv.TryToAddNewPhone(phoneDTO);
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
        public async Task<ActionResult> UpdatePhone(PhoneDTO phoneDTO)
        {
            try
            {
                await _serv.Update(phoneDTO);
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
        public async Task<ActionResult> DeletePhone(long id)
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

    public class PhoneQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? PhoneId { get; set; }
        public int? PersonId { get; set; }
        public int? ClientId { get; set; }
        public int? TouragencyEmployeeId { get; set; }
        public int? ContactTypeId { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

