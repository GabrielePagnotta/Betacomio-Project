using Betacomio_Project.NewModels;
using System.Text.RegularExpressions;
using RegexCheck;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;
using Betacomio_Project.LogModels;

namespace Betacomio_Project.BusinessLogic
{
    public class InsertUS
    {

        public bool Usnew(UserCredential user)
        {
			try
			{
                
				RegexCh regex = new RegexCh();
                
				if(regex.CheckUserEmail(user.Username , user.Email , user.Name , user.Surname) == true)
                {} else { throw new Exception("email o username non valido "); };
                if (user.PasswordHash.IsNullOrEmpty()) { throw new Exception("Il Campo Password non può essere Null o Vuoto");}
				Regex RegexPassword = new Regex(@"^(?=.*[!@#$%^&*()\-_=+[\]{}|\\;:'""<>,.?/~])\S{6,15}$");
                if (RegexPassword.IsMatch(user.PasswordHash)){
                   var PassSalt = regex.EncryptSalt(user.PasswordHash);

                    user.PasswordHash = PassSalt.Key;
                   user.PasswordSalt = PassSalt.Value;
				}
				else { throw new Exception("La password inserita non rispecchia i parametri di base almeno un carattere SPECIALE e NUMERI"); };
               

                if ( user.Phone != null)
                {
                   regex.checkNumOrDate(  user.BirthYear , user.Phone);
                }
                return true;
            }
			catch (Exception err)
			{
                Console.WriteLine("Errore nel metodo di USnew : " + err.Message);
                return false;
			}

        }

      
    }
}
