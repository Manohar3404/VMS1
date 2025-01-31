using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitWork _unitwork;   
        public HomeController(ILogger<HomeController> logger,IUnitWork unitWork)
        {
            _logger = logger;
            _unitwork = unitWork;
        }
        [Area("User")]
        [AllowAnonymous] 
        public IActionResult Index()
        {
            IEnumerable<Event> objList = _unitwork.Events.GetAll(null, null).Take(3);
            return View(objList);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
