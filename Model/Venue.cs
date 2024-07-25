using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MeroPartyPalace.Model
{
    public class Venue
    {
        public int VenueID { get; set; }
        public string VenueName { get; set; }
        public int Price { get; set; }
        public int PAN_Number { get; set; }
        public int VenueOwnerID { get; set; }
        public string VenueStatus { get; set; }
        public string VenueDescription { get; set; }
        public string Address_Province { get; set; }
        public string Address_District { get; set; }
        public string Address_City { get; set; }
        public int VenueRating { get; set; }
        public string Validate { get; set; }
        public string PhoneNumber { get; set; }
        public int Capacity { get; set; }
        public  List<IFormFile> Photos { get; set; }
    }
}
