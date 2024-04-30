using Microsoft.AspNetCore.Mvc;

namespace TouragencyWebApi.Controllers
{
    [Route("api/SessionAuthorization")]
    [ApiController]
    public class SessionAuthorizationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<ObjectResult> Get([FromQuery] SessionAuthorizationQuery query)
        {
            try
            {
                switch (query.SearchParameter)
                {
                    case "ClientId":
                        {
                            var clientId = HttpContext.Session.GetInt32("ClientId");
                            if (clientId == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { ClientId = clientId });
                        }
                    case "TouragencyEmployeeId":
                        {
                            var touragencyEmployeeId = HttpContext.Session.GetInt32("TouragencyEmployeeId");
                            if (touragencyEmployeeId == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { TouragencyEmployeeId = touragencyEmployeeId });
                        }
                    case "TouragencyAccountRoleId":
                        {
                            var touragencyAccountRoleId = HttpContext.Session.GetInt32("TouragencyAccountRoleId");
                            if (touragencyAccountRoleId == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { TouragencyAccountRoleId = touragencyAccountRoleId });
                        }
                    case "TouragencyEmployeeLogin":
                        {
                            var touragencyEmployeeLogin = HttpContext.Session.GetString("TouragencyEmployeeLogin");
                            if (touragencyEmployeeLogin == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { TouragencyEmployeeLogin = touragencyEmployeeLogin });
                        }
                    case "ClientTouristNickname":
                        {
                            var clientTouristNickname = HttpContext.Session.GetString("ClientTouristNickname");
                            if (clientTouristNickname == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { ClientTouristNickname = clientTouristNickname });
                        }
                    case "IsClient":
                        {
                            var isClient = HttpContext.Session.GetInt32("IsClient");
                            if (isClient == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { IsClient = isClient });
                        }
                    case "GetAll":
                        {
                            var ClientId = HttpContext.Session.GetInt32("ClientId");
                            var TouragencyEmployeeId = HttpContext.Session.GetInt32("TouragencyEmployeeId");
                            var TouragencyAccountRoleId = HttpContext.Session.GetInt32("TouragencyAccountRoleId");
                            var TouragencyEmployeeLogin = HttpContext.Session.GetString("TouragencyEmployeeLogin");
                            var ClientTouristNickname = HttpContext.Session.GetString("ClientTouristNickname");
                            var IsClient = HttpContext.Session.GetInt32("IsClient");

                            if (ClientId == null && TouragencyEmployeeId == null && TouragencyAccountRoleId == null &&
                                TouragencyEmployeeLogin == null && ClientTouristNickname == null && IsClient == null)
                            {
                                // Всі змінні сесії є null
                                return NoContent();
                            }
                            else
                            {
                                return Ok(new SessionAuthorizationQuery
                                {
                                    ClientId = ClientId,
                                    TouragencyEmployeeId = TouragencyEmployeeId,
                                    TouragencyAccountRoleId = TouragencyAccountRoleId,
                                    TouragencyEmployeeLogin = TouragencyEmployeeLogin,
                                    ClientTouristNickname = ClientTouristNickname,
                                    IsClient = IsClient
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
        public void Post([FromBody] SessionAuthorizationQuery query)
        {
            if (query.ClientId != null)
            {
                HttpContext.Session.SetInt32("ClientId", query.ClientId.Value);
            }
            if (query.TouragencyEmployeeId != null)
            {
                HttpContext.Session.SetInt32("TouragencyEmployeeId", query.TouragencyEmployeeId.Value);
            }
            if (query.TouragencyAccountRoleId != null)
            {
                HttpContext.Session.SetInt32("TouragencyAccountRoleId", query.TouragencyAccountRoleId.Value);
            }
            if (query.TouragencyEmployeeLogin != null)
            {
                HttpContext.Session.SetString("TouragencyEmployeeLogin", query.TouragencyEmployeeLogin);
            }
            if (query.ClientTouristNickname != null)
            {
                HttpContext.Session.SetString("ClientTouristNickname", query.ClientTouristNickname);
            }
            if (query.IsClient != null)
            {
                HttpContext.Session.SetInt32("IsClient", (int)query.IsClient);
            }
        }

        [HttpDelete]
        public void Delete()
        {
            HttpContext.Session.Remove("ClientId");
            HttpContext.Session.Remove("TouragencyEmployeeId");
            HttpContext.Session.Remove("TouragencyAccountRoleId");
            HttpContext.Session.Remove("TouragencyEmployeeLogin");
            HttpContext.Session.Remove("ClientTouristNickname");
            HttpContext.Session.Remove("IsClient");
        }
    }

    public class SessionAuthorizationQuery
    {
        public string? SearchParameter { get; set; }
        public int? ClientId { get; set; }
        public int? TouragencyEmployeeId { get; set; }
        // поверне 1, якщо клієнт, 0 - якщо співробітник турагенції
        public int? IsClient { get; set; }
        public string? TouragencyEmployeeLogin { get; set; }
        public string? ClientTouristNickname { get; set; }
        public int? TouragencyAccountRoleId { get; set; }
    }
}
