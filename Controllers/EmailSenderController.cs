//using MeroPartyPalace.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace MeroPartyPalace.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmailSenderController : ControllerBase
//    {
//        private readonly emailSender _emailSender;

//        public EmailSenderController(emailSender emailSender)
//        {
//            this._emailSender = emailSender;
//        }

//        public async void SendOtp()
//        {
//            var receiver = "anjanlc5644@gmail.com";
//            var subject = "no-reply";
//            var message = "567843";

//            await _emailSender.SendEmailAsync (receiver, subject, message);
//        }
//    }
//}
