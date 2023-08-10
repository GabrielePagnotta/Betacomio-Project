using Betacomio_Project.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol.Plugins;
//using Org.BouncyCastle.Utilities.Encoders;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RegexCheck
{
    public class RegexCh
    {
        SqlConnection sqlConnection = new SqlConnection();
        
        public void connectDB()
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=UserRegistry;Integrated Security=True;TrustServerCertificate=True";
                    sqlConnection.Open();
               

                }

            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"si è verificato un errore : {ex.Message}");
            }

        }
        #region CheckEmail
        public bool CheckUserEmail(string user, string email, string name, string surname)
        {
            bool User = false;
            bool Email = false;
            try
            {
                Regex regexNameSurName = new Regex(@"^([a-zA-Z]{5})$");
                if ((name.ToLower() == null || name.ToLower() == "") && (surname.ToLower() == null || surname.ToLower() == ""))
                { new Exception("il campo Nome/Cognome non può essere vuoto"); }
                if (regexNameSurName.IsMatch(name) && regexNameSurName.IsMatch(surname)) { }
                else { new Exception("Il campo nome deve contenere solo caratteri e non può contenere numeri o caratteri speciali"); };
                Regex regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d.*\d).+$");  //questa regex controlla che la username abbia almeno 2 numeri e dei caratteri
                if (user.Length >= 8)
                {
                    if (regex.IsMatch(user))
                    {
                        Console.WriteLine("ottimo");
                        User = true;
                    }
                    else
                    {
                        throw new Exception("devi inserire almeno dei caratteri ed almeno 2 numeri ");
                    }
                }
                else
                {
                    throw new Exception("la lunghezza dei caratteri minimi deve essere di almeno 8 caratteri compresi di numeri ");
                }
                Regex regexùEmail = new Regex(@"([a-z\d.-]+)@([a-z\d-]+)\.([a-z]{2,6})(\.[a-z]{2,4})?"); // questa regex controlla l'inserimento della email che abbia sempre la chiocciola
                if (regexùEmail.IsMatch(email)) { Email = true; } else { throw new Exception("le email deve essere di questo tipo di formato anche compresnsivo di numeri se vuoi pippi90@outlook.pip "); }
                if (User == true && Email == true) { return true; }
            }
            catch (Exception ex)
            {

                Console.WriteLine("errore nel metodo di CheckUserEmail: " + ex.Message);
            }




            return false;
        }
        #endregion

        public KeyValuePair<string, string> EncryptSalt(string valu)
        {
            //keyvaluepair --- questo è un oggetto che ci ritorna 2 stringhe 



            KeyValuePair<string, string> keyValue = new KeyValuePair<string, string>();
            byte[] bytes = new byte[16];
            try
            {
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    rng.GetNonZeroBytes(bytes);

                }
                string hashed2 = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: valu,
                    salt: bytes,
                    prf: KeyDerivationPrf.HMACSHA256, //chiave cifratura 256
                    iterationCount: 100000,   //tutte le iterazioni 
                    numBytesRequested: 32  //quanti bit vuoi  
                    )

                    );
                keyValue = new KeyValuePair<string, string>(hashed2, Convert.ToBase64String(bytes));
                Console.WriteLine(keyValue);
            }
            catch (Exception ex)
            {

                Console.WriteLine("errore nel metodo EcryptSalt:" + ex.Message);
            }


            return keyValue;
        }


        public List<int> checkNumOrDate(int age, int phone = 0)
        {


            try
            {
                if (age == null) { age = 0; }
                if (phone == null) { phone = 0; }
                Regex regexAge = new Regex(@"^[0-9]{2}/[0-9]{2}/[0-9]{4}$");
                if (regexAge.IsMatch(age.ToString()))
                {
                    if (age < 01 / 01 / 1950 || age > 01 / 01 / 2020)
                    {
                        throw new Exception("l'Età inserita non può essere minore del 01/01/1950 o magggiore di 01/01/2020");
                    }
                    else
                    {

                        string newAge = age.ToString();
                        foreach (string item in newAge.Split("/"))
                        {
                            string data = item[0].ToString() + item[1].ToString();
                            if (Int32.Parse(data) < 1 || Int32.Parse(data) > 31) { throw new Exception("il campo Giorno deve essere maggiore di 1 e non oltre il 31"); }
                            string mounth2 = item[2].ToString() + item[3].ToString();
                            if (Int32.Parse(mounth2) < 1 || Int32.Parse(mounth2) > 12) { throw new Exception("il campo del Mese deve essere maggiore di 1 e non oltre il 12"); }
                            string years = item[4].ToString() + item[5].ToString() + item[6].ToString() + item[7].ToString();
                            if (Int32.Parse(mounth2) < 1950 || Int32.Parse(mounth2) > 2020) { throw new Exception("il campo dell'anno deve essere maggiore di 2020 e non sotto il 1950"); }


                        }

                        List<int> days = new List<int>();
                        days.Add(Int32.Parse(newAge));
                        days.Add(phone);
                        return days;
                    }
                }





            }
            catch (Exception)
            {

                throw;
            }
            List<int> allEmpty = new List<int>();
            return allEmpty;
        }  // metodo da utilizzare con la data 

        public bool CheckAgeAndPhone(int age, int phone)
        {
            bool CheckOK = false;
            try
            {
                if (age < 18 || age > 90)
                {
                    throw new Exception("L'età inserita è minore di 18 o maggiore di 90");
                }
                Regex checkPhonec = new Regex(@"^([0-9]{10})$");
                if (checkPhonec.IsMatch(phone.ToString())) { return CheckOK = true; }
                else { new Exception("Attenzione il numero inserito non è corretto"); }
            }
            catch (Exception error)
            {

                Console.WriteLine("errore nel metodo CheckAgeAndPhone" + error.Message);
            }




            return true;
        }

        public string[] CheckUsernameAndPassword(string username)
        {
            bool hasUser = false;
            try
            {
                connectDB();
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "CheckUsernameAndPassword";
                sql.Parameters.AddWithValue("@username", username);
                
                SqlDataReader dataReader = sql.ExecuteReader();
                if (dataReader.HasRows)
                {

                    Console.WriteLine("elemento trovato");
                    hasUser = true;
                    while (dataReader.Read())
                    {

                        string[] pass = { dataReader.GetString(1) , dataReader.GetString(2) }; 
                      
                        return pass;
                    }



                    dataReader.Close(); // chiusura del data reader
                }
                else
                {

                    throw new Exception("nessun user trovato controlla i parametri inseriti ");
                   

                }

                

            }
            catch (Exception err)
            {

                Console.WriteLine("errore nel metodo di controllo ìnserimento Usename e Password : " + err.Message);
                
            }

            return null;
        }

        public bool CheckLogin(string pass, string passHash, string passSalt)
        {
            
            byte[] byteSalt = Convert.FromBase64String(passSalt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass,
                salt: byteSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));

            return (hashed == passHash);

        }

        public string[] convertInsertCredential(string autorization)
        {
            try
            {
                string[] arr = autorization.Split(" ");
                string UsernamePas = Encoding.UTF8.GetString(Convert.FromBase64String(arr[1]));
                int separatoreIndex = UsernamePas.IndexOf(":");
                // indice di partenza
                //string Username = UsernamePas.Substring(0, separatoreIndex);  //parto dalla posizione zero fino ad arrivare all'indice
                //string passWord = UsernamePas.Substring(separatoreIndex + 1); //parto dall'indice +1
                string[] users = { UsernamePas.Substring(0, separatoreIndex), UsernamePas.Substring(separatoreIndex + 1) };
                return users;
            }
            catch (Exception err)
            {

                Console.WriteLine("errore nel metodo ConvertInsertCredential" + err.Message);
                return null;
            }
        
            
         }
    }
    
}