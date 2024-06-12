using Dapper;
using MeroPartyPalace.Model;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using static Dapper.SqlMapper;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using MeroPartyPalace.Constant;

namespace MeroPartyPalace.Service
{
    public class UserRepository
    {

        public int LoginUser(LoginUser loginUser)
        {
            bool isUserAuthenticate = false;
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Email", loginUser.UserEmail);
            dynamicParameter.Add("Password", loginUser.UserPassword);
            dynamicParameter.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //Console.WriteLine($"email : {loginUser.UserEmail} and password: {loginUser.UserPassword}");
            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                var LogIn = connection.Query<LoginUser>("spForLoginUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    int id = dynamicParameter.Get<int>("id");
                    return id;
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
        public int SignUpUser(User signUpUser)
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
            dynamicParameter.Add("roleId", signUpUser.RoleID);
            dynamicParameter.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                if(signUpUser != null)
                {
                    var SignUp = connection.Query<User>("spForInsertUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
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
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Email", loginUser.UserEmail);
            dynamicParameter.Add("Password", loginUser.UserPassword);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                if(loginUser != null)
                {
                    var SignUp = connection.Query<User>("spForUpdatePassword", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    isPasswordChanged = true;
                }           
            }
            return isPasswordChanged;
        }

        public bool UpdateUser(User signUpUser)
        {
            bool success = false;
            UserRepository userRepository = new UserRepository();
            DynamicParameters dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Id", signUpUser.UserID);
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
            dynamicParameter.Add("roleId", signUpUser.RoleID);
            dynamicParameter.Add("RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(DBConstant.ConnectionString))
            {
                if (signUpUser != null)
                {
                    var SignUp = connection.Query<User>("spForUpdateUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    var rowCount = dynamicParameter.Get<int>("RowsAffected");
                    Console.WriteLine($"{rowCount}");
                    if(rowCount > 0)
                    {
                        success = true;
                    }
                }
            }
            return success;
        }

    }
}
