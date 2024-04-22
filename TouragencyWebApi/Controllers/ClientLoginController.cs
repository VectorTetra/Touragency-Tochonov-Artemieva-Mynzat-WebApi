using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.DTO;
namespace TouragencyWebApi.Controllers
{
    [Route("api/ClientLogin")]
    [ApiController]
    public class ClientLoginController : ControllerBase
    {
        private readonly IClientService _serv;
        public ClientLoginController(IClientService serv)
        {
            _serv = serv;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> LoginUser(ClientLoginDTO clientLoginDTO)
        {
            try
            {
                var authorizedUser = await _serv.TryToLogin(clientLoginDTO);
                return new ObjectResult(authorizedUser);
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
}
