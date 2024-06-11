using Microsoft.AspNetCore.Mvc.ViewEngines;

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
        public int ImageID { get; set; }
    }
}
