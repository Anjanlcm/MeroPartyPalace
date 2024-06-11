using Dapper;
using MeroPartyPalace.Model;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using static Dapper.SqlMapper;
using System.Security.Cryptography;

namespace MeroPartyPalace.Service
{
    public class UserRepository
    {

        public bool LoginUser(LoginUser loginUser)
        {
            bool isUserAuthenticate = false;
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Email", loginUser.UserEmail);
            dynamicParameter.Add("Password", loginUser.UserPassword);
            //Console.WriteLine($"email : {loginUser.UserEmail} and password: {loginUser.UserPassword}");
            using (var connection = new SqlConnection(Constant.ConnectionString))
            {
                var LogIn = connection.Query<LoginUser>("spForLoginUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                //var id = dynamicParameter.Get<int>("UserID");
                //Console.WriteLine($"{id}");
                if (LogIn.Count > 0)
                {
                    isUserAuthenticate = true;
                    return isUserAuthenticate;
                }
                else
                {
                    return isUserAuthenticate;
                    //{ this is for checking purpose only}
                }
            }


        }
        //public string GetUserById()
        //{
        //    using (var connection = new SqlConnection(Constant.ConnectionString))
        //    {
        //        var LogIn = connection.Query<LoginUser>("spForLoginUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();


        //        string details = "hello there this is anjan lamichhane";
        //        return details;
        //    }

        //}
    }
}
