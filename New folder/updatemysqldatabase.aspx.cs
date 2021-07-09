using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Data;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;

namespace merit
{
    public partial class updatemysqldatabase : System.Web.UI.Page
    {
        List<int> CummCap = new List<Int32>(); 
        List<string> stationlist = new List<string>();
        List<decimal> VClist = new List<decimal>(); 
        string empnumber = "asdsadasd";
        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
        protected void Page_Load(object sender, EventArgs e)
        {
            //generate_table_chart("Jan-21");
            

            if (!IsPostBack)
            {
                populate_state(); 
                Literal2.Text = "<a href=\".\\files_download\\MO_download\\1.xlsx\">download sample file for Andhra Pradesh</a>";
            }
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void state_DDL_change(object sender, EventArgs e)
        {
            Literal2.Text= "<a href=\".\\files_download\\MO_download\\"+ DDL_for_state.SelectedItem.Value+ ".xlsx\">download sample file for " + DDL_for_state.SelectedItem.Text+"</a>";
        }
       
        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            if (FileUpLoad1.HasFile)
            {
                FileUpLoad1.PostedFile.SaveAs(Server.MapPath(" ") + "\\files_upload\\" + FileUpLoad1.FileName);

//                FileUpLoad1.SaveAs(@"E:\" + FileUpLoad1.FileName);
                Literal1.Text = "File Uploaded: " + FileUpLoad1.FileName;
            }
            else
            {
                Literal1.Text = "No File Uploaded.";
            }
        }
        protected void populate_state()
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                string cmdText = "SELECT SrNo,StateName FROM state_master WHERE SrNo<35 ORDER BY SrNo ASC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);



                if (dt.Rows.Count > 0)
                {
                    DDL_for_state.DataSource = dt;
                    DDL_for_state.DataBind();
                    DDL_for_state.SelectedIndex = 0;
                    //populate_month(DDL_for_state.SelectedValue.ToString());
                    // generate_table_chart(dt.Rows[0].ItemArray[0].ToString(), "1");
                }
                else
                {
                    ltrChart.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
    }
}