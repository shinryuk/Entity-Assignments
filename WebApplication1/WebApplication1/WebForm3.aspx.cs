using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "Delete from Product where ProductId = 4";
                int Total = cmd.ExecuteNonQuery();
                Response.Write("Total rows deleted= " + Total + "<br/>");

                cmd.CommandText = "Insert into Product values(4, 'Calculators', 100, 230)";
                Total = cmd.ExecuteNonQuery();
                Response.Write("Total rows inserted= " + Total +"<br/>");

               cmd.CommandText= "Update Product set QtyAvailable = 200 where ProductId = 2";
               Total= cmd.ExecuteNonQuery();
               Response.Write("Total rows updated= " + Total +"<br/>");

            }
        }
    }
}