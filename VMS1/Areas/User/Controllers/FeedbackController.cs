using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VMS1.Models;
using VMS1.Models.ViewModels;
using VMS1.Repository.IRepository;
using VMS1.Utility;

namespace VMS1.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Admin,User")]
    public class FeedbackController : Controller
    {
        private readonly IUnitWork _unitwork;
        private readonly HttpClient _httpClient;
        Uri baseAddress = new Uri("https://localhost:7003/api/");

        public FeedbackController(IUnitWork unitwork)
        {
            _unitwork = unitwork;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Feedbacks()
        {
            IEnumerable<Feedback> feedbacks = new List<Feedback>() { new Feedback() };
            HttpResponseMessage response = _httpClient.GetAsync("Feedback/GetFeedbacks").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                feedbacks = JsonConvert.DeserializeObject<List<Feedback>>(data);
            }
            var events = _unitwork.Events.GetAll();
            var users = _unitwork.ApplicationUsers.GetAll();

            // Join feedbacks with Event and ApplicationUser data
            var feedbacksWithDetails = feedbacks
                .Join(events,
                    feedback => feedback.EventId,
                    eventEntity => eventEntity.Id,
                    (feedback, eventEntity) => new { feedback, eventEntity })
                .Join(users,
                    combined => combined.feedback.VolunteerId,
                    user => user.Id,
                    (combined, user) => new FeedbackVM
                    {
                        Feedback = combined.feedback,
                        Event = combined.eventEntity,
                        User = user
                    })
                .ToList();

            return View(feedbacksWithDetails);
        }

        public IActionResult AddFeedback(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (id == default(int)) return NotFound();
            IEnumerable<Feedback> feedbacks = new List<Feedback>() { new Feedback() }; //_unitwork.Feedback.GetAll(null, "Event,ApplicationUser").ToList();
            HttpResponseMessage response = _httpClient.GetAsync("Feedback/GetFeedbacks").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                feedbacks = JsonConvert.DeserializeObject<List<Feedback>>(data);
            }
            VolunteerRegistrations obj = _unitwork.VolunteerRegistrations.Get(s => s.RegistrationId == id, "Event");
            Feedback feedback = feedbacks.Where(s => obj.EventId == s.EventId && s.VolunteerId == userId).FirstOrDefault();

            var feedbackVM = new FeedbackViewModel()
            {
                VolunteerRegistrations = obj,
                Feedback = feedback ?? new Feedback()
            };

            return View(feedbackVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(FeedbackViewModel obj)
        {
            if (obj.Feedback.FeedbackId == 0)
            {
                obj.Feedback.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            }
            if (!ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                VolunteerRegistrations ob = _unitwork.VolunteerRegistrations
                    .Get(s => s.EventId == obj.Feedback.EventId && s.VolunteerId == userId, "Event");

                obj = new FeedbackViewModel()
                {
                    VolunteerRegistrations = ob,
                    Feedback = obj.Feedback
                };
                return View(obj); // Return the view with the model to show validation errors
            }

            var jsonContent = JsonConvert.SerializeObject(obj.Feedback);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            if (obj.Feedback.FeedbackId == 0)
            {
                obj.Feedback.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
                HttpResponseMessage response = await _httpClient.PostAsync("Feedback/AddFeedback", contentString);

                if (!response.IsSuccessStatusCode)
                {
                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                    VolunteerRegistrations ob = _unitwork.VolunteerRegistrations
                        .Get(s => s.EventId == obj.Feedback.EventId && s.VolunteerId == userId, "Event");

                    obj = new FeedbackViewModel()
                    {
                        VolunteerRegistrations = ob,
                        Feedback = obj.Feedback
                    };
                    // Handle the error 
                    ModelState.AddModelError(string.Empty, "Failed to post feedback to the API.");
                    return View(obj); // Return the view with the model to show validation errors
                }

                _unitwork.Feedback.Add(obj.Feedback);
            }
            else
            {
                HttpResponseMessage response = await _httpClient.PutAsync("Feedback/UpdateFeedback", contentString);

                if (!response.IsSuccessStatusCode)
                {
                    // Handle the error appropriately
                    ModelState.AddModelError(string.Empty, "Failed to update feedback to the API.");
                    return View(obj); // Return the view with the model to show validation errors
                }

                _unitwork.Feedback.Update(obj.Feedback);
            }

            _unitwork.Save();
            return RedirectToAction("RegisteredEvents", "Registration", new { area = "User" });
        }


        //public IActionResult Delete(int? id)
        //{
        //    var obj = _unitwork.Feedback.Get(s => s.FeedbackId == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitwork.Feedback.Remove(obj);
        //    _unitwork.Save();
        //    return RedirectToAction("RegisteredEvents", "Registration", new { area = "User" });

        //    //gsdhd
        //}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Send DELETE request to the API
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Feedback/DeleteFeedback?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                // Handle the error appropriately
                ModelState.AddModelError(string.Empty, "Failed to delete feedback from the API.");
                return RedirectToAction("RegisteredEvents", "Registration", new { area = "User" });
            }

            // Assuming the API call was successful
            //_unitwork.Feedback.Remove(_unitwork.Feedback.Get(s => s.FeedbackId == id));
            _unitwork.Save();

            return RedirectToAction("RegisteredEvents", "Registration", new { area = "User" });
        }
    }
}