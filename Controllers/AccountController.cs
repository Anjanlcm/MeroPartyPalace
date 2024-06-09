using MeroPartyPalace.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Dapper;
using static Dapper.SqlMapper;
using System.Web.Http.Results;

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("LoginUser")]
        public bool LoginUser(LoginUser loginUser)
        {
            bool isUserAuthenticate = false;
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Email", loginUser.UserEmail);
            dynamicParameters.Add("Password", loginUser.UserPassword);

            using (var connection = new SqlConnection(Constant.ConnectionString))
            {
                var LogIn = connection.Query<LoginUser>("spForInsertUser", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

                if (LogIn.Count > 0)
                {
                    isUserAuthenticate = true;
                    return isUserAuthenticate;
                }
                return isUserAuthenticate;

            }
            
        }
    }
}
 