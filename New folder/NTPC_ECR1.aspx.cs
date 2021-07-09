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
using System.Drawing;

namespace merit
{
    public partial class NTPC_ECR1 : System.Web.UI.Page
    {
        List<int> CummCap = new List<Int32>();
        List<string> stationlist = new List<string>();
        List<string> monthlist = new List<string>();
        List<decimal> VClist = new List<decimal>();
        List<decimal> ECRlist = new List<decimal>();
        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populate_ddl();
                populate_ddl_filter("13");
                generate_table_chart("13","2021-05-01");
            }
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void station_DDL_change(object sender, EventArgs e)
        {
            
                    generate_table_chart(DDL_for_station.SelectedItem.Value, DDL_for_month.SelectedItem.Value);
              
           
        }
        protected void month_DDL_change(object sender, EventArgs e)
        {

            generate_table_chart(DDL_for_station.SelectedItem.Value, DDL_for_month.SelectedItem.Value);
        }
        protected void generate_table_chart(string station_input,string filter_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            monthlist.Clear();
            VClist.Clear();
            ECRlist.Clear();
            string cmdText = "";
            string month_string = "";




            // Literal1.Text = station_input + "****" + filter_input;
            try
            {
                con.Open();
                if (filter_input == "Min")
                {
                    cmdText = "SELECT a.ECR AS ECR, b.display_name AS station_name, b.display_name, MONTH(a.Date_from) AS month, YEAR(a.Date_from) AS year FROM ecr_master01 a, station_master b WHERE  a.StationID = b.SrNo AND a.StationID=" + Convert.ToInt32(station_input);
                    MySqlCommand cmd = new MySqlCommand(cmdText, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(cmdText, con);
                    adapter.Fill(dt);

                    //System.Data.DataColumn newColumn = new System.Data.DataColumn("Cumm", typeof(System.Int32));
                    // newColumn.DefaultValue = 0;
                    // dt.Columns.Add(newColumn);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //  Cumm = Cumm + 0;//Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString());
                            // dt.Rows[i][4] = Cumm;
                            VClist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[0].ToString()));
                            // CummCap.Add(Cumm);
                            if (dt.Rows[i].ItemArray[3].ToString() == "1")
                                month_string = "Jan";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "2")
                                month_string = "Feb";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "3")
                                month_string = "Mar";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "4")
                                month_string = "Apr";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "5")
                                month_string = "May";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "6")
                                month_string = "Jun";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "7")
                                month_string = "Jul";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "8")
                                month_string = "Aug";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "9")
                                month_string = "Sep";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "10")
                                month_string = "Oct";
                            else if (dt.Rows[i].ItemArray[3].ToString() == "11")
                                month_string = "Nov";
                            else 
                                month_string = "Dec";





                            monthlist.Add(month_string+"-"+dt.Rows[i].ItemArray[4].ToString());
                        }
                        testgrid1.Visible = false;
                        testgrid.Visible = true;
                        testgrid.DataSource = dt;
                        testgrid.DataBind();
                        testgrid.HeaderRow.TableSection = TableRowSection.TableHeader;

                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
               .SetTitle(new Title { Text = "" })
               .SetCredits(new Credits { Enabled = false })
               .SetXAxis(new XAxis
               {
                   Categories = monthlist.ToArray(),
                   Labels = new XAxisLabels
                   {
                       Rotation = -45,
                       Align = HorizontalAligns.Right,
                       Style = "fontSize: '8px'"
                   }
               })
               .SetYAxis(new[]
                   {
                    new YAxis
                    {
                        Labels = new YAxisLabels
                        {
                            Formatter = "function() { return this.value +' Rs/KWh'; }",
                            Style = "color: '#AA4643'"
                        },
                    }
                   })
               .SetSeries(new[]
               {
                    new Series { Type = ChartTypes.Column,Name = "VC", Data =new Data(Array.ConvertAll(VClist.ToArray(), x => (object)x)) },
               });
                        ltrChart.Text = chart.ToHtmlString();
                    }
                    else
                    {
                        ltrChart.Text = "No data Found";
                        testgrid.Visible = false;
                        testgrid1.Visible = false;
                    }
                }
                else
                {
                    Literal2.Text = "++++"+filter_input;
                    cmdText = "SELECT a.StationName AS station_name, b.Date_from AS date_from, b.Date_to AS date_to, b.ECR AS ECR,c.VC as VC,d.StateName AS state FROM station_master a, ecr_master01 b, mo_master01 c, state_master d WHERE b.StationID = a.SrNo AND b.StationID = " + Convert.ToInt32(station_input) + " AND c.StationID = b.StationID AND b.Date_from='" + filter_input + "' AND c.StateID=d.SrNo AND ((c.date_from <= b.Date_from  AND c.date_to >= b.Date_from ) OR (c.date_from <= b.Date_from  AND c.date_to IS NULL)) ";
                    //cmdText = "SELECT a.StationName AS station_name, b.month AS month, b.month_no AS month_no, b.ECR AS ECR,c.VC as VC,d.StateName AS state FROM station_master a, ecr_master b, mo_master c, state_master d WHERE b.StationID = a.SrNo AND b.StationID = " + Convert.ToInt32(station_input) + " AND c.StationID = b.StationID AND c.month_no = b.month_no AND b.month_no=" + Convert.ToInt32(filter_input)+" AND c.StateID=d.SrNo";
                    MySqlCommand cmd = new MySqlCommand(cmdText, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(cmdText, con);
                    adapter.Fill(dt);

                    //System.Data.DataColumn newColumn = new System.Data.DataColumn("Cumm", typeof(System.Int32));
                    // newColumn.DefaultValue = 0;
                    // dt.Columns.Add(newColumn);

                    if (dt.Rows.Count > 0)
                    {

                        Color[] colors = new Color[dt.Rows.Count+1];
                        colors[0] = ColorTranslator.FromHtml("#1efefe");
                        VClist.Add(Convert.ToDecimal(dt.Rows[0].ItemArray[3].ToString()));
                        monthlist.Add("ECR");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //  Cumm = Cumm + 0;//Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString());
                            // dt.Rows[i][4] = Cumm;

                            colors[i+1] = ColorTranslator.FromHtml("#2e44fe");
                            VClist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[4].ToString()));
                           // ECRlist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[4].ToString()));
                            // CummCap.Add(Cumm);
                            monthlist.Add(dt.Rows[i].ItemArray[5].ToString());
                        }
                        testgrid.Visible = false;
                        testgrid1.Visible = true;
                        testgrid1.DataSource = dt;
                        testgrid1.DataBind();
                        testgrid1.HeaderRow.TableSection = TableRowSection.TableHeader;

                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
               .SetTitle(new Title { Text = "" })
               .SetOptions(new GlobalOptions {Colors = new[] { ColorTranslator.FromHtml("#000F0D"),ColorTranslator.FromHtml("#7798BF"), ColorTranslator.FromHtml("#55BF3B"), ColorTranslator.FromHtml("#DF5353") } })
               .SetCredits(new Credits { Enabled = false })
               .SetXAxis(new XAxis
               {
                   Categories = monthlist.ToArray(),
                   Labels = new XAxisLabels
                   {
                       Rotation = -45,
                       Align = HorizontalAligns.Right,
                       Style = "fontSize: '8px'"
                   }
               })
               .SetYAxis(new[]
                   {
                    new YAxis
                    {
                        Labels = new YAxisLabels
                        {
                            Formatter = "function() { return this.value +' Rs/KWh'; }",
                            Style = "color: '#AA4643'"
                        },
                    }
                   })
               .SetSeries(new[]
               {
                    new Series { Type = ChartTypes.Column,Name = "VC", Data =new Data(Array.ConvertAll(VClist.ToArray(), x => (object)x)),PlotOptionsColumn = new PlotOptionsColumn { ColorByPoint = true, Colors =  colors   } },
               });
                        ltrChart.Text = chart.ToHtmlString();
                    }
                    else
                    {
                        ltrChart.Text = "No data Found";
                        testgrid.Visible = false;
                        testgrid1.Visible = false;
                    }
                }
               
               

            }
            catch (MySqlException err)
            {
                Literal1.Text += err+ "<br/><br/><br/>++++" + filter_input;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        protected void populate_ddl()
        {
           // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            stationlist.Clear();
            VClist.Clear();
            try
            {
                con.Open();
                string cmdText = "SELECT SrNo,StationName FROM station_master WHERE Utility_subtype='NTPC' ORDER BY StationName ASC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DDL_for_station.DataSource = dt;
                    DDL_for_station.DataBind();
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
        protected void populate_ddl_filter(string stationID_input)
        {
            /*DDL_for_month.Items.Clear();
            DDL_for_month.Items.Insert(0, new ListItem("Display only ECR", "Min"));
            DDL_for_month.Items.Insert(2, new ListItem("Feb-21", "2"));
            DDL_for_month.Items.Insert(3, new ListItem("Mar-21", "3"));
            DDL_for_month.Items.Insert(4, new ListItem("Apr-21", "4"));
            DDL_for_month.Items.Insert(5, new ListItem("May-21", "5"));
            DDL_for_month.SelectedIndex = 0;*/



            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            //int Cumm = 0;
           // CummCap.Clear();
            //statelist.Clear();
            //CurrVClist.Clear();
            //PrevVClist.Clear();



            try
            {
                con.Open();
                string cmdText = "SELECT DISTINCT MONTH(Date_from) AS month1,YEAR(Date_from) AS month2 FROM `ecr_master01` ORDER BY Date_from DESC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                DDL_for_month.Items.Clear();
                DDL_for_month.Items.Insert(0, new ListItem("Display only ECR", "Min"));
                string month_string = "";
                string value_string = "";

                if (dt.Rows.Count > 0)
                {
                    // DDL_for_month.DataSource = dt;
                    // DDL_for_month.DataBind();
                    //  DDL_for_month.SelectedIndex = 0;



                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j].ItemArray[0].ToString() == "1")
                        {
                            month_string = "Jan";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-01-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "2")
                        {
                            month_string = "Feb";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-02-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "3")
                        {
                            month_string = "Mar";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-03-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "4")
                        {
                            month_string = "Apr";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-04-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "5")
                        {
                            month_string = "May";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-05-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "6")
                        {
                            month_string = "Jun";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-06-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "7")
                        {
                            month_string = "Jul";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-07-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "8")
                        {
                            month_string = "Aug";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-08-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "9")
                        {
                            month_string = "Sep";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-09-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "10")
                        {
                            month_string = "Oct";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-10-01";
                        }
                        else if (dt.Rows[j].ItemArray[0].ToString() == "11")
                        {
                            month_string = "Nov";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-11-01";
                        }
                        else
                        {
                            month_string = "Dec";
                            value_string = dt.Rows[j].ItemArray[1].ToString() + "-12-01";
                        }

                        DDL_for_month.Items.Insert(j+1, new ListItem(month_string + "-" + dt.Rows[j].ItemArray[1].ToString(), value_string));
                    }

                    DDL_for_month.SelectedIndex = 0;



                    //populate_month_filter(state_input, DDL_for_month.SelectedValue.ToString());
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