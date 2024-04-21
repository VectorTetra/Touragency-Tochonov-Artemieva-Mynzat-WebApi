﻿using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;

namespace TouragencyWebApi.Controllers
{
    [Route("api/TouragencyAccountLogin")]
    [ApiController]
    public class TouragencyAccountLoginController
    {
        private readonly ITouragencyAccountService _serv;
        public TouragencyAccountLoginController(ITouragencyAccountService serv)
        {
            _serv = serv;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> RegisterUser(TouragencyAccountLoginDTO accountLogin)
        {
            try
            {
                await _serv.TryToLogin(accountLogin);
                return new ObjectResult(accountLogin);
            }
            catch (ValidationException ex)
            {
                return new ObjectResult(ex.Message);
            }

        }
    }
}
