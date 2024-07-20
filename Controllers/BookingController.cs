using Dapper;
using MeroPartyPalace.Constant;
using MeroPartyPalace.Model;
using MeroPartyPalace.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        [HttpPost("Book Venue")]
        public void BookVenue(Booking bookingVenue)
        {
            BookingRepository bookingRepository = new BookingRepository();

            int BookingId = bookingRepository.BookingVenue(bookingVenue);

            //DynamicParameters dynamicParameters = new DynamicParameters();
            //dynamicParameters.Add("bookingId", BookingId);

            if (BookingId != 0)
            {
                Console.WriteLine("Successfully Booked");
            }
            else
            {
                Console.WriteLine("Sorry the venue is not booked");

            }

        }
        [HttpPost("Cancel Booking")]
        public void CancelBooking(int id)
        {
            BookingRepository bookingRepository = new BookingRepository();
            bool success;
            success = bookingRepository.CancelBooking(id);
            if (success)
            {
                Console.WriteLine("Successfully Cancelled.");
            }
            else
            {
                Console.WriteLine("Sorry the venue is not Cancelled.");

            }
        }
    }
}
