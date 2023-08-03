

using Betacomio_Project.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Betacomio_Project.Controllers
{
    public class SqlQueryViews
    {
        SqlConnection conn = new SqlConnection();
        bool IsDok;

        public SqlQueryViews() 
        {
            try
            {
                if(conn.State==System.Data.ConnectionState.Closed)
                {
                    conn.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=AdventureWorksLT2019;Integrated Security=True;TrustServerCertificate=True";
                    conn.Open();
                    IsDok = true;

                }

            }catch (Exception ex) 
            {
                IsDok=false;
                Console.WriteLine($"si è verificato un errore : {ex.Message}");
            }
        }
       // private string GetRowDetail(DataRow row)
       // {
       //     string r=string.Empty;
       //     int nCol = 0;
       //     foreach(var buff in row.ItemArray){
       //         r += $"({row.Table.Columns[nCol].ColumnName} : {buff}";
       //         nCol++;
       //     }
       //     return r;
       // }
       //// public string GetHome = "";

       // public List<string> Homequery()
       // {

       //     List<string> r = new List<string>();
       //     try
       //     {
       //         //  SqlCommand cmd = conn.CreateCommand();
       //         //  cmd.CommandText = "SELECT SalesLT.Product.Name, SalesLT.Product.Color, SalesLT.Product.ListPrice, SalesLT.Product.Size, SalesLT.Product.Weight, SalesLT.ProductCategory.Name AS Expr1, SalesLT.ProductDescription.Description, \r\n                  SalesLT.ProductModel.Name AS Expr2\r\nFROM     SalesLT.Product INNER JOIN\r\n                  SalesLT.ProductCategory ON SalesLT.Product.ProductCategoryID = SalesLT.ProductCategory.ProductCategoryID INNER JOIN\r\n                  SalesLT.ProductModel ON SalesLT.Product.ProductModelID = SalesLT.ProductModel.ProductModelID CROSS JOIN\r\n                  SalesLT.ProductDescription";
       //         DataTable dt = new DataTable();
       //         // assolutamente evitare hardcode comando sql
       //         SqlDataAdapter adapter = new SqlDataAdapter("SELECT SalesLT.Product.Name AS NomeProdotto, SalesLT.Product.Color, SalesLT.Product.ListPrice, SalesLT.Product.Size, SalesLT.Product.Weight, SalesLT.ProductCategory.Name AS NomeCategoria, SalesLT.ProductDescription.Description, \r\n                  SalesLT.ProductModel.Name AS NomeModello\r\nFROM     SalesLT.Product INNER JOIN\r\n                  SalesLT.ProductCategory ON SalesLT.Product.ProductCategoryID = SalesLT.ProductCategory.ProductCategoryID INNER JOIN\r\n                  SalesLT.ProductModel ON SalesLT.Product.ProductModelID = SalesLT.ProductModel.ProductModelID CROSS JOIN\r\n                  SalesLT.ProductDescription", conn);
       //         adapter.Fill(dt);
       //         foreach (DataRow dr in dt.Rows)
       //         {
       //             r.Add(GetRowDetail(dr));
       //         }
       //     }
       //     catch (Exception ex)
       //     {

       //         Console.WriteLine($"Errore nel GetData: {ex.Message}");
       //     }

       //     return r;
       // }

       // public List<string> HomequeryProductNames(string product_name)
       // {

       //     List<string> r = new List<string>();
       //     try
       //     {
       //         SqlCommand cmd = new SqlCommand("SELECT SalesLT.Product.Name AS NomeProdotto, SalesLT.Product.Color, SalesLT.Product.ListPrice, SalesLT.Product.Size, SalesLT.Product.Weight, SalesLT.ProductCategory.Name AS NomeCategoria, SalesLT.ProductDescription.Description, \r\n                  SalesLT.ProductModel.Name AS NomeModello\r\nFROM     SalesLT.Product INNER JOIN\r\n                  SalesLT.ProductCategory ON SalesLT.Product.ProductCategoryID = SalesLT.ProductCategory.ProductCategoryID INNER JOIN\r\n                  SalesLT.ProductModel ON SalesLT.Product.ProductModelID = SalesLT.ProductModel.ProductModelID CROSS JOIN\r\n                  SalesLT.ProductDescription\r\nWHERE  (SalesLT.Product.Name LIKE '@product_name%')", conn);
       //         cmd.Parameters.Add(new SqlParameter("@product_name",product_name));
       //         DataTable dt = new DataTable();
       //         SqlDataAdapter adapter = new SqlDataAdapter(cmd);
       //         adapter.Fill(dt);
       //         foreach (DataRow dr in dt.Rows)
       //         {
       //             r.Add(GetRowDetail(dr));
       //         }
       //     }
       //     catch (Exception ex)
       //     {

       //         Console.WriteLine($"Errore nel GetData: {ex.Message}");
       //     }

       //     return r;
       // }

       // public void Homequery(string _contex)
       // {
       //     SqlCommand cmd = conn.CreateCommand();
            
            
        //}

        /// <summary>
        /// Visualizzazione di tutti i prodotti per Admin
        /// </summary>
        /// <returns></returns>
        public List<AdminProductsView> AdminProductsV()
        {
            List<AdminProductsView> adminView = new();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Admin_ProductsView";

            decimal decimalValue;

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (DBNull.Value.Equals(dr["Weight"]))
                    {
                        decimalValue = 0;
                    }
                    else
                    {
                        decimalValue = (decimal)dr["Weight"];

                    }

                    adminView.Add(new AdminProductsView
                    {
                        ProductId = (int)dr["ProductId"],
                        Name = dr["Name"].ToString(),
                        ProductNumber = dr["ProductNumber"].ToString(),
                        Color = dr["color"].ToString(),
                        Size = dr["size"].ToString(),
                        Weight = decimalValue,
                        StandardCost = (decimal)dr["StandardCost"],
                        ListPrice = (decimal)dr["ListPrice"],
                        ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]),
                        ThumbnailPhotoFileName = dr["ThumbnailPhotoFileName"].ToString(),
                        ProductType = dr["ProductType"].ToString(),
                        Description = dr["Description"].ToString(),
                        ModelType = dr["ModelType"].ToString(),
                        CatalogDescription = dr["CatalogDescription"].ToString(),
                        Culture = dr["Culture"].ToString()

                    });




                }
            }
           


            return adminView;
        }


        /// <summary>
        /// Visualizzazione di tutti i prodotti per User
        /// </summary>
        /// <returns></returns>
        public List<UserProductsView> UserProductsV()
        {
            List<UserProductsView> userView = new();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM User_ProductsView";

            decimal decimalValue;

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (DBNull.Value.Equals(dr["Weight"]))
                    {
                        decimalValue = 0;
                    }
                    else
                    {
                        decimalValue = (decimal)dr["Weight"];

                    }

                    userView.Add(new UserProductsView
                    {
                        Name = dr["Name"].ToString(),
                        Color = dr["color"].ToString(),
                        Size = dr["size"].ToString(),
                        Weight = decimalValue,
                        ListPrice = (decimal)dr["ListPrice"],
                        ProductType = dr["ProductType"].ToString(),
                        Description = dr["Description"].ToString(),
                        ModelType = dr["ModelType"].ToString(),
                        Culture = dr["Culture"].ToString()

                    });




                }
            }
           


            return userView;
        }


        public List<AdminUsersRegistry> UserRegistryV()
        {
            List<AdminUsersRegistry> registryView = new();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Admin_UserRegistry";

            Guid rowguid;

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    if (DBNull.Value.Equals(dr["Rowguid"]))
                    {
                        rowguid = Guid.Empty;
                    }
                    else
                    {
                        rowguid = dr.GetGuid(dr.GetOrdinal("Rowguid"));

                    }


                    registryView.Add(new AdminUsersRegistry
                    {
                        CustomerId = Convert.ToInt16(dr["CustomerId"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        EmailAddress = dr["EmailAddress"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        AddressId = Convert.ToInt16(dr["AddressId"]),
                        AddressLine1 = dr["AddressLine1"].ToString(),
                        AddressLine2 = dr["AddressLine2"].ToString(),
                        City = dr["City"].ToString(),
                        StateProvince = dr["StateProvince"].ToString(),
                        CountryRegion = dr["CountryRegion"].ToString(),
                        PostalCode = dr["PostalCode"].ToString(),
                        ModifiedDate = Convert.ToDateTime(dr["ModelType"]),
                        Rowguid = rowguid

                    });




                }
            }



            return registryView;
        }

        public bool CheckUsernameAndPassword(string username)
        {
            bool hasUser = false;
            try
            {
                SqlCommand sql = conn.CreateCommand();
                
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "CheckUsernameAndPassword";
                sql.Parameters.AddWithValue("@username", username);
              
                SqlDataReader dataReader = sql.ExecuteReader();
                if (dataReader.HasRows)
                {
                    Console.WriteLine("elemento trovato");
                    hasUser = true;
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

            return hasUser = false;
        }
    }
}
