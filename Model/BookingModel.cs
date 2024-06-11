namespace MeroPartyPalace.Model
{
    public class BookingModel
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int VenueId { get; set; }
        public DateTime BookedOn { get; set; }
        public DateTime BookedFor { get; set; }
        public int BookingDuration { get; set; }
        
    }
}
