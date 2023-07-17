using System.Security.Principal;

namespace FirstMVC.Auth
{
    public class AuthUser : IIdentity
    {
        public AuthUser(string authType, bool isAuthenticated, string name)
        {
            AuthenticationType = authType;
            IsAuthenticated = isAuthenticated;
            Name = name;
        }
        public string? AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? Name { get; set; }
    }
}
