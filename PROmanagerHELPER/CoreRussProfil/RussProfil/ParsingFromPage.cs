using HtmlAgilityPack;
using PROmanagerHELPER.CoreRussProfil.KompanyType;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PROmanagerHELPER.CoreRussProfil.RussProfil
{
    class ParsingFromPage
    {
        public void ParsingFromPageMethod(WebPage document, List<IKOMPANY> MyList)
        {
            var items = document.Html.CssSelect(".company-item");

                foreach (HtmlNode node in items)
                {

                    //Объявим экземпляр Класса Компании
                    KompanyClass record = new KompanyClass();
                    record.Adress = new AdressClass();
                    record.NumberOfEmployees = new EmployeesClass();
                    record.Owner = new OwnerClass();

                    //Поиск названия компании
                    var node2 = node.SelectSingleNode("div [@class='company-item__title']");
                    string Name = HtmlUtilities.DeleteSpaces(HtmlUtilities.ConvertToPlainText(node2.InnerText));

                    //Поиск ID компании
                    string IDValue = node2.SelectSingleNode("a").Attributes["href"].Value;
                    IDValue = (IDValue.Replace("/", "")).Replace("id", "");

                    //поиск Адреса(NotFuulAdress)
                    node2 = node.SelectSingleNode("address [@class='company-item__text']");
                    var Adress = HtmlUtilities.DeleteSpaces(HtmlUtilities.ConvertToPlainText(node2.InnerText));

                    //Поиск ИНН, ОГРН и Владельца
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
                    string INN = mass[(mass.IndexOf("ИНН") + 1)];
                    string OGRN = mass[(mass.IndexOf("ОГРН") + 1)];
                    string TypeOfOwner = "";
                    IOWNER Owner = new OwnerClass();
                    if (mass.IndexOf("ИНН") > 0)
                    {
                        TypeOfOwner = mass[mass.IndexOf("ИНН") - 2];
                        string[] owner = mass[mass.IndexOf("ИНН") - 1].Split();
                        int i = 0;
                        foreach (var item in owner)
                        {

                            switch (i)
                            {
                                case 0:
                                Owner.Surname = owner[i];
                                break;
                                case 1:
                                Owner.Name = owner[i];
                                break;
                                case 2:
                                Owner.MiddleName = owner[i];
                                break;
                            }
                            i++;
                        }
                        if (i < 3)
                        {
                            for (int j = i; j < 3; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                    Owner.Surname = "";
                                    break;
                                    case 1:
                                    Owner.Name = "";
                                    break;
                                    case 2:
                                    Owner.MiddleName = "";
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        TypeOfOwner = "No Data";
                        Owner.Surname = "";
                        Owner.Name = "";
                        Owner.MiddleName = "";
                    }

                    //Передача данных
                    record.Name = Name;
                    record.ID = Convert.ToInt32(IDValue);
                    record.Adress.StatusFullAdress = false;
                    record.Adress.NotFuulAdress = Adress;
                    record.INN = Convert.ToInt64(INN);
                    record.OGRN = Convert.ToInt64(OGRN);
                    record.TypeOfOwner = TypeOfOwner;
                    record.Owner = Owner;
                    MyList.Add(record);
                }            
        }
    }
}
