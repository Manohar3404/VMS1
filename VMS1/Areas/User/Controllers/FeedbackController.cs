using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VMS1.Models;
using VMS1.Models.ViewModels;
using VMS1.Repository.IRepository;
using VMS1.Utility;

namespace VMS1.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = SD.Role_User)]
    public class FeedbackController : Controller
    {
        private readonly IUnitWork _unitwork;
        public FeedbackController(IUnitWork unitwork)
        {
            _unitwork = unitwork;
        }
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult AddFeedback(int id)
        {
            if(id == null) return NotFound();
            VolunteerRegistrations obj = _unitwork.VolunteerRegistrations.Get(s => s.RegistrationId == id,"Event");
            Feedback? feedback = _unitwork.Feedback.Get(s => obj.Event.Id == s.EventId);
            var feedbackVM= new FeedbackViewModel()
            {
                VolunteerRegistrations = obj,
                Feedback= feedback==null? new Feedback(): feedback
            };
            
            return View(feedbackVM);
        }
        [HttpPost]
        public IActionResult AddFeedback(FeedbackViewModel obj,int Rating)
        {
            if (obj.Feedback.FeedbackId==null)
            {
                obj.Feedback.CreatedAt = DateOnly.FromDateTime(DateTime.Now);

            }
            if (Rating != 0)
            {
                obj.Feedback.Rating = Rating;
              
            }
            _unitwork.Feedback.Add(obj.Feedback);
            _unitwork.Save();
            return RedirectToAction("RegisteredEvents", "Registration", new { area = "User" });
        }

    }
}
