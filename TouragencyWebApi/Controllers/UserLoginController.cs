using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TouragencyWebApi.Models;
using TouragencyWebApi.EFContext;
namespace TouragencyWebApi.Controllers
{
    [Route("api/UserLogin")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly TouragencyContext _context;
        public UserLoginController(TouragencyContext context)
        {
            _context = context;
        }
        // GET: api/UserLogin
        [HttpGet]
        public string Get()
        {
            return "value";
        }

        // GET: api/UserLogin/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserLogin
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/UserLogin/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}
