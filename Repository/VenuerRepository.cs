using Dapper;
using MeroPartyPalace.Model;
using System.Data.SqlClient;
using System.Data;

namespace MeroPartyPalace.Repository
{
    public class VenuerRepository 
    {
        public List<Venue> getVenues(UserAddress userAddress, bool filterValues)
        {
            List<Venue> venues = new List<Venue>();
            if (!filterValues)
            {
                using (var connection = new SqlConnection(Constant.ConnectionString))
                {
                    //Fetch all venues from database
                }
                venues = sortVenues(venues, userAddress);
            }
            else
            {
                DynamicParameters dynamicParameter = new DynamicParameters();
                dynamicParameter.Add("City", userAddress.Address_City);
                dynamicParameter.Add("District", userAddress.Address_District);
                dynamicParameter.Add("Province", userAddress.Address_Province);
                using (var connection = new SqlConnection(Constant.ConnectionString))
                {
                    //Only fetch venues whose addresses match
                }
            }
            return venues;
        }
        
        public bool addVenue(Venue venue)
        {
            bool success = false;
            using (var connection = new SqlConnection(Constant.ConnectionString))
            {
                //Add venue to database
            }
            return success;
        }

        public bool editVenue(Venue venue)
        {
            bool success = false;
            using (var connection = new SqlConnection(Constant.ConnectionString))
            {
                //Save changes to database
            }
            return success;
        }

        public bool deleteVenue(Venue venue)
        {
            bool success = false;
            using (var connection = new SqlConnection(Constant.ConnectionString))
            {
                //Delete venue from database
            }
            return success;
        }

        public List<Venue> sortVenues(List<Venue> venues, UserAddress userAddress)
        {
            /*Sort the venue by address (City, District, Province then Other venues) */
            Dictionary<Venue, int> SortedVenues = new Dictionary<Venue, int>();
            int weight;
            for each venue in venues
            {
                weight = 0;
                if (venue.Address_City == UserAddress.Address_City)
                {
                    weight = 10 + venue.VenueRating;
                    SortedVenues.Add(venue, weight);
                }
                if (venue.Address_District == UserAddress.Address_District)
                {
                    weight = 8 + venue.VenueRating;
                    SortedVenues.Add(venue, weight);
                }
                if (venue.Address_Province == UserAddress.Address_Province)
                {
                    weight = 5 + venue.VenueRating;
                    SortedVenues.Add(venue, weight);
                }
                else
                {
                    weight = 0 + venue.VenueRating;
                    SortedVenues.Add(venue, 10);
                }
            }
            var sortedVenues = SortedVenues.OrderByDescending(v => v.Value).ToDictionary(x => x.Key, x => x.Value);
            return sortedVenues;
        }
    }
}
