using System.ComponentModel.DataAnnotations.Schema;

namespace meroPartyPalace.Models
{
    [Table("tbl_Booking")]
    public class Booking
    {
        public int BookingID { get; set; }
        public DateTime BookedOn { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime BookingDuration {  get; set; }
        public User User { get; set; }
        public Venue Venue { get; set; }
        public ICollection<UserBooking> UserBookings { get; set; }
        public ICollection<VenueBooking> VenueBookings { get; set; }
    }
}
