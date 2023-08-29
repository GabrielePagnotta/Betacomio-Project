

using Betacomio_Project.NewModels;

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
        public List<ViewAdminProduct> AdminProductsV()
        {
            List<ViewAdminProduct> adminView = new();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM View_AdminProducts";

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

                    adminView.Add(new ViewAdminProduct
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
                        ThumbnailPhoto = (byte[])dr["ThumbnailPhotoFileName"],
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
        public List<ViewUserProduct> UserProductsV()
        {
            List<ViewUserProduct> userView = new();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM View_UserProducts";

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

                    userView.Add(new ViewUserProduct
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


        public List<ViewAdminUserRegistry> UserRegistryV()
        {
            List<ViewAdminUserRegistry> registryView = new();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM View_Admin_UserRegistry";

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {

                    registryView.Add(new ViewAdminUserRegistry
                    {
                        UserId = Convert.ToInt16(dr["UserId"]),
                        Name = dr["Name"].ToString(),
                        Surname = dr["Surname"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        AddressId = Convert.ToInt16(dr["AddressId"]),
                        Address = dr["Address"].ToString(),
                        AddressDetail = dr["AddressDetail"].ToString(),
                        City = dr["City"].ToString(),
                        Region = dr["Region"].ToString(),
                        Country = dr["Country"].ToString(),
                        PostalCode = dr["PostalCode"].ToString(),
                        ModifiedDate = Convert.ToDateTime(dr["ModelType"])

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
