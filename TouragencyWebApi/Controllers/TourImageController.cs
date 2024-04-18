using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/TourImage")]
    [ApiController]
    public class TourImageController : ControllerBase
    {
        private readonly ITourImageService _serv;
        public TourImageController(ITourImageService serv)
        {
            _serv = serv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourImageDTO>>> GetTourImages([FromQuery] TourImageQuery tourImageQuery)
        {
            try
            {
                IEnumerable<TourImageDTO> collection = null;
                switch (tourImageQuery.SearchParameter)
                {
                    case "GetAll":
                        {
                            collection = await _serv.GetAll();
                        }
                        break;
                    case "GetById":
                        {
                            if (tourImageQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано TourImageId для пошуку!", nameof(tourImageQuery.Id));
                            }
                            var acc = await _serv.GetById((long)tourImageQuery.Id);
                            if (acc != null)
                            {
                                collection = new List<TourImageDTO?> { acc };
                            }
                        }
                        break;
                    case "GetByImageUrl":
                        {
                            if (tourImageQuery.ImageUrl is null)
                            {
                                throw new ValidationException("Не вказано ImageUrl для пошуку!", nameof(tourImageQuery.ImageUrl));
                            }
                            collection = await _serv.GetByImageUrlSubstring(tourImageQuery.ImageUrl);
                        }
                        break;
                    case "GetByTourNameId":
                        {
                            if (tourImageQuery.TourNameId is null)
                            {
                                throw new ValidationException("Не вказано TourNameId для пошуку!", nameof(tourImageQuery.TourNameId));
                            }
                            collection = await _serv.GetByTourNameId((int)tourImageQuery.TourNameId);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невідомий параметр пошуку!", nameof(tourImageQuery.SearchParameter));
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
        public async Task<ActionResult<TourImageDTO>> CreateTourImage(TourImageDTO tourImageDTO)
        {
            try
            {
                await _serv.Create(tourImageDTO);
                return Ok(tourImageDTO);
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
        public async Task<ActionResult<TourImageDTO>> UpdateTourImage(TourImageDTO tourImageDTO)
        {
            try
            {
                await _serv.Update(tourImageDTO);
                return Ok(tourImageDTO);
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
        public async Task<ActionResult<TourImageDTO>> DeleteTourImage(long id)
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

    public class TourImageQuery
    {
        public string SearchParameter { get; set; }
        public long? Id { get; set; }
        public string? ImageUrl { get; set; }
        public int? TourNameId { get; set; }
    }
}
