

using Microsoft.Data.SqlClient;

namespace Betacomio_Project.Controllers
{
    public class SqlQuery
    {
        SqlConnection conn = new SqlConnection();

        public SqlQuery() 
        {
            try
            {
                if(conn.State==System.Data.ConnectionState.Closed)
                {
                    conn.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=AdventureWorksLT2019;Integrated Security=True;TrustServerCertificate=True";
                }

            }catch (Exception ex) 
            {
                Console.WriteLine("si è verificato un errore");
            }
        }
        public string GetHome = "";

        public void Homequery(string _contex)
        {
            SqlCommand cmd = conn.CreateCommand();vvv
            
            
        }
    }
}
