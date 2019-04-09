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
        public List<IKOMPANY> Parse(WebPage document)
        {

            var list = new List<IKOMPANY>();
            
            if (document.Html.CssSelect(@".main-content.search-result.emptyresult").Count() == 0)
            {

                var items = document.Html.CssSelect(".company-item");

                foreach (HtmlNode node in items)
                {

                    //Объявим экземпляр Класса Компании
                    KompanyClass record = new KompanyClass();
                    record.Adress = new AdressClass();
                    record.NumberOfEmployees = new EmployeesClass();           

                    //Поиск названия компании
                    var node2 = node.SelectSingleNode("div [@class='company-item__title']");
                    string Name = HtmlUtilities.DeleteSpaces(HtmlUtilities.ConvertToPlainText(node2.InnerText));
                    
                    //Поиск ID компании
                    string IDValue = node2.SelectSingleNode("a").Attributes["href"].Value;
                    IDValue = (IDValue.Replace("/", "")).Replace("id", "");
                    
                    //поиск Адреса(NotFuulAdress)
                    node2 = node.SelectSingleNode("address [@class='company-item__text']");
                    var Adress = HtmlUtilities.DeleteSpaces(HtmlUtilities.ConvertToPlainText(node2.InnerText));
                    
                    //Поиск ИНН и ОГРН
                    var node3 = node.SelectNodes("div [@class='company-item-info']");
                    string data = null;
                    foreach (var item in node3)
                    {
                        data += item.InnerText;
                    }
                    string[] source = data.Split(new char[] { '\n' });                                            
                    List<string> mass = new List<string>();
                    foreach (var item in source)
                    {
                        if (item.Trim() != "")
                            mass.Add(item.Trim());               
                    }                    
                    string INN = mass[(mass.IndexOf("ИНН")+1)];
                    string OGRN = mass[(mass.IndexOf("ОГРН") + 1)];

                    //Передача данных
                    record.Name = Name;
                    record.ID = Convert.ToInt32(IDValue);
                    record.Adress.StatusFullAdress = false;
                    record.Adress.NotFuulAdress = Adress;
                    record.INN = Convert.ToInt64(INN);
                    record.OGRN = Convert.ToInt64(OGRN);
                    list.Add(record);

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