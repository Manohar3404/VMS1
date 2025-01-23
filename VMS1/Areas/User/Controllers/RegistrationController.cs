using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VMS1.Models;
using VMS1.Repository.IRepository;

namespace VMS1.Areas.User.Controllers
{
    [Area("User")]
    public class RegistrationController : Controller
    {
        private readonly IUnitWork _unitwork;
        public RegistrationController(IUnitWork unitwork)
        {
            _unitwork = unitwork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(int? id)
        {
            if (id == null) return NotFound();
            Event obj = _unitwork.Events.Get(s => s.Id == id);
            return View(obj);
        }
        [HttpPost]
        [ActionName("Register")]
        public IActionResult RegisterEvent(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


            if (ModelState.IsValid)
            {
                VolunteerRegistrations reg = new VolunteerRegistrations()
                {
                    EventId = id,
                    VolunteerId = userId.ToString(),
                    RegistrationDate = DateOnly.FromDateTime(DateTime.Now)
                };
                _unitwork.VolunteerRegistrations.Add(reg);
                _unitwork.Save();
                return RedirectToAction("Index", "Event", new { area = "Admin" });
            }
            Event obj = _unitwork.Events.Get(s => s.Id == id);
            return View(obj);
        }
        public IActionResult Cancel(int? id)
        {
            if (id == null) return NotFound();
            VolunteerRegistrations obj = _unitwork.VolunteerRegistrations.Get(s => s.RegistrationId == id,"Event");
            return View(obj);
        }
        [HttpPost]
        [ActionName("Cancel")]
        public IActionResult CancelEvent(int id)
        {
            if (id == null) return NotFound();
            var obj = _unitwork.VolunteerRegistrations.Get(x =>x.RegistrationId== id);
            _unitwork.VolunteerRegistrations.Remove(obj);
            _unitwork.Save();
            return RedirectToAction("Index", "Event", new { area = "Admin" });
        }
        public IActionResult RegisteredEvents()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<VolunteerRegistrations> objList = _unitwork.VolunteerRegistrations.GetAll(null, "Event").Where(x=>x.VolunteerId==userId);
            if (objList == null)
            {
                return NotFound();
            }
            return View(objList);
        }



    }
}