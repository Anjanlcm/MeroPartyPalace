using Microsoft.AspNetCore.Mvc;

namespace MeroPartyPalace.Controllers
{
    public class VenueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
