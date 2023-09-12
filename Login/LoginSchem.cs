namespace Betacomio_Project.Login
{
    public class LoginSchem
    {
        public string email {  get; set; }
        public int token { get; set; }
        public string id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string date  { get; set; }
        public string age { get; set; }
        public string language { get; set; }

        public LoginSchem(string email, int token , string id ,string username , string name , string surname , string date  , string age, string language)
        {
            this.email = email;
            this.token = token;
            this.id = id;
            this.username = username;
            this.name = name;
            this.surname = surname;
            this.date = date;
            this.age = age;
            this.language = language;

        }
    }
}
