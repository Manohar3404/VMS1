using Microsoft.AspNetCore.Mvc;

namespace VMS1.Areas.User.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }
    }
}
