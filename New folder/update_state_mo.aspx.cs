using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Data.OleDb;
using System.Data;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
using System.Drawing;
using System.Configuration;

namespace merit
{
    public partial class update_state_mo : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["commercial_mySQL"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                date_from.Text = DateTime.Now.ToString("yyyy-MM-dd");
                // Literal3.Text = "<br/>" + str + "<br/>" + str;
                //populate_fy();
                populate_state();
                //DDL_for_fy.Visible = true;
                DDL_for_state.Visible = true;
                //Label2.Text = "Select FY :  ";
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
                string cmdText = "SELECT SrNo, StateName FROM state_master ORDER BY SrNo";
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
                    Label2.Text = "No state list Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Label2.Text += err;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        protected void state_DDL_change(object sender, EventArgs e)
        {
            generate_table_chart(DDL_for_state.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            //generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                (e.Item.FindControl("lblRowNumber") as Label).Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        protected void generate_table_chart(string state_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");



            try
            {
                con.Open();
               

                    cmdText = "SELECT b.display_name,a.VC,a.StationID FROM mo_master01 a, station_master b WHERE b.SrNo=a.StationID AND StateID=" + Convert.ToInt32(DDL_for_state.SelectedValue) + " AND date_from IN (SELECT * FROM(SELECT DISTINCT date_from FROM mo_master01 WHERE StateID=" + Convert.ToInt32(DDL_for_state.SelectedValue) +" ORDER BY date_from DESC LIMIT 1) AS t1)";
                
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    
                    testgrid.Visible = true;
                    dt.Columns.Add("Sr_No", typeof(String));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i][3] = i+1;
                       // Label2.Text += "<br/>" + i + "=" + dt.Rows[i].ItemArray[6].ToString();
                    }
                    testgrid.DataSource = dt;
                    testgrid.DataBind();
                    //testgrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
                    testgrid.HeaderRow.TableSection = TableRowSection.TableHeader;





                }
                else
                {
                    Label2.Text = "No data Found";
                    testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Label2.Text += err;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }



        }
        protected void update_mo(object sender, System.EventArgs e)
        {
            Label2.Text = "You clicked the first button.";

            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new System.Data.DataColumn("SrNo", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("StationID", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("StationName", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("VC_old", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("VC", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("VC_compare", typeof(String)));

            foreach (GridViewRow row in testgrid.Rows)
            {

                Label SrNo = (Label)row.FindControl("SrNo");
                Label StationID = (Label)row.FindControl("StationID");
                Label StationName = (Label)row.FindControl("StationName");
                Label VC_old = (Label)row.FindControl("VC_old");
                TextBox VC_text = (TextBox)row.FindControl("VC_text");
                dr = dt.NewRow();
                dr[0] = SrNo.Text;
                dr[1] = StationID.Text;
                dr[2] = StationName.Text;
                dr[3] = VC_old.Text;
                dr[4] = VC_text.Text.ToString();
                if (VC_old.Text == VC_text.Text.ToString())
                    dr[5] = "yes";
                else
                    dr[5] = "no";

                dt.Rows.Add(dr);
            }
            for(int i = 0,k=0; i < dt.Rows.Count; i++)
            {
               // if (i == 0)
                  //  Label2.Text += "INSERT INTO mo_master01 (StateID,StationID,VC,date_from,date_to) VALUES ";
                if(dt.Rows[i].ItemArray[5].ToString()=="no")
                {
                    if (k == 0)
                    {
                        Label2.Text += "INSERT INTO mo_master01 (StateID,StationID,VC,date_from,date_to) VALUES ";
                        k++;
                    }
                    Label2.Text += "(" + +Convert.ToInt32(DDL_for_state.SelectedValue) + "," + dt.Rows[i].ItemArray[1].ToString() + "," + dt.Rows[i].ItemArray[4].ToString() + ",'2021-07-07',NULL) ";
                }
            }
        }

        protected void txt_date_TextChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(date_from.Text);
            date_from.Text = date.ToString("yyyy-MM-dd");
            Label2.Text += date.ToString("yyyy-MM-dd");
            //date_from.text = Datetime.Now.Tostring("yyyy-MM-dd");
        }

        

    }
}