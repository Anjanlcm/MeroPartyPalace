using Dapper;
using MeroPartyPalace.Model;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using static Dapper.SqlMapper;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using MeroPartyPalace.Constant;
using System.Net.Mail;
using System.Net;

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
        //public static string sendOTP(string reciever_email, string username)
        //{
        //    string randomcode, messagebody;
        //    Random rand = new Random();
        //    randomcode = (rand.Next(100000, 999999)).ToString();
        //    string sender_email = "shresthabattey@gmail.com";
        //    string sender_email_application_password = "Battu@0013";
        //    MailMessage mail = new MailMessage();
        //    messagebody = "Dear " + username + ",\n\nYour Password Reset code is: " + randomcode;
        //    try
        //    {
        //        mail.To.Add(reciever_email);
        //    }
        //    catch (Exception ex)
        //    {

        //        return "Invalid Email";
        //    }
        //    mail.From = new MailAddress(sender_email);
        //    mail.Body = messagebody;
        //    mail.Subject = "Password Reset Code";
        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
        //    smtp.EnableSsl = true;
        //    smtp.Port = 587;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.Credentials = new NetworkCredential(sender_email, sender_email_application_password);
        //    smtp.Send(mail);
        //    return randomcode;

        //}

        public bool isEmailValid(string OTP,string UserOTP)
        {
            bool isValid = false;

            if(OTP == UserOTP)
            {
                isValid = true;
               
            }

            return isValid;
        }

    }
}
