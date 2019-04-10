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
        ParsingFromPage PagePars; 

        
        public List<IKOMPANY> Parse(WebPage document, HtmlLoaderRUS loaderRUS)
        {
            
            var list = new List<IKOMPANY>();

            if (document.Html.CssSelect(@".main-content.search-result.emptyresult").Count() == 0)
            {

                PagePars = new ParsingFromPage(document, list);

                int a = 0;
                var n = Int32.TryParse("123", out a);
                                
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