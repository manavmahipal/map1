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
using System.Drawing;

namespace merit
{
    public partial class meritindia_display : System.Web.UI.Page
    {
        public string Data_blk
        {
            get;
            set;
        }
        public string Data_blk1
        {
            get;
            set;
        }
        public string Data_blk2
        {
            get;
            set;
        }

        public string Data_Dem_met
        {
            get;
            set;
        }

        public string Data_T_gen
        {
            get;
            set;
        }
        public string Data_G_gen
        {
            get;
            set;
        }
        public string Data_N_gen
        {
            get;
            set;
        }

        public string Data_H_gen
        {
            get;
            set;
        }

        public string Data_R_gen
        {
            get;
            set;
        }
        public string Data_demand_st
        {
            get;
            set;
        }
        public string Data_isgs_st
        {
            get;
            set;
        }
        public string Data_import_st
        {
            get;
            set;
        }

        List<int> blk_list = new List<int>();
        List<int> blk1_list = new List<int>();
        List<string> table_list = new List<string>();
        List<string> table_list1 = new List<string>();
        List<decimal> Dem_met_list = new List<decimal>();
        List<decimal> T_gen_list = new List<decimal>();
        List<decimal> G_gen_list = new List<decimal>();
        List<decimal> N_gen_list = new List<decimal>();
        List<decimal> H_gen_list = new List<decimal>();
        List<decimal> R_gen_list = new List<decimal>();
        List<string> State_st_list = new List<string>();
        List<decimal> demand_list = new List<decimal>();
        List<decimal> isgs_list = new List<decimal>();
        List<decimal> import_list = new List<decimal>();
        string str = ConfigurationManager.ConnectionStrings["commercial_mySQL"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                populate_date_data();

                //generate_Q_chart(Qtr_list.SelectedValue);
                //generate_m_chart("1");
            }
            else
            {
                generate_All_chart();
                generate_State_chart();
                //generate_Q_chart(Qtr_list.SelectedValue);
                //generate_m_chart(Month_list.SelectedValue);
            }

        }

        protected void populate_date_data()
        {
            Date_Input.Text = DateTime.Now.ToString("yyyy-MM-dd");
            populate_ddl_state();

            generate_All_chart();
            generate_State_chart();

        }
        protected void populate_ddl_state()
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            DDL_for_state.Items.Clear();
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string cmdText = "SELECT c.statename AS StateName,a.state AS StateCode FROM meritindia_statedata a,meritindia_statecodes b,state_master c WHERE date_data='" + Date_Input.Text + "' AND a.state=b.statecode AND b.stateID=c.SrNo GROUP BY a.state ORDER BY b.stateID";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);



                if (dt.Rows.Count > 0)
                {
                    DDL_for_state.DataSource = dt;
                    DDL_for_state.DataBind();
                    DDL_for_state.SelectedIndex = 0;
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
        protected void Date_DDL_change(object sender, EventArgs e)
        {

            populate_ddl_state();
            generate_All_chart();
            generate_State_chart();
            //generate_table_chart(Date_Input.Text.ToString());
        }
        protected void State_DDL_change(object sender, EventArgs e)
        {

            generate_All_chart();
            generate_State_chart();
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void generate_All_chart()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            blk_list.Clear();
            Dem_met_list.Clear();
            T_gen_list.Clear();
            G_gen_list.Clear();
            N_gen_list.Clear();
            H_gen_list.Clear();
            R_gen_list.Clear();



            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");
            //Label4.Text = "1111 --";

            try
            {
                con.Open();
                cmdText = "SELECT Date_data,Blk,ROUND(AVG(NULLIF(Dem_met, 0)),0) AS Dem_met,ROUND(AVG(NULLIF(T_gen, 0)),0) AS T_gen,ROUND(AVG(NULLIF(G_gen, 0)),0) AS G_gen,ROUND(AVG(NULLIF(N_gen, 0)),0) AS N_gen,ROUND(AVG(NULLIF(H_gen, 0)),0) AS H_gen,ROUND(AVG(NULLIF(R_gen, 0)),0) AS R_gen FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data,Blk ORDER BY Blk";

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
                    testgrid.Visible = true;
                    testgrid1.Visible = true;
                    container.Visible = true;
                    container1.Visible = true;
                    target.Visible = true;
                    target1.Visible = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i].ItemArray[2].ToString() == "")
                            Dem_met_list.Add(Convert.ToDecimal("0"));
                        else
                            Dem_met_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[2].ToString()));

                        if (dt.Rows[i].ItemArray[3].ToString() == "")
                            T_gen_list.Add(Convert.ToDecimal("0"));
                        else
                            T_gen_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[3].ToString()));

                        if (dt.Rows[i].ItemArray[4].ToString() == "")
                            G_gen_list.Add(Convert.ToDecimal("0"));
                        else
                            G_gen_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[4].ToString()));

                        if (dt.Rows[i].ItemArray[5].ToString() == "")
                            N_gen_list.Add(Convert.ToDecimal("0"));
                        else
                            N_gen_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[5].ToString()));

                        if (dt.Rows[i].ItemArray[6].ToString() == "")
                            H_gen_list.Add(Convert.ToDecimal("0"));
                        else
                            H_gen_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[6].ToString()));

                        if (dt.Rows[i].ItemArray[7].ToString() == "")
                            R_gen_list.Add(Convert.ToDecimal("0"));
                        else
                            R_gen_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[7].ToString()));




                        blk_list.Add(Convert.ToInt32(dt.Rows[i].ItemArray[1].ToString()));
                    }



                    //DataView dv = dt.DefaultView;
                    //dv.Sort = "name_Q";

                    //testgrid.Visible = true;
                    testgrid.DataSource = dt;
                    testgrid.DataBind();
                    testgrid.HeaderRow.TableSection = TableRowSection.TableHeader;

                    Literal1.Text = "";
                }
                else
                {
                    Literal1.Text = "No data Found";
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
            Data_blk = jss1.Serialize(blk_list);
            //Literal1.Text += Data_blk;

            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            Data_Dem_met = jss2.Serialize(Dem_met_list);

            JavaScriptSerializer jss3 = new JavaScriptSerializer();
            Data_T_gen = jss3.Serialize(T_gen_list);

            JavaScriptSerializer jss4 = new JavaScriptSerializer();
            Data_G_gen = jss4.Serialize(G_gen_list);

            JavaScriptSerializer jss5 = new JavaScriptSerializer();
            Data_N_gen = jss5.Serialize(N_gen_list);

            JavaScriptSerializer jss6 = new JavaScriptSerializer();
            Data_H_gen = jss6.Serialize(H_gen_list);

            JavaScriptSerializer jss7 = new JavaScriptSerializer();
            Data_R_gen = jss7.Serialize(R_gen_list);


            table_list.Clear();
            generate_All_Dem_met_table();
            generate_All_T_gen_table();
            generate_All_G_gen_table();
            generate_All_N_gen_table();
            generate_All_H_gen_table();
            generate_All_R_gen_table();


            table_list1.Clear();
            generate_Max_Min_Dem_met_table();
            /*Literal2.Text += "<br/>" + table_list.Count;
            for (int i = 0; i < table_list.Count; i++)
            {
                Literal2.Text += "<br/>" + table_list[i];
            }*/

            if (table_list.Count == 30)
            {
                DataTable dt1 = new DataTable();
                dt1.Clear();
                dt1.Columns.Add("param", typeof(string));
                dt1.Columns.Add("time1", typeof(string));
                dt1.Columns.Add("max", typeof(string));
                dt1.Columns.Add("time2", typeof(string));
                dt1.Columns.Add("min", typeof(string));
                dt1.Columns.Add("avg", typeof(string));

                for (int i = 0; i < 30; i += 5)
                {
                    DataRow _roww = dt1.NewRow();
                    if (i < 1)
                        _roww["param"] = "Dem_met";
                    else if (i < 8)
                        _roww["param"] = "Coal";
                    else if (i < 12)
                        _roww["param"] = "Gas";
                    else if (i < 16)
                        _roww["param"] = "Nuclear";
                    else if (i < 22)
                        _roww["param"] = "Hydro";
                    else
                        _roww["param"] = "RE";





                    _roww["time1"] = table_list[i];
                    _roww["max"] = table_list[i + 1];
                    _roww["time2"] = table_list[i + 2];
                    _roww["min"] = table_list[i + 3];
                    _roww["avg"] = table_list[i + 4];
                    dt1.Rows.Add(_roww);
                }
                testgrid3.DataSource = dt1;
                testgrid3.DataBind();
                testgrid3.HeaderRow.TableSection = TableRowSection.TableHeader;



            }

           


            if (table_list1.Count == 14)
            {
                DataTable dt1 = new DataTable();
                dt1.Clear();
                dt1.Columns.Add("param", typeof(string));
                dt1.Columns.Add("Dem_met", typeof(string));
                dt1.Columns.Add("Coal", typeof(string));
                dt1.Columns.Add("Gas", typeof(string));
                dt1.Columns.Add("Nuclear", typeof(string));
                dt1.Columns.Add("Hydro", typeof(string));
                dt1.Columns.Add("RE", typeof(string));

                for (int i = 0; i < 14; i++)
                {
                    DataRow _roww = dt1.NewRow();
                    if (i < 6)
                    {
                        _roww["param"] = "MAX";
                        _roww["Dem_met"] = table_list1[i];
                        _roww["Coal"] = table_list1[i+1];
                        _roww["Gas"] = table_list1[i + 2];
                        _roww["Nuclear"] = table_list1[i + 3];
                        _roww["Hydro"] = table_list1[i + 4];
                        _roww["RE"] = table_list1[i + 5];
                        i = i + 6;

                    }
                    else
                    {
                        _roww["param"] = "MIN";
                        _roww["Dem_met"] = table_list1[i];
                        _roww["Coal"] = table_list1[i + 1];
                        _roww["Gas"] = table_list1[i + 2];
                        _roww["Nuclear"] = table_list1[i + 3];
                        _roww["Hydro"] = table_list1[i + 4];
                        _roww["RE"] = table_list1[i + 5];
                        i = i + 6;
                    }




                    dt1.Rows.Add(_roww);
                }


                testgrid4.DataSource = dt1;
                testgrid4.DataBind();
                testgrid4.HeaderRow.TableSection = TableRowSection.TableHeader;

            }


        }
        protected void generate_State_chart()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            DataTable dt1 = new DataTable();

            blk1_list.Clear();
            demand_list.Clear();
            isgs_list.Clear();
            import_list.Clear();



            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");
            //Label4.Text = "1111 --";

            try
            {
                con.Open();
                cmdText = "SELECT blk,ROUND(AVG(NULLIF(demand, 0)),0) AS Demand,ROUND(AVG(NULLIF(isgs, 0)),0) AS ISGS,ROUND(AVG(NULLIF(import, 0)),0) AS Import_Q FROM meritindia_statedata WHERE date_data='" + Date_Input.Text + "' AND state = '" + DDL_for_state.SelectedItem.Value + "' GROUP BY blk ORDER By blk";

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

                // Literal2.Text += "ffffff";
                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    testgrid.Visible = true;
                    testgrid1.Visible = true;
                    container.Visible = true;
                    container1.Visible = true;
                    target.Visible = true;
                    target1.Visible = true;
                    //Literal1.Text += "asdasdsada";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[1].ToString() == "")
                            demand_list.Add(Convert.ToDecimal("0"));
                        else
                            demand_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[1].ToString()));

                        if (dt.Rows[i].ItemArray[2].ToString() == "")
                            isgs_list.Add(Convert.ToDecimal("0"));
                        else
                            isgs_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[2].ToString()));

                        if (dt.Rows[i].ItemArray[3].ToString() == "")
                            import_list.Add(Convert.ToDecimal("0"));
                        else
                            import_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[3].ToString()));


                        blk1_list.Add(Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString()));
                    }



                    //DataView dv = dt.DefaultView;
                    //dv.Sort = "name_Q";

                    //testgrid.Visible = true;
                    testgrid1.DataSource = dt;
                    testgrid1.DataBind();
                    testgrid1.HeaderRow.TableSection = TableRowSection.TableHeader;

                    Literal1.Text = "";
                }
                else
                {
                    Literal1.Text = "No data Found";
                    testgrid1.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (System.NullReferenceException err)
            {
                testgrid.Visible = false;
                testgrid1.Visible = false;
                container.Visible = false;
                container1.Visible = false;
                target.Visible = false;
                target1.Visible = false;
                Literal1.Text += err;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }



            JavaScriptSerializer jss8 = new JavaScriptSerializer();
            Data_blk1 = jss8.Serialize(blk1_list);
            //Literal1.Text += Data_blk;

            JavaScriptSerializer jss9 = new JavaScriptSerializer();
            Data_demand_st = jss9.Serialize(demand_list);

            JavaScriptSerializer jss10 = new JavaScriptSerializer();
            Data_isgs_st = jss10.Serialize(isgs_list);

            JavaScriptSerializer jss11 = new JavaScriptSerializer();
            Data_import_st = jss11.Serialize(import_list);



        }
        protected void generate_All_Dem_met_table()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            table_list.Clear();

            string cmdText = "";

            try
            {
                con.Open();
                cmdText = "SELECT Time_data,ROUND(NULLIF(Dem_met, 0),0) FROM meritindia_allindia WHERE Dem_met=(SELECT ROUND(MAX(NULLIF(Dem_met, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    table_list.Add(dt.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT Time_data,ROUND(NULLIF(Dem_met, 0),0) FROM meritindia_allindia WHERE Dem_met=(SELECT ROUND(MIN(NULLIF(Dem_met, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt1.Rows.Count > 0)
                {
                    table_list.Add(dt1.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt1.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT ROUND(AVG(NULLIF(Dem_met, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt2);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt2.Rows.Count > 0)
                {
                    table_list.Add(dt2.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
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
        protected void generate_All_T_gen_table()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            //table_list.Clear();

            string cmdText = "";

            try
            {
                con.Open();
                cmdText = "SELECT Time_data,ROUND(NULLIF(T_gen, 0),0) FROM meritindia_allindia WHERE T_gen=(SELECT ROUND(MAX(NULLIF(T_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    table_list.Add(dt.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT Time_data,ROUND(NULLIF(T_gen, 0),0) FROM meritindia_allindia WHERE T_gen=(SELECT ROUND(MIN(NULLIF(T_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt1.Rows.Count > 0)
                {
                    table_list.Add(dt1.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt1.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT ROUND(AVG(NULLIF(T_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt2);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt2.Rows.Count > 0)
                {
                    table_list.Add(dt2.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
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
        protected void generate_All_G_gen_table()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            // table_list.Clear();

            string cmdText = "";

            try
            {
                con.Open();
                cmdText = "SELECT Time_data,ROUND(NULLIF(G_gen, 0),0) FROM meritindia_allindia WHERE G_gen=(SELECT ROUND(MAX(NULLIF(G_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    table_list.Add(dt.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT Time_data,ROUND(NULLIF(G_gen, 0),0) FROM meritindia_allindia WHERE G_gen=(SELECT ROUND(MIN(NULLIF(G_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt1.Rows.Count > 0)
                {
                    table_list.Add(dt1.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt1.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT ROUND(AVG(NULLIF(G_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt2);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt2.Rows.Count > 0)
                {
                    table_list.Add(dt2.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
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
        protected void generate_All_N_gen_table()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            //table_list.Clear();

            string cmdText = "";

            try
            {
                con.Open();
                cmdText = "SELECT Time_data,ROUND(NULLIF(N_gen, 0),0) FROM meritindia_allindia WHERE N_gen=(SELECT ROUND(MAX(NULLIF(N_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    table_list.Add(dt.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT Time_data,ROUND(NULLIF(N_gen, 0),0) FROM meritindia_allindia WHERE N_gen=(SELECT ROUND(MIN(NULLIF(N_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt1.Rows.Count > 0)
                {
                    table_list.Add(dt1.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt1.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT ROUND(AVG(NULLIF(N_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt2);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt2.Rows.Count > 0)
                {
                    table_list.Add(dt2.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
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
        protected void generate_All_H_gen_table()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            //table_list.Clear();

            string cmdText = "";

            try
            {
                con.Open();
                cmdText = "SELECT Time_data,ROUND(NULLIF(H_gen, 0),0) FROM meritindia_allindia WHERE H_gen=(SELECT ROUND(MAX(NULLIF(H_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    table_list.Add(dt.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT Time_data,ROUND(NULLIF(H_gen, 0),0) FROM meritindia_allindia WHERE H_gen=(SELECT ROUND(MIN(NULLIF(H_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt1.Rows.Count > 0)
                {
                    table_list.Add(dt1.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt1.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT ROUND(AVG(NULLIF(H_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt2);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt2.Rows.Count > 0)
                {
                    table_list.Add(dt2.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
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
        protected void generate_All_R_gen_table()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            // table_list.Clear();

            string cmdText = "";

            try
            {
                con.Open();
                cmdText = "SELECT Time_data,ROUND(NULLIF(R_gen, 0),0) FROM meritindia_allindia WHERE R_gen=(SELECT ROUND(MAX(NULLIF(R_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    table_list.Add(dt.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT Time_data,ROUND(NULLIF(R_gen, 0),0) FROM meritindia_allindia WHERE R_gen=(SELECT ROUND(MIN(NULLIF(R_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt1.Rows.Count > 0)
                {
                    table_list.Add(dt1.Rows[0].ItemArray[0].ToString().Substring(0, 5));
                    table_list.Add(dt1.Rows[0].ItemArray[1].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list.Add("0");
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
                cmdText = "SELECT ROUND(AVG(NULLIF(R_gen, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt2);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt2.Rows.Count > 0)
                {
                    table_list.Add(dt2.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    Literal1.Text = "No data Found";
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

        protected void generate_Max_Min_Dem_met_table()
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            table_list1.Clear();

            string cmdText = "";

            try
            {
                con.Open();
                cmdText = "SELECT SrNo,ROUND(NULLIF(Dem_met, 0),0),ROUND(NULLIF(T_gen, 0),0),ROUND(NULLIF(G_gen, 0),0),ROUND(NULLIF(N_gen, 0),0),ROUND(NULLIF(H_gen, 0),0),ROUND(NULLIF(R_gen, 0),0),"
                    + "Date_data,Time_data,Blk  FROM meritindia_allindia WHERE Time_data IN (SELECT Time_data FROM meritindia_allindia WHERE Dem_met=(SELECT ROUND(MAX(NULLIF(Dem_met, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "') AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    table_list1.Add(dt.Rows[0].ItemArray[1].ToString());
                    table_list1.Add(dt.Rows[0].ItemArray[2].ToString());
                    table_list1.Add(dt.Rows[0].ItemArray[3].ToString());
                    table_list1.Add(dt.Rows[0].ItemArray[4].ToString());
                    table_list1.Add(dt.Rows[0].ItemArray[5].ToString());
                    table_list1.Add(dt.Rows[0].ItemArray[6].ToString());
                    table_list1.Add(dt.Rows[0].ItemArray[8].ToString().Substring(0, 5));
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list1.Add("0");
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
                cmdText = "SELECT SrNo,ROUND(NULLIF(Dem_met, 0),0),ROUND(NULLIF(T_gen, 0),0),ROUND(NULLIF(G_gen, 0),0),ROUND(NULLIF(N_gen, 0),0),ROUND(NULLIF(H_gen, 0),0),ROUND(NULLIF(R_gen, 0),0),"
                    + "Date_data,Time_data,Blk FROM meritindia_allindia WHERE Time_data IN (SELECT Time_data FROM meritindia_allindia WHERE Dem_met=(SELECT ROUND(MIN(NULLIF(Dem_met, 0)),0) FROM meritindia_allindia WHERE Date_data='" + Date_Input.Text + "' GROUP BY Date_data) AND Date_data='" + Date_Input.Text + "') AND Date_data='" + Date_Input.Text + "'";

                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);

                //Label4.Text = "Row copunt --" + dt.Rows.Count;
                if (dt1.Rows.Count > 0)
                {
                    table_list1.Add(dt1.Rows[0].ItemArray[1].ToString());
                    table_list1.Add(dt1.Rows[0].ItemArray[2].ToString());
                    table_list1.Add(dt1.Rows[0].ItemArray[3].ToString());
                    table_list1.Add(dt1.Rows[0].ItemArray[4].ToString());
                    table_list1.Add(dt1.Rows[0].ItemArray[5].ToString());
                    table_list1.Add(dt1.Rows[0].ItemArray[6].ToString());
                    table_list1.Add(dt1.Rows[0].ItemArray[8].ToString().Substring(0, 5));
                }
                else
                {
                    Literal1.Text = "No data Found";
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Literal1.Text += err;
            }
            catch (IndexOutOfRangeException err)
            {
                table_list1.Add("0");
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