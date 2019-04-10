using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROmanagerHELPER.CoreRussProfil.KompanyType
{
    interface IOWNER
    {
        string Surname { set; get; }
        string Name { set; get; }
        string MiddleName { set; get; }
    }
}
