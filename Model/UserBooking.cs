namespace meroPartyPalace.Models
{
    public class UserBooking
    {
        public int UserID { get; set; }
        public int BookingID { get; set; }
        public User User { get; set; }
        public Booking Booking { get; set; }
    }
}
