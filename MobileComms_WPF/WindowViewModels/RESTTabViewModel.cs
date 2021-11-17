using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileComms_WPF.WindowViewModels
{
    public  class RESTTabViewModel : Core.ViewModelBase
    {
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { Set(ref _IsLoading, value); }
        }
        private bool _IsLoading;

        public bool IsVisible
        {
            get { return _IsVisible; }
            set { Set(ref _IsVisible, value); }
        }
        private bool _IsVisible = true;

    }
}
