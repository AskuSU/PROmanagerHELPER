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

        
        public async Task<List<IKOMPANY>> Parse(WebPage document, HtmlLoaderRUS loaderRUS, string request)
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
                List<int> QuantityList = new List<int>(); //кол-во организаций + ИП
                foreach (var item in QuantityString3)
                {
                    int a = 0;
                    if ( Int32.TryParse(item, out a))
                    {
                        QuantityList.Add(a);
                    }
                }                

                if (QuantityList.Count > 1)
                {
                    // Считаем Организации
                    int Page = 0;
                    N = QuantityList[0];
                    while (N > list.Count)
                    {
                        Page++;
                        document = await loaderRUS.GetSourceByPageId(request, Page, 1);
                        PagePars.ParsingFromPageMethod(document, list);
                        if (Page > 9)
                            break;
                    }
                }
                

                ////Считаем первую страницу с организациями
                //PagePars.ParsingFromPageMethod(document, list);

                //int Page = 1;
                //while (N > list.Count)
                //{
                //    Page++;
                //    document = await loaderRUS.GetSourceByPageId(request, Page);
                //    PagePars.ParsingFromPageMethod(document, list);
                //    if (Page > 9)
                //        break;
                //}



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