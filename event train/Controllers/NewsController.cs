using Microsoft.AspNetCore.Mvc;

namespace event_train.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
