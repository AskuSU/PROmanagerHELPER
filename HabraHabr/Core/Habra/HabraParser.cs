using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace HabraHabr.Core.Habra
{
    class HabraParser : IParcer<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {

            var list = new List<string>();
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));
            //var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("inline-list__item-link hub-link "));

            foreach (var item in items)
            {
                list.Add(item.TextContent);

            }

            return list.ToArray();
        }
    }
}
