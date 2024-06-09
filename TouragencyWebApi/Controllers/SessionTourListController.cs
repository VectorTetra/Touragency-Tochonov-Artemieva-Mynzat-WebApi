using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TouragencyWebApi.Controllers
{
    [Route("api/SessionTourList")]
    [ApiController]
    public class SessionTourListController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SessionTourListQuery>> Get([FromQuery] SessionTourListQuery query)
        {
            try
            {
                switch (query.SearchParameter)
                {
                    case "CountryName":
                        {
                            var countryName = HttpContext.Session.GetString("CountryName");
                            if (countryName == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionTourListQuery { CountryName = countryName });
                        }
                    case "ContinentName":
                        {
                            var continentName = HttpContext.Session.GetString("ContinentName");
                            if (continentName == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionTourListQuery { ContinentName = continentName });
                        }
                    case "TransportTypeName":
                        {
                            var transportTypeName = HttpContext.Session.GetString("TransportTypeName");
                            if (transportTypeName == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionTourListQuery { TransportTypeName = transportTypeName });
                        }
                    case "GetAll":
                        {
                            var countryName = HttpContext.Session.GetString("CountryName");
                            var continentName = HttpContext.Session.GetString("ContinentName");
                            var transportTypeName = HttpContext.Session.GetString("TransportTypeName");

                            if (countryName == null && continentName == null && transportTypeName == null)
                            {
                                // Всі змінні сесії є null
                                return NoContent();
                            }
                            else
                            {
                                return Ok(new SessionTourListQuery
                                {
                                    CountryName = countryName,
                                    ContinentName = continentName,
                                    TransportTypeName = transportTypeName
                                });
                            }
                        }

                    default:
                        {
                            throw new Exception("Неправильно вказаний параметр пошуку!");
                        }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public void Post(SessionTourListQuery query)
        {
            if (query.CountryName != null)
            {
                HttpContext.Session.SetString("CountryName", query.CountryName);
            }
            if (query.ContinentName != null)
            {
                HttpContext.Session.SetString("ContinentName", query.ContinentName);
            }
            if (query.TransportTypeName != null)
            {
                HttpContext.Session.SetString("TransportTypeName", query.TransportTypeName);
            }
            
        }

        [HttpDelete]
        public void Delete()
        {
            HttpContext.Session.Remove("CountryName");
            HttpContext.Session.Remove("ContinentName");
            HttpContext.Session.Remove("TransportTypeName");
        }
    }

    public class SessionTourListQuery
    {
        public string? SearchParameter { get; set; }
        public string? CountryName { get; set; }
        public string? ContinentName { get; set; }
        public string? TransportTypeName { get; set; }
    }
}
