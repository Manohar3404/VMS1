﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using VMS1.Data;
using VMS1.Models;
using VMS1.Models.ViewModels;
using VMS1.Repository.IRepository;
using VMS1.Utility;

namespace VMS1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,User")]
    public class EventController : Controller
    {
     
        private readonly IUnitWork _unitwork;
        private readonly HttpClient _httpClient;
        
        public EventController(IUnitWork unitwork)
        {
         
            _unitwork = unitwork;
            
        }
       
        [Authorize(Roles = "User,Admin")]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<Event> objList = _unitwork.Events.GetAll(null, null).ToList();
            Dictionary<int, int> registeredList = _unitwork.VolunteerRegistrations
                .GetAll(x => x.VolunteerId == userId, "Event")
                .Select(x => new { x.EventId, x.RegistrationId })
                .ToDictionary(x => x.EventId, x => x.RegistrationId);
            EventViewModel obj = new ()
            {
                Events = objList,
                EventIds = registeredList
            };
            return View(obj);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Event obj)
        {
            if (ModelState.IsValid)
            {
                _unitwork.Events.Add(obj);
                _unitwork.Save();
                TempData["SuccessMessage"] = "Event Created Successfully";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error Occured While Creating Event";
            return View(obj);

        }
        public IActionResult Edit(int? id)
        {
            var obj = _unitwork.Events.Get(s => s.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Event obj)
        {
            if (ModelState.IsValid)
            {
                _unitwork.Events.Update(obj);
                _unitwork.Save();
                TempData["SuccessMessage"] = "Event Updated Successfully";

                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error Occured While Updating Event";

            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            var obj = _unitwork.Events.Get(s => s.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Event obj)
        {
            if (obj != null)
            {
                _unitwork.Events.Remove(obj);
                _unitwork.Save();
                TempData["SuccessMessage"] = "Event Deleted Successfully";

                return RedirectToAction("Index");
            }else
            {
                TempData["ErrorMessage"] = "Error Occured While Deleting Event";
                return View(obj);
            }
        }
    }
}
