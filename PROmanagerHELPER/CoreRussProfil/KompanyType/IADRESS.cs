using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROmanagerHELPER.CoreRussProfil.KompanyType
{
    interface IADRESS
    {
        bool StatusFullAdress { set; get; }
        string NotFuulAdress { set; get; }
        int Index { set; get; }
        bool StatIndrex { set; get; }
        string Region { set; get; }
        string City { set; get; }
        bool StatCity { set; get; }
        string Street { set; get; }

    }
}
