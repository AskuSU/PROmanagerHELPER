using HtmlAgilityPack;
using PROmanagerHELPER.CoreRussProfil.KompanyType;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PROmanagerHELPER.CoreRussProfil.RussProfil
{
    class RussProfilParser : IParcerRUS<List<IKOMPANY>>
    {
        ParsingFromPage PagePars = new ParsingFromPage(); 

        
        public List<IKOMPANY> Parse(WebPage document, HtmlLoaderRUS loaderRUS)
        {
            
            var list = new List<IKOMPANY>();

            if (document.Html.CssSelect(@".main-content.search-result.emptyresult").Count() == 0)
            {

                //Определяем кол-во организаций по запросу
                var StartNode = document.Html.SelectSingleNode("//h1");
                var SearchString = document.Html.SelectSingleNode("//span [@class='break-long-text']");

                string[] QuantityString = StartNode.InnerText.Replace("&quot;", "").Split();
                string[] QuantityString2 = SearchString.InnerText.Split();

                string[] QuantityString3 = QuantityString.Except<string>(QuantityString2).ToArray<string>();
                int N = 0; //кол-во организаций
                foreach (var item in QuantityString3)
                {
                    int a = 0;
                    if ( Int32.TryParse(item, out a))
                    {
                        N = a;
                    }
                }                

                //Считаем первую страницу с организациями
                PagePars.ParsingFromPageMethod(document, list);

                while (N > list.Count)
                {
                    document = await loaderRUS.GetSourceByPageId("");
                    PagePars.ParsingFromPageMethod(); 
                }

                
                                
                return list;
            }
            else
            {
                MessageBox.Show("Поиск дал 0 результатов, измените запрос!", "Нет организаций", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

    }
}