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
    public partial class test_highcharts : System.Web.UI.Page
    {
        List<int> CummCap = new List<Int32>();
        List<string> stationlist = new List<string>();
        List<string> monthlist = new List<string>();
        List<decimal> VClist = new List<decimal>();
        List<decimal> ECRlist = new List<decimal>();
        string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";

        protected void Page_Load(object sender, EventArgs e)
        {
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                 .SetTitle(new Title { Text = "Combination chart" })
                 .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
                 .SetXAxis(new XAxis { Categories = new[] { "Apples", "Oranges", "Pears", "Bananas", "Plums" } })
                 .SetTooltip(new Tooltip { Formatter = "TooltipFormatter" })
                 .AddJavascripFunction("TooltipFormatter",
                     @"var s;
                    if (this.point.name) { // the pie chart
                       s = ''+
                          this.point.name +': '+ this.y +' fruits';
                    } else {
                       s = ''+
                          this.x  +': '+ this.y;
                    }
                    return s;")
                 .SetLabels(new Labels
                 {
                     Items = new[]
                     {
                        new LabelsItems
                        {
                            Html = "Total fruit consumption",
                            Style = "left: '40px', top: '8px', color: 'black'"
                        }
                     }
                 })
                 .SetPlotOptions(new PlotOptions
                 {
                     Pie = new PlotOptionsPie
                     {
                         Center = new[] { new PercentageOrPixel(100), new PercentageOrPixel(80) },
                         Size = new PercentageOrPixel(60),
                         ShowInLegend = false,
                         DataLabels = new PlotOptionsPieDataLabels { Enabled = false }
                     }
                 })
                 .SetSeries(new[]
                 {
                    new Series
                    {
                        Type = ChartTypes.Column,
                        Name = "Jane",
                        Data = new Data(new object[] { 1, 2, 1, 3, 4 }),
                        PlotOptionsColumn = new PlotOptionsColumn { ColorByPoint = true },
                        Color = System.Drawing.Color.FromName("Highcharts.getOptions().colors[5]")
                    },
                    new Series
                    {
                        Type = ChartTypes.Column,
                        Name = "John",
                        Data = new Data(new object[] { 2, 3, 5, 7, 6 })
                    },
                    new Series
                    {
                        Type = ChartTypes.Column,
                        Name = "Joe",
                        Data = new Data(new object[] { 4, 3, 3, 9, 0 })
                    },
                    new Series
                    {
                        Type = ChartTypes.Spline,
                        Name = "Average",
                        Data = new Data(new object[] { 3, 2.67, 3, 6.33, 3.33 })
                    },
                    new Series
                    {
                        Type = ChartTypes.Pie,
                        Name = "Total consumption",
                        Data = new Data(new[]
                        {
                            new Point
                            {
                                Name = "Jane",
                                Y = 13,
                                Color = System.Drawing.Color.FromName("Highcharts.getOptions().colors[0]")
                            },
                            new Point
                            {
                                Name = "John",
                                Y = 23,
                                Color = System.Drawing.Color.FromName("Highcharts.getOptions().colors[1]")
                            },
                            new Point
                            {
                                Name = "Joe",
                                Y = 19,
                                Color = System.Drawing.Color.FromName("Highcharts.getOptions().colors[2]")
                            }
                        }
                            )
                    }
                 });

            ltrChart.Text = chart.ToHtmlString();
        }

    }
}