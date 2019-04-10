using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROmanagerHELPER.CoreRussProfil.KompanyType
{
    internal class KompanyClass :  IKOMPANY
    {
        public string Name { get ; set ; }
        public string FuulName { get ; set ; }        
        public long INN { get ; set ; }
        public long OGRN { get ; set ; }
        public int KPP { get ; set ; }
        public int ID { get ; set ; }
        public IADRESS Adress { get ; set ; }
        public IEMPLOYEES NumberOfEmployees { get ; set ; }
        public string TypeOfOwner { get ; set ; }
        public IOWNER Owner { get ; set ; }
    }
}
