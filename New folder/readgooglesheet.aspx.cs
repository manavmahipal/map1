using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mysite
{
    public partial class readgooglesheet : System.Web.UI.Page
    {
     //   static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "Quickstart";

        protected void Page_Load(object sender, EventArgs e)
        {
           /* UserCredential credential;

            using (var stream =
                new FileStream(Server.MapPath("~/client_secret_681757612460-l9b9928s3lkt42pnl235ckor6pr8ai0r.apps.googleusercontent.com.json"), FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = Server.MapPath("~/token.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
                Label1.Text= "Credential file saved to: " + credPath;
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1rugdF9A5ewTbXbCE4mF9x2GNFS4U3_OceTIqrbU_NOw";
            String range = "rihand";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit

            try
            {
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;
                if (values != null && values.Count > 0)
                {
                    Console.WriteLine("Name, Major");
                    foreach (var row in values)
                    {
                        // Print columns A and E, which correspond to indices 0 and 4.
                        Console.WriteLine("{0}, {1}", row[0], row[4]);
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
                Console.Read();
            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message;
            }


            */
            

        }
    }
}