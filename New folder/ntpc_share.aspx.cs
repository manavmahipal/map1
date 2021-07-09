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
    public partial class ntpc_share : System.Web.UI.Page
    {
        public string chartData1
        {
            get;
            set;
        }

        public string chartData2
        {
            get;
            set;
        }

        public string chartData3
        {
            get;
            set;
        }
        public string chartData4
        {
            get;
            set;
        }

        public string chartData5
        {
            get;
            set;
        }
        public string chartData6
        {
            get;
            set;
        }

        List<decimal> ntpc_billedlist = new List<decimal>();
        List<string> xAxislist = new List<string>();
        List<decimal> CEA_conslist = new List<decimal>();
        List<decimal> ntpc_sharelist = new List<decimal>();
        List<decimal> ntpc_sharelist1 = new List<decimal>();
        List<decimal> ntpc_sharelist2 = new List<decimal>();
        //string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
        string str = ConfigurationManager.ConnectionStrings["commercial_mySQL"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Literal3.Text = "<br/>" + str + "<br/>" + str;
                populate_fy();
                populate_state();
                DDL_for_fy.Visible = true;
                DDL_for_state.Visible = false;
                Label2.Text = "Select FY :  ";
            }
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void populate_fy()
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string cmdText = "SELECT DISTINCT FY FROM ntpc_share ORDER BY FY DESC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);



                if (dt.Rows.Count > 0)
                {
                    DDL_for_fy.DataSource = dt;
                    DDL_for_fy.DataBind();
                    DDL_for_fy.SelectedIndex = 0;
                    //populate_month(DDL_for_state.SelectedValue.ToString());
                    generate_table_chart("0", dt.Rows[0].ItemArray[0].ToString());
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
        protected void fy_DDL_change(object sender, EventArgs e)
        {
            generate_table_chart("0", DDL_for_fy.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            //generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }
        protected void state_DDL_change(object sender, EventArgs e)
        {
            generate_table_chart("1", DDL_for_state.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            //generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }
        protected void filter_DDL_change(object sender, EventArgs e)
        {
            if (DDL_for_filter.SelectedValue == "0")
            {
                DDL_for_fy.Visible = true;
                DDL_for_state.Visible = false;
                Label2.Text = "Select FY :  ";
                generate_table_chart("0", DDL_for_fy.SelectedValue);
            }
            else if (DDL_for_filter.SelectedValue == "1")
            {
                DDL_for_fy.Visible = false;
                DDL_for_state.Visible = true;
                Label2.Text = "Select State :  ";
                generate_table_chart("1", DDL_for_state.SelectedValue);
            }

            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            //generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }

        protected void generate_table_chart(string filter_input, string sub_filter_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            ntpc_billedlist.Clear();
            xAxislist.Clear();
            CEA_conslist.Clear();
            ntpc_sharelist.Clear();
            ntpc_sharelist1.Clear();
            ntpc_sharelist2.Clear();
            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");



            try
            {
                con.Open();
                if (filter_input == "0")
                {

                    //Literal1.Text += "<br/><br/>" + get_FY();
                    string[] FYlist = get_FY().Split('~');

                    for(int i=0;i<FYlist.Length;i++)
                    {
                        //Literal1.Text += "<br/><br/>"+i+"-" +FYlist[i];
                        if (FYlist[i] == " ")
                            FYlist[i] = "";
                    }

                    //Literal1.Text += "<br/><br/>" + get_FY();

                    cmdText = "SELECT c3.CEA,c3.NTPC, c3.c2_share0 AS share0,c3.c2_FY AS FY,s1.StateName AS state,c3.c2_share1 AS share1,c3.c2_share2 AS share2 FROM"
+ " (SELECT IF(c2.a1_state IS NULL, c2.c_state, c2.a1_state) AS c_state, c2.CEA, c2.NTPC, c2.a1_share AS c2_share0, c2.c_share1 AS c2_share1, c2.c_share2 AS c2_share2, c2.a1_FY AS c2_FY FROM"
+ " (SELECT * FROM"
+ " (SELECT ROUND(energy_cons_CEA_MU, 0) AS CEA, ROUND(NTPC_billed_MU, 0) AS NTPC, ROUND(NTPC_share_pc * 100, 1) AS a1_share, StateID AS a1_state, FY AS a1_FY FROM ntpc_share WHERE FY = '"+FYlist[1]+"' ORDER BY a1_share) AS a1"
+ " LEFT JOIN"
+ " (SELECT IF(c.a_state IS NULL, c.b_state, c.a_state) AS c_state, c.a_share AS c_share1, c.b_share AS c_share2 FROM"
+ " (SELECT * FROM"
+ " (SELECT ROUND(NTPC_share_pc * 100, 1) AS a_share, StateID AS a_state FROM ntpc_share WHERE FY = '" + FYlist[2] + "' ORDER BY a_share) AS a"
+ " LEFT JOIN"
+ " (SELECT ROUND(NTPC_share_pc * 100, 1) AS b_share, StateID AS b_state FROM ntpc_share WHERE FY = '" + FYlist[3] + "' ORDER BY b_share) AS b"
+ " ON a.a_state = b.b_state"
+ " UNION"
+ " SELECT * FROM"
+ " (SELECT ROUND(NTPC_share_pc * 100, 1) AS a_share, StateID AS a_state FROM ntpc_share WHERE FY = '" + FYlist[2] + "' ORDER BY a_share) AS a"
+ " RIGHT JOIN"
+ " (SELECT ROUND(NTPC_share_pc * 100, 1) AS b_share, StateID AS b_state FROM ntpc_share WHERE FY = '" + FYlist[3] + "' ORDER BY b_share) AS b"
+ " ON a.a_state = b.b_state) AS c) AS b1"
+ " ON a1.a1_state = b1.c_state"
+ " UNION"
+ " SELECT * FROM"
+ " (SELECT ROUND(energy_cons_CEA_MU, 0) AS CEA, ROUND(NTPC_billed_MU, 0) AS NTPC, ROUND(NTPC_share_pc * 100, 1) AS a1_share, StateID AS a1_state, FY AS a1_FY FROM ntpc_share WHERE FY = '" + FYlist[1] + "' ORDER BY a1_share) AS a1"
+ " RIGHT JOIN"
+ " (SELECT IF(c.a_state IS NULL, c.b_state, c.a_state) AS c_state, c.a_share AS c_share1, c.b_share AS c_share2 FROM"
+ " (SELECT * FROM"
+ " (SELECT ROUND(NTPC_share_pc * 100, 1) AS a_share, StateID AS a_state FROM ntpc_share WHERE FY = '" + FYlist[2] + "' ORDER BY a_share) AS a"
+ " LEFT JOIN"
+ " (SELECT ROUND(NTPC_share_pc * 100, 1) AS b_share, StateID AS b_state FROM ntpc_share WHERE FY = '" + FYlist[3] + "' ORDER BY b_share) AS b"
+ " ON a.a_state = b.b_state"
+ " UNION"
+ " SELECT * FROM"
+ " (SELECT ROUND(NTPC_share_pc * 100, 1) AS a_share, StateID AS a_state FROM ntpc_share WHERE FY = '" + FYlist[2] + "' ORDER BY a_share) AS a"
+ " RIGHT JOIN"
+ " (SELECT ROUND(NTPC_share_pc * 100, 1) AS b_share, StateID AS b_state FROM ntpc_share WHERE FY = '" + FYlist[3] + "' ORDER BY b_share) AS b"
+ " ON a.a_state = b.b_state) AS c) AS b1"
+ " ON a1.a1_state = b1.c_state) AS c2) AS c3, state_master as s1 WHERE c3.c_state = s1.SrNo ORDER BY c3.c2_share0";
                }
                else
                    cmdText = "SELECT ROUND(a.energy_cons_CEA_MU,0) AS CEA,ROUND(a.NTPC_billed_MU,0) AS NTPC,ROUND(a.NTPC_share_pc*100,1) AS share0,a.FY As FY,b.StateName AS state FROM ntpc_share a, state_master b WHERE a.StateID=b.SrNo AND a.StateID=" + Convert.ToInt32(DDL_for_state.SelectedItem.Value.ToString()) + " ORDER BY FY DESC";
                
                /*if (filter_input == "0")
                    cmdText = "SELECT ROUND(a.energy_cons_CEA_MU,0) AS CEA,ROUND(a.NTPC_billed_MU,0) AS NTPC,ROUND(a.NTPC_share_pc*100,1) AS share0,a.FY AS FY,b.StateName AS state FROM ntpc_share a, state_master b WHERE a.StateID=b.SrNo AND FY='" + DDL_for_fy.SelectedValue + "' ORDER BY share0";
                else
                    cmdText = "SELECT ROUND(a.energy_cons_CEA_MU,0) AS CEA,ROUND(a.NTPC_billed_MU,0) AS NTPC,ROUND(a.NTPC_share_pc*100,1) AS share0,a.FY As FY,b.StateName AS state FROM ntpc_share a, state_master b WHERE a.StateID=b.SrNo AND a.StateID=" + Convert.ToInt32(DDL_for_state.SelectedItem.Value.ToString()) + " ORDER BY FY DESC";
                */
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (filter_input == "0")
                            xAxislist.Add(dt.Rows[i].ItemArray[4].ToString());
                        else
                            xAxislist.Add(dt.Rows[i].ItemArray[3].ToString());

                        ntpc_billedlist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[1].ToString()));
                        CEA_conslist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[0].ToString()));

                        if (dt.Rows[i].ItemArray[2].ToString() == "")
                            ntpc_sharelist.Add(Convert.ToDecimal("0.0"));
                        else
                            ntpc_sharelist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[2].ToString()));

                        if (filter_input == "0")
                        {
                            if (dt.Rows[i].ItemArray[5].ToString() == "")
                                ntpc_sharelist1.Add(Convert.ToDecimal("0.0"));
                            else
                                ntpc_sharelist1.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[5].ToString()));

                            if (dt.Rows[i].ItemArray[6].ToString() == "")
                                ntpc_sharelist2.Add(Convert.ToDecimal("0.0"));
                            else
                                ntpc_sharelist2.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[6].ToString()));

                            container.Visible = true;
                            container1.Visible = false;
                        }
                        else
                        {

                            container.Visible = false;
                            container1.Visible = true;

                            ntpc_sharelist1.Add(Convert.ToDecimal("0.0"));

                            
                                ntpc_sharelist2.Add(Convert.ToDecimal("0.0"));
                        }


                        //Literal1.Text += "<br/><br/>" + dt.Rows[i].ItemArray[5].ToString();
                        //ntpc_sharelist1.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[5].ToString()));
                        //ntpc_sharelist2.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[6].ToString()));
                        //statelist.Add(dt.Rows[i].ItemArray[3].ToString());
                    }
                    testgrid.Visible = true;
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


            JavaScriptSerializer jss1 = new JavaScriptSerializer();
            chartData1 = jss1.Serialize(xAxislist);

            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            chartData2 = jss2.Serialize(ntpc_sharelist);

            JavaScriptSerializer jss3 = new JavaScriptSerializer();
            chartData3 = jss3.Serialize(CEA_conslist);

            JavaScriptSerializer jss4 = new JavaScriptSerializer();
            chartData4 = jss4.Serialize(ntpc_billedlist);

            JavaScriptSerializer jss5 = new JavaScriptSerializer();
            chartData5 = jss5.Serialize(ntpc_sharelist1);

            JavaScriptSerializer jss6 = new JavaScriptSerializer();
            chartData6 = jss6.Serialize(ntpc_sharelist2);

            //Literal1.Text += "<br/><br/>" + chartData1;
            //Literal1.Text += "<br/><br/>" + chartData2;
            //Literal1.Text += "<br/><br/>" + chartData3;
            //Literal1.Text += "<br/><br/>" + chartData4;

        }
        protected string get_FY()
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            string result = "";
            try
            {
                con.Open();
                string cmdText = "SELECT DISTINCT FY FROM ntpc_share WHERE FY<'" + DDL_for_fy.SelectedValue + "' OR FY='" + DDL_for_fy.SelectedValue + "' ORDER BY FY DESC LIMIT 3";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);



                if (dt.Rows.Count == 3)
                {
                    result += "~" + dt.Rows[0].ItemArray[0].ToString() + "~" + dt.Rows[1].ItemArray[0].ToString() + "~" + dt.Rows[2].ItemArray[0].ToString();
                }
                else if (dt.Rows.Count == 2)
                {
                    result += "~" + dt.Rows[0].ItemArray[0].ToString() + "~" + dt.Rows[1].ItemArray[0].ToString() + "~ ";
                }
                else if (dt.Rows.Count == 1)
                {
                    result += "~" + dt.Rows[0].ItemArray[0].ToString() + "~ ~ ";
                }
                else
                {
                    result += "~ ~ ~ ";
                    ltrChart.Text = "No FY Found";
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
            return result;
        }
    }
}