using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DTO;

namespace TouragencyWebApi.Controllers
{

    [Route("api/TouragencyAccountRegister")]
    [ApiController]
    public class TouragencyAccountRegisterController : ControllerBase
    {
        private readonly ITouragencyAccountService _serv;
        public TouragencyAccountRegisterController(ITouragencyAccountService serv)
        {
            _serv = serv;
        }

        [HttpPost]
        public async Task<ActionResult<TouragencyEmployeeAccountDTO>> RegisterUser(TouragencyAccountRegisterDTO accountRegister)
        {
            try
            {
                var dto = await _serv.TryToRegister(accountRegister);
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
}
