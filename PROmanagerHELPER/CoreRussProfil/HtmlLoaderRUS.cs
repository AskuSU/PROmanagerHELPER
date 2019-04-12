using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using ScrapySharp.Network;
using System.Text;
using System;
using System.Windows.Forms;

namespace PROmanagerHELPER.CoreRussProfil
{
    class HtmlLoaderRUS
    {
        readonly ScrapingBrowser browser;

        readonly string url;

        public HtmlLoaderRUS(IParcerSettingsRUS settings)
        {
            browser = new ScrapingBrowser();
            browser.Encoding = Encoding.UTF8;
            url = $"{settings.BaseUrl}{settings.StreetPref}";
            
        }

        public async Task<WebPage> GetSourceByPageId(string request, int Page)
        {

            string currentUrl = url.Replace("{CurrentStreet}", WebUtility.UrlEncode(request));
            if (Page > 1)
            {
                currentUrl = currentUrl.Replace("{CurrentPage}", $"/{Page.ToString()}");
            }
            else
            {
                currentUrl = currentUrl.Replace("{CurrentPage}", "");
            }

            try
            {
                WebPage source = await browser.NavigateToPageAsync(new Uri(currentUrl));
                return source;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString() ,$"Возникла ошибка на {Page.ToString()} странице", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
            
            
        }

        
        
    }
}
