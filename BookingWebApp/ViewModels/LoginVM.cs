using System.ComponentModel.DataAnnotations;

namespace BookingWebApp.ViewModels
{
    public class LoginVM
    {
       // [Required]
      //  public string Email { get; set; }

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string? RedirectUrl { get; set; }
    }
}
