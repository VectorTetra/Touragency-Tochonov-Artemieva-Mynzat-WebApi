using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/ReviewImage")]
    [ApiController]
    public class ReviewImageController : ControllerBase
    {
        private readonly IReviewImageService _serv;

        public ReviewImageController(IReviewImageService reviewImageService)
        {
            _serv = reviewImageService;
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
        public async Task<ActionResult> Create(ReviewImageDTO reviewImageDTO)
        {
            try
            {
                await _serv.Create(reviewImageDTO);
                return Ok();
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
        public async Task<ActionResult> Update(ReviewImageDTO reviewImageDTO)
        {
            try
            {
                await _serv.Update(reviewImageDTO);
                return Ok();
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
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _serv.Delete(id);
                return Ok();
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

    public class ReviewImageQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? Id { get; set; }
        public long? ReviewId { get; set; }
        public string? ImagePath { get; set; }
    }
}


