using AngleSharp.Html.Dom;


namespace HabraHabr.Core
{
    interface IParcer<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
