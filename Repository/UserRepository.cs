using Dapper;
using MeroPartyPalace.Model;
using System.Data.SqlClient;
using System.Data;

namespace MeroPartyPalace.Service
{
    public class UserService
    {
        public bool LoginUser(LoginUser loginUser)
        {
            bool isUserAuthenticate = false;
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Email", loginUser.UserEmail);
            dynamicParameter.Add("Password", loginUser.UserPassword);
            using (var connection = new SqlConnection(Constant.ConnectionString))
            {
                var LogIn = connection.Query<LoginUser>("spForLoginUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                if (LogIn.Count > 0)
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
