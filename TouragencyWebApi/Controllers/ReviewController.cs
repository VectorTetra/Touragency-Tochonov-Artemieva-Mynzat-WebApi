﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;


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
                    case "Get200Last":
                        {
                            collection = await _serv.Get200Last();
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
                    case "GetByTourName":
                        {
                            if (reviewQuery.TourName is null)
                            {
                                throw new ValidationException("Не вказано TourName для пошуку!", nameof(reviewQuery.TourName));
                            }
                            collection = await _serv.GetByTourNameSubstring(reviewQuery.TourName);
                        }
                        break;
                    case "GetByTouristNickname":
                        {
                            if (reviewQuery.TouristNickname is null)
                            {
                                throw new ValidationException("Не вказано TouristNickname для пошуку!", nameof(reviewQuery.TouristNickname));
                            }
                            collection = await _serv.GetByTouristNicknameSubstring(reviewQuery.TouristNickname);
                        }
                        break;
                    case "GetByClientFirstname":
                        {
                            if (reviewQuery.ClientFirstname is null)
                            {
                                throw new ValidationException("Не вказано ClientFirstname для пошуку!", nameof(reviewQuery.ClientFirstname));
                            }
                            collection = await _serv.GetByClientFirstnameSubstring(reviewQuery.ClientFirstname);
                        }
                        break;
                    case "GetByClientLastname":
                        {
                            if (reviewQuery.ClientLastname is null)
                            {
                                throw new ValidationException("Не вказано ClientLastname для пошуку!", nameof(reviewQuery.ClientLastname));
                            }
                            collection = await _serv.GetByClientLastnameSubstring(reviewQuery.ClientLastname);
                        }
                        break;
                    case "GetByClientMiddlename":
                        {
                            if (reviewQuery.ClientMiddlename is null)
                            {
                                throw new ValidationException("Не вказано ClientMiddlename для пошуку!", nameof(reviewQuery.ClientMiddlename));
                            }
                            collection = await _serv.GetByClientMiddlenameSubstring(reviewQuery.ClientMiddlename);
                        }
                        break;
                    case "GetByCountryName":
                        {
                            if (reviewQuery.CountryName is null)
                            {
                                throw new ValidationException("Не вказано CountryName для пошуку!", nameof(reviewQuery.CountryName));
                            }
                            collection = await _serv.GetByCountryNameSubstring(reviewQuery.CountryName);
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
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(reviewQuery.TourId, reviewQuery.ClientId, reviewQuery.CountryId, reviewQuery.ReviewImageId, reviewQuery.ReviewCaption, reviewQuery.ReviewText, reviewQuery.RatingMinValue, reviewQuery.RatingMaxValue, reviewQuery.CreationDateMinValue, reviewQuery.CreationDateMaxValue, reviewQuery.TourName, reviewQuery.TouristNickname, reviewQuery.ClientFirstname, reviewQuery.ClientLastname, reviewQuery.ClientMiddlename, reviewQuery.CountryName,reviewQuery.TourNameId);
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
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> AddReview(ReviewDTO reviewDTO)
        {
            try
            {
                var dto = await _serv.Create(reviewDTO);
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
        public async Task<ActionResult<ReviewDTO>> UpdateReview(ReviewDTO reviewDTO)
        {
            try
            {
                var dto = await _serv.Update(reviewDTO);
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
        public async Task<ActionResult<ReviewDTO>> DeleteReview(long id)
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

    public class ReviewQuery
    {
        public string SearchParameter { get; set; } = "";
        public long? Id { get; set; }
        public short? RatingMinValue { get; set; }
        public short? RatingMaxValue { get; set; }
        public int? ClientId { get; set; }
        public long? TourId { get; set; }
        public int? TourNameId { get; set; }
        public int? CountryId { get; set; }
        public string? ReviewCaption { get; set; }
        public string? ReviewText { get; set; }
        public string? TourName { get; set; }
        public string? TouristNickname { get; set; }
        public string? ClientFirstname { get; set; }
        public string? ClientLastname { get; set; }
        public string? ClientMiddlename { get; set; }
        public string? CountryName { get; set; }
        public DateTime? CreationDateMinValue { get; set; }
        public DateTime? CreationDateMaxValue { get; set; }
        public long? ReviewImageId { get; set; }
    }
}
