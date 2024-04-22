using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using Microsoft.IdentityModel.Tokens;

namespace TouragencyWebApi.Controllers
{
    [Route("api/BedConfiguration")]
    [ApiController]
    public class BedConfigurationController : ControllerBase
    {
        private readonly IBedConfigurationService _serv;
        public BedConfigurationController(IBedConfigurationService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BedConfigurationDTO>>> GetBedConfigurations([FromQuery] BedConfigurationQuery bedConfigurationQuery)
        {
            try
            {
                IEnumerable<BedConfigurationDTO> collection = null;
                switch (bedConfigurationQuery.SearchParameter)
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
                            if (bedConfigurationQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано BedConfigurationId для пошуку!", nameof(bedConfigurationQuery.Id));
                            }
                            var acc = await _serv.GetById((int)bedConfigurationQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<BedConfigurationDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByLabelSubstring":
                        {
                            if (bedConfigurationQuery.Label is null)
                            {
                                throw new ValidationException("Не вказано Label для пошуку!", nameof(bedConfigurationQuery.Label));
                            }
                            collection = await _serv.GetByLabelSubstring(bedConfigurationQuery.Label);
                        }
                        break;
                    case "GetByDescriptionSubstring":
                        {
                            if (bedConfigurationQuery.Description is null)
                            {
                                throw new ValidationException("Не вказано Description для пошуку!", nameof(bedConfigurationQuery.Description));
                            }
                            collection = await _serv.GetByDescriptionSubstring(bedConfigurationQuery.Description);
                        }
                        break;
                    case "GetByCapacity":
                        {
                            if (bedConfigurationQuery.Capacity is null)
                            {
                                throw new ValidationException("Не вказано Capacity для пошуку!", nameof(bedConfigurationQuery.Capacity));
                            }
                            collection = await _serv.GetByCapacity((short)bedConfigurationQuery.Capacity);
                        }
                        break;
                    case "GetByHotelId":
                        {
                            if (bedConfigurationQuery.HotelId is null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(bedConfigurationQuery.HotelId));
                            }
                            collection = await _serv.GetByHotelId((int)bedConfigurationQuery.HotelId);
                        }
                        break;
                    case "GetByBookingDataId":
                        {
                            if (bedConfigurationQuery.BookingDataId is null)
                            {
                                throw new ValidationException("Не вказано BookingDataId для пошуку!", nameof(bedConfigurationQuery.BookingDataId));
                            }
                            collection = await _serv.GetByBookingDataId((long)bedConfigurationQuery.BookingDataId);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр bedConfigurationQuery.SearchParameter!", nameof(bedConfigurationQuery.SearchParameter));
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
        public async Task<ActionResult<BedConfigurationDTO>> CreateBedConfiguration(BedConfigurationDTO bedConfigurationDTO)
        {
            try
            {
                var createdBConf = await _serv.Create(bedConfigurationDTO);
                return Ok(createdBConf);
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
        public async Task<ActionResult<BedConfigurationDTO>> UpdateBedConfiguration(BedConfigurationDTO bedConfigurationDTO)
        {
            try
            {
                var BConf = await _serv.Update(bedConfigurationDTO);
                return Ok(BConf);
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
        public async Task<ActionResult<BedConfigurationDTO>> DeleteBedConfiguration(int id)
        {
            try
            {
                var BConf = await _serv.Delete(id);
                return Ok(BConf);
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
    public class BedConfigurationQuery
    {
        public string SearchParameter { get; set; } = string.Empty;
        public int? Id { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public short? Capacity { get; set; }
        public int? HotelId { get; set; }
        public long? BookingDataId { get; set; }
    }
}
