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
    public partial class website_curr_ECR : System.Web.UI.Page
    {
        public string chartData1
        {
            get;
            set;
        }

        public string chartData2
        {
            get;
            set;
        }

        public string chartData3
        {
            get;
            set;
        }
        public string chartData4
        {
            get;
            set;
        }

        List<decimal> ecrlist = new List<decimal>();
        List<string> stationlist = new List<string>();
        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populate_ecr("2021-05-02");
            }
        }
       
        

        protected void populate_ecr(string date_input)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();

            ecrlist.Clear();
            stationlist.Clear();
            string cmdText = "";
            //DateTime to_date = Convert.ToDateTime(month_filter_input);
            // Literal1.Text = "<br/><br/>"+state_input + "****" + month_filter_input+"+++++"+ to_date.AddDays(-1).ToString("yyyy-MM-dd");



            try
            {
                con.Open();
                
                    cmdText = "SELECT b.StationName AS station,a.ECR AS ECR FROM ecr_master01 a,station_master b  WHERE Date_from<'"+date_input+ "' AND (Date_to>'" + date_input + "' OR Date_to IS NULL) AND a.StationID=b.SrNo ORDER BY ECR ASC;";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        stationlist.Add(dt.Rows[i].ItemArray[0].ToString());

                        ecrlist.Add(Convert.ToDecimal(dt.Rows[i].ItemArray[1].ToString()));
                        //statelist.Add(dt.Rows[i].ItemArray[3].ToString());
                    }
                    
                }
                else
                {
                    Literal1.Text = "No data Found";
                    //testgrid.Visible = false;
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
            chartData1 = jss1.Serialize(stationlist);

            JavaScriptSerializer jss2 = new JavaScriptSerializer();
            chartData2 = jss2.Serialize(ecrlist);

        }
    }
}