using BookingWebApp.Domain.Entities;
using BookingWebApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookingWebApp.Controllers
{
    public class VillaNumberController : Controller
    {
        public readonly ApplicationDbContext _context;
        public VillaNumberController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Villa = _context.VillaNumbers.ToList();
           
            return View(Villa);
        }
        public IActionResult Delete (int villaId) {

          VillaNumber? villa = _context.VillaNumbers.FirstOrDefault(u=>u.Villa_Number == villaId);
            if (villa == null) { 
            return NotFound();
            }
            else
            {
                return View(villa);
            }
            
       
        }
    }
}
