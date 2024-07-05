using MeroPartyPalace.Services;
using System.Net;
using System.Net.Mail;

namespace MeroPartyPalace.Service
{
    public class EmailSender : emailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "anjan.201205@ncit.edu.np";
            var password = "";
            int portnumber = 587;

            var client = new SmtpClient("smtp.gmail.com", portnumber)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };
            return client.SendMailAsync(
                new MailMessage(
                    from: mail,
                    to: email,
                    subject,
                    message

                    ));
        }
    }
}
