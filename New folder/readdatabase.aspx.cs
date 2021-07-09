using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace mysite
{
    public partial class readdatabase : System.Web.UI.Page
    {
        string cs = "Data Source=SQL5097.site4now.net;Initial Catalog=db_a70e2a_mydata;User Id=db_a70e2a_mydata_admin;Password=Rimmi@1402";
        SqlConnection con2;
        SqlDataAdapter adapt;
        SqlDataAdapter adapt1;
        SqlDataAdapter adapt2;
        protected void Page_Load(object sender, EventArgs e)
        {
            con2 = new SqlConnection(cs);
            DataTable dt1 = new DataTable();
            try
            {
                con2.Open();
                SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[Table] ( [Id] INT NOT NULL PRIMARY KEY, [name] VARCHAR(MAX) NULL)", con2);

                SqlDataAdapter adbtr = new SqlDataAdapter();
                adbtr.UpdateCommand = new SqlCommand("CREATE TABLE [dbo].[Table] ( [Id] INT NOT NULL PRIMARY KEY, [name] VARCHAR(MAX) NULL)", con2);
                adbtr.UpdateCommand.ExecuteNonQuery();
                //clear();
            }
            catch (Exception ex)
            {
                Literal1.Text = ex.Message;
            }
            finally
            {
                con2.Close();
            }
        }
    }
}