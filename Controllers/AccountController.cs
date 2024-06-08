using MeroPartyPalace.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Dapper;
using static Dapper.SqlMapper;

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public bool LoginUser(LoginUser loginUser)
        {
            bool isUserAuthenticate = false;
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Email", loginUser.UserEmail);
            dynamicParameter.Add("Password", loginUser.UserPassword);
            using (var connection = new SqlConnection(Constant.ConnectionString))
            {
                var LogIn = connection.Query<LoginUser>("spForInsertUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                if(LogIn.Count > 0)
                {
                    isUserAuthenticate = true;
                    return isUserAuthenticate;
                }
                else
                {
                    isUserAuthenticate = false;
                    return isUserAuthenticate;
                }
            }

        }
    }
}
