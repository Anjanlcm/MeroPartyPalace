using System.ComponentModel.DataAnnotations.Schema;

namespace meroPartyPalace.Models
{
    [Table("tbl_User")]
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Address_Province { get; set; }
        public string Address_District { get; set; }
        public string Address_City { get; set; }
        public string MobileNo { get; set; }
        public Role UserRole { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<UserBooking> UserBookings { get; set; }
    }
}
