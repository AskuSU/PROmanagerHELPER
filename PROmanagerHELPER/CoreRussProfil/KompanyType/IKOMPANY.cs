using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROmanagerHELPER.CoreRussProfil.KompanyType
{
    interface IKOMPANY
    {
        string Name { get; set; }
        string FuulName { get; set; }
        IADRESS Adress { get; set; }
        IEMPLOYEES NumberOfEmployees { get; set; }
        Int64 INN { get; set; }
        Int64 OGRN { get; set; }
        int KPP { get; set; }
        Int64 ID { get; set; }
        string TypeOfOwner { get; set; }
        IOWNER Owner { get; set;}
        bool IP { get; set; }

    }
}
