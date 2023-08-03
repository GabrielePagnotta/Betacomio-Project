using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Betacomio_Project.Controllers
{
    [Authorize("BasicAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> getSession()
        {
            List<string> sessions = new List<string>();

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(LoginUser.UserName)));
            {
                Guid guid = Guid.NewGuid();
                HttpContext.Session.SetString(LoginUser.UserName , "current username");
                HttpContext.Session.SetString(LoginUser.tokenKey, guid.ToString());
                
            }
            string username = HttpContext.Session.GetString(LoginUser.UserName);
            string token = HttpContext.Session.GetString(LoginUser.tokenKey);
            sessions.Add(username);
            sessions.Add(token);
            
            return sessions;
        }
    }
}
