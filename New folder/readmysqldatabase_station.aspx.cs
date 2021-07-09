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

namespace mysite
{
    public partial class readmysqldatabase_station : System.Web.UI.Page
    {
        List<int> CummCap = new List<Int32>();
        List<string> stationlist = new List<string>();
        List<string> monthlist = new List<string>();
        List<decimal> VClist = new List<decimal>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populate_ddl();
                generate_table_chart("ADANI  (TIRODA  1320 MW) U 2 & 3");
            }
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void station_DDL_change(object sender, EventArgs e)
        {
            
                 generate_table_chart(DDL_for_station.SelectedItem.Value);
        }
        protected void generate_table_chart(string month_input)
        {
            string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            monthlist.Clear();
            VClist.Clear();
            try
            {
                con.Open();
                string cmdText = "SELECT * FROM mo WHERE Station='"+ month_input + "' ORDER BY month_no ASC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                System.Data.DataColumn newColumn = new System.Data.DataColumn("Cumm", typeof(System.Int32));
                newColumn.DefaultValue = 0;
                dt.Columns.Add(newColumn);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cumm = Cumm + Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString());
                        dt.Rows[i][8] = Cumm;
                        VClist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[5].ToString()));
                        CummCap.Add(Cumm);
                        monthlist.Add(dt.Rows[i].ItemArray[7].ToString());
                    }
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
        protected void populate_ddl()
        {
            string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            stationlist.Clear();
            VClist.Clear();
            try
            {
                con.Open();
                string cmdText = "SELECT DISTINCT Station FROM mo ORDER BY Station";
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
    }
}