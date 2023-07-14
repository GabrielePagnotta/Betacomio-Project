using Microsoft.AspNetCore.Authorization;

namespace FirstMVC.Auth
{
    public class BasicAuthAttribute : AuthorizeAttribute
    {
        public BasicAuthAttribute()
        {
            Policy = "BasicAuth";  //policy che chiamo nel controller per l'autorizzazione
        }
    }
}
