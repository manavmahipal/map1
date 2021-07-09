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

namespace merit
{
    public partial class perf_report : System.Web.UI.Page
    {
        public string chartData_OP_HDP_avail_pc_list
        {
            get;
            set;
        }
        public string chartData_OP_HDP_plf_pc_list
        {
            get;
            set;
        }
        public string chartData_OP_LDP_avail_pc_list
        {
            get;
            set;
        }
        public string chartData_OP_LDP_plf_pc_list
        {
            get;
            set;
        }
        public string chartData_OP_CummP_avail_pc_list
        {
            get;
            set;
        }
        public string chartData_OP_CummP_plf_pc_list
        {
            get;
            set;
        }
        public string chartData_OP_Norm_pc_list
        {
            get;
            set;
        }
        public string chartData_OP_Act_pc_list
        {
            get;
            set;
        }
        public string chartData_OP_HR_Norm_kcal_kwhr_list
        {
            get;
            set;
        }
        public string chartData_OP_HR_Act_kcal_kwhr_list
        {
            get;
            set;
        }
        public string chartData_OP_SFC_Norm_ml_kwhr_list
        {
            get;
            set;
        }
        public string chartData_OP_SFC_Act_ml_kwhr_list
        {
            get;
            set;
        }
        public string chartData_MC_Tot_p_kwhr_list
        {
            get;
            set;
        }
        public string chartData_Tot_G_MC_Cr_list
        {
            get;
            set;
        }
        public string chartData_Net_G_MC_af_share_Cr_list
        {
            get;
            set;
        }
        public string chartData_PLC_Cr_list
        {
            get;
            set;
        }
        public string chartData_OP_G_Cr_list
        {
            get;
            set;
        }
        public string chartData_IG_DSM_Cr_list
        {
            get;
            set;
        }
        public string chartData_IG_RRAS_Cr_list
        {
            get;
            set;
        }
        public string chartData_IG_AGC_Cr_list
        {
            get;
            set;
        }
        public string chartData_IG_SCED_Cr_list
        {
            get;
            set;
        }
        public string chartData_IG_Cr_list
        {
            get;
            set;
        }
        public string chartData_AFCloss_Cr_list
        {
            get;
            set;
        }
        public string chartData_Tot_G_Cr_list
        {
            get;
            set;
        }
        public string chartData_station_list
        {
            get;
            set;
        }

        List<decimal> OP_HDP_avail_pc_list = new List<decimal>();
        List<decimal> OP_HDP_plf_pc_list = new List<decimal>();
        List<decimal> OP_LDP_avail_pc_list = new List<decimal>();
        List<decimal> OP_LDP_plf_pc_list = new List<decimal>();
        List<decimal> OP_CummP_avail_pc_list = new List<decimal>();
        List<decimal> OP_CummP_plf_pc_list = new List<decimal>();
        List<decimal> OP_Norm_pc_list = new List<decimal>();
        List<decimal> OP_Act_pc_list = new List<decimal>();
        List<decimal> OP_HR_Norm_kcal_kwhr_list = new List<decimal>();
        List<decimal> OP_HR_Act_kcal_kwhr_list = new List<decimal>();
        List<decimal> OP_SFC_Norm_ml_kwhr_list = new List<decimal>();
        List<decimal> OP_SFC_Act_ml_kwhr_list = new List<decimal>();
        List<decimal> MC_Tot_p_kwhr_list = new List<decimal>();
        List<decimal> Tot_G_MC_Cr_list = new List<decimal>();
        List<decimal> Net_G_MC_af_share_Cr_list = new List<decimal>();
        List<decimal> PLC_Cr_list = new List<decimal>();
        List<decimal> OP_G_Cr_list = new List<decimal>();
        List<decimal> IG_DSM_Cr_list = new List<decimal>();
        List<decimal> IG_RRAS_Cr_list = new List<decimal>();
        List<decimal> IG_AGC_Cr_list = new List<decimal>();
        List<decimal> IG_SCED_Cr_list = new List<decimal>();
        List<decimal> IG_Cr_list = new List<decimal>();
        List<decimal> AFCloss_Cr_list = new List<decimal>();
        List<decimal> Tot_G_Cr_list = new List<decimal>();
        List<string> station_list = new List<string>();


        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populate_fy();
                populate_station();
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
                string cmdText = "SELECT DISTINCT FY FROM perf_report ORDER BY FY DESC";
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
                    //generate_table_chart("0", dt.Rows[0].ItemArray[0].ToString());
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

        protected void populate_station()
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt.Columns.Add("SrNo", typeof(String));
            dt.Columns.Add("StationName", typeof(String));
            dt.Rows.Add();
            dt.Rows[0][0] = 0;
            dt.Rows[0][1] = "All Stations";
            try
            {
                con.Open();
                string cmdText = "SELECT a.StationID AS SrNo,b.display_name AS StationName FROM perf_report a, station_master b WHERE a.StationID=b.SrNo";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt1);



                if (dt1.Rows.Count > 0)
                {
                    for(int i=0;i< dt1.Rows.Count; i++)
                    {
                        dt.Rows.Add();
                        dt.Rows[i+1][0] = dt1.Rows[i].ItemArray[0].ToString();
                        dt.Rows[i+1][1] = dt1.Rows[i].ItemArray[1].ToString();
                    }
                    DDL_for_station.DataSource = dt;
                    DDL_for_station.DataBind();
                    DDL_for_station.SelectedIndex = 0;
                    //populate_month(DDL_for_state.SelectedValue.ToString());
                    generate_table_chart(dt.Rows[0][0].ToString(), DDL_for_fy.SelectedValue);
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
        protected void fy_DDL_change(object sender, EventArgs e)
        {
            //generate_table_chart("0", DDL_for_fy.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            generate_table_chart(DDL_for_station.SelectedValue.ToString(), DDL_for_fy.SelectedValue.ToString());
        }
        protected void station_DDL_change(object sender, EventArgs e)
        {
            //generate_table_chart("1", DDL_for_state.SelectedValue);
            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            generate_table_chart(DDL_for_station.SelectedValue.ToString(), DDL_for_fy.SelectedValue.ToString());
        }

        protected void generate_table_chart(string filter_input, string sub_filter_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            OP_HDP_avail_pc_list.Clear();
            OP_HDP_plf_pc_list.Clear();
            OP_LDP_avail_pc_list.Clear();
            OP_LDP_plf_pc_list.Clear();
            OP_CummP_avail_pc_list.Clear();
            OP_CummP_plf_pc_list.Clear();
            OP_Norm_pc_list.Clear();
            OP_Act_pc_list.Clear();
            OP_HR_Norm_kcal_kwhr_list.Clear();
            OP_HR_Act_kcal_kwhr_list.Clear();
            OP_SFC_Norm_ml_kwhr_list.Clear();
            OP_SFC_Act_ml_kwhr_list.Clear();
            MC_Tot_p_kwhr_list.Clear();
            Tot_G_MC_Cr_list.Clear();
            Net_G_MC_af_share_Cr_list.Clear();
            PLC_Cr_list.Clear();
            OP_G_Cr_list.Clear();
            IG_DSM_Cr_list.Clear();
            IG_RRAS_Cr_list.Clear();
            IG_AGC_Cr_list.Clear();
            IG_SCED_Cr_list.Clear();
            IG_Cr_list.Clear();
            AFCloss_Cr_list.Clear();
            Tot_G_Cr_list.Clear();
            station_list.Clear();

            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");



            try
            {
                con.Open();
                if (filter_input == "0")
                    cmdText = "SELECT b.display_name, a.OP_HDP_avail_pc, a.OP_HDP_plf_pc, a.OP_LDP_avail_pc, a.OP_LDP_plf_pc, a.OP_CummP_avail_pc, a.OP_CummP_plf_pc, a.OP_Norm_pc, a.OP_Act_pc, a.OP_HR_Norm_kcal_kwhr,"
                        +"a.OP_HR_Act_kcal_kwhr, a.OP_SFC_Norm_ml_kwhr, a.OP_SFC_Act_ml_kwhr, a.MC_Tot_p_kwhr, a.Tot_G_MC_Cr, a.Net_G_MC_af_share_Cr, a.PLC_Cr, a.OP_G_Cr, a.IG_DSM_Cr, a.IG_RRAS_Cr, a.IG_AGC_Cr, "
                        +"a.IG_SCED_Cr, a.IG_Cr, a.AFCloss_Cr, a.Tot_G_Cr FROM perf_report a, station_master b WHERE a.FY='"+sub_filter_input+"' AND a.stationID=b.SrNo";
                else
                    cmdText = "SELECT b.display_name, a.OP_HDP_avail_pc, a.OP_HDP_plf_pc, a.OP_LDP_avail_pc, a.OP_LDP_plf_pc, a.OP_CummP_avail_pc, a.OP_CummP_plf_pc, a.OP_Norm_pc, a.OP_Act_pc, a.OP_HR_Norm_kcal_kwhr,"
                        + "a.OP_HR_Act_kcal_kwhr, a.OP_SFC_Norm_ml_kwhr, a.OP_SFC_Act_ml_kwhr, a.MC_Tot_p_kwhr, a.Tot_G_MC_Cr, a.Net_G_MC_af_share_Cr, a.PLC_Cr, a.OP_G_Cr, a.IG_DSM_Cr, a.IG_RRAS_Cr, a.IG_AGC_Cr, "
                        + "a.IG_SCED_Cr, a.IG_Cr, a.AFCloss_Cr, a.Tot_G_Cr FROM perf_report a, station_master b WHERE a.StationID=" + Convert.ToInt32(filter_input) + " AND a.FY='" + sub_filter_input + "' AND a.stationID=b.SrNo";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                            station_list.Add(dt.Rows[i].ItemArray[0].ToString());

                        OP_HDP_avail_pc_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[1].ToString()));
                        OP_HDP_plf_pc_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[2].ToString()));
                        OP_LDP_avail_pc_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[3].ToString()));
                        OP_LDP_plf_pc_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[4].ToString()));
                        OP_CummP_avail_pc_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[5].ToString()));
                        OP_CummP_plf_pc_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[6].ToString()));
                        OP_Norm_pc_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[7].ToString()));
                        OP_Act_pc_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[8].ToString()));
                        OP_HR_Norm_kcal_kwhr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[9].ToString()));
                        OP_HR_Act_kcal_kwhr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[10].ToString()));
                        OP_SFC_Norm_ml_kwhr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[12].ToString()));
                        OP_SFC_Act_ml_kwhr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[12].ToString()));
                        MC_Tot_p_kwhr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[13].ToString()));
                        Tot_G_MC_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[14].ToString()));
                        Net_G_MC_af_share_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[15].ToString()));
                        PLC_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[16].ToString()));
                        OP_G_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[17].ToString()));
                        IG_DSM_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[18].ToString()));
                        IG_RRAS_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[19].ToString()));
                        IG_AGC_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[20].ToString()));
                        IG_SCED_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[21].ToString()));
                        IG_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[22].ToString()));
                        AFCloss_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[23].ToString()));
                        Tot_G_Cr_list.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[24].ToString()));
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

            JavaScriptSerializer jss0 = new JavaScriptSerializer();
            chartData_station_list = jss0.Serialize(station_list);
            JavaScriptSerializer jss1 = new JavaScriptSerializer();
            chartData_OP_HDP_avail_pc_list = jss1.Serialize(OP_HDP_avail_pc_list);
            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            chartData_OP_HDP_plf_pc_list = jss2.Serialize(OP_HDP_plf_pc_list);
            JavaScriptSerializer jss3 = new JavaScriptSerializer();
            chartData_OP_LDP_avail_pc_list = jss3.Serialize(OP_LDP_avail_pc_list);
            JavaScriptSerializer jss4 = new JavaScriptSerializer();
            chartData_OP_LDP_plf_pc_list = jss4.Serialize(OP_LDP_plf_pc_list);
            JavaScriptSerializer jss5 = new JavaScriptSerializer();
            chartData_OP_CummP_avail_pc_list = jss5.Serialize(OP_CummP_avail_pc_list);
            JavaScriptSerializer jss6 = new JavaScriptSerializer();
            chartData_OP_CummP_plf_pc_list = jss6.Serialize(OP_CummP_plf_pc_list);
            JavaScriptSerializer jss7 = new JavaScriptSerializer();
            chartData_OP_Norm_pc_list = jss7.Serialize(OP_Norm_pc_list);
            JavaScriptSerializer jss8 = new JavaScriptSerializer();
            chartData_OP_Act_pc_list = jss8.Serialize(OP_Act_pc_list);
            JavaScriptSerializer jss9 = new JavaScriptSerializer();
            chartData_OP_HR_Norm_kcal_kwhr_list = jss9.Serialize(OP_HR_Norm_kcal_kwhr_list);
            JavaScriptSerializer jss10 = new JavaScriptSerializer();
            chartData_OP_HR_Act_kcal_kwhr_list = jss10.Serialize(OP_HR_Act_kcal_kwhr_list);
            JavaScriptSerializer jss11 = new JavaScriptSerializer();
            chartData_OP_SFC_Norm_ml_kwhr_list = jss11.Serialize(OP_SFC_Norm_ml_kwhr_list);
            JavaScriptSerializer jss12 = new JavaScriptSerializer();
            chartData_OP_SFC_Act_ml_kwhr_list = jss12.Serialize(OP_SFC_Act_ml_kwhr_list);
            JavaScriptSerializer jss13 = new JavaScriptSerializer();
            chartData_MC_Tot_p_kwhr_list = jss13.Serialize(MC_Tot_p_kwhr_list);
            JavaScriptSerializer jss14 = new JavaScriptSerializer();
            chartData_Tot_G_MC_Cr_list = jss14.Serialize(Tot_G_MC_Cr_list);
            JavaScriptSerializer jss15 = new JavaScriptSerializer();
            chartData_Net_G_MC_af_share_Cr_list = jss15.Serialize(Net_G_MC_af_share_Cr_list);
            JavaScriptSerializer jss16 = new JavaScriptSerializer();
            chartData_PLC_Cr_list = jss16.Serialize(PLC_Cr_list);
            JavaScriptSerializer jss17 = new JavaScriptSerializer();
            chartData_OP_G_Cr_list = jss17.Serialize(OP_G_Cr_list);
            JavaScriptSerializer jss18 = new JavaScriptSerializer();
            chartData_IG_DSM_Cr_list = jss18.Serialize(IG_DSM_Cr_list);
            JavaScriptSerializer jss19 = new JavaScriptSerializer();
            chartData_IG_RRAS_Cr_list = jss19.Serialize(IG_RRAS_Cr_list);
            JavaScriptSerializer jss20 = new JavaScriptSerializer();
            chartData_IG_AGC_Cr_list = jss20.Serialize(IG_AGC_Cr_list);
            JavaScriptSerializer jss21 = new JavaScriptSerializer();
            chartData_IG_SCED_Cr_list = jss21.Serialize(IG_SCED_Cr_list);
            JavaScriptSerializer jss22 = new JavaScriptSerializer();
            chartData_IG_Cr_list = jss22.Serialize(IG_Cr_list);
            JavaScriptSerializer jss23 = new JavaScriptSerializer();
            chartData_AFCloss_Cr_list = jss23.Serialize(AFCloss_Cr_list);
            JavaScriptSerializer jss24 = new JavaScriptSerializer();
            chartData_Tot_G_Cr_list = jss24.Serialize(Tot_G_Cr_list);


        }
    }
}