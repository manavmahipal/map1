using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

namespace merit
{
    public partial class update_mo1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            workbookProcessing(Server.MapPath(" ") + "\\files_upload\\MarketMinute (2).xlsx");
        }
        private void workbookProcessing(string workbookname)
        {
            //SqlDatabase objdb = new SqlDatabase(OSMC.constring_Property);
            string fullfilename = workbookname;
            XLWorkbook theWorkBook = new XLWorkbook(fullfilename);
            int worksheetcount = theWorkBook.Worksheets.Count;
            foreach (IXLWorksheet theWorkSheet in theWorkBook.Worksheets)
            {
                Label1.Text += "<table>";
                foreach (IXLRow therow in theWorkSheet.Rows())
                {
                    Label1.Text += "<tr>";
                    foreach (IXLCell thecell in therow.Cells())
                    {                        
                                Label1.Text += "<td style=\"border:1px solid #000000;\">" + thecell.Value + "</td>";
                    }
                    /* int i = 0,j=0;
                     foreach (IXLCell thecell in therow.Cells())
                     {
                         if (i == 0)
                         {
                             if (thecell.Value == "")
                             {
                                 j++;
                                 break;
                             }
                             else
                                 Label1.Text += "<td style=\"border:1px solid #000000;\">" + thecell.Value + "</td>";
                         }
                         else if (j == 1)
                             break;
                         else
                             Label1.Text += "<td style=\"border:1px solid #000000;\">" + thecell.Value + "</td>";
                         i++;
                     }*/
                    Label1.Text += "</tr>";
                }
                Label1.Text += "</table>";
            }
        }
    }
}