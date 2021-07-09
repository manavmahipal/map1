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
    public partial class All_India_MO : System.Web.UI.Page
    {
        List<decimal> CummCap = new List<Decimal>(); 
        List<string> stationlist = new List<string>();
        List<decimal> CurrVClist = new List<decimal>();
        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // date = ;
                generate_table_chart(DateTime.Now.ToString("yyyy-MM-dd"));
              //  generate_table_chart("Jan-21", "Min"); 
            }
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void date_DDL_change(object sender, EventArgs e)
        {

            generate_table_chart(Date_Input.Text.ToString());
        }
        protected void month_DDL_change(object sender, EventArgs e)
        {

            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
        }
        protected void year_DDL_change(object sender, EventArgs e)
        {

            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
        }

        protected void generate_table_chart(string date_filter_input)
        {
           
             MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            decimal Cumm = 0.0000m;
            CummCap.Clear();
            stationlist.Clear();
            CurrVClist.Clear();
            //PrevVClist.Clear();
            string cmdText = "";
             //Literal1.Text =  date_filter_input;




            try
            {
                con.Open();

                cmdText = "SELECT MIN(a.VC) AS VC, a.StationID, b.display_name, b.Utility_subtype,b.Installed_cap_MW FROM mo_master01 a, station_master b WHERE ((a.date_from <= '" + date_filter_input + "' AND a.date_to >= '" + date_filter_input + "') OR (a.date_from <= '" + date_filter_input + "' AND a.date_to IS NULL)) AND a.StationID = b.SrNo AND a.VC > 0 GROUP BY a.StationID ORDER BY VC ";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                System.Data.DataColumn newColumn = new System.Data.DataColumn("Cumm", typeof(System.Int32));
                newColumn.DefaultValue = 0;
                dt.Columns.Add(newColumn);
                //System.Data.DataColumn newColumn = new System.Data.DataColumn("Cumm", typeof(System.Int32));
                // newColumn.DefaultValue = 0;
                // dt.Columns.Add(newColumn);

                if (dt.Rows.Count > 0)
                {
                    ltrChart.Text = "";
                    Color[] colors = new Color[dt.Rows.Count + 1];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[3].ToString()=="NTPC")
                            colors[i] = ColorTranslator.FromHtml("#51d1f7");
                        else
                            colors[i] = ColorTranslator.FromHtml("#d877fc");

                        //  Cumm = Cumm + 0;//Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString());
                        // dt.Rows[i][4] = Cumm;
                        if (dt.Rows[i].ItemArray[0].ToString() == "")
                            CurrVClist.Add(0.0m);
                        else
                            CurrVClist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[0].ToString()));

                        Cumm = Cumm + Convert.ToDecimal(dt.Rows[i].ItemArray[4].ToString());
                        dt.Rows[i][5] = Cumm;
                        CummCap.Add(Cumm);


                        stationlist.Add(dt.Rows[i].ItemArray[2].ToString());
                        // CummCap.Add(Cumm);
                        /////////////////////   monthlist.Add(dt.Rows[i].ItemArray[1].ToString());
                    }
                    testgrid.Visible = true;
                    testgrid.DataSource = dt;
                    testgrid.DataBind();
                    testgrid.HeaderRow.TableSection = TableRowSection.TableHeader;



                    DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
          .SetTitle(new Title { Text = "" })
          .SetOptions(new GlobalOptions { Colors = colors })
          .SetCredits(new Credits { Enabled = false })
          .SetXAxis(new XAxis
          {
              Categories = stationlist.ToArray(),
              Labels = new XAxisLabels
              {
                  Rotation = -45,
                  Align = HorizontalAligns.Right,
                  Style = "fontSize: '8px',color:'#55555'"
              },
              LineColor = ColorTranslator.FromHtml("#000000"),
              GridLineColor = ColorTranslator.FromHtml("#000000")
          })
          .SetYAxis(new[]
              {
                    new YAxis
                    {
                        Labels = new YAxisLabels
                        {
                            Formatter = "function() { return this.value +' MW'; }",
                            Style = "color: '#AA4643'"
                        },
                        Title = new YAxisTitle
                        {
                            Text = "Cumm. Capacity",
                            Style = "color: '#AA4643'"
                        },
                    },
                    new YAxis
                    {
                        Labels = new YAxisLabels
                        {
                            Formatter = "function() { return this.value +' Rs/KWh'; }",
                            Style = "color: '#89A54E'"
                        },
                        Title = new YAxisTitle
                        {
                            Text = "VC",
                            Style = "color: '#89A54E'"
                        },
                        GridLineWidth = 0,
                        Opposite = true
                    }
              })
          .SetTooltip(new Tooltip
          {
              HeaderFormat = "<b>{point.x:.0f}</b><br>",
              PointFormat = "<span style=\"color:{ series.color}\">\u25CF</span> {series.name}: {point.y:.f} <br/>",
              Enabled = true,
              Shared = true
          })
          .SetSeries(new[]
          {
              new Series {Type = ChartTypes.Column, Name = "Cumm. Cap", Data =new Data(Array.ConvertAll(CummCap.ToArray(), x => (object)x)),PlotOptionsColumn = new PlotOptionsColumn {Visible=false,  ColorByPoint = true,Colors =  colors,  DataLabels=new PlotOptionsColumnDataLabels{Enabled=true, Rotation=-90,  Color =ColorTranslator.FromHtml("#000000"),Formatter="function() { return this.y;}", VerticalAlign = VerticalAligns.Top}   }  },
                    new Series {PlotOptionsColumn=new PlotOptionsColumn{ ColorByPoint=true }, Type = ChartTypes.Line,YAxis = "1",Name = "Curr VC",Data =new Data(Array.ConvertAll(CurrVClist.ToArray(), x => (object)x)),  PlotOptionsLine=new PlotOptionsLine{  DataLabels=new PlotOptionsLineDataLabels{Enabled=true,  Overflow="allow",Crop=false, Rotation=270, X=2, Y= -5, Align= HorizontalAligns.Left} } },
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


            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
        }
        protected void populate_month(string day_input)
        {

        }
        protected void populate_year(string day_input, string month_input)
        {

        }

    }
}