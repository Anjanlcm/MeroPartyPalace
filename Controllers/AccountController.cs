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
        public User LoginUser(LoginUser loginUser)
        {
            UserRepository userRepository = new UserRepository(); 
            User loggedInUser = new User();
            int UserId = userRepository.LoginUser(loginUser);
            DynamicParameters   dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("id", UserId);

            if (UserId != 0)
            {
                using (var connection = new SqlConnection(DBConstant.ConnectionString))
                {

                    //return new JsonResult(new { 
                    //    result = "Login Successful",
                    //});
                    loggedInUser = connection.Query<User>("spForGetUserById", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();                    
                    return loggedInUser;
                }
                
            }

            //return new JsonResult(new
            //{
            //    result = "Login Unsuccessful",
            //});
            return loggedInUser;
        }

        [HttpPost("SignUp_User")]
        public string SignUpUser(User signUpUser)
        {
            UserRepository userRepository = new UserRepository();
            string OTP = "";
            string generatedOTP = ""; 
            userRepository.isEmailValid(generatedOTP, OTP);
            generatedOTP = UserRepository.sendOtp(signUpUser.UserEmail, signUpUser.FirstName);
            Console.WriteLine("Enter OTP");
            OTP = Console.ReadLine();
            int UserId = userRepository.SignUpUser(signUpUser);
            if (UserId != null)
            {
                return ("Successfully signed up");
            }
            else
            {
                return ("Error Occured");
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
 