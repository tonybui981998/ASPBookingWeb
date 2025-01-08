using BookingWebApp.Application.Common.Interfaces;
using BookingWebApp.Infrastructure.Data;
using BookingWebApp.Models;
using BookingWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                villaList = _unitOfWork.Villa.GetAll(includeProperties: "Amenities"),
                Night = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
            };
            return View(homeVM);
        }
        [HttpPost]
        public IActionResult Index(HomeVM homeVM)
        {
            homeVM.villaList = _unitOfWork.Villa.GetAll(includeProperties: "Amenities");
              foreach(var villa in homeVM.villaList)
            {
                if(homeVM.CheckInDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    villa.IsAvailable = true;
                }
                else
                {
                    villa.IsAvailable = false;
                }
            }
            
            return View(homeVM);
        }
        public IActionResult GetVillaBydate(int nights, DateOnly checkInDate, HomeVM homeVM)
        {
            var villaList = _unitOfWork.Villa.GetAll(includeProperties: "Amenities");
            foreach (var villa in villaList)
            {
                if (checkInDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    villa.IsAvailable = true;
                }
                else
                {
                    villa.IsAvailable = false;
                }

             

            }
            HomeVM homeVM1 = new()
            {
                Night = nights,
                CheckInDate = checkInDate,
                villaList = villaList
            };
            return PartialView("_VillaList",homeVM1);
        }



        

        public IActionResult Privacy()
        {
            return View();
        }

       
        public IActionResult Error()
        {
            return View();
        }
    }
}
