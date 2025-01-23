using Microsoft.AspNetCore.Mvc;
using VMS1.Models;
using VMS1.Models.ViewModels;
using VMS1.Repository.IRepository;

namespace VMS1.Areas.User.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IUnitWork _unitwork;
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult AddFeedback(int id)
        {
            if(id == null) return NotFound();
            VolunteerRegistrations obj = _unitwork.VolunteerRegistrations.Get(s => s.RegistrationId == id,"Event");
            var feedbackVM= new FeedbackViewModel()
            {
                VolunteerRegistrations = obj,
                Feedback= new Feedback()
            };
            
            return View(obj);
        }
        [HttpPost]
        public IActionResult AddFeedback(FeedbackViewModel obj)
        {
            _unitwork.Feedback.Add(obj.Feedback);
            _unitwork.Save();
            return RedirectToAction("RegisteredEvents", "Registration", new { area = "User" });
        }

    }
}
