using MeroPartyPalace.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//namespace MeroPartyPalace.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SessionController : ControllerBase
//    {
//        [HttpGet]
//        public IEnumerable<string> GetSessionInfo()
//        {
//            List<string> sessionInfo = new List<Sting>();
//            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVariables.SessionKeyUsername)))
//            {
//                HttpContext.Session.SetString(SessionVariables.SessionKeyUsername, User.UserId.ToString());
//                HttpContext.Session.SetString(SessionVariables.SessionKeySessionId, Guid.newGuid.ToString());
//            }

//            var username = HttpContext.Session.GetString(SessionVariables.SessionKeyUsername);
//            var sessionId = HttpContext.Session.GetString(SessionVariables.SessionKeySessionId);

//            sessionInfo.Add(username);
//            sessionInfo.Add(sessionId);

//        }
//    }
//}
