using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace HabraHabr.Core.Habra
{
    class HtmlLoader
    {
        readonly HttpClient client;

        readonly string url;

        public HtmlLoader(IParcerSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}/{settings.Prefix}/";
            //url = $"{settings.BaseUrl}/id/8548895";
        }

        public async Task<string> GetSourceByPageId(int id)
        {
            var currentUrl = url.Replace("{CurrentId}", id.ToString());
            var responce = await client.GetAsync(currentUrl);
            string source = null;

            if (responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                source = await responce.Content.ReadAsStringAsync();
            }

            return source;
        } 
    }
}
