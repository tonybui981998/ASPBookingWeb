using BookingWebApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingWebApp.ViewModels
{
    public class AmenityViewModel
    {
        public Amenity? Amenity { get; set; }
        [ValidateNever]

        public IEnumerable<SelectListItem> VillaList { get; set; }  
    }
}
