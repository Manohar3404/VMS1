using EventServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventController(ApplicationDbContext context)

        {

            _context = context;

        }

        [HttpGet]

        public IActionResult GetEvents()
        {

            var events = _context.Events.ToList();

            return Ok(events);

        }

        [HttpPost]

        public IActionResult AddEvent(Event model)
        {
            _context.Events.Add(model);

            _context.SaveChanges();

            return Ok("Added successfully");

        }

        
    
        [HttpDelete]

        public IActionResult DeleteEvent(int id)
        {

            var car = _context.Events.Find(id);

            _context.Events.Remove(car);

            _context.SaveChanges();

            return Ok("Deleted successfully");

        }
        


    }
}    
