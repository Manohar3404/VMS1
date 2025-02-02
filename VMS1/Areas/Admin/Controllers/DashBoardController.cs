using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly HttpClient _httpClient;
       
        public DashBoardController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
           
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser obj = _unitWork.ApplicationUsers.Get(x => x.Id == userId);

            return View(obj);
        }
        [Area("Admin")]
        public IActionResult VolunteersList(int? id)
        {
            ViewBag.Events = _unitWork.Events.GetAll();

            if (id == null)
            {
                ViewBag.specifier=-1;
                // Get all volunteers with RoleSpecifier == 0
                IEnumerable<ApplicationUser> volunteers = _unitWork.ApplicationUsers.GetAll(x => x.RoleSpecifier == 0);
                return View(volunteers);
            }
            else
            {
                ViewBag.specifier = id;
                // Get volunteers registered for the specified event
                var volunteerRegistrations = _unitWork.VolunteerRegistrations
                    .GetAll(x => x.EventId == id && x.Volunteer.RoleSpecifier == 0,  "Volunteer");

                // Select the volunteer details
                IEnumerable<ApplicationUser> volunteers = volunteerRegistrations.Select(u => new ApplicationUser()
                {
                    Id = u.VolunteerId,
                    Name = u.Volunteer.Name,
                    City = u.Volunteer.City,
                    Email = u.Volunteer.Email
                });

                return View(volunteers);
            }
        }

        public IActionResult Profile(string id)
        {
            if (id == null) return NotFound();
            ApplicationUser obj = _unitWork.ApplicationUsers.Get(x=>x.Id==id);
            return View(obj);
        }
        [HttpGet]
        public JsonResult GetVolunteersByEvent(int eventId)
        {
            var volunteerRegistrations = _unitWork.VolunteerRegistrations.GetAll(vr => vr.EventId == eventId);
            var volunteerIds = volunteerRegistrations.Select(vr => vr.VolunteerId).ToList();
            var volunteers = _unitWork.ApplicationUsers.GetAll(u => volunteerIds.Contains(u.Id));

            return Json(volunteers);
        }
    }
    
}
