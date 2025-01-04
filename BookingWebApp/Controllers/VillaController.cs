using BookingWebApp.Application.Common.Interfaces;
using BookingWebApp.Domain.Entities;
using BookingWebApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;

namespace BookingWebApp.Controllers
{
    [Authorize]
    public class VillaController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            var allvilla = _unitOfWork.Villa.GetAll();

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
                if(obj.Image != null)
                {
                   string fileName =Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Villa");
                   using var fileStream = new FileStream(Path.Combine(imagePath,fileName),FileMode.Create);
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\Villa\" + fileName;

                }
                else
                {
                    obj.ImageUrl = "http://placehold.co/600x400";
                }
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
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
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == VillaId);
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
                //if (obj.Image != null)
                //{
                //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                //    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Villa");
                //    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                //    obj.Image.CopyTo(fileStream);
                //    obj.ImageUrl = @"images\Villa\" + fileName;

                //}
                //else
                //{
                //    obj.ImageUrl = "http://placehold.co/600x400";
                //}
                if (obj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Villa");
                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\Villa\" + fileName;

                }
              

                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();
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
            Villa? obj = _unitOfWork.Villa.Get(u => u.Id == VillaId);
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

            Villa? objss = _unitOfWork.Villa.Get (u => u.Id == obj.Id);
            if (objss is not null) {
                if (!string.IsNullOrEmpty(objss.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objss.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.Villa.Delete(objss);
                _unitOfWork.Save();
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
