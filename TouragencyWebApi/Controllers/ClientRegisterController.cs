using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.DTO;
namespace TouragencyWebApi.Controllers
{
    [Route("api/ClientRegister")]
    [ApiController]
    public class ClientRegisterController : ControllerBase
    {
        private readonly IClientService _serv;
        public ClientRegisterController(IClientService serv)
        {
            _serv = serv;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> RegisterUser(ClientRegisterDTO clientRegisterDTO)
        {
            try
            {
                await _serv.TryToRegister(clientRegisterDTO);
                return new ObjectResult(clientRegisterDTO);
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
