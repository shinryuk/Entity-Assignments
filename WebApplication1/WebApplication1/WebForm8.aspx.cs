using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void GetDataFromDB()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            string strSelectQuery = "Select * from tblStudents";
            SqlDataAdapter da = new SqlDataAdapter(strSelectQuery, con);

            DataSet ds = new DataSet();
            da.Fill(ds, "Students");
            ds.Tables["Students"].PrimaryKey = new DataColumn[] { ds.Tables["Students"].Columns["Id"] };
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
            gvStudents.DataSource = ds;
            gvStudents.DataBind();

            lblMessage.Text = "Data Loaded From Database";
        } 
        private void GetDataFromCache()
        {
            if (Cache["DATASET"]!=null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                gvStudents.DataSource = ds;
                gvStudents.DataBind();
            }
        }

        protected void btnGetDataFromDB_Click(object sender, EventArgs e)
        {
            GetDataFromDB();
        }

        protected void gvStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStudents.EditIndex = e.NewEditIndex;
            GetDataFromCache();
        }

        protected void gvStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Cache["DATASET"]!=null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["Id"]);
                dr["Name"] = e.NewValues["Name"];
                dr["Gender"] = e.NewValues["Gender"];
                dr["TotalMarks"] = e.NewValues["TotalMarks"];

                Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                gvStudents.EditIndex = -1;
                GetDataFromCache();
            }
        }

        protected void gvStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStudents.EditIndex = -1;
            GetDataFromCache();
        }

        protected void gvStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["Id"]);
                dr.Delete();

                Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                GetDataFromCache();
            }
        }

        protected void btnUpdateDB_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            string strSelectQuery = "Select * from tblStudents";
            SqlDataAdapter da = new SqlDataAdapter(strSelectQuery, con);

            DataSet ds = (DataSet)Cache["DATASET"];

            string strUpdateCommand = "Update tblStudents set Name=@Name,Gender=@Gender,TotalMarks=@TotalMarks where Id=@Id";
            SqlCommand updateCommand = new SqlCommand(strUpdateCommand, con);
            updateCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");
            updateCommand.Parameters.Add("@Gender", SqlDbType.VarChar, 20, "Gender");
            updateCommand.Parameters.Add("@TotalMarks", SqlDbType.Int, 0, "TotalMarks");
            updateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            da.UpdateCommand = updateCommand;

            string strDeleteCommand = "Delete from tblStudents  where Id=@Id";
            SqlCommand deleteCommand = new SqlCommand(strDeleteCommand, con);
            deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            da.DeleteCommand = deleteCommand;
            lblMessage.Text = "Database Table Updated";



            da.Update(ds, "Students");

        }
    }
}