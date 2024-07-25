﻿using Dapper;
using MeroPartyPalace.Constant;
using MeroPartyPalace.Model;
using MeroPartyPalace.Service;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Reflection.Metadata;
using static Dapper.SqlMapper;

namespace MeroPartyPalace.Repository
{
    public class VenueRepository
    {
        public List<Venue> getVenues(UserAddress userAddress, bool filterValues, int Capacity)
        {
            List<Venue> venues = new List<Venue>();
            if (!filterValues)
            {
                using (var connection = new SqlConnection(DBConstant.ConnectionString))
                {

                    //return new JsonResult(new { 
                    //    result = "Login Successful",
                    //});
                    venues = connection.Query<Venue>("spForGetAllVenues", commandType: CommandType.StoredProcedure).ToList();


                }


            }
            else
            {

                DynamicParameters dynamicParameter = new DynamicParameters();
                dynamicParameter.Add("City", userAddress.Address_City);
                dynamicParameter.Add("District", userAddress.Address_District);
                dynamicParameter.Add("Province", userAddress.Address_Province);
                dynamicParameter.Add("capacity", userAddress.Capacity);

                using (var connection = new SqlConnection(DBConstant.ConnectionString))
                {
                    venues = connection.Query<Venue>("spForGetVenuesByParameter", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    //Only fetch venues whose addresses match
                }
            }
            venues = sortVenues(venues, userAddress);
            return venues;
        }

        //public bool AddImage(Venue venue)
        //{
        //    var imageString = "";
        //    List<IFormFile> imageFiles = venue.Photos;
        //    UtilityService utilityService = new UtilityService();
        //    if (imageFiles != null)
        //    {
        //        for (int i = 0; i < imageFiles.Count; i++)
        //        {
        //            imageString = utilityService.ConvertImageToBase64Async(imageFiles);
        //            DynamicParameters parameters = new DynamicParameters();
        //            parameters.Add("@PhotoDetails", imageString);
        //            parameters.Add("@VenueId", venue.VenueID);

        //            using (var connection = new SqlConnection(DBConstant.ConnectionString))
        //            {

        //                var imageList = connection.Query<Venue>("spForInsertPhoto", parameters, commandType: CommandType.StoredProcedure).ToList();
        //            }
        //        }
        //        return true;

        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        //public async Task<bool> AddImageAsync(Venue venue)
        //{
        //    List<IFormFile> imageFiles = venue.Photos;
        //    if (imageFiles == null || imageFiles.Count == 0)
        //    {
        //        return false; // No images to process
        //    }

        //    UtilityService utilityService = new UtilityService();
        //    foreach (var imageFile in imageFiles)
        //    {
        //        try
        //        {
        //            // Convert each image to Base64 string
        //            string imageString = await utilityService.ConvertImageToBase64Async(new List<IFormFile> { imageFile });

        //            // Prepare parameters for the stored procedure
        //            DynamicParameters parameters = new DynamicParameters();
        //            parameters.Add("@PhotoDetails", imageString);
        //            parameters.Add("@VenueId", venue.VenueID);

        //            // Execute stored procedure
        //            using (var connection = new SqlConnection(DBConstant.ConnectionString))
        //            {
        //                await connection.ExecuteAsync("spForInsertPhoto", parameters, commandType: CommandType.StoredProcedure);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log or handle the exception as needed
        //            Console.WriteLine($"Error processing image: {ex.Message}");
        //            // Optionally, return false or handle the error accordingly
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        public int addVenue(Venue venue)
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
                    //Add venue to database
                    var AddVenue = connection.Query<User>("spForInsertVenue", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    var id = dynamicParameter.Get<int>("Id");
                    return id;

                }
                else
                {
                    return 0;
                }

            }

        }

        public bool editVenue(Venue venue)
        {
            bool success = false;
            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                //Save changes to database
            }
            return success;
        }

        public bool deleteVenue(Venue venue)
        {
            bool success = false;
            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                //Delete venue from database
            }
            return success;
        }

        public List<Venue> sortVenues(List<Venue> venues, UserAddress userAddress)
        {
            // List to store venues with their corresponding weight
            var venueWeights = new List<(Venue venue, int weight)>();

            foreach (var venue in venues)
            {
                int weight = 0;

                // Calculate weight based on address matching
                if (venue.Address_City == userAddress.Address_City)
                {
                    weight = 10;
                }
                else if (venue.Address_District == userAddress.Address_District)
                {
                    weight = 8;
                }
                else if (venue.Address_Province == userAddress.Address_Province)
                {
                    weight = 5;
                }

                // Add venue rating to weight
                weight += venue.Rating;

                // Add the venue and its weight to the list
                venueWeights.Add((venue, weight));
            }

            // Sort venues by weight in descending order
            var sortedVenues = venueWeights
                .OrderByDescending(v => v.weight)
                .Select(v => v.venue)
                .ToList();

            return sortedVenues;
        }


        public bool UpdateVenue(Venue updateVenue)
        {
            bool success = false;
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("VenueID", updateVenue.VenueID);
            dynamicParameter.Add("VenueName", updateVenue.VenueName);
            dynamicParameter.Add("Price", updateVenue.Price);
            dynamicParameter.Add("PAN_Number", updateVenue.PANnumber);
            dynamicParameter.Add("VenueOwnerID", updateVenue.VenueOwnerID);
            dynamicParameter.Add("VenueStatus", updateVenue.VenueStatus);
            dynamicParameter.Add("VenueDescription", updateVenue.VenueDescription);
            dynamicParameter.Add("Address_Province", updateVenue.Address_Province);
            dynamicParameter.Add("Address_District", updateVenue.Address_District);
            dynamicParameter.Add("Address_City", updateVenue.Address_City);
            dynamicParameter.Add("VenueRating", updateVenue.Rating);
            dynamicParameter.Add("Validate", updateVenue.Validate);
            dynamicParameter.Add("PhoneNumber", updateVenue.PhoneNumber);
            dynamicParameter.Add("Capacity", updateVenue.Capacity);
            dynamicParameter.Add("RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                if (updateVenue != null)
                {
                    var SignUp = connection.Query<User>("spForUpdateVenue", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    var rowCount = dynamicParameter.Get<int>("RowsAffected");
                    if (rowCount > 0)
                    {
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteVenue(Venue Deletevenue)
        {
            bool isDeleted = false;
            DynamicParameters dynamicParameter = new DynamicParameters();

            dynamicParameter.Add("VenueID", Deletevenue.VenueID);
            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {

                    var SignUp = connection.Query<User>("spForDeleteVenue", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    var rowCount = dynamicParameter.Get<int>("RowsAffected");
                    if (rowCount > 0)
                    {
                        isDeleted = true;
                    }
                return isDeleted;
            }

        }

    }
}
