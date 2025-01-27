using Microsoft.AspNetCore.Mvc;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        private readonly IUnitWork _unitWork;
        public DashBoardController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Area("Admin")]
        public IActionResult VolunteersList()
        {
            IEnumerable<ApplicationUser> Volunteers = _unitWork.ApplicationUsers.GetAll();
            return View(Volunteers);
        }
    }
}
