using BookingWebApp.Domain.Entities;
using BookingWebApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;

namespace BookingWebApp.Controllers
{
    public class VillaController : Controller
    {
        public readonly ApplicationDbContext _context;

        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var allvilla = _context.Villas.ToList();

            return View(allvilla);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj)
        {

            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("", "The name and the description can't the same");

            }

            if (ModelState.IsValid)
            {
                _context.Villas.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "the villa create successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Sorry something went wrong";
                return View();
            }

        }
        public IActionResult Update(int VillaId)
        {
            Villa? obj = _context.Villas.FirstOrDefault(u => u.Id == VillaId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(obj);
            }

        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _context.Villas.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "The villa update successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Sorry something wrong";
                return View();
            }
        }
        public IActionResult Delete(int VillaId)
        {
            Villa? obj = _context.Villas.FirstOrDefault(u => u.Id == VillaId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(obj);
            }
        }
        [HttpPost]
        public IActionResult Delete(Villa obj) {

            Villa? objss = _context.Villas.FirstOrDefault(u => u.Id == obj.Id);
            if (objss is not null) { 
            _context.Villas.Remove(objss);
                _context.SaveChanges();
                TempData["success"] = "The villa has been delete successfully";
                return RedirectToAction("Index");
            }
           
            else {
                TempData["error"] = "Sorry something wrong";
                return View(); 
            }
           
         
      
        }

    }
}
