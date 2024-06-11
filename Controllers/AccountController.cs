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

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("LoginUser")]
        public string LoginUser(LoginUser loginUser)
        {
            UserRepository userRepository = new UserRepository(); 

            bool isLoggedIn = userRepository.LoginUser(loginUser);

            if (isLoggedIn)
            {
                //return new JsonResult(new { 
                //    result = "Login Successful",
                //});
                string data = "hello this is anjan";
                return data;
            }

            //return new JsonResult(new
            //{
            //    result = "Login Unsuccessful",
            //});
            return ("Login Unsuccessful");
        }

        [HttpPost]
        public string SignUpUser(SignUpUser signUpUser)
        {
            UserRepository userRepository = new UserRepository();

           int UserId = userRepository.SignUpUser(signUpUser);
            if(UserId != null)
            {
                return ("Successfully signed up");
            }
            else
            {
                return ("Error Occured");
            }
            
        }

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
    }
}
 