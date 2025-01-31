using FeedbackServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeedbackServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackDbContext _context;
        public FeedbackController(FeedbackDbContext context)

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

            var car = _context.Feedbacks.Find(model.FeedbackId);

            car.FeedbackText = model.FeedbackText;

            car.Rating = model.Rating;

            car.EventId = model.EventId;

            car.VolunteerId = model.VolunteerId;

            _context.SaveChanges();

            return Ok("Updated successfully");

        }

        [HttpDelete]

        public IActionResult DeleteFeedback(int FeedbackId)
        {

            var feedback = _context.Feedbacks.Find(FeedbackId);

            _context.Feedbacks.Remove(feedback);

            _context.SaveChanges();

            return Ok("Deleted successfully");

        }

    }
}
