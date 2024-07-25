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
using System.Net.Http;
using System;
using System.Text.RegularExpressions;

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
            
            if (signUpUser == null)
            {
                return 0; // Return 0 if the input user is null
            }

            signUpUser.RoleID = 1;

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

            try
            {
                using (var connection = new SqlConnection(DBConstant.ConnectionString))
                {
                    //if (signUpUser != null)
                    // {
                    var SignUp = connection.Query<User>("spForInsertUser", dynamicParameter, commandType: CommandType.StoredProcedure).ToList();
                    var id = dynamicParameter.Get<int>("Id");
                    return id;
                    //}
                }
            }
            catch (SqlException ex) when (ex.Number == 2627) // Unique Key Violation
            {
                // Handle unique key constraint violation
                Console.WriteLine("A user with this email already exists: " + ex.Message);
                return -1; // Return a specific value to indicate a duplicate entry
            }
            catch (SqlException ex)
            {
                // Handle other SQL exceptions
                Console.WriteLine("SQL Error occurred: " + ex.Message);
                return 0; // Return 0 to indicate a general error
            }
            catch (Exception ex)
            {
                // Handle non-SQL exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
                return 0; // Return 0 to indicate an error
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
        public static string sendOtp(string reciever_email, string username)
        {
 
            string randomcode, messagebody;
            Random rand = new Random();
            randomcode = (rand.Next(100000, 999999)).ToString();
            string sender_email = "meropartypalace@gmail.com";
            string sender_email_application_password = "bkag wezh mylv zurf";
            MailMessage mail = new MailMessage();
            messagebody = "Dear " + username + ",\n\nYour password reset code is: " + randomcode;
            mail.To.Add(reciever_email);
            mail.From = new MailAddress(sender_email);
            mail.Body = messagebody;
            mail.Subject = "password reset code";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(sender_email, sender_email_application_password);


            try
            {
                smtp.Send(mail);
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine($"Email does not exit: {ex.Message}");
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return "";
            }
            return randomcode;

        }

        public bool isEmailValid(string OTP, string UserOTP)
                {
            bool isValid = false;

            if (OTP == UserOTP)
            {
                isValid = true;

            }

            return isValid;
        }

    }
}
