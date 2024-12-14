using BookingWebApp.Application.Common.Interfaces;
using BookingWebApp.Domain.Entities;
using BookingWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingWebApp.Controllers
{
    public class AmenityController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var getall = _unitOfWork.Amenity.GetAll(includeProperties:"Villa");
            return View(getall);
        }
        public IActionResult Create()
        {
            AmenityViewModel amenityViewModel = new ()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(amenityViewModel);
        }
        [HttpPost]
        public IActionResult Create (AmenityViewModel obj)
        {
            bool IsNameisUnique = _unitOfWork.Amenity.Any(u => u.Name == obj.Amenity.Name);
            if (ModelState.IsValid && !IsNameisUnique)
            {
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Add successfully";
                return RedirectToAction("Index");
            }
            if (IsNameisUnique) {
                TempData["error"] = "Sorry the Name already exist";
            }
           
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(U => new SelectListItem
            {
                Text = U.Name,
                Value = U.Id.ToString()
            });
            return View(obj);

          
        
        }
        public IActionResult Delete (int VillaId)
        {
            AmenityViewModel amenityViewModel = new AmenityViewModel()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == VillaId)
            };
            if(amenityViewModel.Amenity == null)
            {
                return View("error", "Home");
            }
            else
            {
                return View(amenityViewModel);
            }
            
        }
        [HttpPost]
        public IActionResult Delete(AmenityViewModel amenityViewModel)
        {
            Amenity? amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityViewModel.Amenity.Id);
            if (amenity == null)
            {
                return View("error", "Home");
            }
            else {
                _unitOfWork.Amenity.Delete(amenity);
                _unitOfWork.Save();
                TempData["success"] = "Delete amenity success";
                return RedirectToAction("Index");
            }
            
          
        }
        public IActionResult Edit(int VillaId)
        {
            AmenityViewModel amenityViewModel = new ()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == VillaId)
            };
            if (amenityViewModel.Amenity == null)
            {
                return RedirectToAction("error", "index");
            }
            else {
                return View(amenityViewModel);
            }

           
        }
        [HttpPost]
        public IActionResult Edit(AmenityViewModel amenityViewModel)
        {
            if (ModelState.IsValid) {
                _unitOfWork.Amenity.Update(amenityViewModel.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "Edit successful";
                return RedirectToAction("Index");
            } 
            amenityViewModel.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(amenityViewModel);
           
            }
           
    }
}
