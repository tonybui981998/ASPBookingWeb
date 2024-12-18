using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWebApp.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }


        [MaxLength(20)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Range(10,1000)]
        public double Price { get; set; }

        public int Sqft { get; set; }

        [Range (1,10)]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }    

        [Display(Name = "ImageUrl")]
        public string? ImageUrl { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Update_Date { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity> Amenities { get; set; }
    }
}
