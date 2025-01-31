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
        //Uri baseAddress = new Uri("https://localhost:7013/api/");
        public DashBoardController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
            //_httpClient=new HttpClient();
            //_httpClient.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        [Area("Admin")]
        public IActionResult VolunteersList()
        {
            IEnumerable<ApplicationUser> Volunteers = _unitWork.ApplicationUsers.GetAll(x=>x.RoleSpecifier==0);
            return View(Volunteers);
        }
        public IActionResult Profile(string id)
        {
            if (id == null) return NotFound();
            ApplicationUser obj = _unitWork.ApplicationUsers.Get(x=>x.Id==id);
            return View(obj);
        }
       
    }
    
}
