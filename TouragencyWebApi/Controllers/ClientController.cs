using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _serv;
        IWebHostEnvironment _appEnvironment;
        public ClientController(IClientService serv, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClient([FromQuery] ClientQuery clientQuery)
        {
            try
            {
                IEnumerable<ClientDTO> collection = null;
                switch (clientQuery.SearchParameter)
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
                            if (clientQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано ClientId для пошуку!", nameof(clientQuery.Id));
                            }
                            var client = await _serv.GetById(clientQuery.Id.Value);
                            if (client != null)
                            {
                                collection = new List<ClientDTO?> { client };
                            }
                        }
                        break;
                    case "GetByPersonId":
                        {
                            if (clientQuery.PersonId is null)
                            {
                                throw new ValidationException("Не вказано PersonId для пошуку!", nameof(clientQuery.PersonId));
                            }
                            var cl = await _serv.GetByPersonId(clientQuery.PersonId.Value);
                            if(cl != null)
                                collection = new List<ClientDTO?> { cl };
                        }
                        break;
                    case "GetByBookingId":
                        {
                            if (clientQuery.BookingId is null)
                            {
                                throw new ValidationException("Не вказано BookingId для пошуку!", nameof(clientQuery.BookingId));
                            }
                            var cl = await _serv.GetByBookingId(clientQuery.BookingId.Value);
                            if (cl != null)
                                collection = new List<ClientDTO?> { cl };
                        }
                        break;
                    case "GetByTouristNickname":
                        {
                            if (clientQuery.TouristNickname == null)
                            {
                                throw new ValidationException("Не вказано TouristNickname для пошуку!", nameof(clientQuery.TouristNickname));
                            }
                            collection = await _serv.GetByTouristNickname(clientQuery.TouristNickname);
                        }
                        break;
                    case "GetByFirstname":
                        {
                            if (clientQuery.Firstname == null)
                            {
                                throw new ValidationException("Не вказано Firstname для пошуку!", nameof(clientQuery.Firstname));
                            }
                            collection = await _serv.GetByFirstname(clientQuery.Firstname);
                        }
                        break;
                    case "GetByLastname":
                        {
                            if (clientQuery.Lastname == null)
                            {
                                throw new ValidationException("Не вказано Lastname для пошуку!", nameof(clientQuery.Lastname));
                            }
                            collection = await _serv.GetByLastname(clientQuery.Lastname);
                        }
                        break;
                    case "GetByMiddlename":
                        {
                            if (clientQuery.Middlename == null)
                            {
                                throw new ValidationException("Не вказано Middlename для пошуку!", nameof(clientQuery.Middlename));
                            }
                            collection = await _serv.GetByMiddlename(clientQuery.Middlename);
                        }
                        break;
                    case "GetByPhoneNumber":
                        {
                            if (clientQuery.Phone == null)
                            {
                                throw new ValidationException("Не вказано Phone для пошуку!", nameof(clientQuery.Phone));
                            }
                            collection = await _serv.GetByPhoneNumber(clientQuery.Phone);
                        }
                        break;
                    case "GetByEmailAddress":
                        {
                            if (clientQuery.Email == null)
                            {
                                throw new ValidationException("Не вказано Email для пошуку!", nameof(clientQuery.Email));
                            }
                            collection = await _serv.GetByEmailAddress(clientQuery.Email);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(clientQuery.TouristNickname, clientQuery.Email, clientQuery.Phone, clientQuery.Firstname, clientQuery.Lastname, clientQuery.Middlename);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Неправильно вказаний параметр пошуку!", nameof(clientQuery.SearchParameter));
                        }

                }
                if (collection.IsNullOrEmpty())
                {
                    return NoContent();
                }
                return Ok(collection);
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
        public async Task<ActionResult<ClientDTO>> UpdateClient(ClientDTO clientDTO)
        {
            try
            {
                var dto = await _serv.Update(clientDTO);
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
        [Route("UploadAvatarImage")]
        public async Task<ActionResult<string>> UploadAvatarImage([FromForm] int clientId, [FromForm] IFormFile FormFile)
        {
            try
            {
                if (FormFile is null)
                {
                    throw new ValidationException("Файл не було завантажено!", nameof(FormFile));
                }
                var clientData = await _serv.GetById(clientId);
                if (clientData is null)
                {
                    throw new ValidationException("Клієнт не знайдений!", nameof(clientId));
                }
                if (clientData.AvatarImagePath != null)
                {
                    var oldFileUri = new Uri(clientData.AvatarImagePath);
                    var oldFilePath = Path.Combine(_appEnvironment.WebRootPath, oldFileUri.AbsolutePath.TrimStart('/'));
                    Console.WriteLine(oldFilePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                // получаем имя файла
                string fileName = System.IO.Path.GetFileNameWithoutExtension(FormFile.FileName);

                // генерируем новый GUID
                string guid = Guid.NewGuid().ToString();

                // добавляем GUID к имени файла
                string newFileName = $"{fileName}_{guid}{Path.GetExtension(FormFile.FileName)}";

                // Путь к папке Files
                string path = "/ClientAvatarImages/" + newFileName; // новое имя файла

                // Сохраняем файл в папку Files в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await FormFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                //return new ObjectResult(_appEnvironment.WebRootPath + path);
                return new ObjectResult(path);
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
        public async Task<ActionResult<ClientDTO>> DeleteClient(int id)
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

    public class ClientQuery
    {
        public string SearchParameter { get; set; } = "";
        public int? Id { get; set; }
        public int? PersonId { get; set; }
        public long? BookingId { get; set; }
        public string? TouristNickname { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Middlename { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
