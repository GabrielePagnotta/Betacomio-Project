using Betacomio_Project.Models;
using System.Text.RegularExpressions;
using RegexCheck;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;

namespace Betacomio_Project.Controllers
{
    public class InsertUS
    {


        public void Usnew(User user)
        {
			try
			{
				RegexCh regex = new RegexCh();
				
				if(regex.CheckUserEmail(user.Username , user.Email , user.Name , user.Surname) == true)
                {} else { throw new Exception("email o username non valido "); };
                if (user.PasswordHash.IsNullOrEmpty()) { throw new Exception("Il Campo Password non può essere Null o Vuoto");}
				Regex RegexPassword = new Regex(@"^(?=.*[!@#$%^&*()\-_=+[\]{}|\\;:'""<>,.?/~])\S{7,12}$");
                if (RegexPassword.IsMatch(user.PasswordHash)){
                    var PassSalt = regex.EncryptSalt(user.PasswordHash);

                    user.PasswordHash = PassSalt.Key;
                    user.PasswordSalt = PassSalt.Value;
				}
				else { new Exception("La password inserita non rispecchia i parametri di base almeno un carattere SPECIALE e NUMERI"); };
                user.BirthYear = null;
                //if ( user.Phone != null)
                //{
                //    string? birth = user.BirthYear.ToString();
                //    int? Phonw = user.Phone;
                //    regex.checkNumOrDate(  Int32.Parse(birth) , Phonw.Value);
                //}
           






            }
			catch (Exception err)
			{
                Console.WriteLine("Errore nel metodo di USnew : " + err.Message);
			}

        }
    }
}
