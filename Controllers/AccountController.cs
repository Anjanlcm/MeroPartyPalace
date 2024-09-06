using Dapper;
using MeroPartyPalace.Constant;
using MeroPartyPalace.Model;
using MeroPartyPalace.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AccountController : ControllerBase
    {
        [HttpPost("Login_User")]

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

       // [HttpPost("SignUp_User")]

        // this is the previous code ========================================================================


        //public IActionResult SignUpUser(User signUpUser)
        //{
        //    UserRepository userRepository = new UserRepository();
        //    string OTP = "";
        //    string generatedOTP = "";
        //    generatedOTP = UserRepository.sendOtp(signUpUser.UserEmail, signUpUser.FirstName);
        //    Console.WriteLine(generatedOTP);
        //    Console.WriteLine("Enter OTP");
        //    OTP = Console.ReadLine();
        //    bool validOtp = userRepository.isEmailValid(generatedOTP, OTP);
        //    if (!validOtp)
        //    {
        //        return BadRequest(new { message = "Invalid Otp" });
        //    }
        //    if (generatedOTP == "")
        //    {
        //        return BadRequest(new { message = " Email does not exist." });
        //    }
        //    int UserId = userRepository.SignUpUser(signUpUser);
        //    if (UserId != 0) // Assuming UserId != 0 indicates successful signup
        //    {
        //        return Ok(new { message = "Successfully signed up" });
        //    }
        //    else
        //    {
        //        return BadRequest(new { message = "Error Occurred" });
        //    }
        //}

        //     up to here ==================================================


// ============================================from here the code is generated ============================================================

        [HttpPost("SignUp_User")]
        public IActionResult SignUpUser([FromBody] User signUpUser)
        {
            UserRepository userRepository = new UserRepository();
            string generatedOTP = UserRepository.sendOtp(signUpUser.UserEmail, signUpUser.FirstName);

            if (string.IsNullOrEmpty(generatedOTP))
            {
                return BadRequest(new { message = "Email does not exist." });
            }

            // Return the OTP for testing purposes (you wouldn't do this in production)
            return Ok(new { message = "OTP sent to email.", otp = generatedOTP, user = signUpUser });
        }

        [HttpPost("Verify_Otp")]
        public IActionResult VerifyOtp([FromBody] OtpVerificationModel otpVerification)
        {
            UserRepository userRepository = new UserRepository();
            bool validOtp = userRepository.isEmailValid(otpVerification.GeneratedOtp, otpVerification.EnteredOtp);

            if (!validOtp)
            {
                return BadRequest(new { message = "Invalid OTP" });
            }

            int userId = userRepository.SignUpUser(otpVerification.User);
            if (userId != 0)
            {
                return Ok(new { message = "Successfully signed up" });
            }
            else
            {
                return BadRequest(new { message = "Error Occurred" });
            }
        }


        public class OtpVerificationModel
        {
            public string GeneratedOtp { get; set; }
            public string EnteredOtp { get; set; }
            public User User { get; set; }
        }




//  ================================================  otp logic ====================================================







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
 