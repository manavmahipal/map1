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
    public partial class testhighchart : System.Web.UI.Page
    {
        public string chartData1
        {
            get;
            set;
        }

        public string chartData3
        {
            get;
            set;
        }

        public string chartData5
        {
            get;
            set;
        }

        List<decimal> CummCap = new List<decimal>();
        List<string> stationlist = new List<string>();
        List<decimal> VClist = new List<decimal>();
        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  Chart1.Visible = ddlCountries.SelectedValue != "";



                MySqlConnection con = new MySqlConnection(str);
                DataTable dt = new DataTable();
                decimal Cumm = 0.0000m;
                CummCap.Clear();
                stationlist.Clear();
                VClist.Clear();
                try
                {
                    con.Open();
                    string cmdText = "SELECT a.VC as VC,b.display_name as station,b.Utility_subtype as ntpc,c.state_19 as allocation FROM mo_master a, station_master b, allocation_master c WHERE a.StationID=b.SrNo AND a.StationID=c.StationID AND a.month='Jan-21' AND (a.VC <> 0 OR a.VC IS NOT NULL) AND a.VC < 10 AND a.StateID=19 AND c.state_19 <> 0 ORDER BY VC ASC ";
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
                chartData1 = jss1.Serialize(VClist);


                JavaScriptSerializer jss3 = new JavaScriptSerializer();
                chartData3 = jss3.Serialize(stationlist);


                JavaScriptSerializer jss5 = new JavaScriptSerializer();
                chartData5 = jss5.Serialize(CummCap);







                //     JavaScriptSerializer jss1 = new JavaScriptSerializer();
                // chartData1 = data2; //jss1.Serialize(data2);
                //      Label1.Text = chartData + "<br/>" + chartData1 + "<br/><br/><br/>" + dt.Rows.Count;
                //     Label1.Text += "<br/><br/><br/><br/>" + Total_DC+"<br/>" + Total_SG + "<br/>" + Thermal_DC + "<br/>" + Thermal_SG + "<br/>" + Total_DC1 + "<br/>" + Total_SG1 + "<br/>" + Thermal_DC1 + "<br/>" + Thermal_SG1;
            }
        }
    }
}