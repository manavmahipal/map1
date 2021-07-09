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
    public partial class state_MO_new : System.Web.UI.Page
    {
        List<decimal> CummCap = new List<Decimal>(); 
        List<string> statelist = new List<string>();
        List<decimal> CurrVClist = new List<decimal>();
        List<decimal> PrevVClist = new List<decimal>();
        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                populate_state();

            }
           // generate_table_chart("Jan-21","19");
        }
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void update_test(object sender, EventArgs e)
        {
            
        }
        protected void state_DDL_change(object sender, EventArgs e)
        {

            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            statelist.Clear();
            CurrVClist.Clear();
            PrevVClist.Clear();



            try
            {
                con.Open();
                string cmdText = "SELECT DISTINCT MONTH(date_from) AS month1,YEAR(date_from) AS month2 FROM `mo_master01` WHERE `StateID`=" + DDL_for_state.SelectedValue.ToString() + " ORDER BY date_from DESC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                DDL_for_month.Items.Clear();
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

                        DDL_for_month.Items.Insert(j, new ListItem(month_string + "-" + dt.Rows[j].ItemArray[1].ToString(), value_string));
                    }

                    DDL_for_month.SelectedIndex = 0;



                    populate_month_filter(DDL_for_state.SelectedValue.ToString(), DDL_for_month.SelectedValue.ToString());
                    // generate_table_chart(dt.Rows[0].ItemArray[0].ToString(), "1");
                }
                else
                {
                    DDL_for_month_filter.Items.Clear();
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

        protected void month_DDL_change(object sender, EventArgs e)
        {

            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            statelist.Clear();
            CurrVClist.Clear();
            PrevVClist.Clear();

            try
            {
                con.Open();
                string cmdText = "SELECT DISTINCT DATE_FORMAT(date_from,'%d-%m-%Y') AS date_from1,DATE_FORMAT(date_to,'%d-%m-%Y') AS date_to,DATE_FORMAT(date_from,'%Y-%m-%d') AS date_from2 FROM `mo_master01` WHERE `StateID`=" + DDL_for_state.SelectedValue.ToString() + " AND `date_from`<LAST_DAY('" + DDL_for_month.SelectedValue.ToString() + "') AND (`date_to`>DATE_SUB('" + DDL_for_month.SelectedValue.ToString() + "',INTERVAL DAYOFMONTH('" + DDL_for_month.SelectedValue.ToString() + "')-1 DAY) OR `date_to` IS NULL) ";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);
                //Literal2.Text += Convert.ToInt32(DDL_for_state.SelectedValue.ToString()) + "----" + DDL_for_month.SelectedValue.ToString();

                DDL_for_month_filter.Items.Clear();
                string to_string = "";
                if (dt.Rows.Count > 0)
                {
                    //DDL_for_month_filter.DataSource = dt;
                    //DDL_for_month_filter.DataBind();
                    //DDL_for_month_filter.SelectedIndex = 0;



                    // populate_month_filter(state_input, DDL_for_month.SelectedValue.ToString());
                    // generate_table_chart(dt.Rows[0].ItemArray[0].ToString(), "1");

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j].ItemArray[1].ToString() == "")
                            to_string = "Present";
                        else
                            to_string = dt.Rows[j].ItemArray[1].ToString();
                        DDL_for_month_filter.Items.Insert(j, new ListItem("From " + dt.Rows[j].ItemArray[0].ToString() + " To " + to_string, dt.Rows[j].ItemArray[2].ToString()));

                        //Literal2.Text += "<br/>" + dt.Rows[j].ItemArray[2].ToString();
                    }

                    DDL_for_month_filter.SelectedIndex = 0;

                    generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
                }
                else
                {
                    DDL_for_month_filter.Items.Insert(0, new ListItem("No data found", "0"));
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
        protected void month_filter_DDL_change(object sender, EventArgs e)
        {

            // generate_table_chart(DDL_for_month.SelectedItem.Value, DDL_for_state.SelectedItem.Value);
            generate_table_chart(DDL_for_state.SelectedValue.ToString(), DDL_for_month_filter.SelectedValue.ToString());
        }
        protected void populate_state()
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            statelist.Clear();
            CurrVClist.Clear();
            PrevVClist.Clear();
            try
            {
                con.Open();
                string cmdText = "SELECT SrNo,StateName FROM state_master ORDER BY SrNo ASC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);



                if (dt.Rows.Count > 0)
                {
                    DDL_for_state.DataSource = dt;
                    DDL_for_state.DataBind();
                    DDL_for_state.SelectedIndex = 0;
                    populate_month(DDL_for_state.SelectedValue.ToString());
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
        protected void populate_month(string state_input)
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            statelist.Clear();
            CurrVClist.Clear();
            PrevVClist.Clear();



            try
            {
                con.Open();
                string cmdText = "SELECT DISTINCT MONTH(date_from) AS month1,YEAR(date_from) AS month2 FROM `mo_master01` WHERE `StateID`=" + Convert.ToInt32(state_input) + " ORDER BY date_from DESC";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                DDL_for_month.Items.Clear();
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
                            value_string = dt.Rows[j].ItemArray[1].ToString()+"-01-01";
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

                        DDL_for_month.Items.Insert(j, new ListItem(month_string + "-" + dt.Rows[j].ItemArray[1].ToString(), value_string));
                    }

                    DDL_for_month.SelectedIndex = 0;



                    populate_month_filter(state_input, DDL_for_month.SelectedValue.ToString());
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

        protected void populate_month_filter(string state_input, string month_input)
        {
            // string str = @"Server=MYSQL5044.site4now.net;Database=db_a70e2a_mydata;Uid=a70e2a_mydata;Pwd=Rimmi@1402";
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            int Cumm = 0;
            CummCap.Clear();
            statelist.Clear();
            CurrVClist.Clear();
            PrevVClist.Clear();

            try
            {
                con.Open();
                string cmdText = "SELECT DISTINCT DATE_FORMAT(date_from,'%d-%m-%Y') AS date_from1,DATE_FORMAT(date_to,'%d-%m-%Y') AS date_to,DATE_FORMAT(date_from,'%Y-%m-%d') AS date_from2 FROM `mo_master01` WHERE `StateID`=" + Convert.ToInt32(state_input) +" AND `date_from`<LAST_DAY('"+ month_input + "') AND (`date_to`>DATE_SUB('" + month_input + "',INTERVAL DAYOFMONTH('" + month_input + "')-1 DAY) OR `date_to` IS NULL) ";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);
               // Literal2.Text += Convert.ToInt32(state_input) + "----" + month_input;

                DDL_for_month_filter.Items.Clear();
                string to_string = "";
                if (dt.Rows.Count > 0)
                {
                    //DDL_for_month_filter.DataSource = dt;
                    //DDL_for_month_filter.DataBind();
                    //DDL_for_month_filter.SelectedIndex = 0;
                    
                    
                    
                    // populate_month_filter(state_input, DDL_for_month.SelectedValue.ToString());
                    // generate_table_chart(dt.Rows[0].ItemArray[0].ToString(), "1");

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j].ItemArray[1].ToString() == "")
                            to_string = "Present";
                        else
                            to_string = dt.Rows[j].ItemArray[1].ToString();
                        DDL_for_month_filter.Items.Insert(j, new ListItem("From "+ dt.Rows[j].ItemArray[0].ToString()+" To "+to_string, dt.Rows[j].ItemArray[2].ToString()));

                       // Literal2.Text +=  "<br/>" + dt.Rows[j].ItemArray[2].ToString();
                    }

                    DDL_for_month_filter.SelectedIndex = 0;
                    generate_table_chart(state_input, DDL_for_month_filter.SelectedValue.ToString());
                }
                else
                {
                    DDL_for_month_filter.Items.Insert(0, new ListItem("No data found", "0"));
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
        protected void generate_table_chart(string state_input, string month_filter_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            decimal Cumm = 0.0m;
            CummCap.Clear();
            statelist.Clear();
            CurrVClist.Clear();
            PrevVClist.Clear();
            string cmdText = "";
            DateTime to_date = Convert.ToDateTime(month_filter_input);
            Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");
            


            try
            {
                con.Open();

                cmdText = "SELECT (ROW_NUMBER() OVER (ORDER BY b.VC)) AS SrNo1, a.StationID AS StationID1, b.StationID AS StationID2, c.display_name AS StationName,"
                    + "a.VC AS VC1, b.VC AS VC2, a.SrNo2 AS Day0, b.SrNo3 AS DAY, (COALESCE(a.SrNo2, 0) - COALESCE(b.SrNo3, 0)) AS Diff_SrNo, (COALESCE(b.VC, 0) - COALESCE(a.VC, 0)) AS diff_VC,"
                    + "c.Utility_subtype AS Type, d.State_"+state_input+ " AS allocation FROM (SELECT(ROW_NUMBER() OVER (ORDER BY d.VC)) AS SrNo2, d.StationID, d.VC FROM mo_master01 d WHERE"
                    + " d.StateID = " + Convert.ToInt32(state_input) + " AND d.date_to = '" + to_date.AddDays(-1).ToString("yyyy-MM-dd") + "' ORDER BY d.VC) a,"
                    + "(SELECT(ROW_NUMBER() OVER (ORDER BY f.VC)) AS SrNo3, f.StationID, f.VC FROM mo_master01 f WHERE"
                    + " f.StateID = " + Convert.ToInt32(state_input) + " AND f.date_from = '" + to_date.ToString("yyyy-MM-dd") + "' ORDER BY f.VC) b, station_master c,allocation_master d"
                    + " WHERE a.StationID = b.StationID AND a.StationID = c.SrNo AND (a.VC > 0 OR b.VC > 0) AND a.StationID=d.StationID";
                    
                   // "SELECT(ROW_NUMBER() OVER(ORDER BY b.VC)) AS SrNo1, a.StationID AS StationID1,b.StationID AS StationID2,c.display_name AS StationName,a.VC AS VC1,b.VC AS VC2,a.SrNo2 AS Day0,b.SrNo3 AS Day, "
                   // + " (COALESCE(a.SrNo2, 0) - COALESCE(b.SrNo3, 0)) AS Diff_SrNo,(COALESCE(b.VC, 0) - COALESCE(a.VC, 0)) AS diff_VC FROM (SELECT(ROW_NUMBER() OVER(ORDER BY d.VC)) AS SrNo2, d.StationID, d.VC, "
                   /// + " FROM mo_master01 d WHERE d.StateID = " + Convert.ToInt32(state_input) + " AND d.date_to = '" + to_date.AddDays(-1).ToString("yyyy-MM-dd") + "' ORDER BY d.VC) a,(SELECT(ROW_NUMBER() OVER(ORDER BY f.VC)) AS SrNo3, f.StationID,f.VC FROM mo_master01 f "
                   /// +" WHERE f.StateID = "+Convert.ToInt32(state_input) +" AND f.date_from = '"+ to_date.ToString("yyyy-MM-dd") + "' ORDER BY f.VC) b,station_master c WHERE a.StationID = b.StationID AND a.StationID=c.SrNo AND (a.VC>0 OR b.VC>0)";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);

                System.Data.DataColumn newColumn = new System.Data.DataColumn("Cumm", typeof(System.Decimal));
                 newColumn.DefaultValue = 0.0;
                dt.Columns.Add(newColumn);

                if (dt.Rows.Count > 0)
                {
                    ltrChart.Text = "";
                    Color[] colors = new Color[dt.Rows.Count + 1];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Literal1.Text += dt.Rows[i].ItemArray[10].ToString();
                        if (dt.Rows[i].ItemArray[10].ToString()=="NTPC")
                            colors[i] = ColorTranslator.FromHtml("#51d1f7");
                        else
                            colors[i] = ColorTranslator.FromHtml("#d877fc");

                          Cumm = Cumm + Convert.ToDecimal(dt.Rows[i].ItemArray[11].ToString());
                         dt.Rows[i][12] = Cumm;
                        if (dt.Rows[i].ItemArray[4].ToString() == "")
                            CurrVClist.Add(0.0m);
                        else
                            CurrVClist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[4].ToString()));

                        if (dt.Rows[i].ItemArray[5].ToString() == "")
                            PrevVClist.Add(0.0m);
                        else
                            PrevVClist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[5].ToString()));

                        statelist.Add(dt.Rows[i].ItemArray[3].ToString());
                         CummCap.Add(Cumm);
                        /////////////////////   monthlist.Add(dt.Rows[i].ItemArray[1].ToString());
                    }
                    testgrid.Visible = true;
                    testgrid.DataSource = dt;
                    testgrid.DataBind();
                    testgrid.HeaderRow.TableSection = TableRowSection.TableHeader;

                    DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
           .SetTitle(new Title { Text = "" })
           .SetOptions(new GlobalOptions { Colors=colors })
           .SetCredits(new Credits { Enabled = false })
           .SetXAxis(new XAxis
           {
               Categories = statelist.ToArray(),
               Labels = new XAxisLabels
               {
                   Rotation = -45,
                   Align = HorizontalAligns.Right,
                   Style = "fontSize: '8px',color:'#55555'"
               },
                LineColor= ColorTranslator.FromHtml("#000000"),
               GridLineColor = ColorTranslator.FromHtml("#000000")
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
                        },
                        GridLineWidth = 0,
                        Opposite = true
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
                    new Series {Color=ColorTranslator.FromHtml("#000000"), YAxis = "1",Type = ChartTypes.Line,Name = "Prev VC",Data =new Data(Array.ConvertAll(CurrVClist.ToArray(), x => (object)x)), PlotOptionsLine =new PlotOptionsLine{ Visible=false,  DataLabels =new PlotOptionsLineDataLabels{Enabled=true,  Overflow="allow",Crop=false, Rotation=270, X=2, Y= -5, Align= HorizontalAligns.Left} } },
                    new Series {PlotOptionsColumn=new PlotOptionsColumn{ ColorByPoint=true }, YAxis = "1",Type = ChartTypes.Line,Name = "Curr VC", Data =new Data(Array.ConvertAll(PrevVClist.ToArray(), x => (object)x)), PlotOptionsLine=new PlotOptionsLine{ DataLabels=new PlotOptionsLineDataLabels{Enabled=true, Overflow="allow",Crop=false, Rotation=270, X=2, Y= -5, Align= HorizontalAligns.Left} } },
                    new Series {Type = ChartTypes.Column, Name = "Cumm. Allocation (MW)", Data =new Data(Array.ConvertAll(CummCap.ToArray(), x => (object)x)),PlotOptionsColumn = new PlotOptionsColumn { Visible=false, ColorByPoint = true,Colors =  colors   }  },
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

        protected string get_imglink(string diff_vc)
        {
            int diff = Convert.ToInt32(diff_vc);
            if (diff < 0)
                return "down.png";
            else if (diff > 0)
                return "up.png";
            else
                return "same.png";

        }
        protected string get_colorlink(string diff_vc)
        {
            int diff = Convert.ToInt32(diff_vc);
            if (diff < 0)
                return "red";
            else if (diff > 0)
                return "green";
            else
                return "blue";

        }
    }
}