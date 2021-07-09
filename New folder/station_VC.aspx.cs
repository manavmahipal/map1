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
    public partial class station_VC : System.Web.UI.Page
    {
        List<int> CummCap = new List<Int32>();
        List<string> stationlist = new List<string>();
        List<string> monthlist = new List<string>();
        List<decimal> VClist = new List<decimal>(); 
        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populate_ddl_filter(); 
               // generate_table_chart("1","Min");
            }
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void station_DDL_change(object sender, EventArgs e)
        {
           
            generate_table_chart(DDL_for_station.SelectedItem.Value, DDL_for_VC_filter.SelectedItem.Value);
        }
        protected void VC_filter_DDL_change(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            stationlist.Clear();
            VClist.Clear();
            try
            {
                con.Open();
                string cmdText = "SELECT SrNo,StationName FROM station_master WHERE SrNo IN (SELECT DISTINCT stationID FROM mo_master WHERE stateID=" + Convert.ToInt32(DDL_for_VC_filter.SelectedItem.Value) + ") ORDER BY StationName";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);



                DDL_for_station.Items.Clear();
                if (dt.Rows.Count > 0)
                {

                    // DDL_for_VC_filter.DataSource = dt;
                    // DDL_for_VC_filter.DataBind();
                   // DDL_for_VC_filter.Items.Insert(0, new ListItem("Min", "Min"));
                   // DDL_for_VC_filter.Items.Insert(1, new ListItem("Max", "Max"));
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DDL_for_station.Items.Insert(j, new ListItem(dt.Rows[j].ItemArray[1].ToString(), dt.Rows[j].ItemArray[0].ToString()));
                    }

                    DDL_for_station.SelectedIndex = 0;
                }
                else
                    DDL_for_station.Items.Insert(0, new ListItem("No data found", "0"));
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
            generate_table_chart(DDL_for_station.SelectedItem.Value, DDL_for_VC_filter.SelectedItem.Value);
        }
        protected void generate_table_chart(string station_input,string filter_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            monthlist.Clear();
            VClist.Clear();
            string cmdText = "";
            //Literal1.Text = station_input + "****" + filter_input;
            try
            {
                con.Open();
                if (filter_input == "Min")
                    cmdText = "SELECT a.display_name,b.month,b.month_no,MIN(b.VC) AS VC FROM station_master a, mo_master b WHERE a.SrNo=b.StationID AND a.SrNo=" + Convert.ToInt32(station_input)+ " AND (b.VC <> 0 OR b.VC IS NOT NULL OR b.VC < 10) GROUP BY b.month_no ORDER BY b.month_no ASC ";
                else if (filter_input == "Max")
                    cmdText = "SELECT a.display_name,b.month,b.month_no,MAX(b.VC) AS VC FROM station_master a, mo_master b WHERE a.SrNo=b.StationID AND a.SrNo=" + Convert.ToInt32(station_input) + " AND (b.VC <> 0 OR b.VC IS NOT NULL OR b.VC < 10) GROUP BY b.month_no ORDER BY b.month_no ASC ";
                else
                    cmdText = "SELECT a.display_name,b.month,b.month_no,MAX(b.VC) AS VC FROM station_master a, mo_master b WHERE a.SrNo=b.StationID AND a.SrNo=" + Convert.ToInt32(station_input) + " AND (b.VC <> 0 OR b.VC IS NOT NULL OR b.VC < 10) AND b.StateID=" + Convert.ToInt32(filter_input) + " GROUP BY b.month_no ORDER BY b.month_no ASC ";
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
                        VClist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[3].ToString()));
                       // CummCap.Add(Cumm);
                        monthlist.Add(dt.Rows[i].ItemArray[1].ToString());
                    }
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
                        Title = new YAxisTitle
                        {
                            Text = "VC",
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
        protected void populate_ddl(string stateID_input)
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
                string cmdText = "SELECT SrNo,StationName FROM station_master WHERE SrNo IN(SELECT DISTINCT stationID FROM mo_master WHERE stateID = " + Convert.ToInt32(stateID_input) + ") ORDER BY StationName ASC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                

                if (dt.Rows.Count > 0)
                {
                    DDL_for_station.DataSource = dt;
                    DDL_for_station.DataBind();
                    DDL_for_station.SelectedIndex = 0;
                    generate_table_chart(dt.Rows[0].ItemArray[0].ToString(), "1");
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
        }
        protected void populate_ddl_filter()
        {
          //  string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            stationlist.Clear();
            VClist.Clear();
            try
            {
                con.Open();
                string cmdText = "SELECT SrNo,StateName FROM state_master ORDER BY SrNo"; //WHERE SrNo IN(SELECT DISTINCT stateID FROM mo_master WHERE stationID = "+Convert.ToInt32(stationID_input)+")
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DDL_for_VC_filter.DataSource = dt;
                    DDL_for_VC_filter.DataBind();
                    populate_ddl("1");
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