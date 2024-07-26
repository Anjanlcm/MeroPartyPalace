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

        public IActionResult AddVenue([FromBody] Venue venue)
        {
            if (venue == null)
            {
                return BadRequest("Venue data is null.");
            }

            VenueResponse response = AddVenueToDatabase(venue);

            if (response.VenueID > 0)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.Message);
            }
        }

        private VenueResponse AddVenueToDatabase(Venue venue)
        {
            VenueRepository venueRepository = new VenueRepository();
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("venueName", venue.VenueName);
            dynamicParameter.Add("panNumber", venue.PANnumber);
            dynamicParameter.Add("price", venue.Price);
            dynamicParameter.Add("venueStatus", venue.VenueStatus);
            dynamicParameter.Add("venueOwnerID", venue.VenueOwnerID);
            dynamicParameter.Add("venueDescription", venue.VenueDescription);
            dynamicParameter.Add("addressProvince", venue.Address_Province);
            dynamicParameter.Add("addressDistrict", venue.Address_District);
            dynamicParameter.Add("addressCity", venue.Address_City);
            dynamicParameter.Add("validate", venue.Validate);
            dynamicParameter.Add("rating", venue.Rating);
            dynamicParameter.Add("phoneNumber", venue.PhoneNumber);
            dynamicParameter.Add("capacity", venue.Capacity);
            dynamicParameter.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                if (venue != null)
                {
                    // Add venue to database
                    connection.Execute("spForInsertVenue", dynamicParameter, commandType: CommandType.StoredProcedure);
                    var id = dynamicParameter.Get<int>("Id");

                    if (id > 0)
                    {
                        // Add images to database
                        bool isImageAdded = venueRepository.AddImage(id, venue.Photos);

                        return new VenueResponse
                        {
                            VenueID = id,
                            IsImageAdded = isImageAdded,
                            Message = isImageAdded ? "Venue and images added successfully" : "Venue added, but images addition failed"
                        };
                    }
                    else
                    {
                        return new VenueResponse
                        {
                            VenueID = 0,
                            IsImageAdded = false,
                            Message = "Failed to add venue"
                        };
                    }
                }
                else
                {
                    return new VenueResponse
                    {
                        VenueID = 0,
                        IsImageAdded = false,
                        Message = "Venue is null"
                    };
                }
            }
        }



        [HttpGet("Get_Venue")]
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
                    PANnumber = venue.PANnumber,
                    VenueOwnerID = venue.VenueOwnerID,
                    VenueStatus = venue.VenueStatus,
                    VenueDescription = venue.VenueDescription,
                    Address_Province = venue.Address_Province,
                    Address_District = venue.Address_District,
                    Address_City = venue.Address_City,
                    Rating = venue.Rating,
                    PhoneNumber = venue.PhoneNumber,
                    Capacity = venue.Capacity
                });
            });

            return Ok(venuedetail);
        }

        [HttpGet("Get_VenueByID/{id}")]
        public IActionResult GetVenueByVenueID(int id)
        {
            // Create an instance of VenueRepository
            VenueRepository venueRepo = new VenueRepository();

            // Fetch venue details from the repository by ID
            Venue venue = venueRepo.GetVenueByID(id);

            // Check if the venue was found
            if (venue == null)
            {
                return NotFound(); // Return a 404 status if venue not found
            }

            return Ok(venue); // Return the venue details with a 200 status
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

        [HttpPost("Delete Venue")]

        public IActionResult DeleteVenue(Venue venue)
        {

            return Ok(new { message = "Successfully Deleted" });
        }
    }
}
