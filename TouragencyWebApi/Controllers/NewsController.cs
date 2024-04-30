using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/News")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _serv;
        public NewsController(INewsService Service)
        {
            _serv = Service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetNews([FromQuery] NewsQuery newsQuery)
        {
            try
            {
                IEnumerable<NewsDTO> collection = null;
                switch (newsQuery.SearchParameter)
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
                            if (newsQuery.Id is null)
                            {
                                throw new ValidationException("Не вказано NewsId для пошуку!", nameof(newsQuery.Id));
                            }
                            var news = await _serv.GetById(newsQuery.Id.Value);
                            if (news != null)
                            {
                                collection = new List<NewsDTO?> { news };
                            }
                        }
                        break;
                    case "GetByCaption":
                        {
                            if (newsQuery.Caption is null)
                            {
                                throw new ValidationException("Не вказано Caption для пошуку!", nameof(newsQuery.Caption));
                            }
                            collection = await _serv.GetByCaptionSubstring(newsQuery.Caption);
                        }
                        break;
                    case "GetByText":
                        {
                            if (newsQuery.Text is null)
                            {
                                throw new ValidationException("Не вказано Text для пошуку!", nameof(newsQuery.Text));
                            }
                            collection = await _serv.GetByTextSubstring(newsQuery.Text);
                        }
                        break;
                    case "GetByPublishDateTimeDiapazon":
                        {
                            if (newsQuery.PublishDateTimeDiapazonStart is null || newsQuery.PublishDateTimeDiapazonEnd is null)
                            {
                                throw new ValidationException("Не вказано діапазон дат для пошуку!", nameof(newsQuery.PublishDateTimeDiapazonStart));
                            }
                            collection = await _serv.GetByDateDiapazon(newsQuery.PublishDateTimeDiapazonStart.Value, newsQuery.PublishDateTimeDiapazonEnd.Value);
                        }
                        break;
                    case "GetByIsVisible":
                        {
                            if (newsQuery.IsVisible is null)
                            {
                                throw new ValidationException("Не вказано IsVisible для пошуку!", nameof(newsQuery.IsVisible));
                            }
                            collection = await _serv.GetByVisibility(newsQuery.IsVisible.Value);
                        }
                        break;
                    case "GetByIsImportant":
                        {
                            if (newsQuery.IsImportant is null)
                            {
                                throw new ValidationException("Не вказано IsImportant для пошуку!", nameof(newsQuery.IsImportant));
                            }
                            collection = await _serv.GetByImportance(newsQuery.IsImportant.Value);
                        }
                        break;
                    case "GetByCompositeSearch":
                        {
                            collection = await _serv.GetByCompositeSearch(newsQuery.Caption, newsQuery.Text, newsQuery.PublishDateTimeDiapazonStart, newsQuery.PublishDateTimeDiapazonEnd, newsQuery.IsVisible, newsQuery.IsImportant);
                        }
                        break;
                    default:
                        {
                            throw new ValidationException("Невідомий параметр пошуку!", nameof(newsQuery.SearchParameter));
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
        public async Task<ActionResult<NewsDTO>> Post([FromBody] NewsDTO news)
        {
            try
            {
                var createdNews = await _serv.Create(news);
                return createdNews;
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
        public async Task<ActionResult<NewsDTO>> UpdateNews(NewsDTO news)
        {
            try
            {
                var updatedNews = await _serv.Update(news);
                return updatedNews;
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
        public async Task<ActionResult<NewsDTO>> DeleteNews(long id)
        {
            try
            {
                var deletedNews = await _serv.Delete(id);
                return deletedNews;
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



    public class NewsQuery
    {
        public string SearchParameter { get; set; } = "Get200Last";
        public long? Id { get; set; }
        public string? Caption { get; set; }
        public string? Text { get; set; }
        public DateTime? PublishDateTimeDiapazonStart { get; set; }
        public DateTime? PublishDateTimeDiapazonEnd { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsImportant { get; set; }
    }
}
