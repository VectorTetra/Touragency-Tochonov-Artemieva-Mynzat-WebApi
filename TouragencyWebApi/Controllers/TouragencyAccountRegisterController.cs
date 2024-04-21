using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DTO;

namespace TouragencyWebApi.Controllers
{

    [Route("api/TouragencyAccountRegister")]
    [ApiController]
    public class TouragencyAccountRegisterController
    {
        private readonly ITouragencyAccountService _serv;
        public TouragencyAccountRegisterController(ITouragencyAccountService serv)
        {
            _serv = serv;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> RegisterUser(TouragencyAccountRegisterDTO accountRegister)
        {
            try
            {
                await _serv.TryToRegister(accountRegister);
                return new ObjectResult(accountRegister);
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }

        }
    }
}
