using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROmanagerHELPER.CoreRussProfil
{
    interface IParcerSettingsRUS
    {
        string BaseUrl { get; set; }

        string PrefixID { get; set; }

        string StreetPref { get; set; }

        string INNPref { get; set; }

        string PrefixYur { get; set; } 

        string PrefixIp { get; set; }
        string PrefixIsActive { get; set; } 
        
    }
}
