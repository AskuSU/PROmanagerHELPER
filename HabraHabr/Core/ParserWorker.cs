using AngleSharp.Html.Parser;
using HabraHabr.Core.Habra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabraHabr.Core
{
    class ParserWorker<T> where T : class
    {
        IParcer<T> parcer;
        IParcerSettings parcerSettings;

        HtmlLoader loader;

        bool isActive;

        #region Properties

        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public IParcer<T> Parcer
        {
            get
            {
                return parcer;
            }
            set
            {
                parcer = value;
            }
        }

        public IParcerSettings Settings
        {
            get
            {
                return parcerSettings;
            }
            set
            {
                parcerSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        #endregion

        public ParserWorker(IParcer<T> parcer)
        {
            this.parcer = parcer;
        }

        public ParserWorker(IParcer<T> parcer, IParcerSettings parcerSettings) : this(parcer)
        {
            this.parcerSettings = parcerSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            for (int i = parcerSettings.StartPoint; i < parcerSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parcer.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            isActive = false;
        }
    }
}
