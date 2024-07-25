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
        [HttpPost("Add Venue")]
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
        [HttpGet("Get_Venue")]

        //public IActionResult GetVenue(UserAddress userAddress, bool filterValues, int capacity)
        //{
        //    VenueRepository venueRepo = new VenueRepository();

        //    List<Venue> venuedetails = venueRepo.getVenues(userAddress, filterValues, capacity);
        //    List<Venue> venuedetail = new List<Venue>();
        //   int i = 0;
        //    venuedetails.ForEach(venue => {
        //        venuedetail[i] = new Venue
        //        {
        //            VenueID = venue.VenueID,
        //            VenueName = venue.VenueName,
        //            Price = venue.Price,
        //            PAN_Number = venue.PAN_Number,
        //            VenueOwnerID = venue.VenueOwnerID,
        //            VenueStatus = venue.VenueStatus,
        //            VenueDescription = venue.VenueDescription,
        //            Address_Province = venue.Address_Province,
        //            Address_District = venue.Address_District,
        //            Address_City = venue.Address_City,
        //            VenueRating = venue.VenueRating,
        //            PhoneNumber = venue.PhoneNumber,
        //            Capacity = venue.Capacity

        //        };
        //        i++;
        //    });


        //    return Ok(venuedetail);

        //}

        public IActionResult GetVenue([FromQuery] string province, [FromQuery] string district, [FromQuery] string city, [FromQuery] bool filterValues, [FromQuery] int capacity)
        {
            UserAddress userAddress = new UserAddress
            {
                Address_Province = province,
                Address_District = district,
                Address_City = city,
                Capacity = capacity
            };

            VenueRepository venueRepo = new VenueRepository();

            List<Venue> venuedetails = venueRepo.getVenues(userAddress, filterValues, capacity);
            List<Venue> venuedetail = new List<Venue>();

            venuedetails.ForEach(venue =>
            {
                venuedetail.Add(new Venue
                {
                    VenueID = venue.VenueID,
                    VenueName = venue.VenueName,
                    Price = venue.Price,
                    PAN_Number = venue.PAN_Number,
                    VenueOwnerID = venue.VenueOwnerID,
                    VenueStatus = venue.VenueStatus,
                    VenueDescription = venue.VenueDescription,
                    Address_Province = venue.Address_Province,
                    Address_District = venue.Address_District,
                    Address_City = venue.Address_City,
                    VenueRating = venue.VenueRating,
                    PhoneNumber = venue.PhoneNumber,
                    Capacity = venue.Capacity
                });
            });

            return Ok(venuedetail);
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
