namespace Betacomio_Project.Login
{
    public class LoginSchem
    {
        public string username {  get; set; }
        public int token { get; set; }

        public LoginSchem(string username, int token)
        {
            this.username = username;
            this.token = token;
        }
    }
}
