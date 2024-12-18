using BookingWebApp.Domain.Entities;

namespace BookingWebApp.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Villa>? villaList { get; set; }
        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get;set; }

        public int Night {  get; set; } 
    }
}
