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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //string Command = "Select * from tblProductInventory where ProductName like '" + TextBox1.Text + "%'";
                //parameterized queries
                //string Command = "Select * from tblProductInventory where ProductName like @ProductName";

                //stored procedures
                string Command = "spGetProceductsByName";
                SqlCommand cmd = new SqlCommand(Command, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", TextBox1.Text+"%");
                con.Open();
                SqlDataReader rdr=cmd.ExecuteReader();
                GridView1.DataSource = rdr;
                GridView1.DataBind();
            }

        }
    }
}