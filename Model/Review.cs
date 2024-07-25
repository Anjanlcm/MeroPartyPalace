using System;

namespace MeroPartyPalace.Model
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public int VenueId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewDescription { get; set; }
    }
}
