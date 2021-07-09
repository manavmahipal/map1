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
    public partial class state_MO : System.Web.UI.Page
    {
        List<decimal> CummCap = new List<Decimal>(); 
        List<string> stationlist = new List<string>();
        List<decimal> VClist = new List<decimal>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            generate_table_chart("Jan-21","19");
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void month_DDL_change(object sender, EventArgs e)
        {

            generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
        }
        protected void state_DDL_change(object sender, EventArgs e)
        {

            generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
        }
        protected void generate_table_chart(string month_input, string state_input)
        {
            string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            decimal Cumm = 0.0000m;
            CummCap.Clear();
            stationlist.Clear();
            VClist.Clear();
            try
            {
                con.Open();
                string cmdText = "SELECT a.VC as VC,b.display_name as station,b.Utility_subtype as ntpc,c.state_"+state_input+" as allocation FROM mo_master a, station_master b, allocation_master c WHERE a.StationID=b.SrNo AND a.StationID=c.StationID AND a.month='"+month_input+ "' AND (a.VC <> 0 OR a.VC IS NOT NULL) AND a.VC < 10 AND a.StateID=" + state_input+ " AND c.state_" + state_input + " <> 0 ORDER BY VC ASC ";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                System.Data.DataColumn newColumn = new System.Data.DataColumn("Cumm", typeof(System.Int32));
                newColumn.DefaultValue = 0;
                dt.Columns.Add(newColumn);

                if (dt.Rows.Count > 0)
                {
                    Color[] colors = new Color[dt.Rows.Count + 1];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[2].ToString() == "NTPC")
                            colors[i] = ColorTranslator.FromHtml("#000000");
                        else
                            colors[i] = ColorTranslator.FromHtml("#2e44fe");

                        Cumm = Cumm + Convert.ToDecimal(dt.Rows[i].ItemArray[3].ToString());
                        dt.Rows[i][4] = Cumm;
                        VClist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[0].ToString()));
                        CummCap.Add(Cumm);
                        stationlist.Add(dt.Rows[i].ItemArray[1].ToString());
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
                    new Series {Type = ChartTypes.Column, Name = "Cumm. Cap", Data =new Data(Array.ConvertAll(CummCap.ToArray(), x => (object)x)),PlotOptionsColumn = new PlotOptionsColumn {  ColorByPoint = true,Colors =  colors,  DataLabels=new PlotOptionsColumnDataLabels{Enabled=true, Rotation=-90,  Color =ColorTranslator.FromHtml("#000000"),Formatter="function() { return this.y;}", VerticalAlign = VerticalAligns.Top}   }  },
                    new Series {  Type = ChartTypes.Spline,YAxis = "1",Name = "VC", Data =new Data(Array.ConvertAll(VClist.ToArray(), x => (object)x)),PlotOptionsSpline=new PlotOptionsSpline{ DataLabels=new PlotOptionsSplineDataLabels{ Enabled=true, Overflow="allow",Crop=false, Rotation=270, X=2, Y= -5, Align= HorizontalAligns.Left} } },
           });
                    ltrChart.Text = chart.ToHtmlString();
                }
                else
                {
                    ltrChart.Text = "Data not Found";
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
    }
}