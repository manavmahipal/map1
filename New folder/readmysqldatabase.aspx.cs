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
    public partial class readmysqldatabase : System.Web.UI.Page
    {
        List<int> CummCap = new List<Int32>(); 
        List<string> stationlist = new List<string>();
        List<decimal> VClist = new List<decimal>();
        protected void Page_Load(object sender, EventArgs e)
        {
            generate_table_chart("Jan-21");
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void month_DDL_change(object sender, EventArgs e)
        {
            
                 generate_table_chart(DDL_for_month.SelectedItem.Value);
        }
        protected void generate_table_chart(string month_input)
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
                string cmdText = "SELECT * FROM mo WHERE month='"+ month_input + "' AND VC <> 0 ORDER BY VC ASC";
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
                        stationlist.Add(dt.Rows[i].ItemArray[1].ToString());
                    }
                    testgrid.DataSource = dt;
                    testgrid.DataBind();
                    testgrid.HeaderRow.TableSection = TableRowSection.TableHeader;

                    DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
           .SetTitle(new Title { Text = "" })
           .SetCredits(new Credits { Enabled = false })
           .SetXAxis(new XAxis
           {
               Categories = stationlist.ToArray(),
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
                            Formatter = "function() { return this.value +' MW'; }",
                            Style = "color: '#89A54E'"
                        },
                        Title = new YAxisTitle
                        {
                            Text = "Cumm. Capacity",
                            Style = "color: '#89A54E'"
                        }
                    },
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
                        GridLineWidth = 0,
                        Opposite = true
                    }
               })
           .SetSeries(new[]
           {
                    new Series {Type = ChartTypes.Column,Name = "Cumm. Cap", Data =new Data(Array.ConvertAll(CummCap.ToArray(), x => (object)x)) },
                    new Series { Type = ChartTypes.Spline,YAxis = "1",Name = "VC", Data =new Data(Array.ConvertAll(VClist.ToArray(), x => (object)x)) },
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
    }
}