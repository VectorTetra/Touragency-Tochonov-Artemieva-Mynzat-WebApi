using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.Controllers
{
    [Route("api/ReviewImage")]
    [ApiController]
    public class ReviewImageController : ControllerBase
    {
        private readonly IReviewImageService _serv;
        IWebHostEnvironment _appEnvironment;

        public ReviewImageController(IReviewImageService reviewImageService, IWebHostEnvironment appEnvironment)
        {
            _serv = reviewImageService;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewImageDTO>>> Get([FromQuery] ReviewImageQuery reviewImageQuery)
        {
            try
            {
                IEnumerable<ReviewImageDTO> collection = null;
                switch (reviewImageQuery.SearchParameter)
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
                            if (reviewImageQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано ReviewImageId для пошуку!", nameof(reviewImageQuery.Id));
                            }
                            var acc = await _serv.GetById((long)reviewImageQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<ReviewImageDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByReviewId":
                        {
                            if (reviewImageQuery.ReviewId is null)
                            {
                                throw new ValidationException("Не вказано ReviewId для пошуку!", nameof(reviewImageQuery.ReviewId));
                            }
                            collection = await _serv.GetByReviewId((long)reviewImageQuery.ReviewId);
                        }
                        break;
                    case "GetByImagePath":
                        {
                            if (reviewImageQuery.ImagePath is null)
                            {
                                throw new ValidationException("Не вказано ImagePath для пошуку!", nameof(reviewImageQuery.ImagePath));
                            }
                            collection = await _serv.GetByImagePathSubstring(reviewImageQuery.ImagePath);
                        }
                        break;
                        case "GetByTourId":
                        {
                            if (reviewImageQuery.TourId is null)
                            {
                                throw new ValidationException("Не вказано TourId для пошуку!", nameof(reviewImageQuery.TourId));
                            }
                            collection = await _serv.GetByTourId((long)reviewImageQuery.TourId);
                        }
                        break;
                        case "GetByTourNameId":
                        {
                            if (reviewImageQuery.TourNameId is null)
                            {
                                throw new ValidationException("Не вказано TourNameId для пошуку!", nameof(reviewImageQuery.TourNameId));
                            }
                            collection = await _serv.GetByTourNameId((int)reviewImageQuery.TourNameId);
                        }
                        break;
                        case "GetByTourName":
                        {
                            if (reviewImageQuery.TourName is null)
                            {
                                throw new ValidationException("Не вказано TourName для пошуку!", nameof(reviewImageQuery.TourName));
                            }
                            collection = await _serv.GetByTourName(reviewImageQuery.TourName);
                        }
                        break;
                        case "GetByClientFirstname":
                        {
                            if (reviewImageQuery.ClientFirstname is null)
                            {
                                throw new ValidationException("Не вказано ClientFirstname для пошуку!", nameof(reviewImageQuery.ClientFirstname));
                            }
                            collection = await _serv.GetByClientFirstname(reviewImageQuery.ClientFirstname);
                        }
                        break;
                        case "GetByClientLastname":
                        {
                            if (reviewImageQuery.ClientLastname is null)
                            {
                                throw new ValidationException("Не вказано ClientLastname для пошуку!", nameof(reviewImageQuery.ClientLastname));
                            }
                            collection = await _serv.GetByClientLastname(reviewImageQuery.ClientLastname);
                        }
                        break;
                        case "GetByTouristNickname":
                        {
                            if (reviewImageQuery.TouristNickname is null)
                            {
                                throw new ValidationException("Не вказано TouristNickname для пошуку!", nameof(reviewImageQuery.TouristNickname));
                            }
                            collection = await _serv.GetByTouristNickname(reviewImageQuery.TouristNickname);
                        }
                        break;
                        case "GetByCountryName":
                        {
                            if (reviewImageQuery.CountryName is null)
                            {
                                throw new ValidationException("Не вказано CountryName для пошуку!", nameof(reviewImageQuery.CountryName));
                            }
                            collection = await _serv.GetByCountryName(reviewImageQuery.CountryName);
                        }
                        break;
                        case "GetBySettlementName":
                        {
                            if (reviewImageQuery.SettlementName is null)
                            {
                                throw new ValidationException("Не вказано SettlementName для пошуку!", nameof(reviewImageQuery.SettlementName));
                            }
                            collection = await _serv.GetBySettlementName(reviewImageQuery.SettlementName);
                        }
                        break;
                        case "GetByHotelName":
                        {
                            if (reviewImageQuery.HotelName is null)
                            {
                                throw new ValidationException("Не вказано HotelName для пошуку!", nameof(reviewImageQuery.HotelName));
                            }
                            collection = await _serv.GetByHotelName(reviewImageQuery.HotelName);
                        }
                        break;
                        case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(reviewImageQuery.ReviewId, 
                                reviewImageQuery.MinRating, reviewImageQuery.MaxRating, reviewImageQuery.ImagePath, reviewImageQuery.TourId,
                                reviewImageQuery.TourNameId, reviewImageQuery.TourName, reviewImageQuery.ClientFirstname, 
                                reviewImageQuery.ClientLastname, reviewImageQuery.ClientMiddlename, reviewImageQuery.TouristNickname, 
                                reviewImageQuery.CountryName, reviewImageQuery.SettlementName, reviewImageQuery.HotelName);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр reviewQuery.SearchParameter!", nameof(reviewImageQuery.SearchParameter));
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
        public async Task<ActionResult<ReviewImageDTO>> Create(ReviewImageDTO reviewImageDTO)
        {
            try
            {
                var dto = await _serv.Create(reviewImageDTO);
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
        [Route("UploadReviewImage")]
        public async Task<ActionResult<ICollection<string>>> PostReviewImage([FromForm] long ReviewId, [FromForm] IFormFileCollection FormFiles)
        {
            try
            {
                if (FormFiles is null || FormFiles.Count == 0)
                {
                    throw new ValidationException("Файли не було завантажено!", nameof(FormFiles));
                }
                List<string> paths = new List<string>();
                foreach (var FormFile in FormFiles)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(FormFile.FileName);
                    fileName = fileName.Replace(" ", "_");

                    // генерируем новый GUID
                    string guid = Guid.NewGuid().ToString();

                    // добавляем GUID к имени файла
                    string newFileName = $"{fileName}_{guid}{Path.GetExtension(FormFile.FileName)}";

                    // Путь к папке Files
                    string path = "/ReviewImages/" + newFileName; // новое имя файла

                    // Сохраняем файл в папку Files в каталоге wwwroot
                    // Для получения полного пути к каталогу wwwroot
                    // применяется свойство WebRootPath объекта IWebHostEnvironment
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await FormFile.CopyToAsync(fileStream); // копируем файл в поток
                    }
                    //return new ObjectResult(_appEnvironment.WebRootPath + path);
                    path = "https://26.162.95.213:7100" + path;
                    await _serv.Create(new ReviewImageDTO { Id=0,ReviewId = ReviewId, ImagePath = path });
                    paths.Add(path);
                }
                return new ObjectResult(paths);
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
        public async Task<ActionResult<ReviewImageDTO>> Update(ReviewImageDTO reviewImageDTO)
        {
            try
            {
                var dto = await _serv.Update(reviewImageDTO);
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
        [Route("UploadReviewImage")]
        public async Task<ActionResult<ICollection<string>>> PutReviewImage([FromForm] long ReviewId, [FromForm] IFormFileCollection FormFiles)
        {
            try
            {
                if (FormFiles is null || FormFiles.Count == 0)
                {
                    throw new ValidationException("Файли не було завантажено!", nameof(FormFiles));
                }
                List<string> paths = new List<string>();
                var oldImages = await _serv.GetByReviewId(ReviewId);
                var oldImagesIds = oldImages.Select(x => x.Id).ToList();
                foreach (var oldImage in oldImages)
                {
                    if (oldImage.ImagePath != null)
                    {
                        var oldFileUri = new Uri(oldImage.ImagePath);
                        var oldFilePath = Path.Combine(_appEnvironment.WebRootPath, oldFileUri.AbsolutePath.TrimStart('/'));
                        Console.WriteLine(oldFilePath);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    await _serv.Delete(oldImage.Id);
                }
                foreach (var FormFile in FormFiles)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(FormFile.FileName);
                    fileName = fileName.Replace(" ", "_");
                    // генерируем новый GUID
                    string guid = Guid.NewGuid().ToString();

                    // добавляем GUID к имени файла
                    string newFileName = $"{fileName}_{guid}{Path.GetExtension(FormFile.FileName)}";

                    // Путь к папке Files
                    string path = "/ReviewImages/" + newFileName; // новое имя файла

                    // Сохраняем файл в папку Files в каталоге wwwroot
                    // Для получения полного пути к каталогу wwwroot
                    // применяется свойство WebRootPath объекта IWebHostEnvironment
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await FormFile.CopyToAsync(fileStream); // копируем файл в поток
                    }
                    //return new ObjectResult(_appEnvironment.WebRootPath + path);
                    path = "https://26.162.95.213:7100" + path;
                    var dto = await _serv.Create(new ReviewImageDTO { Id = 0, ReviewId = ReviewId, ImagePath = path });
                    paths.Add(path);
                }
                return new ObjectResult(paths);
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
        public async Task<ActionResult<ReviewImageDTO>> Delete(long id)
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
        [HttpDelete]
        [Route("DeleteReviewImage/{id}")]
        public async Task<ActionResult<ICollection<ReviewImageDTO>>> DeleteReviewImage(long id)
        {
            var deletedImagesDTO = new List<ReviewImageDTO>();
            var oldImages = await _serv.GetByReviewId(id);
            var oldImagesIds = oldImages.Select(x => x.Id).ToList();
            foreach (var oldImage in oldImages)
            {
                if (oldImage.ImagePath != null)
                {
                    var oldFileUri = new Uri(oldImage.ImagePath);
                    var oldFilePath = Path.Combine(_appEnvironment.WebRootPath, oldFileUri.AbsolutePath.TrimStart('/'));
                    Console.WriteLine(oldFilePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                deletedImagesDTO.Add(await _serv.Delete(oldImage.Id));
            }
            return new ObjectResult(deletedImagesDTO);
        }
    }

    public class ReviewImageQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? Id { get; set; }
        public long? ReviewId { get; set; }
        public long? TourId { get; set; }
        public int? TourNameId { get; set; }
        public short? MinRating { get; set; }
        public short? MaxRating { get; set; }
        public string? ImagePath { get; set; }
        public string? TourName { get; set; }
        public string? ClientFirstname { get; set; }
        public string? ClientLastname { get; set; }
        public string? ClientMiddlename { get; set; }
        public string? TouristNickname { get; set; }
        public string? CountryName { get; set; }
        public string? SettlementName { get; set; }
        public string? HotelName { get; set; }
    }
}


