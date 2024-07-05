using Dapper;
using MeroPartyPalace.Constant;
using MeroPartyPalace.Model;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace MeroPartyPalace.Repository
{
    public class BookingRepository
    {
        public int BookingVenue(Booking booking)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            //dynamicParameters.Add("BookingId", booking.BookingId);
            booking.BookedOn = DateTime.Now;
            dynamicParameters.Add("userId",booking.UserId);
            dynamicParameters.Add("venueId", booking.VenueId);
            dynamicParameters.Add("bookedOn", booking.BookedOn);
            dynamicParameters.Add("bookingDate", booking.BookedFor);
            dynamicParameters.Add("bookedUpTo", booking.BookedUpTo);
            dynamicParameters.Add("bookingId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                var Booking = connection.Query<Booking>("SPForBooking", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
                int BookingId = dynamicParameters.Get<int>("bookingId");
                return BookingId;
            }

        }
        public bool CancelBooking(int id)
        {
            bool IsCancelled = false;
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("bookingId", id);
            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                var Booking = connection.Query<Booking>("SPForCancelBooking", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
                IsCancelled = true;
            }
            return IsCancelled;
        }
     
        } 
    
}
