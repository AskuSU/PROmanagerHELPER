using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROmanagerHELPER.CoreRussProfil.KompanyType
{
    class AdressClass : IADRESS
    {
        public bool StatusFullAdress { get ; set ; }
        public string NotFuulAdress { get ; set ; }
        public int Index { get ; set ; }
        public bool StatIndrex { get ; set ; }
        public string Region { get ; set ; }
        public string City { get ; set ; }
        public bool StatCity { get ; set ; }
        public string Street { get ; set ; }
    }
}
