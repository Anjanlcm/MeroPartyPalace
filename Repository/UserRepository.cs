using Dapper;
using MeroPartyPalace.Model;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using static Dapper.SqlMapper;
using System.Security.Cryptography;
using System.Reflection.Metadata;

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
            using (var connection = new SqlConnection(DBConstant.ConnectionString))
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
        public int SignUpUser(SignUpUser signUpUser)
        {
            UserRepository userRepository = new UserRepository();
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("firstName", signUpUser.FirstName);
            dynamicParameter.Add("middleName", signUpUser.MiddleName);
            dynamicParameter.Add("lastName", signUpUser.LastName);
            dynamicParameter.Add("Email", signUpUser.UserEmail);
            dynamicParameter.Add("Password", signUpUser.UserPassword);
            dynamicParameter.Add("gender", signUpUser.Gender);
            dynamicParameter.Add("Province", signUpUser.Address_Province);
            dynamicParameter.Add("District", signUpUser.Address_District);
            dynamicParameter.Add("City", signUpUser.Address_City);
            dynamicParameter.Add("mobileNumber", signUpUser.MobileNo);
            dynamicParameter.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                if(signUpUser != null)
                {
                    var SignUp = connection.Query<SignUpUser>("spForInsertUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    var id = dynamicParameter.Get<int>("Id");
                    return id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool ChangePassword(LoginUser loginUser)
        {
            bool isPasswordChanged = false;
            UserRepository userRepository = new UserRepository();
            return isPasswordChanged;
        }
        
    }
}
