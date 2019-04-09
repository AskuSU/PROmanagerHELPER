using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;


namespace ScrapySharpParser
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        public class Record
        {
            public string Name { get; set; }
            public string company { get; set; }
            public string street { get; set; }
        }

        

        

        private async void Button1_Click(object sender, EventArgs e)
        {
            string n = "\n";

            //Создать веб-браузер
            ScrapingBrowser browser = new ScrapingBrowser();
            //Загрузить веб-страницу
            browser.Encoding = Encoding.UTF8;
            string url = WebUtility.UrlEncode("Федеративный проспект 20");
            WebPage page = await browser.NavigateToPageAsync(new Uri("https://www.rusprofile.ru/search?query="+url));


            //PageWebForm form = page.FindFormById("sb_form");
            //form["q"] = "scrapysharp";
            //form.Method = HttpVerb.Get;
            //WebPage resultsPage = form.Submit();

            //HtmlNode[] resultsLinks = resultsPage.Html.CssSelect("div.sb_tlst h3 a").ToArray();

            //WebPage blogPage = resultsPage.FindLinks(By.Text("romcyber blog | Just another WordPress site")).Single().Click();


            var te = page.Html.CssSelect(".company-item");


            List<Record> lstRecords = new List<Record>();
            foreach (HtmlNode node in te)
            {
                Record record = new Record();
                foreach (HtmlNode node2 in node.SelectNodes("div [@class='company-item__title']"))
                {
                    string attributeValue = node2.GetAttributeValue("company-item__title", "");
                    
                        record.Name = node2.InnerText;
                    
                }
                lstRecords.Add(record);
                richTextBox1.Text += HtmlUtilities.DeleteSpaces(HtmlUtilities.ConvertToPlainText(record.Name))+n;
            }


            //foreach (HtmlNode node in te)
            //{

            //    var a = node.SelectSingleNode("div.company-item__title > a");
            //    //richTextBox1.Text +=  node.SelectSingleNode("div.company-item__title > a").InnerText.ToString();

            //}

            


        }
    }
}
