using FeedbackServices1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbackServices1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FeedbackController(ApplicationDbContext context)

        {

            _context = context;

        }

        [HttpGet]

        public IActionResult GetFeedbacks()
        {

            var cars = _context.Feedbacks.ToList();

            return Ok(cars);

        }

        [HttpPost]

        public IActionResult AddFeedback(Feedback model)
        {
            _context.Feedbacks.Add(model);

            _context.SaveChanges();

            return Ok("Added successfully");

        }

        [HttpPut]

        public IActionResult UpdateFeedback(Feedback model)
        {

            var feedback = _context.Feedbacks.Find(model.FeedbackId);

            feedback.Rating = model.Rating;
            feedback.FeedbackText = model.FeedbackText;
            feedback.EventId = model.EventId;
            feedback.VolunteerId = model.VolunteerId;
            feedback.CreatedAt = model.CreatedAt;
            _context.Feedbacks.Update(feedback);
            _context.SaveChanges();

            return Ok("Updated successfully");

        }

        [HttpDelete]

        public IActionResult DeleteFeedback(int Id)
        {

            var feedback = _context.Feedbacks.Find(Id);

            _context.Feedbacks.Remove(feedback);

            _context.SaveChanges();

            return Ok("Deleted successfully");

        }

    }
}
