
namespace PROmanagerHELPER.CoreRussProfil.RussProfil
{
    class RussProfilSettings : IParcerSettingsRUS
    {
        
        public string BaseUrl { get ; set ; } = "https://www.rusprofile.ru/";
        public string PrefixID { get ; set ; } = "id/{CurrentId}";
        public string StreetPref { get; set; } = "search{CurrentPage}?query={CurrentStreet}";
        public string INNPref { get; set; } = "search?query={CurrentINN}";
        public string PrefixYur { get; set; } = "&type=ul";
        public string PrefixIp { get; set; } = "&type=ip";
    }

    
}
