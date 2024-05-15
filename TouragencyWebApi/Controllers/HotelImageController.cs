using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/HotelImage")]
    [ApiController]
    public class HotelImageController : ControllerBase
    {
        private readonly IHotelImageService _serv;
        private readonly IHotelService _hotelServ;
        IWebHostEnvironment _appEnvironment;
        public HotelImageController(IHotelImageService serv, IHotelService HotelServ, IWebHostEnvironment appEnvironment)
        {
            _serv = serv;
            _hotelServ = HotelServ;
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelImageDTO>>> GetHotelImages([FromQuery] HotelImageQuery hotelImageQuery)
        {
            try
            {
                IEnumerable<HotelImageDTO> collection = null;
                switch (hotelImageQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (hotelImageQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано Id для пошуку!", nameof(hotelImageQuery.Id));
                            }
                            var acc = await _serv.GetById((long)hotelImageQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<HotelImageDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByHotelId":
                        {
                            if (hotelImageQuery.HotelId is null)
                            {
                                throw new ValidationException("Не вказано HotelId для пошуку!", nameof(hotelImageQuery.HotelId));
                            }
                            collection = await _serv.GetByHotelId((int)hotelImageQuery.HotelId);
                        }
                        break;
                    case "GetByImageUrl":
                        {
                            if (hotelImageQuery.ImageUrl is null)
                            {
                                throw new ValidationException("Не вказано ImageUrl для пошуку!", nameof(hotelImageQuery.ImageUrl));
                            }
                            collection = await _serv.GetByImageUrlSubstring(hotelImageQuery.ImageUrl);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невідомий параметр пошуку!", nameof(hotelImageQuery.SearchParameter));
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
        public async Task<ActionResult<HotelImageDTO>> CreateHotelImage(HotelImageDTO hotelImageDTO)
        {
            try
            {
                // Додавання даних про зображення в БД
                var dto = await _serv.Create(hotelImageDTO);
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
        [HttpPost]
        [Route("UploadImage")]
        public async Task<ActionResult<string>> UploadImage([FromForm] int hotelId, [FromForm] IFormFile file)
        {
            try
            {
                if (file is null)
                {
                    throw new ValidationException("Файл не було завантажено!", nameof(file));
                }
                var hotelData = await _hotelServ.GetById(hotelId);
                if (hotelData is null)
                {
                    throw new ValidationException("Готель не знайдений!", nameof(hotelId));
                }
                //if (hotelData.AvatarImagePath != null)
                //{
                //    var oldFileUri = new Uri(hotelData.AvatarImagePath);
                //    var oldFilePath = Path.Combine(_appEnvironment.WebRootPath, oldFileUri.AbsolutePath.TrimStart('/'));

                //    if (System.IO.File.Exists(oldFilePath))
                //    {
                //        System.IO.File.Delete(oldFilePath);
                //    }
                //}
                // получаем имя файла
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);

                // генерируем новый GUID
                string guid = Guid.NewGuid().ToString();

                // добавляем GUID к имени файла
                string newFileName = $"{fileName}_{guid}{Path.GetExtension(file.FileName)}";

                // Путь к папке Files
                string path = "/HotelImages/" + newFileName; // новое имя файла

                // Сохраняем файл в папку Files в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream); // копируем файл в поток
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
        [HttpPut]
        public async Task<ActionResult<HotelImageDTO>> UpdateHotelImage(HotelImageDTO hotelImageDTO)
        {
            try
            {
                var dto = await _serv.Update(hotelImageDTO);
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
        [Route("UploadImage")]
        public async Task<ActionResult<HotelImageDTO>> PutImage([FromForm] int hotelId, [FromForm] IFormFile file)
        {
            try
            {
                if (file is null)
                {
                    throw new ValidationException("Файл не було завантажено!", nameof(file));
                }
                var hotelData = await _hotelServ.GetById(hotelId);
                if (hotelData is null)
                {
                    throw new ValidationException("Готель не знайдений!", nameof(hotelId));
                }
                var imgDto = new HotelImageDTO { HotelId = hotelId, ImageUrl = "", Id = 0 };
                if (!hotelData.HotelImages.IsNullOrEmpty())
                {
                    var imgList = hotelData.HotelImages.ToList();
                    var img = imgList[0];
                    imgDto.Id = img.Id;
                    var oldFileUri = new Uri(img.ImageUrl);
                    var oldFilePath = Path.Combine(_appEnvironment.WebRootPath, oldFileUri.AbsolutePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string guid = Guid.NewGuid().ToString();
                string newFileName = $"{fileName}_{guid}{Path.GetExtension(file.FileName)}";
                string path = "/HotelImages/" + newFileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                imgDto.ImageUrl = path;
                return imgDto;
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
        public async Task<ActionResult<HotelImageDTO>> DeleteHotelImage(long id)
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

    public class HotelImageQuery
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? ImageUrl { get; set; }
        public int? HotelId { get; set; }
    }
}
