using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.DAL.Entities;


namespace TouragencyWebApi.Controllers
{
    [Route("api/Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _serv;
        public ReviewController(IReviewService serv)
        {
            _serv = serv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews([FromQuery] ReviewQuery reviewQuery)
        {
            try
            {
                IEnumerable<ReviewDTO> collection = null;
                switch (reviewQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (reviewQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано ReviewId для пошуку!", nameof(reviewQuery.Id));
                            }
                            var acc = await _serv.GetById((long)reviewQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<ReviewDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByRating":
                        {
                            if (reviewQuery.RatingMinValue is null || reviewQuery.RatingMaxValue is null)
                            {
                                throw new ValidationException("Не вказано RatingMinValue або RatingMaxValue для пошуку!", nameof(reviewQuery.RatingMinValue));
                            }
                            collection = await _serv.GetByRatingDiapazone((short)reviewQuery.RatingMinValue, (short)reviewQuery.RatingMaxValue);
                        }
                        break;
                    case "GetByClientId":
                        {
                            if (reviewQuery.ClientId is null)
                            {
                                throw new ValidationException("Не вказано ClientId для пошуку!", nameof(reviewQuery.ClientId));
                            }
                            collection = await _serv.GetByClientId((int)reviewQuery.ClientId);
                        }
                        break;
                    case "GetByTourId":
                        {
                            if (reviewQuery.TourId is null)
                            {
                                throw new ValidationException("Не вказано TourId для пошуку!", nameof(reviewQuery.TourId));
                            }
                            collection = await _serv.GetByTourId((int)reviewQuery.TourId);
                        }
                        break;
                    case "GetByReviewCaption":
                        {
                            if (reviewQuery.ReviewCaption is null)
                            {
                                throw new ValidationException("Не вказано ReviewCaption для пошуку!", nameof(reviewQuery.ReviewCaption));
                            }
                            collection = await _serv.GetByReviewCaptionSubstring(reviewQuery.ReviewCaption);
                        }
                        break;
                    case "GetByReviewText":
                        {
                            if (reviewQuery.ReviewText is null)
                            {
                                throw new ValidationException("Не вказано ReviewText для пошуку!", nameof(reviewQuery.ReviewText));
                            }
                            collection = await _serv.GetByReviewTextSubstring(reviewQuery.ReviewText);
                        }
                        break;

                    case "GetByCreationDate":
                        {
                            if (reviewQuery.CreationDateMinValue is null || reviewQuery.CreationDateMaxValue is null)
                            {
                                throw new ValidationException("Не вказано CreationDateMinValue або CreationDateMaxValue для пошуку!", nameof(reviewQuery.CreationDateMinValue));
                            }
                            collection = await _serv.GetByCreationDateDiapazone((DateTime)reviewQuery.CreationDateMinValue, (DateTime)reviewQuery.CreationDateMaxValue);
                        }
                        break;
                    case "GetByReviewImageId":
                        {
                            if (reviewQuery.ReviewImageId is null)
                            {
                                throw new ValidationException("Не вказано ReviewImageId для пошуку!", nameof(reviewQuery.ReviewImageId));
                            }
                            collection = await _serv.GetByReviewImageId((long)reviewQuery.ReviewImageId);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Вказано неправильний параметр reviewQuery.SearchParameter!", nameof(reviewQuery.SearchParameter));
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
                return new ObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddReview(ReviewDTO reviewDTO)
        {
            try
            {
                await _serv.Create(reviewDTO);
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
        public async Task<ActionResult> UpdateReview(ReviewDTO reviewDTO)
        {
            try
            {
                await _serv.Update(reviewDTO);
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
        public async Task<ActionResult> DeleteReview(long id)
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

    public class ReviewQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? Id { get; set; }
        public short? RatingMinValue { get; set; }
        public short? RatingMaxValue { get; set; }
        public int? ClientId { get; set; }
        public int? TourId { get; set; }
        public string? ReviewCaption { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? CreationDateMinValue { get; set; }
        public DateTime? CreationDateMaxValue { get; set; }
        public long? ReviewImageId { get; set; }
    }
}
