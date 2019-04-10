using ScrapySharp.Network;

namespace PROmanagerHELPER.CoreRussProfil
{
    interface IParcerRUS<T> where T : class
    {
        T Parse(WebPage document, HtmlLoaderRUS loaderRUS);
    }
}
