using Microsoft.AspNetCore.Mvc;
using System;

public class LoginUser : ControllerBase
{
	public const string UserName = "SessionUsername";
	public const string tokenKey = "SessionKeyToken" ;

    public IEnumerable<string> getSession()
    {
        List<string> sessions = new List<string>();

        if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(LoginUser.UserName))) ;
        {
            Guid guid = Guid.NewGuid();
            HttpContext.Session.SetString(LoginUser.UserName, "current username");
            HttpContext.Session.SetString(LoginUser.tokenKey, guid.ToString());
        }
        string username = HttpContext.Session.GetString(LoginUser.UserName);
        string token = HttpContext.Session.GetString(LoginUser.tokenKey);
        sessions.Add(username);
        sessions.Add(token);

        return sessions;
    }
}
