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
using System.Text.RegularExpressions;

namespace merit
{
    public partial class meritindiadatacapture : System.Web.UI.Page
    {
        string str = "Server=127.0.0.1;Uid=root;Database=commercial;Pwd=Manav@009944";
        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(str);
            DataTable dt = new DataTable();
            dt.Columns.Add("SrNo", typeof(int));
            dt.Columns.Add("Dem_met", typeof(decimal));
            dt.Columns.Add("T_gen", typeof(decimal));
            dt.Columns.Add("G_gen", typeof(decimal));
            dt.Columns.Add("N_gen", typeof(decimal));
            dt.Columns.Add("H_gen", typeof(decimal));
            dt.Columns.Add("R_gen", typeof(decimal));
            dt.Columns.Add("Date_data", typeof(string));
            dt.Columns.Add("Time_data", typeof(string));
            dt.Columns.Add("Blk", typeof(int));
            System.Net.WebClient wc = new System.Net.WebClient();
            string webData = wc.DownloadString("http://meritindia.in/Dashboard/BindAllIndiaMap");
            //Regex.Split(webData, "xx");
            try
            {
                con.Open();
                //string webData = System.Text.Encoding.UTF8.GetString(wc.DownloadData("http://meritindia.in/Dashboard/BindAllIndiaMap"));
                //Literal1.Text = Regex.Replace(webData, "<.*?>", String.Empty);
                string teststring = webData.Replace(",", "");
            teststring = Regex.Replace(teststring, "<.*?>", "");
           // Literal1.Text = teststring;
            string[] digits = Regex.Split(teststring, @"\D+"); //teststring.Split(' ');
            if(digits.Length>0)
            {
                //for(int i=0;i<digits.Length;i++)
                //{
                //    Literal1.Text +="<br/>"+i+"   -   "+digits[i];
                //}
                //string time_data= generate_blk()
                dt.Rows.Add(1, Convert.ToDecimal(digits[1]), Convert.ToDecimal(digits[2]), Convert.ToDecimal(digits[3]), Convert.ToDecimal(digits[4]), Convert.ToDecimal(digits[5]), Convert.ToDecimal(digits[6]), DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm"),generate_blk());

                string cmdText2 = "INSERT INTO meritindia_allindia (Dem_met, T_gen, G_gen, N_gen, H_gen, R_gen, Date_data, Time_data, Blk) VALUES ("+ Convert.ToDecimal(digits[1])+","
                    +Convert.ToDecimal(digits[2]) + "," + Convert.ToDecimal(digits[3]) + "," + Convert.ToDecimal(digits[4]) + "," + Convert.ToDecimal(digits[5]) + "," + Convert.ToDecimal(digits[6]) 
                    + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm") + "'," + generate_blk() + ")";
                //MySqlCommand cmd = new MySqlCommand(cmdText, con);
                //Literal1.Text += cmdText2;

                MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                adapter2.InsertCommand = new MySqlCommand(cmdText2, con);
                adapter2.InsertCommand.ExecuteNonQuery();
            }
            //if(dt.Rows.Count>0)
            //{
            //    testgrid.DataSource = dt;
            //    testgrid.DataBind();
            //    testgrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}




            
            DataTable dt2 = new DataTable();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("State", typeof(string));
            dt1.Columns.Add("Demand", typeof(string));
            dt1.Columns.Add("ISGS", typeof(string));
            dt1.Columns.Add("Import", typeof(string));
            dt1.Columns.Add("Date_data", typeof(string));
            dt1.Columns.Add("Time_data", typeof(string));
            dt1.Columns.Add("Blk", typeof(int));
            
                string cmdText = "SELECT * FROM meritindia_statecodes ORDER BY statecode";
                MySqlCommand cmd = new MySqlCommand(cmdText, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand(cmdText, con);
                adapter.Fill(dt2);

                

                if (dt2.Rows.Count > 0)
                {
                    

                    for(int i=0; i< dt2.Rows.Count; i++)
                    {
                        System.Net.WebClient wc1 = new System.Net.WebClient();
                        string webData1 = wc1.DownloadString("http://meritindia.in/StateWiseDetails/BindCurrentStateStatus?StateCode=" + dt2.Rows[i].ItemArray[1].ToString());
                        //Regex.Split(webData, "xx");
                        webData1 = webData1.Replace("null", "\"0\"");
                        //Literal1.Text += "<br/><br/>"+ webData1;// + jsonStringArray[0];
                        //string webData = System.Text.Encoding.UTF8.GetString(wc.DownloadData("http://meritindia.in/Dashboard/BindAllIndiaMap"));
                        //Literal1.Text = Regex.Replace(webData, "<.*?>", String.Empty);
                        //string teststring1 = webData.Replace(",", "");
                        //teststring = Regex.Replace(teststring, "<.*?>", "");
                        //string jsonStringArray = webData1.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "");

                        string[] jsonStringArray = Regex.Split(webData1.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"Demand\":\"", "").Replace("ISGS\":\"", "").Replace("ImportData\":\"", ""), "\",\"");
                        //Literal1.Text += "-----" + jsonStringArray.Length;
                        if (jsonStringArray.Length>2) 
                        {
                            //Literal1.Text += "<br/><br/>" + jsonStringArray[0].Replace(",", "").Replace("\"", "") + "<br/><br/>" + jsonStringArray[1].Replace(",", "").Replace("\"", "") + "<br/><br/>" + jsonStringArray[2].Replace(",", "").Replace("\"", "");
                            dt1.Rows.Add(dt2.Rows[i].ItemArray[1].ToString(), jsonStringArray[0].Replace(",", "").Replace("\"", ""), jsonStringArray[1].Replace(",", "").Replace("\"", ""), jsonStringArray[2].Replace(",", "").Replace("\"", ""), DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm"), generate_blk());

                            string cmdText1 = "INSERT INTO meritindia_statedata (state, demand, isgs, import, date_data, time_data, blk) VALUES ('" + dt2.Rows[i].ItemArray[1].ToString()+"',"
                                + Convert.ToDecimal(jsonStringArray[0].Replace(",", "").Replace("\"", ""))+","+ Convert.ToDecimal(jsonStringArray[1].Replace(",", "").Replace("\"", ""))+"," + Convert.ToDecimal(jsonStringArray[2].Replace(",", "").Replace("\"", ""))
                                +",'"+ DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToString("HH:mm") + "'," + generate_blk()+")";
                            //MySqlCommand cmd = new MySqlCommand(cmdText, con);
                            //Literal1.Text += cmdText1;

                            MySqlDataAdapter adapter1 = new MySqlDataAdapter();
                            adapter1.InsertCommand = new MySqlCommand(cmdText1, con);
                            adapter1.InsertCommand.ExecuteNonQuery();


                        } //string[] jsonStringArray = Regex.Split(webData1, "},{");
                        //Literal1.Text += "<br/><br/>";// + jsonStringArray[0];
                    }




                    //testgrid1.DataSource = dt1;
                    //testgrid1.DataBind();




                }
                else
                {
                    Console.WriteLine("No data Found");
                    // testgrid.Visible = false;
                }

            }
            catch (MySqlException err)
            {
                Console.WriteLine(err);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }











        }

        protected int generate_blk()
        {
            string[] time_data = DateTime.Now.ToString("HH:mm").Split(':');

            if (time_data[0] == "00")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 1;
                        else
                            return 2;
                    }
                    else
                        return 3;
                }
                else
                    return 4;
            }
            else if (time_data[0] == "01")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 5;
                        else
                            return 6;
                    }
                    else
                        return 7;
                }
                else
                    return 8;
            }
            else if (time_data[0] == "02")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 9;
                        else
                            return 10;
                    }
                    else
                        return 11;
                }
                else
                    return 12;
            }
            else if (time_data[0] == "03")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 13;
                        else
                            return 14;
                    }
                    else
                        return 15;
                }
                else
                    return 16;
            }
            else if (time_data[0] == "04")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 17;
                        else
                            return 18;
                    }
                    else
                        return 19;
                }
                else
                    return 20;
            }
            else if (time_data[0] == "05")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 21;
                        else
                            return 22;
                    }
                    else
                        return 23;
                }
                else
                    return 24;
            }
            else if (time_data[0] == "06")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 25;
                        else
                            return 26;
                    }
                    else
                        return 27;
                }
                else
                    return 28;
            }
            else if (time_data[0] == "07")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 29;
                        else
                            return 30;
                    }
                    else
                        return 31;
                }
                else
                    return 32;
            }
            else if (time_data[0] == "08")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 33;
                        else
                            return 34;
                    }
                    else
                        return 35;
                }
                else
                    return 36;
            }
            else if (time_data[0] == "09")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 37;
                        else
                            return 38;
                    }
                    else
                        return 39;
                }
                else
                    return 40;
            }
            else if (time_data[0] == "10")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 41;
                        else
                            return 42;
                    }
                    else
                        return 43;
                }
                else
                    return 44;
            }
            else if (time_data[0] == "11")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 45;
                        else
                            return 46;
                    }
                    else
                        return 47;
                }
                else
                    return 48;
            }
            else if (time_data[0] == "12")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 49;
                        else
                            return 50;
                    }
                    else
                        return 51;
                }
                else
                    return 52;
            }
            else if (time_data[0] == "13")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 53;
                        else
                            return 54;
                    }
                    else
                        return 55;
                }
                else
                    return 56;
            }
            else if (time_data[0] == "14")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 57;
                        else
                            return 58;
                    }
                    else
                        return 59;
                }
                else
                    return 60;
            }
            else if (time_data[0] == "15")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 61;
                        else
                            return 62;
                    }
                    else
                        return 63;
                }
                else
                    return 64;
            }
            else if (time_data[0] == "16")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 65;
                        else
                            return 66;
                    }
                    else
                        return 67;
                }
                else
                    return 68;
            }
            else if (time_data[0] == "17")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 69;
                        else
                            return 70;
                    }
                    else
                        return 71;
                }
                else
                    return 72;
            }
            else if (time_data[0] == "18")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 73;
                        else
                            return 74;
                    }
                    else
                        return 75;
                }
                else
                    return 76;
            }
            else if (time_data[0] == "19")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 77;
                        else
                            return 78;
                    }
                    else
                        return 79;
                }
                else
                    return 80;
            }
            else if (time_data[0] == "20")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 81;
                        else
                            return 82;
                    }
                    else
                        return 83;
                }
                else
                    return 84;
            }
            else if (time_data[0] == "21")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 85;
                        else
                            return 86;
                    }
                    else
                        return 87;
                }
                else
                    return 88;
            }
            else if (time_data[0] == "22")
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 89;
                        else
                            return 90;
                    }
                    else
                        return 91;
                }
                else
                    return 92;
            }
            else 
            {
                if (Convert.ToInt32(time_data[1]) < 45)
                {
                    if (Convert.ToInt32(time_data[1]) < 30)
                    {
                        if (Convert.ToInt32(time_data[1]) < 15)
                            return 93;
                        else
                            return 94;
                    }
                    else
                        return 95;
                }
                else
                    return 96;
            }
            //return 1;// time_data[0] + "----" + time_data[1];


        }
        }
}