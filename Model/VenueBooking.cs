namespace meroPartyPalace.Models
{
    public class VenueBooking
    {
        public int VenueID { get; set; }
        public int BookingID { get; set; }
        public Venue Venue { get; set; }
        public Booking Booking { get; set; }
    }
}
