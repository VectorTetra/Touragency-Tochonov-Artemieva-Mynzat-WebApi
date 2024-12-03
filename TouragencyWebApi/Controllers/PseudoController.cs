using Microsoft.AspNetCore.Mvc;

namespace TouragencyWebApi.Controllers
{
    [ApiController]
    public class PseudoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
