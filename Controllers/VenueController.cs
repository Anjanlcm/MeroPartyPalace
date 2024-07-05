using Dapper;
using MeroPartyPalace.Constant;
using MeroPartyPalace.Model;
using MeroPartyPalace.Repository;
using MeroPartyPalace.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        [HttpPost(Name ="Add_Venue")]
        public string addVenue(Venue venue)
        {
            VenueRepository venueRepository = new VenueRepository();
            int id = venueRepository.addVenue(venue);

            if (id != null)
            {
                return ("Added venue successfully");
            }
            else
            {
                return ("Added venue Unsuccessfully");

            }

        }
        [HttpPost("Get_Venue")]

        public List<Venue> GetVenue(UserAddress userAddress, bool filterValues, int capacity)
        {
            VenueRepository venueRepo = new VenueRepository();

            List<Venue> venuedetails = venueRepo.getVenues(userAddress, filterValues, capacity);

            return venuedetails;

        }
        [HttpPost("Update_Venue_Info")]

        public string UpdateVenueInfo(Venue venue)
        {
            VenueRepository venueRepository = new VenueRepository();
            bool success = venueRepository.UpdateVenue(venue);
            if (success)
            {
                return ("Venue details Changed");
            }
            return ("Error Occured");
        }

    }
}
