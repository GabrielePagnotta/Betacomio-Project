using Betacomio_Project.ConnectDb;
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
//using Org.BouncyCastle.Utilities.Encoders;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

using System.Drawing;
using Microsoft.CodeAnalysis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using NLog;
using Microsoft.SqlServer.Server;
using Microsoft.AspNetCore.Mvc;

namespace RegexCheck
{
    public class RegexCh
    {
        SqlConnection sqlConnection = new SqlConnection();
        private static Logger logger = LogManager.GetCurrentClassLogger();



        public void connectDB(string connection)
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.ConnectionString = connection;
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
                Regex regexNameSurName = new Regex(@"^([a-zA-Z]{4,})$");
                if ((name.ToLower() == null || name.ToLower() == "") && (surname.ToLower() == null || surname.ToLower() == ""))
                { throw new Exception("il campo Nome/Cognome non può essere vuoto"); }
                if (regexNameSurName.IsMatch(name) && regexNameSurName.IsMatch(surname)) { }
                else { throw new Exception("Il campo nome deve contenere solo caratteri e non può contenere numeri o caratteri speciali"); };
                Regex regex =  new Regex(@"^(?=.*[a-zA-Z]{4,12})(?=.*\d.*\d).+$");  //questa regex controlla che la username abbia almeno 2 numeri e dei caratteri
                if (user.Length >= 6)
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
                logger.WithProperty("ErrorCode", ex.HResult)
                       .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                       .Error("{Message}", ex.Message);
            }


            return keyValue;
        }


        public bool checkNumOrDate(string age, string phone)
        {
            
            string reverse = string.Empty;
            
            foreach (var item in age.Split("/"))
            {
                
                string[] years = item.Split("-");
                Console.WriteLine(years);
                reverse = years[2] + "/" + years[1] + "/" + years[0] ;
                if ( int.Parse(years[0]) < 1930 || int.Parse(years[0]) > 2005 )
                {
                    Console.WriteLine("errore la data inserita è minore del 1930 o maggiore del 2005");
                    return false;
                }
            }
            Console.WriteLine(reverse);
            try
            {
                
               
                Regex regexAge = new Regex(@"^[0-9]{2}/[0-9]{2}/[0-9]{4}$");
                if (regexAge.IsMatch(reverse))
                {
                    Console.WriteLine("ottimo");
                }
                else
                {
                   throw new Exception("errore nell'inserimento della data ");
                }

                if (phone == null || phone == "" || phone.Length > 12)
                {
                    throw new Exception("errone nell'inserimento del numero lunghezza numero cellulare troppo lungo");
                }

                return true;

            }
            catch (Exception err)
            {
                Console.WriteLine("Errore nel metodo per l'inserimento della data ed il numero");
                
                return false;
            }
         
           
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

        public string[] CheckUsernameAndPassword(string email , SingleTonConnectDB connession)
        {
            bool hasUser = false;
            try
            {
                string connect = connession.ConnectDb();
                Console.WriteLine(connect);
                connectDB(connect);
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "CheckUsernameAndPassword";
                sql.Parameters.AddWithValue("@email", email);
                
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
            finally
            {
                connession.Dispose();
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

         #region metodo che controlla che in fase di registrazione l'utente non esista
        public bool Checkusername(SingleTonConnectDB connession, string username , string email) //ricerca se username o email sono le stesse
        {
            bool isOk = true;

            try
            {
                connectDB(connession.ConnectDb());
                
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "CheckUsername";
                if (username.IsNullOrEmpty() | email.IsNullOrEmpty())
                {
                    throw new Exception("il campo inserito è vuoto o nullo ");
                }
                // stiamo accedendo a dei dati del db e quindi dobbiamo convertire la nostra variabile nel tipo specificato
                //System.Data.SqlDbType.NVarChar).Value = username;
                sql.Parameters.AddWithValue("@username" , username);
                sql.Parameters.AddWithValue("@email", email);
                SqlDataReader sqlData = sql.ExecuteReader();

                using (sqlData)
                {
                    if (sqlData.HasRows)
                    {
                        isOk = true;
                        throw new Exception("Il nome utente o email sono già presenti del nostro DataBase");

                    }
                    return isOk = false;

                }
            }
            catch (Exception err)
            {

                throw new Exception("Errore nel metodo checkUsername" + err.Message);
            }
            finally
            {
                connession.Dispose();
            }

        }
        #endregion //

        /// <summary>
        /// Inserisci utente registrato negli Users
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="userId"></param>
        /// <exception cref="Exception"></exception>
        public async void PassUserCredentials(SingleTonConnectDB connection, int userId)
        {

            try
            {
                connectDB(connection.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "UserCredentialsToMainDB"; //Si passano i dati dell'utente da AdminLog a BetacomioCycles

                sql.Parameters.AddWithValue("@userIdentifier", userId);

                sql.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Dispose();
            }
        }


        /// <summary>
        /// Mostra prodotti di ciascuna lingua a seconda della nazionalità scelta dallo User
        /// </summary>
        /// <param name="connession"></param>
        /// <param name="nationality"></param>
        ///<returns> Lista di prodotti in lingua specifica </returns>
        public async Task<IEnumerable<ViewUserProduct>> ProductsWithLanguage(MainSingleton connectao, int nationality)
        {

                List<ViewUserProduct> langProducts = new();
                decimal decimalValue;

            try
            {
                connectDB(connectao.ConnectToMainDB()); //necessario Singleton che si connetta a DB BetacomioCycles
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "ShowProductsLanguage";

               // mostra prodotti della stessa lingua dell'utente (in base a Nationality)
               sql.Parameters.AddWithValue("@userNationality", nationality);

              //  avvio dataReader per leggere record della view UserProducts
                SqlDataReader dr = sql.ExecuteReader();

                using (dr)
                {
                    if (dr.HasRows)
                    {
                        while (await dr.ReadAsync())
                        {

                            if (DBNull.Value.Equals(dr["Weight"]))
                            {
                                decimalValue = 0;
                            }
                            else
                            {
                                decimalValue = (decimal)dr["Weight"];
                            }

                            langProducts.Add(new ViewUserProduct
                            {
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                Name = dr["Name"].ToString(),
                                ProductType = dr["ProductType"].ToString(),
                                ModelType = dr["ModelType"].ToString(),
                                ListPrice = (decimal)dr["ListPrice"],
                                Color = dr["Color"].ToString(),
                                Size = dr["Size"].ToString(),
                                Weight = decimalValue,
                                Description = dr["Description"].ToString(),
                                //ThumbnailPhoto = ImgConverter.GetImageFromByteArray((byte[])dr["ThumbnailPhoto"]), //converto array di byte in bitmap
                                ThumbnailPhoto = (byte[])dr["ThumbnailPhoto"],
                                Culture = dr["Culture"].ToString()

                            });
                        }
                    }
                    else
                    {
                        throw new Exception("Il prodotto ricercato non è presente");
                    }
                    
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel metodo ProductsWithLanguage: " + ex.Message);

            }
            finally
            {
                connectao.Dispose(); //si rilasciano le risorse
            }

            return langProducts;



        }

        public List<string> myProfileLogin(SingleTonConnectDB _connession , string email)
        {
            List<string> dati = new List<string>();
            connectDB(_connession.ConnectDb());
            try
            {
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "[dbo].[UserData]";
                sql.Parameters.AddWithValue("@email", email);
                SqlDataReader sqlData = sql.ExecuteReader();

                using (sql)
                {
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            dati.Add(sqlData[0].ToString());
                            dati.Add(sqlData[1].ToString());
                            dati.Add(sqlData[2].ToString());
                            dati.Add(sqlData[3].ToString());
                            dati.Add(sqlData[4].ToString());
                            dati.Add(sqlData[5].ToString());
                            dati.Add(sqlData[6].ToString());
                            dati.Add(sqlData[7].ToString());
                            return dati;
                        }

                    }
                    else
                    {
                        new Exception("Errore username inserito");
                    }
                }


            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            finally
            {
                _connession.Dispose();
            }

            return dati;
        }


        /// <summary>
        /// Richiesta POST di un prodotto nella Wishlist
        /// </summary>
        /// <param name="_connession"></param>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        public void PassWishlistData(SingleTonConnectDB _connession, int productId, int userId)
        {
           
            connectDB(_connession.ConnectDb());
            try
            {
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "WishlistDataToMainDB"; //SP che inserisce dati da wishlist temporanea a quella originale
                sql.Parameters.AddWithValue("@userIdentifier", userId);
                sql.Parameters.AddWithValue("@prodIdentifier", productId);

                sql.ExecuteNonQuery(); //esegui SQL query

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
            finally
            {
                _connession.Dispose();

            }



        }

        /// <summary>
        /// Richiesta POST di un prodotto nel carrello
        /// </summary>
        /// <param name="_connession"></param>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        public void PassShoppingCartData(SingleTonConnectDB _connession, int productId, int userId)
        {

            connectDB(_connession.ConnectDb());
            try
            {
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "ShoppingDataToMainDB"; 
                sql.Parameters.AddWithValue("@userIdentifier", userId);
                sql.Parameters.AddWithValue("@prodIdentifier", productId);

                sql.ExecuteNonQuery();

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message); 
            }
            finally
            {
                _connession.Dispose();
            }


        }
        public bool ExistUser(SingleTonConnectDB connect , string email)
        {
            bool ok = false;
            try
            {
                connectDB(connect.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "CheckUsername";
                sql.Parameters.AddWithValue("@email", email);
                sql.ExecuteNonQuery();
                connect.Dispose();
                return ok = true;
            }
            catch (Exception err)
            {

                Console.WriteLine("errore nel metodo ExistUser" + err.Message);
                return ok = false;
            }
            
        }


        #region Inserimento nuovo indirizzo con return ADDRESSID
        public int NewAddress(SingleTonConnectDB connect , string Address , string AddressDetail,string City ,string Region , string Country ,string PostalCode)
        {
            try
            {
                connectDB(connect.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "AddressInsertData";
               
                sql.Parameters.AddWithValue("@address", Address);
                sql.Parameters.AddWithValue("@addrDetail", AddressDetail);
                sql.Parameters.AddWithValue("@city", City);
                sql.Parameters.AddWithValue("@region", Region);
                sql.Parameters.AddWithValue("@country", Country);
                sql.Parameters.AddWithValue("@postcode", PostalCode);
                int AddressId = Convert.ToInt32(sql.ExecuteScalar());
                connect.Dispose();
                return AddressId;
            }
            catch (Exception err)
            {

                Console.WriteLine(err.Message);
                return 0;
            }
           
        }
        #endregion
        /// <summary>
        /// Unione dei dati dello User con il corrispondente Address in tabella di congiunzione UserAddress
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="userID"></param>
        /// <param name="addressID"></param>
        /// 
        #region INSERIMENTO ID UTENTE E ID ADDRESS
        public void BindUSerAndAddress(SingleTonConnectDB connect, int userID, int addressID)
        {
            try
            {
                connectDB(connect.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "BindAddress&User";

                sql.Parameters.AddWithValue("@userID", userID);
                sql.Parameters.AddWithValue("@addressID", addressID);
                sql.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                connect.Dispose();
            }
        }
        #endregion

        public int OrderHeaderInsert(SingleTonConnectDB connect, int addressID , int customerId , int subtotal)
        {
            DateTime date = DateTime.Today;
            
            try
            {
                connectDB(connect.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "OrderHeader_heloData";
                sql.Parameters.AddWithValue("@customerID", customerId);
                sql.Parameters.AddWithValue("@addressID", addressID);
                sql.Parameters.AddWithValue("@orderdate", date);
                sql.Parameters.AddWithValue("@subtotal", subtotal);
                int OrderID = Convert.ToInt32(sql.ExecuteScalar());
                return OrderID;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                connect.Dispose();
            }
        }

        /// <summary>
        /// Inserimento dati in OrderDetail dopo aver effettuato un acquisto
        /// </summary>
        /// <param name="connect"></param>
        /// <param name="quantity"></param>
        /// <param name="productID"></param>
        /// <param name="unitPrice"></param>
        /// <param name="totPrice"></param>
        public void OrderDetilInsert(SingleTonConnectDB connect, int quantity, int productID, decimal unitPrice, decimal totPrice , int OrderId)
        {
            try
            {
                connectDB(connect.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "OrderDetailData_mainDB";

                sql.Parameters.AddWithValue("@prodID", productID);
                sql.Parameters.AddWithValue("@quantity", quantity);
                sql.Parameters.AddWithValue("@unitprice", unitPrice);
                sql.Parameters.AddWithValue("@totprice", totPrice);
                sql.Parameters.AddWithValue("@orderID", OrderId);

                sql.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                connect.Dispose();
            }
        }

        public int CheckAddress(SingleTonConnectDB connect , int userid , string address)
        {
            try
            {
                connectDB(connect.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "CheckAddress";
                sql.Parameters.AddWithValue("@UserID", userid);
                sql.Parameters.AddWithValue("@address", address);
                int AddressId = Convert.ToInt32(sql.ExecuteScalar());
                return AddressId;
            }
            catch (Exception err)
            {

                Console.WriteLine("nessun indirizzo con questo utente ");
                return 0;
            }
            finally { connect.Dispose(); }
        }

        public void ClearCash(SingleTonConnectDB connect)
        {
            connectDB(connect.ConnectDb());
            SqlCommand sql = sqlConnection.CreateCommand();
            sql.CommandType = System.Data.CommandType.StoredProcedure;
        }

    }

}