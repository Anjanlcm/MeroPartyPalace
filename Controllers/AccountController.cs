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
        [HttpPost]
        [Route("LoginUser")]
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

        [HttpPost]
        public string SignUpUser(User signUpUser)
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
        [HttpPut]
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

        [HttpPatch]
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
 