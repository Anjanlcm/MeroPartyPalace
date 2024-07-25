using MeroPartyPalace.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Dapper;
using static Dapper.SqlMapper;
using System.Web.Http.Results;
using MeroPartyPalace.Service;
using System.Reflection.Metadata.Ecma335;
using MeroPartyPalace.Constant;
using MeroPartyPalace.Repository;

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AccountController : ControllerBase
    {
        [HttpPost("Login_User")]
        //public User LoginUser(LoginUser loginUser)
        //{
        //    UserRepository userRepository = new UserRepository(); 
        //    User loggedInUser = new User();
        //    int UserId = userRepository.LoginUser(loginUser);
        //    DynamicParameters   dynamicParameters = new DynamicParameters();
        //    dynamicParameters.Add("id", UserId);

        //    if (UserId != 0)
        //    {
        //        using (var connection = new SqlConnection(DBConstant.ConnectionString))
        //        {

        //            //return new JsonResult(new { 
        //            //    result = "Login Successful",
        //            //});
        //            loggedInUser = connection.Query<User>("spForGetUserById", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

        //            var sampleData = new User
        //            {
        //                UserID = loggedInUser.UserID,
        //                FirstName = loggedInUser.FirstName,
        //                MiddleName = loggedInUser.MiddleName,
        //                LastName = loggedInUser.LastName,
        //                UserEmail = loggedInUser.UserEmail,
        //                Address_Province = loggedInUser.Address_Province,
        //                Address_District = loggedInUser.Address_District,
        //                Address_City = loggedInUser.Address_City,
        //                MobileNo = loggedInUser.MobileNo,
        //                RoleID = loggedInUser.RoleID,
        //            };

        //            return Ok(sampleData);
        //        }

        //    }

        //    //return new JsonResult(new
        //    //{
        //    //    result = "Login Unsuccessful",
        //    //});
        //    return loggedInUser;
        //}

        public async Task<IActionResult> LoginUser([FromBody] LoginUser loginUser)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                int userId = userRepository.LoginUser(loginUser);

                if (userId != 0)
                {
                    using (var connection = new SqlConnection(DBConstant.ConnectionString))
                    {
                        DynamicParameters dynamicParameters = new DynamicParameters();
                        dynamicParameters.Add("id", userId);

                        var loggedInUser = (await connection.QueryAsync<User>(
                            "spForGetUserById",
                            dynamicParameters,
                            commandType: CommandType.StoredProcedure
                        )).FirstOrDefault();

                        if (loggedInUser != null)
                        {
                            var userData = new
                            {
                                loggedInUser.UserID,
                                loggedInUser.FirstName,
                                loggedInUser.MiddleName,
                                loggedInUser.LastName,
                                loggedInUser.UserEmail,
                                loggedInUser.Address_Province,
                                loggedInUser.Address_District,
                                loggedInUser.Address_City,
                                loggedInUser.MobileNo,
                                loggedInUser.RoleID
                            };

                            return Ok(new { success = true, user = userData });
                        }
                    }
                }

                return Unauthorized(new { success = false, message = "Login Unsuccessful" });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, new { success = false, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpPost("SignUp_User")]
        public IActionResult SignUpUser(User signUpUser)
        {
            UserRepository userRepository = new UserRepository();
            string OTP = "";
            string generatedOTP = "";
            generatedOTP = UserRepository.sendOtp(signUpUser.UserEmail, signUpUser.FirstName);
            Console.WriteLine(generatedOTP);
            Console.WriteLine("Enter OTP");
            OTP = Console.ReadLine();
            bool validOtp= userRepository.isEmailValid(generatedOTP, OTP);
            if(!validOtp) 
            {
                return BadRequest(new { message = "Invalid Otp" });
            }
            if(generatedOTP == "")
            {
                return BadRequest(new { message = " Email does not exist." });
            }
            int UserId = userRepository.SignUpUser(signUpUser);

            if (UserId != 0) // Assuming UserId != 0 indicates successful signup
            {
                return Ok(new { message = "Successfully signed up" });
            }
            else
            {
                return BadRequest(new { message = "Error Occurred" });
            }
        }


        [HttpPost("Change_Password")]
        public string ChangePassword(LoginUser loginUser)
        {
            UserRepository userRepository = new UserRepository();
            bool ispasswordChange = userRepository.ChangePassword(loginUser);
            if (ispasswordChange)
            {
                return ("Password Changed");
            }
            return ("Error Occured");
        }

        [HttpPost("Update_User_Account_Info")]
        public string UpdateUserAccountInfo(User signUpUser)
        {
            UserRepository userRepository = new UserRepository();
            bool ispasswordChange = userRepository.UpdateUser(signUpUser);
            if (ispasswordChange)
            {
                return ("Account Modified");
            }
            return ("Error Occured");
        }

        
    }

}
 