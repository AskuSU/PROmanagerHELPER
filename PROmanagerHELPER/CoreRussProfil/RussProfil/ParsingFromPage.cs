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
                    string Name = HtmlUtilities.DeleteSpacesInWord(HtmlUtilities.DeleteSpaces(HtmlUtilities.ConvertToPlainText(node2.InnerText)));

                    //Поиск ID компании и определение ИП
                    string IDValue = node2.SelectSingleNode("a").Attributes["href"].Value;
                    //IDValue = (IDValue.Replace("/", "")).Replace("id", "");
                    string[] IDValueMas = IDValue.Split(new char[] { '/' });
                    IDValue = IDValueMas[2];
                    if (IDValueMas[1] == "ip")
                    {
                        record.IP = true;
                    }

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
                    string INN = "";
                    if (mass.IndexOf("ИНН") >= 0)
                    {
                        INN = mass[(mass.IndexOf("ИНН") + 1)];
                    }else
                    {
                        INN = "0";
                    }

                    string OGRN = "";
                    if (record.IP)
                    {
                        if (mass.IndexOf("ОГРНИП") >= 0)
                        {
                            OGRN = mass[(mass.IndexOf("ОГРНИП") + 1)];
                        }
                        else
                        {
                            OGRN = "0";
                        }                        
                    }
                    else
                    {
                        if (mass.IndexOf("ОГРН") >= 0)
                        {
                            OGRN = mass[(mass.IndexOf("ОГРН") + 1)];
                        }
                        else
                        {
                            OGRN = "0";
                        }                    
                    }
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
                        if (record.IP)
                        {
                        var person = Name.Split();
                        List<string> personList = new List<string>();
                        foreach (var item in person)
                        {
                            if (item != "")
                                personList.Add(item);
                        }

                        int i = 0;
                        foreach (var item in personList)
                        {

                            switch (i)
                            {
                                case 1:
                                    Owner.Surname = item;
                                    break;
                                case 2:
                                    Owner.Name = item;
                                    break;
                                case 3:
                                    Owner.MiddleName = item;
                                    break;
                                case 0:
                                    TypeOfOwner = item;
                                    break;
                            }
                            i++;
                        }
                        if (i < 4)
                        {
                            for (int j = i; j < 4; j++)
                            {
                                switch (j)
                                {
                                    case 1:
                                        Owner.Surname = "";
                                        break;
                                    case 2:
                                        Owner.Name = "";
                                        break;
                                    case 3:
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
                    }

                    //Передача данных
                    record.Name = Name;
                    record.ID = Convert.ToInt64(IDValue);
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
