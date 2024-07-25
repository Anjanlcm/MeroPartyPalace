using Dapper;
using MeroPartyPalace.Constant;
using MeroPartyPalace.Model;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static Dapper.SqlMapper;

namespace MeroPartyPalace.Repository
{
    public class ReviewRepository
    {
        public int addReview(Review review)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            //dynamicParameters.Add("BookingId", booking.BookingId);
            review.ReviewDate = DateTime.Today;
            dynamicParameters.Add("userID", review.UserId);
            dynamicParameters.Add("venueID", review.VenueId);
            dynamicParameters.Add("reviewDate", review.ReviewDate);
            dynamicParameters.Add("review", review.ReviewDescription);
            dynamicParameters.Add("rating", review.Rating);
            dynamicParameters.Add("reviewID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            { 
                var Review = connection.Query<Review>("spForAddReview", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
                int ReviewID = dynamicParameters.Get<int>("reviewID");
                return ReviewID;
            }

        }
        public Review modifyReview(Review review)
        {
            Review review1 = new Review();
            DynamicParameters dynamicParameters = new DynamicParameters();
            //dynamicParameters.Add("BookingId", booking.BookingId);
            dynamicParameters.Add("reviewID", review.ReviewId);
            dynamicParameters.Add("userID", review.UserId);
            dynamicParameters.Add("venueID", review.VenueId);
            dynamicParameters.Add("reviewDate", review.ReviewDate);
            dynamicParameters.Add("review", review.ReviewDescription);
            dynamicParameters.Add("rating", review.Rating);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                review1 = connection.QuerySingle<Review>("spForModifyReview", dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            return review1;
        }

        public bool DeleteReview(int id)
        {
            bool IsDeleted = false;
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("reviewID", id);
            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                connection.Query<Review>("spForDeleteReview", dynamicParameters, commandType: CommandType.StoredProcedure);
                IsDeleted = true;
            }
            return IsDeleted;
        }

        public List<Review> GetReview(int VenueID)
        {
            List <Review> reviews = new List<Review>();
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("VenueID", VenueID);
            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                reviews = connection.Query<Review>("spForGetReview", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
            }
            return reviews;
        }
    }
}
