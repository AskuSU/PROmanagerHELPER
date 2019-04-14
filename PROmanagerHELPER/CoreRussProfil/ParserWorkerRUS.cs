using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROmanagerHELPER.CoreRussProfil
{
    class ParserWorkerRUS<T> where T : class
    {
        IParcerSettingsRUS parcerSettings;

        HtmlLoaderRUS loader;

        #region Properties

        public event Action<object, T> OnNewData;
        public event Action<object, T> OnCompleted;

        public IParcerRUS<T> Parcer { get; set; }

        public IParcerSettingsRUS Settings
        {
            get
            {
                return parcerSettings;
            }
            set
            {
                parcerSettings = value;
                loader = new HtmlLoaderRUS(value);
            }
        }

        #endregion

        public ParserWorkerRUS(IParcerRUS<T> parcer)
        {
            this.Parcer = parcer;
        }

        public ParserWorkerRUS(IParcerRUS<T> parcer, IParcerSettingsRUS parcerSettings) : this(parcer)
        {
            this.parcerSettings = parcerSettings;
        }

        public void Start(string street, int IsActive)
        {
            Worker(street, IsActive);
        }      

        private async void Worker(string street, int IsActive)
        {
            var source = await loader.GetSourceByPageId(street, 0, 0, IsActive);

            var result = await Parcer.Parse(source, loader, street, IsActive);

            OnNewData?.Invoke(this, result);

            OnCompleted?.Invoke(this, result);
            
        }
    }
}
