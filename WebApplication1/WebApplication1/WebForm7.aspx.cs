using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;


namespace WebApplication1
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            if (Cache["Data"] == null)
            {
                string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select * from tblProductInventory", con);
                    DataSet ds = new DataSet();

                    da.Fill(ds);
                    Cache["Data"] = ds;
                    gvProducts.DataSource = ds;
                    gvProducts.DataBind();
                }
                lblMessage.Text = "Data loaded from the Database";
            }
            else
            {
                gvProducts.DataSource = (DataSet)Cache["Data"];
                gvProducts.DataBind();
                lblMessage.Text = "Data loaded from the Cache";
            }
        }

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            if (Cache["Data"]!=null)
            {
                Cache.Remove("Data");
                lblMessage.Text = "The DataSet is removed from the cache";
            }
            else
            {
                lblMessage.Text = "There is nothing in the cache to remove";
            }
        }
    }
}