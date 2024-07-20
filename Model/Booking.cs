namespace MeroPartyPalace.Model
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int VenueId { get; set; }
        public DateTime BookedOn { get; set; }
        public DateTime BookedFor { get; set; }
        public DateTime BookedUpTo { get; set; }
        public string BookingStatus { get; set; }
        
    }
}
