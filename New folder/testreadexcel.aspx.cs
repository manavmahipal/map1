using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace merit
{
    public partial class testreadexcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filePath = Server.MapPath(" ") + "\\files_upload\\ntpc share.xlsx";

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                Literal1.Text += "good<br/>";
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                bool firstRow = true;
                int row1 = 0;
                foreach (IXLRow row in workSheet.Rows())
                {
                    Literal1.Text += "<br/>row " + row1;
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        int col = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            Literal1.Text += "<br/>column "+col+" : "+ cell.Value.ToString();
                            dt.Columns.Add(cell.Value.ToString());
                            col++;
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        Literal1.Text += "<br/>"+row.Cells();
                        foreach (IXLCell cell in row.Cells())
                        {
                            Literal1.Text += "     [" + (dt.Rows.Count - 1)+"]["+i+"] : "+ cell.Value.ToString();
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                    row1++;
                }

                Literal1.Text += "<br/><br/>" + dt.Rows.Count;
                testgrid.DataSource = dt;
                testgrid.DataBind();
            }
        }
    }
}