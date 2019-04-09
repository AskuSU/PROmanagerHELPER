using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using ScrapySharp.Network;
using System.Text;
using System;

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

        public async Task<WebPage> GetSourceByPageId(string request)
        {
            
            string currentUrl = url.Replace("{CurrentStreet}", WebUtility.UrlEncode(request));
            WebPage source = await browser.NavigateToPageAsync(new Uri( currentUrl));
            
            return source;
        }

        
        
    }
}
