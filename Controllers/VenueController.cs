using MeroPartyPalace.Model;
using MeroPartyPalace.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        [HttpPost]
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
        [HttpPut]
        public List<Venue> GetVenue(UserAddress userAddress, bool filterValues, int capacity)
        {
            VenueRepository venueRepo = new VenueRepository();

            List<Venue> venuedetails = venueRepo.getVenues(userAddress, filterValues, capacity);

            return venuedetails;
                
        }

    }
}
