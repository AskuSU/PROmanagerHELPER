using ScrapySharp.Network;
using System.Threading.Tasks;

namespace PROmanagerHELPER.CoreRussProfil
{
    interface IParcerRUS<T> where T : class
    {
        Task<T> Parse(WebPage document, HtmlLoaderRUS loaderRUS, string request);
    }
}
