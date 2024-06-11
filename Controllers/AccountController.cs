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

        [HttpGet]
        public string GetUser()
        {

            return ("Hello this is get user function");
        }
    }
}
 