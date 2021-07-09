using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Data;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
using System.Web.Script.Serialization;

namespace merit
{
    public partial class Mkt_Q_data : System.Web.UI.Page
    {
        public string Data_Q_Curr_st
        {
            get;
            set;
        }

        public string Data_Q_Prev_st
        {
            get;
            set;
        }

        public string Data_Q_Prev_yr_st
        {
            get;
            set;
        }
        public string Data_Q_Station_st
        {
            get;
            set;
        }
        public string Data_Q_Curr_m
        {
            get;
            set;
        }

        public string Data_Q_Prev_m
        {
            get;
            set;
        }

        public string Data_Q_Prev_yr_m
        {
            get;
            set;
        }
        public string Data_Q_Month_m
        {
            get;
            set;
        }
        public string Data_Q_fy
        {
            get;
            set;
        }
        public string Data_Q_py
        {
            get;
            set;
        }
        public string Data_Q_pq
        {
            get;
            set;
        }

        public string Data_m_Curr_m
        {
            get;
            set;
        }

        public string Data_m_Prev_yr_m
        {
            get;
            set;
        }
        public string Data_m_Month_m
        {
            get;
            set;
        }
        public string Data_m_fy_fy
        {
            get;
            set;
        }
        public string Data_m_Qtm_fy
        {
            get;
            set;
        }
        public string Data_m_sel_m
        {
            get;
            set;
        }
        public string Data_m_sel_m_fy
        {
            get;
            set;
        }
        public string Data_m_sel_m_py
        {
            get;
            set;
        }


        List<decimal> Q_Curr_st_list = new List<decimal>();
        List<decimal> Q_Prev_st_list = new List<decimal>();
        List<decimal> Q_Prev_yr_st_list = new List<decimal>();
        List<string> Q_Station_st_list = new List<string>(); 
        List<decimal> Q_Curr_m_list = new List<decimal>();
        List<decimal> Q_Prev_m_list = new List<decimal>();
        List<decimal> Q_Prev_yr_m_list = new List<decimal>();
        List<string> Q_Month_m_list = new List<string>();
        List<decimal> m_Curr_m_list = new List<decimal>();
        List<decimal> m_Prev_yr_m_list = new List<decimal>();
        List<string> m_Month_m_list = new List<string>();
        List<decimal> m_Qtm_fy_list = new List<decimal>();
        List<string> m_fy_fy_list = new List<string>();
        string str = ConfigurationManager.ConnectionStrings["commercial_mySQL"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populate_ddl_station();
                if (DateTime.Now.ToString("MM") == "01")
                {
                    Qtr_list.SelectedIndex = 3;
                    Month_list.SelectedIndex = 0;
                }
                else if (DateTime.Now.ToString("MM") == "02")
                {
                    Qtr_list.SelectedIndex = 3;
                    Month_list.SelectedIndex = 1;
                }
                else if (DateTime.Now.ToString("MM") == "03")
                {
                    Qtr_list.SelectedIndex = 3;
                    Month_list.SelectedIndex = 2;
                }
                else if (DateTime.Now.ToString("MM") == "04")
                {
                    Qtr_list.SelectedIndex = 0;
                    Month_list.SelectedIndex = 3;
                }
                else if (DateTime.Now.ToString("MM") == "05")
                {
                    Qtr_list.SelectedIndex = 0;
                    Month_list.SelectedIndex = 4;
                }
                else if (DateTime.Now.ToString("MM") == "06")
                {
                    Qtr_list.SelectedIndex = 0;
                    Month_list.SelectedIndex = 5;
                }
                else if (DateTime.Now.ToString("MM") == "07")
                {
                    Qtr_list.SelectedIndex = 1;
                    Month_list.SelectedIndex = 6;
                }
                else if (DateTime.Now.ToString("MM") == "08")
                {
                    Qtr_list.SelectedIndex = 1;
                    Month_list.SelectedIndex = 7;
                }
                else if (DateTime.Now.ToString("MM") == "09")
                {
                    Qtr_list.SelectedIndex = 1;
                    Month_list.SelectedIndex = 8;
                }
                else if (DateTime.Now.ToString("MM") == "10")
                {
                    Qtr_list.SelectedIndex = 2;
                    Month_list.SelectedIndex = 9;
                }
                else if (DateTime.Now.ToString("MM") == "11")
                {
                    Qtr_list.SelectedIndex = 2;
                    Month_list.SelectedIndex = 10;
                }
                else
                {
                    Qtr_list.SelectedIndex = 2;
                    Month_list.SelectedIndex = 11;
                }


                Year_list.SelectedIndex = 0;
                DDL_for_station.SelectedIndex = 0;

                generate_Q_chart(Qtr_list.SelectedValue);
                generate_m_chart("1");
                // generate_table_chart("1","Min");
            }
            else
            {
                generate_Q_chart(Qtr_list.SelectedValue);
                generate_m_chart(Month_list.SelectedValue);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "window.top.location = ('WebForm1.aspx');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "$(document).ready(myFunction);", true);
                //ClientScriptManager.RegisterClientScriptBlock(this.GetType, "myFunction", "$(document).ready(myFunction);", true);
            }

        }
        protected void populate_ddl_station()
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string cmdText = "SELECT SrNo,display_name AS StationName FROM station_master WHERE SrNo IN (SELECT DISTINCT StationID FROM mkt_q_master) ORDER BY display_name";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);



                if (dt.Rows.Count > 0)
                {
                    DDL_for_station.DataSource = dt;
                    DDL_for_station.DataBind();
                    DDL_for_station.SelectedIndex = 0;
                    //generate_table_chart(dt.Rows[0].ItemArray[0].ToString(), "1");
                }
                else
                {
                    //ltrChart.Text = "No data Found";
                    //testgrid.Visible = false;
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
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void Qtr_change(object sender, EventArgs e)
        {
            //Label1.Text += "---" + Qtr_list.SelectedValue;
            generate_Q_chart(Qtr_list.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            //generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }
        protected void Month_change(object sender, EventArgs e)
        {
            //Label1.Text += "---" + Month_list.SelectedValue;
            generate_m_chart(Month_list.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            //generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }
        protected void Year_change(object sender, EventArgs e)
        {
            Label1.Text += "---" + Year_list.SelectedValue;
            //generate_table_chart("0", DDL_for_fy.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            //generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }
        protected void station_DDL_change(object sender, EventArgs e)
        {
            Label1.Text += "---" + DDL_for_station.SelectedValue;
            //generate_table_chart("0", DDL_for_fy.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            //generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }

        protected void generate_Q_chart(string filter_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            DataTable dt1 = new DataTable();

            Q_Curr_st_list.Clear();
            Q_Prev_st_list.Clear();
            Q_Prev_yr_st_list.Clear();
            Q_Station_st_list.Clear();
            Q_Curr_m_list.Clear();
            Q_Prev_m_list.Clear();
            Q_Prev_yr_m_list.Clear();
            Q_Month_m_list.Clear();
            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");



            try
            {
                con.Open();
                if (filter_input == "1")
                    cmdText = "SELECT a6.*,b6.display_name FROM (SELECT IF(a5.st_id IS NULL,a5.StationID,a5.st_id) AS st_id_final,a5.Inj_Q1,a5.Inj_Q2,a5.Inj_Q3 FROM (SELECT * FROM (SELECT IF(a3.StationID IS NULL,a3.ID2,a3.StationID) AS st_id,a3.Inj_Q1,a3.Inj_Q2 FROM"
+ " (SELECT * FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr = 'I' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS a"
+ " LEFT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr = 'I' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2"
+ " UNION"
+ " SELECT * FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr = 'I' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS a"
+ " RIGHT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr = 'I' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2) AS a3) AS a4"
+ " LEFT JOIN(SELECT StationID, SUM(Inj_Q) AS Inj_Q3 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b3 ON a4.st_id = b3.StationID"
+ " UNION"
+ " SELECT* FROM(SELECT IF(a3.StationID IS NULL, a3.ID2, a3.StationID) AS st_id, a3.Inj_Q1,a3.Inj_Q2 FROM"
+ "  (SELECT* FROM (SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS a"
+ " LEFT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2"
+ " UNION"
+ " SELECT* FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS a"
+ " RIGHT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2) AS a3) AS a4"
+ " RIGHT JOIN(SELECT StationID, SUM(Inj_Q) AS Inj_Q3 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b3 ON a4.st_id = b3.StationID) AS a5) AS a6, station_master AS b6 WHERE a6.st_id_final = b6.SrNo";
                else if (filter_input == "2")
                    cmdText = "SELECT a6.*,b6.display_name FROM (SELECT IF(a5.st_id IS NULL,a5.StationID,a5.st_id) AS st_id_final,a5.Inj_Q1,a5.Inj_Q2,a5.Inj_Q3 FROM (SELECT * FROM (SELECT IF(a3.StationID IS NULL,a3.ID2,a3.StationID) AS st_id,a3.Inj_Q1,a3.Inj_Q2 FROM"
+ " (SELECT * FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr = 'II' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS a"
+ " LEFT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr = 'II' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2"
+ " UNION"
+ " SELECT * FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr = 'II' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS a"
+ " RIGHT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr = 'II' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2) AS a3) AS a4"
+ " LEFT JOIN(SELECT StationID, SUM(Inj_Q) AS Inj_Q3 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b3 ON a4.st_id = b3.StationID"
+ " UNION"
+ " SELECT* FROM(SELECT IF(a3.StationID IS NULL, a3.ID2, a3.StationID) AS st_id, a3.Inj_Q1,a3.Inj_Q2 FROM"
+ "  (SELECT* FROM (SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS a"
+ " LEFT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2"
+ " UNION"
+ " SELECT* FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS a"
+ " RIGHT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2) AS a3) AS a4"
+ " RIGHT JOIN(SELECT StationID, SUM(Inj_Q) AS Inj_Q3 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b3 ON a4.st_id = b3.StationID) AS a5) AS a6, station_master AS b6 WHERE a6.st_id_final = b6.SrNo";
                else if (filter_input == "3")
                    cmdText = "SELECT a6.*,b6.display_name FROM (SELECT IF(a5.st_id IS NULL,a5.StationID,a5.st_id) AS st_id_final,a5.Inj_Q1,a5.Inj_Q2,a5.Inj_Q3 FROM (SELECT * FROM (SELECT IF(a3.StationID IS NULL,a3.ID2,a3.StationID) AS st_id,a3.Inj_Q1,a3.Inj_Q2 FROM"
+ " (SELECT * FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr = 'III' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS a"
+ " LEFT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr = 'III' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2"
+ " UNION"
+ " SELECT * FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr = 'III' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS a"
+ " RIGHT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr = 'III' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2) AS a3) AS a4"
+ " LEFT JOIN(SELECT StationID, SUM(Inj_Q) AS Inj_Q3 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b3 ON a4.st_id = b3.StationID"
+ " UNION"
+ " SELECT* FROM(SELECT IF(a3.StationID IS NULL, a3.ID2, a3.StationID) AS st_id, a3.Inj_Q1,a3.Inj_Q2 FROM"
+ "  (SELECT* FROM (SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS a"
+ " LEFT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2"
+ " UNION"
+ " SELECT* FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS a"
+ " RIGHT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2) AS a3) AS a4"
+ " RIGHT JOIN(SELECT StationID, SUM(Inj_Q) AS Inj_Q3 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b3 ON a4.st_id = b3.StationID) AS a5) AS a6, station_master AS b6 WHERE a6.st_id_final = b6.SrNo";
                else
                    cmdText = "SELECT a6.*,b6.display_name FROM (SELECT IF(a5.st_id IS NULL,a5.StationID,a5.st_id) AS st_id_final,a5.Inj_Q1,a5.Inj_Q2,a5.Inj_Q3 FROM (SELECT * FROM (SELECT IF(a3.StationID IS NULL,a3.ID2,a3.StationID) AS st_id,a3.Inj_Q1,a3.Inj_Q2 FROM"
+ " (SELECT * FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr = 'IV' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS a"
+ " LEFT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr = 'IV' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2"
+ " UNION"
+ " SELECT * FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr = 'IV' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS a"
+ " RIGHT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr = 'IV' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID, FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2) AS a3) AS a4"
+ " LEFT JOIN(SELECT StationID, SUM(Inj_Q) AS Inj_Q3 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b3 ON a4.st_id = b3.StationID"
+ " UNION"
+ " SELECT* FROM(SELECT IF(a3.StationID IS NULL, a3.ID2, a3.StationID) AS st_id, a3.Inj_Q1,a3.Inj_Q2 FROM"
+ "  (SELECT* FROM (SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS a"
+ " LEFT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2"
+ " UNION"
+ " SELECT* FROM(SELECT StationID, SUM(Inj_Q) AS Inj_Q1 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS a"
+ " RIGHT JOIN(SELECT StationID AS ID2, SUM(Inj_Q) AS Inj_Q2 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b"
+ " ON a.StationID = b.ID2) AS a3) AS a4"
+ " RIGHT JOIN(SELECT StationID, SUM(Inj_Q) AS Inj_Q3 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN (SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID,FY ORDER BY StationID) AS b3 ON a4.st_id = b3.StationID) AS a5) AS a6, station_master AS b6 WHERE a6.st_id_final = b6.SrNo";

                /*cmdText = "SELECT * FROM (SELECT b.display_name AS name_Q,SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr='IV' AND a.StationID=b.SrNo AND a.FY IN (SELECT * FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " LEFT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ";
                */

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[1].ToString() == "")
                            Q_Curr_st_list.Add(Convert.ToDecimal("0.0"));
                        else
                            Q_Curr_st_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[1].ToString()));

                        if (dt.Rows[i].ItemArray[3].ToString() == "")
                            Q_Prev_st_list.Add(Convert.ToDecimal("0.0"));
                        else
                            Q_Prev_st_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[3].ToString()));

                        if (dt.Rows[i].ItemArray[2].ToString() == "")
                            Q_Prev_yr_st_list.Add(Convert.ToDecimal("0.0"));
                        else
                            Q_Prev_yr_st_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[2].ToString()));

                        Q_Station_st_list.Add(dt.Rows[i].ItemArray[4].ToString());
                    }

                    

                    //DataView dv = dt.DefaultView;
                    //dv.Sort = "name_Q";

                    //testgrid.Visible = true;
                    testgrid.DataSource = dt;
                    testgrid.DataBind();
                    testgrid.HeaderRow.TableSection = TableRowSection.TableHeader;


                }
                else
                {
                    ltrChart.Text = "No data Found";
                    testgrid.Visible = false;
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





            try
            {
                con.Open();
                if (filter_input == "1")
                    cmdText = "SELECT * FROM"
+ " (SELECT Del_month, SUM(Inj_Q) AS Q1,FY AS FY1 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY Del_month_no) AS a"
+ " LEFT JOIN"
+ " (SELECT Del_month AS py, SUM(Inj_Q) AS Q2,FY AS FY2 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY Del_month_no) AS b"
+ " ON a.Del_month= b.py"
+ " UNION"
+ " SELECT* FROM"
+ " (SELECT Del_month, SUM(Inj_Q) AS Q1,FY AS FY1 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY Del_month_no) AS a"
+ " RIGHT JOIN"
+ " (SELECT Del_month AS py, SUM(Inj_Q) AS Q2,FY AS FY2 FROM mkt_q_master WHERE Qtr= 'I' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'I' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY Del_month_no) AS b"
+ " ON a.Del_month= b.py";
                else if (filter_input == "2")
                    cmdText = "SELECT * FROM"
+ " (SELECT Del_month, SUM(Inj_Q) AS Q1,FY AS FY1 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY Del_month_no) AS a"
+ " LEFT JOIN"
+ " (SELECT Del_month AS py, SUM(Inj_Q) AS Q2,FY AS FY2 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY Del_month_no) AS b"
+ " ON a.Del_month= b.py"
+ " UNION"
+ " SELECT* FROM"
+ " (SELECT Del_month, SUM(Inj_Q) AS Q1,FY AS FY1 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY Del_month_no) AS a"
+ " RIGHT JOIN"
+ " (SELECT Del_month AS py, SUM(Inj_Q) AS Q2 ,FY AS FY2 FROM mkt_q_master WHERE Qtr= 'II' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'II' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY Del_month_no) AS b"
+ " ON a.Del_month= b.py";
                else if (filter_input == "3")
                    cmdText = "SELECT * FROM"
+ " (SELECT Del_month, SUM(Inj_Q) AS Q1,FY AS FY1 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY Del_month_no) AS a"
+ " LEFT JOIN"
+ " (SELECT Del_month AS py, SUM(Inj_Q) AS Q2,FY AS FY2 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY Del_month_no) AS b"
+ " ON a.Del_month= b.py"
+ " UNION"
+ " SELECT* FROM"
+ " (SELECT Del_month, SUM(Inj_Q) AS Q1,FY AS FY1 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY Del_month_no) AS a"
+ " RIGHT JOIN"
+ " (SELECT Del_month AS py, SUM(Inj_Q) AS Q2,FY AS FY2 FROM mkt_q_master WHERE Qtr= 'III' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'III' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY Del_month_no) AS b"
+ " ON a.Del_month= b.py";
                else
                    cmdText = "SELECT * FROM"
+ " (SELECT Del_month, SUM(Inj_Q) AS Q1,FY AS FY1 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY Del_month_no) AS a"
+ " LEFT JOIN"
+ " (SELECT Del_month AS py, SUM(Inj_Q) AS Q2,FY AS FY2 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY Del_month_no) AS b"
+ " ON a.Del_month= b.py"
+ " UNION"
+ " SELECT* FROM"
+ " (SELECT Del_month, SUM(Inj_Q) AS Q1,FY AS FY1 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY Del_month_no) AS a"
+ " RIGHT JOIN"
+ " (SELECT Del_month AS py, SUM(Inj_Q) AS Q2,FY AS FY2 FROM mkt_q_master WHERE Qtr= 'IV' AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr = 'IV' ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY Del_month_no) AS b"
+ " ON a.Del_month= b.py";

                /*cmdText = "SELECT * FROM (SELECT b.display_name AS name_Q,SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr='IV' AND a.StationID=b.SrNo AND a.FY IN (SELECT * FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " LEFT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ";
                */

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);



       // public string Data_Q_fy
       // public string Data_Q_py
       // public string Data_Q_pq
                if (dt1.Rows.Count > 0)
                {

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i].ItemArray[1].ToString() == "")
                            Q_Curr_m_list.Add(Convert.ToDecimal("0.0"));
                        else
                            Q_Curr_m_list.Add(Convert.ToDecimal(dt1.Rows[i].ItemArray[1].ToString()));

                        if (dt1.Rows[i].ItemArray[4].ToString() == "")
                            Q_Prev_m_list.Add(Convert.ToDecimal("0.0"));
                        else
                            Q_Prev_m_list.Add(Convert.ToDecimal(dt1.Rows[i].ItemArray[4].ToString()));


                        Q_Month_m_list.Add(dt1.Rows[i].ItemArray[0].ToString());
                    }

                    if (filter_input == "1")
                    {
                        Data_Q_fy = "Qtr I (" + dt1.Rows[0].ItemArray[2].ToString() + ")";
                        Data_Q_py = "Qtr I (" + dt1.Rows[0].ItemArray[5].ToString() + ")";
                        Data_Q_pq = "Qtr IV (" + dt1.Rows[0].ItemArray[5].ToString() + ")";
                    }
                    else if (filter_input == "2")
                    {
                        Data_Q_fy = "Qtr II (" + dt1.Rows[0].ItemArray[2].ToString() + ")";
                        Data_Q_py = "Qtr II (" + dt1.Rows[0].ItemArray[5].ToString() + ")";
                        Data_Q_pq = "Qtr I (" + dt1.Rows[0].ItemArray[2].ToString() + ")";
                    }
                    else if (filter_input == "3")
                    {
                        Data_Q_fy = "Qtr III (" + dt1.Rows[0].ItemArray[2].ToString() + ")";
                        Data_Q_py = "Qtr III (" + dt1.Rows[0].ItemArray[5].ToString() + ")";
                        Data_Q_pq = "Qtr II (" + dt1.Rows[0].ItemArray[2].ToString() + ")";
                    }
                    else 
                    {
                        Data_Q_fy = "Qtr IV (" + dt1.Rows[0].ItemArray[2].ToString() + ")";
                        Data_Q_py = "Qtr IV (" + dt1.Rows[0].ItemArray[5].ToString() + ")";
                        Data_Q_pq = "Qtr III (" + dt1.Rows[0].ItemArray[2].ToString() + ")";
                    }

                    //DataView dv = dt1.DefaultView;
                    //dv.Sort = "name_Q";

                    //testgrid.Visible = true;
                    testgrid2.DataSource = dt1;
                    testgrid2.DataBind();
                    testgrid2.HeaderRow.TableSection = TableRowSection.TableHeader;


                }
                else
                {
                    ltrChart.Text = "No data Found";
                    testgrid2.Visible = false;
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


            JavaScriptSerializer jss1 = new JavaScriptSerializer();
            Data_Q_Station_st = jss1.Serialize(Q_Station_st_list);

            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            Data_Q_Curr_st = jss2.Serialize(Q_Curr_st_list);

            JavaScriptSerializer jss3 = new JavaScriptSerializer();
            Data_Q_Prev_st = jss3.Serialize(Q_Prev_st_list);

            JavaScriptSerializer jss4 = new JavaScriptSerializer();
            Data_Q_Prev_yr_st = jss4.Serialize(Q_Prev_yr_st_list);

            JavaScriptSerializer jss5 = new JavaScriptSerializer();
            Data_Q_Curr_m = jss5.Serialize(Q_Curr_m_list);

            JavaScriptSerializer jss6 = new JavaScriptSerializer();
            Data_Q_Prev_m = jss6.Serialize(Q_Prev_m_list);

            JavaScriptSerializer jss7 = new JavaScriptSerializer();
            Data_Q_Month_m = jss7.Serialize(Q_Month_m_list);




        }

        protected void generate_m_chart(string filter_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            DataTable dt1 = new DataTable();

            m_Curr_m_list.Clear();
            m_Prev_yr_m_list.Clear();
            m_Month_m_list.Clear();
            m_fy_fy_list.Clear();
            m_Qtm_fy_list.Clear();


            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");
            //Label4.Text = "1111 --";

            try
            {
                con.Open();
                cmdText = "SELECT IF(a2.st1 IS NULL, a2.st2, a2.st1) AS st_id_final, a2.Q1,a2.Q2,b2.display_name FROM(SELECT* FROM"
+ " (SELECT StationID AS st1, SUM(Inj_Q) AS Q1 FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID) AS a"
+ " LEFT JOIN"
+ " (SELECT StationID AS st2, SUM(Inj_Q) AS Q2 FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID) AS b"
+ " ON a.st1 = b.st2"
+ " UNION"
+ " SELECT* FROM"
+ " (SELECT StationID AS st1, SUM(Inj_Q) AS Q1 FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID) AS a"
+ " RIGHT JOIN"
+ " (SELECT StationID AS st2, SUM(Inj_Q) AS Q2 FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID) AS b"
+ " ON a.st1 = b.st2) AS a2, station_master AS b2 WHERE(a2.st1 = b2.SrNo OR a2.st2 = b2.SrNo) ORDER BY st_id_final";

                /*cmdText = "SELECT * FROM (SELECT b.display_name AS name_Q,SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr='IV' AND a.StationID=b.SrNo AND a.FY IN (SELECT * FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " LEFT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ";
                */

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[1].ToString() == "")
                            m_Curr_m_list.Add(Convert.ToDecimal("0.0"));
                        else
                            m_Curr_m_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[1].ToString()));

                        if (dt.Rows[i].ItemArray[2].ToString() == "")
                            m_Prev_yr_m_list.Add(Convert.ToDecimal("0.0"));
                        else
                            m_Prev_yr_m_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[2].ToString()));

                        m_Month_m_list.Add(dt.Rows[i].ItemArray[3].ToString());
                    }



                    //DataView dv = dt.DefaultView;
                    //dv.Sort = "name_Q";

                    //testgrid.Visible = true;
                    testgrid3.DataSource = dt;
                    testgrid3.DataBind();
                    testgrid3.HeaderRow.TableSection = TableRowSection.TableHeader;


                }
                else
                {
                    ltrChart.Text = "No data Found";
                    testgrid3.Visible = false;
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





            try
            {
                con.Open();
                cmdText = "SELECT FY, SUM(Inj_Q) AS Q1 FROM mkt_q_master WHERE Del_month_no=" + Convert.ToInt32(Month_list.SelectedValue) + " GROUP BY FY ORDER BY FY DESC";

                /*cmdText = "SELECT * FROM (SELECT b.display_name AS name_Q,SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr='IV' AND a.StationID=b.SrNo AND a.FY IN (SELECT * FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " LEFT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ";
                */

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);



       //Data_m_sel_m
        //Data_m_sel_m_fy
         //Data_m_sel_m_py
                if (dt1.Rows.Count > 0)
                {

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i].ItemArray[1].ToString() == "")
                            m_Qtm_fy_list.Add(Convert.ToDecimal("0.0"));
                        else
                            m_Qtm_fy_list.Add(Convert.ToDecimal(dt1.Rows[i].ItemArray[1].ToString()));

                        m_fy_fy_list.Add(dt1.Rows[i].ItemArray[0].ToString());
                    }


                    Data_m_sel_m = Month_list.SelectedItem.Text;
                    Data_m_sel_m_fy = Month_list.SelectedItem.Text + "(" + dt1.Rows[0].ItemArray[0].ToString() + ")";
                    Data_m_sel_m_py = Month_list.SelectedItem.Text + "(" + dt1.Rows[1].ItemArray[0].ToString() + ")";

                    //DataView dv = dt1.DefaultView;
                    //dv.Sort = "name_Q";

                    //testgrid.Visible = true;
                    testgrid4.DataSource = dt1;
                    testgrid4.DataBind();
                    testgrid4.HeaderRow.TableSection = TableRowSection.TableHeader;


                }
                else
                {
                    ltrChart.Text = "No data Found";
                    testgrid4.Visible = false;
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






            JavaScriptSerializer jss1 = new JavaScriptSerializer();
            Data_m_Curr_m = jss1.Serialize(m_Curr_m_list);

            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            Data_m_Prev_yr_m = jss2.Serialize(m_Prev_yr_m_list);

            JavaScriptSerializer jss3 = new JavaScriptSerializer();
            Data_m_Month_m = jss3.Serialize(m_Month_m_list);

            JavaScriptSerializer jss4 = new JavaScriptSerializer();
            Data_m_Qtm_fy = jss4.Serialize(m_Qtm_fy_list);

            JavaScriptSerializer jss5 = new JavaScriptSerializer();
            Data_m_fy_fy = jss5.Serialize(m_fy_fy_list);




        }

        protected void generate_y_chart(string filter_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            DataTable dt1 = new DataTable();

            m_Curr_m_list.Clear();
            m_Prev_yr_m_list.Clear();
            m_Month_m_list.Clear();
            m_fy_fy_list.Clear();
            m_Qtm_fy_list.Clear();


            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");
            //Label4.Text = "1111 --";

            try
            {
                con.Open();
                cmdText = "SELECT IF(a2.st1 IS NULL, a2.st2, a2.st1) AS st_id_final, a2.Q1,a2.Q2,b2.display_name FROM(SELECT* FROM"
+ " (SELECT StationID AS st1, SUM(Inj_Q) AS Q1 FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID) AS a"
+ " LEFT JOIN"
+ " (SELECT StationID AS st2, SUM(Inj_Q) AS Q2 FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID) AS b"
+ " ON a.st1 = b.st2"
+ " UNION"
+ " SELECT* FROM"
+ " (SELECT StationID AS st1, SUM(Inj_Q) AS Q1 FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " ORDER BY FY DESC LIMIT 1) AS t) GROUP BY StationID) AS a"
+ " RIGHT JOIN"
+ " (SELECT StationID AS st2, SUM(Inj_Q) AS Q2 FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " AND FY IN(SELECT * FROM(SELECT DISTINCT FY FROM mkt_q_master WHERE Del_month_no= " + Convert.ToInt32(Month_list.SelectedValue) + " ORDER BY FY DESC LIMIT 1, 1) AS t) GROUP BY StationID) AS b"
+ " ON a.st1 = b.st2) AS a2, station_master AS b2 WHERE(a2.st1 = b2.SrNo OR a2.st2 = b2.SrNo) ORDER BY st_id_final";

                /*cmdText = "SELECT * FROM (SELECT b.display_name AS name_Q,SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr='IV' AND a.StationID=b.SrNo AND a.FY IN (SELECT * FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " LEFT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ";
                */

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[1].ToString() == "")
                            m_Curr_m_list.Add(Convert.ToDecimal("0.0"));
                        else
                            m_Curr_m_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[1].ToString()));

                        if (dt.Rows[i].ItemArray[2].ToString() == "")
                            m_Prev_yr_m_list.Add(Convert.ToDecimal("0.0"));
                        else
                            m_Prev_yr_m_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[2].ToString()));

                        m_Month_m_list.Add(dt.Rows[i].ItemArray[3].ToString());
                    }



                    //DataView dv = dt.DefaultView;
                    //dv.Sort = "name_Q";

                    //testgrid.Visible = true;
                    testgrid3.DataSource = dt;
                    testgrid3.DataBind();
                    testgrid3.HeaderRow.TableSection = TableRowSection.TableHeader;


                }
                else
                {
                    ltrChart.Text = "No data Found";
                    testgrid3.Visible = false;
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





            try
            {
                con.Open();
                cmdText = "SELECT FY, SUM(Inj_Q) AS Q1 FROM mkt_q_master WHERE Del_month_no=" + Convert.ToInt32(Month_list.SelectedValue) + " GROUP BY FY ORDER BY FY DESC";

                /*cmdText = "SELECT * FROM (SELECT b.display_name AS name_Q,SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr='IV' AND a.StationID=b.SrNo AND a.FY IN (SELECT * FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " LEFT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " LEFT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ"
                        + " UNION"
                        + " SELECT* FROM(SELECT b.display_name AS name_Q, SUM(a.Inj_Q) AS inj_Q FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS aa"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PY, SUM(a.Inj_Q) AS inj_PY FROM mkt_q_master a,station_master b WHERE a.Qtr = 'IV' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='IV' ORDER BY FY DESC LIMIT 1,1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS bb ON aa.name_Q = bb.name_PY"
                        + " RIGHT JOIN(SELECT b.display_name AS name_PQ, SUM(a.Inj_Q) AS inj_PQ FROM mkt_q_master a,station_master b WHERE a.Qtr = 'III' AND a.StationID = b.SrNo AND a.FY IN(SELECT* FROM (SELECT DISTINCT FY FROM mkt_q_master WHERE Qtr='III' ORDER BY FY DESC LIMIT 1) AS t) GROUP BY a.StationID,a.FY ORDER BY b.display_name) AS cc ON bb.name_PY = cc.name_PQ";
                */

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);



                //Data_m_sel_m
                //Data_m_sel_m_fy
                //Data_m_sel_m_py
                if (dt1.Rows.Count > 0)
                {

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (dt1.Rows[i].ItemArray[1].ToString() == "")
                            m_Qtm_fy_list.Add(Convert.ToDecimal("0.0"));
                        else
                            m_Qtm_fy_list.Add(Convert.ToDecimal(dt1.Rows[i].ItemArray[1].ToString()));

                        m_fy_fy_list.Add(dt1.Rows[i].ItemArray[0].ToString());
                    }


                    Data_m_sel_m = Month_list.SelectedItem.Text;
                    Data_m_sel_m_fy = Month_list.SelectedItem.Text + "(" + dt1.Rows[0].ItemArray[0].ToString() + ")";
                    Data_m_sel_m_py = Month_list.SelectedItem.Text + "(" + dt1.Rows[1].ItemArray[0].ToString() + ")";

                    //DataView dv = dt1.DefaultView;
                    //dv.Sort = "name_Q";

                    //testgrid.Visible = true;
                    testgrid4.DataSource = dt1;
                    testgrid4.DataBind();
                    testgrid4.HeaderRow.TableSection = TableRowSection.TableHeader;


                }
                else
                {
                    ltrChart.Text = "No data Found";
                    testgrid4.Visible = false;
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






            JavaScriptSerializer jss1 = new JavaScriptSerializer();
            Data_m_Curr_m = jss1.Serialize(m_Curr_m_list);

            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            Data_m_Prev_yr_m = jss2.Serialize(m_Prev_yr_m_list);

            JavaScriptSerializer jss3 = new JavaScriptSerializer();
            Data_m_Month_m = jss3.Serialize(m_Month_m_list);

            JavaScriptSerializer jss4 = new JavaScriptSerializer();
            Data_m_Qtm_fy = jss4.Serialize(m_Qtm_fy_list);

            JavaScriptSerializer jss5 = new JavaScriptSerializer();
            Data_m_fy_fy = jss5.Serialize(m_fy_fy_list);




        }
    }
}