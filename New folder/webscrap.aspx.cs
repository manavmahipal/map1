using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;

namespace mysite
{
    public partial class webscrap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			HtmlDocument htmlDoc = new HtmlDocument();
			string url = "http://meritindia.in/Dashboard/BindAllIndiaMap";
			//string url = "https://www.thestar.com/";
			//string url = "https://www.c-sharpcorner.com/UploadFile/mahesh/split-string-in-C-Sharp/";
			//string url = "https://www.google.com/search?q=anchor+tags+html&oq=anchor+tags";
			string urlResponse = URLRequest(url);

			//Convert the Raw HTML into an HTML Object
			htmlDoc.LoadHtml(urlResponse);

			//Find all title tags in the document
			/*
            <head>
                <title>Page Title</title>
            </head>
             */
			var titleNodes = htmlDoc.DocumentNode.SelectNodes("//title");
			string tableee = Regex.Replace(urlResponse, "<.*?>", String.Empty);
			tableee = Regex.Replace(tableee, @"&nbsp;MW", "");
			tableee = Regex.Replace(tableee, @"\s+", " ");
			tableee = Regex.Replace(tableee, @",", "");
			//tableee=Regex.Replace(tableee, @"[^0-9]+", ""); 
			string[] testyy = tableee.Split(' ');
			List<int> AgeList = new List<int>();
			for (int kk = 1; kk < (testyy.Length - 1); kk++)
			{
				///////////Console.WriteLine(testyy[kk]);
				bool isIntString = testyy[kk].All(char.IsDigit);
				if (isIntString)
				{
					AgeList.Add(Convert.ToInt32(testyy[kk]));
				}
				/////////////Console.WriteLine(isIntString);
			}


			foreach (int Age in AgeList)
			{
				Label1.Text+=Age;
			}
		}
		static string URLRequest(string url)
		{
			// Prepare the Request
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

			// Set method to GET to retrieve data
			request.Method = "GET";
			request.Timeout = 18000; //60 second timeout
			//request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)";
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
			request.ContentType = "application/x-www-form-urlencoded";
			request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			//request.AcceptEncoding = "gzip,deflate,sdch";
			//request.AcceptLanguage = "en-GB,en-US;q=0.8,en;q=0.6";
			//request.AcceptCharset = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
			string responseContent = null;

			// Get the Response
			using (WebResponse response = request.GetResponse())
			{
				// Retrieve a handle to the Stream
				using (Stream stream = response.GetResponseStream())
				{
					// Begin reading the Stream
					using (StreamReader streamreader = new StreamReader(stream))
					{
						// Read the Response Stream to the end
						responseContent = streamreader.ReadToEnd();
					}
				}
			}

			return (responseContent);
		}
	}
}