using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace TouragencyWebApi.Controllers
{
    [Route("api/SessionAuthorization")]
    [ApiController]
    public class SessionAuthorizationController : ControllerBase
    {
        /*
        [HttpGet]
        public ActionResult<SessionAuthorizationQuery> Get([FromQuery] SessionAuthorizationQuery query)
        {
            try
            {
                switch (query.SearchParameter)
                {
                    case "ClientId":
                        {
                            var clientId = HttpContext.Session.GetInt32("clientId");
                            if (clientId == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { clientId = clientId });
                        }
                    case "TouragencyEmployeeId":
                        {
                            var touragencyEmployeeId = HttpContext.Session.GetInt32("touragencyEmployeeId");
                            if (touragencyEmployeeId == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { touragencyEmployeeId = touragencyEmployeeId });
                        }
                    case "TouragencyAccountRoleId":
                        {
                            var touragencyAccountRoleId = HttpContext.Session.GetInt32("touragencyAccountRoleId");
                            if (touragencyAccountRoleId == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { touragencyAccountRoleId = touragencyAccountRoleId });
                        }
                    case "TouragencyEmployeeLogin":
                        {
                            var touragencyEmployeeLogin = HttpContext.Session.GetString("touragencyEmployeeLogin");
                            if (touragencyEmployeeLogin == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { touragencyEmployeeLogin = touragencyEmployeeLogin });
                        }
                    case "ClientTouristNickname":
                        {
                            var clientTouristNickname = HttpContext.Session.GetString("clientTouristNickname");
                            if (clientTouristNickname == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { clientTouristNickname = clientTouristNickname });
                        }
                    case "IsClient":
                        {
                            var isClient = HttpContext.Session.GetInt32("isClient");
                            if (isClient == null)
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { isClient = isClient });
                        }
                    case "GetAll":
                        {
                            var ClientId = HttpContext.Session.GetInt32("clientId");
                            var TouragencyEmployeeId = HttpContext.Session.GetInt32("touragencyEmployeeId");
                            var TouragencyAccountRoleId = HttpContext.Session.GetInt32("touragencyAccountRoleId");
                            var TouragencyEmployeeLogin = HttpContext.Session.GetString("touragencyEmployeeLogin");
                            var ClientTouristNickname = HttpContext.Session.GetString("clientTouristNickname");
                            var IsClient = HttpContext.Session.GetInt32("isClient");
                            Console.WriteLine($"Session keys {HttpContext.Session.Keys.Count()}");
                            Console.WriteLine($"Session ID {HttpContext.Session.Id}");
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
                                    clientId = ClientId,
                                    touragencyEmployeeId = TouragencyEmployeeId,
                                    touragencyAccountRoleId = TouragencyAccountRoleId,
                                    touragencyEmployeeLogin = TouragencyEmployeeLogin,
                                    clientTouristNickname = ClientTouristNickname,
                                    isClient = IsClient
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
        */

        [HttpGet]
        public ActionResult<SessionAuthorizationQuery> Get([FromQuery] SessionAuthorizationQuery query)
        {
            try
            {
                switch (query.SearchParameter)
                {
                    case "ClientId":
                        {
                            if (!Request.Cookies.TryGetValue("clientId", out string clientId))
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { clientId = int.Parse(clientId) });
                        }
                    case "TouragencyEmployeeId":
                        {
                            if (!Request.Cookies.TryGetValue("touragencyEmployeeId", out string touragencyEmployeeId))
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { touragencyEmployeeId = int.Parse(touragencyEmployeeId) });
                        }
                    case "TouragencyAccountRoleId":
                        {
                            if (!Request.Cookies.TryGetValue("touragencyAccountRoleId", out string touragencyAccountRoleId))
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { touragencyAccountRoleId = int.Parse(touragencyAccountRoleId) });
                        }
                    case "TouragencyEmployeeLogin":
                        {
                            if (!Request.Cookies.TryGetValue("touragencyEmployeeLogin", out string touragencyEmployeeLogin))
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { touragencyEmployeeLogin = touragencyEmployeeLogin });
                        }
                    case "ClientTouristNickname":
                        {
                            if (!Request.Cookies.TryGetValue("clientTouristNickname", out string clientTouristNickname))
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { clientTouristNickname = clientTouristNickname });
                        }
                    case "IsClient":
                        {
                            if (!Request.Cookies.TryGetValue("isClient", out string isClient))
                            {
                                return NoContent();
                            }
                            return Ok(new SessionAuthorizationQuery { isClient = int.Parse(isClient) });
                        }
                    case "GetAll":
                        {
                            int? dtoClientId = null;
                            int? dtoTouragencyEmployeeId = null;
                            int? dtoTouragencyAccountRoleId = null;
                            string? dtoTouragencyEmployeeLogin = null;
                            string? dtoClientTouristNickname = null;
                            int? dtoIsClient = null;
                            if (Request.Cookies.TryGetValue("clientId", out string clientId))
                            {
                                dtoClientId = int.Parse(clientId);
                            }
                            if (Request.Cookies.TryGetValue("touragencyEmployeeId", out string touragencyEmployeeId))
                            {
                                dtoTouragencyEmployeeId = int.Parse(touragencyEmployeeId);
                            }
                            if (Request.Cookies.TryGetValue("touragencyAccountRoleId", out string touragencyAccountRoleId))
                            {
                                dtoTouragencyAccountRoleId = int.Parse(touragencyAccountRoleId);
                            }
                            if (Request.Cookies.TryGetValue("touragencyEmployeeLogin", out string touragencyEmployeeLogin))
                            {
                                dtoTouragencyEmployeeLogin = touragencyEmployeeLogin;
                            }
                            if (Request.Cookies.TryGetValue("clientTouristNickname", out string clientTouristNickname))
                            {
                                dtoClientTouristNickname = clientTouristNickname;
                            }
                            if (Request.Cookies.TryGetValue("isClient", out string isClient))
                            {
                                dtoIsClient = int.Parse(isClient);
                            }
                            return Ok(new SessionAuthorizationQuery
                            {
                                clientId = dtoClientId,
                                touragencyEmployeeId = dtoTouragencyEmployeeId,
                                touragencyAccountRoleId = dtoTouragencyAccountRoleId,
                                touragencyEmployeeLogin = dtoTouragencyEmployeeLogin,
                                clientTouristNickname = dtoClientTouristNickname,
                                isClient = dtoIsClient
                            });
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
        /*
         [HttpPost]
        public async Task<ActionResult> Post(SessionAuthorizationQuery query)
        {
            try
            {
                if (query.clientId != null)
                {
                    HttpContext.Response.Cookies.Append("clientId", query.clientId.ToString());
                }
                if (query.touragencyEmployeeId != null)
                {
                    HttpContext.Session.SetInt32("touragencyEmployeeId", (int)query.touragencyEmployeeId);
                }
                if (query.touragencyAccountRoleId != null)
                {
                    HttpContext.Session.SetInt32("touragencyAccountRoleId", (int)query.touragencyAccountRoleId);
                }
                if (query.touragencyEmployeeLogin != null)
                {
                    HttpContext.Session.SetString("touragencyEmployeeLogin", (string)query.touragencyEmployeeLogin);
                }
                if (query.clientTouristNickname != null)
                {
                    HttpContext.Session.SetString("clientTouristNickname", (string)query.clientTouristNickname);
                }
                if (query.isClient != null)
                {
                    HttpContext.Session.SetInt32("isClient", (int)query.isClient);
                }
                await HttpContext.Session.CommitAsync();
                Console.WriteLine($"Session keys {HttpContext.Session.Keys.Count()}");
                Console.WriteLine($"Session ID {HttpContext.Session.Id}");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
         */
        [HttpPost]
        public async Task<ActionResult> Post(SessionAuthorizationQuery query)
        {
            try
            {
                var cookieOptions = new CookieOptions
                {
                    //Secure = true // Встановлюємо кукі як безпечний
                    //HttpOnly = true, // Кукі можна буде читати лише на сервері
                    Domain = "26.162.95.213:7098"

                };

                if (query.clientId != null)
                {
                    Response.Cookies.Append("clientId", query.clientId.Value.ToString(), cookieOptions);
                }
                if (query.touragencyEmployeeId != null)
                {
                    Response.Cookies.Append("touragencyEmployeeId", query.touragencyEmployeeId.Value.ToString(), cookieOptions);
                }
                if (query.touragencyAccountRoleId != null)
                {
                    Response.Cookies.Append("touragencyAccountRoleId", query.touragencyAccountRoleId.Value.ToString(), cookieOptions);
                }
                if (query.touragencyEmployeeLogin != null)
                {
                    Response.Cookies.Append("touragencyEmployeeLogin", query.touragencyEmployeeLogin, cookieOptions);
                }
                if (query.clientTouristNickname != null)
                {
                    Response.Cookies.Append("clientTouristNickname", query.clientTouristNickname, cookieOptions);
                }
                if (query.isClient != null) 
                { 
                    Response.Cookies.Append("isClient", query.isClient.Value.ToString(), cookieOptions); 
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /*
        [HttpDelete]
        public void Delete()
        {
            HttpContext.Session.Remove("clientId");
            HttpContext.Session.Remove("touragencyEmployeeId");
            HttpContext.Session.Remove("touragencyAccountRoleId");
            HttpContext.Session.Remove("touragencyEmployeeLogin");
            HttpContext.Session.Remove("clientTouristNickname");
            HttpContext.Session.Remove("isClient");
        }
        */

        [HttpDelete]
        public void Delete()
        {
            Response.Cookies.Delete("clientId");
            Response.Cookies.Delete("touragencyEmployeeId");
            Response.Cookies.Delete("touragencyAccountRoleId");
            Response.Cookies.Delete("touragencyEmployeeLogin");
            Response.Cookies.Delete("clientTouristNickname");
            Response.Cookies.Delete("isClient");
            // Видаліть інші кукі тут...
        }
    }

    public class SessionAuthorizationQuery
    {
        public string? SearchParameter { get; set; }
        public int? clientId { get; set; }
        public int? touragencyEmployeeId { get; set; }
        // поверне 1, якщо клієнт, 0 - якщо співробітник турагенції
        public int? isClient { get; set; }
        public string? touragencyEmployeeLogin { get; set; }
        public string? clientTouristNickname { get; set; }
        public int? touragencyAccountRoleId { get; set; }
    }
}
