using BookingWebApp.Domain.Entities;
using BookingWebApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using BookingWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using BookingWebApp.Application.Common.Interfaces;

namespace BookingWebApp.Controllers
{
    public class VillaNumberController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public IActionResult Index()
        {
            var Villa = _unitOfWork.VillaNumber.GetAll(includeProperties:"Villa");
           
            return View(Villa);
        }
        public IActionResult Delete (int villaId) {

            VillaNumberVM villaNumberVM = new()
            {
                VillaLst = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u=>u.Villa_Number == villaId)
                
            };
            if(villaNumberVM.VillaNumber == null)
            {
                return View("error", "Home");
            }
            else
            {
                return View(villaNumberVM);
            }

            
       
        }
        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {

            VillaNumber? obj = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number); 
         if(obj is not null)
            {
                _unitOfWork.VillaNumber.Delete(obj);
                _unitOfWork.Save();
                TempData["success"] = "Delete success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Sorry something wrong";
                return RedirectToAction("error", "Home");
            }
                
           
           
        }
        // add new villa 
        public IActionResult Create() {
            VillaNumberVM villaNumberVM = new()
            {
                VillaLst = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
            };
           
        return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj) {

         
           
            bool IsNumberUnique = _unitOfWork.VillaNumber.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);
        
            if (ModelState.IsValid && !IsNumberUnique)
            {
                _unitOfWork.VillaNumber.Add(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa add successful";
                return RedirectToAction("Index");
            }
            if (IsNumberUnique)
            {
                TempData["error"] = "Sorry the Villa-Number already exist";
            }
            obj.VillaLst = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
           return View(obj);
            
           
        }
        // edit
        public IActionResult Edit(int villaId) { 
          
            VillaNumberVM villaNumberVM = new()
            {
                VillaLst= _unitOfWork.Villa.GetAll().Select(u=> new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaId)
            };
            if (villaNumberVM.VillaNumber == null) {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(villaNumberVM);
            }

  

        }
        [HttpPost]
        public IActionResult Edit(VillaNumberVM villaNumberVM) {

          //  ModelState.Remove("Villa");
            if (ModelState.IsValid) { 
            _unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "Edit Villa success";
                return RedirectToAction("Index");
            }
            villaNumberVM.VillaLst = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            return View(villaNumberVM);
               
          
        }
    }
}
