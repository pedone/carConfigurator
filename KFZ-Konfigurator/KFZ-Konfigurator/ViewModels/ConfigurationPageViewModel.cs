using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationPageViewModel : ViewModelBase
    {
        public string PartialViewName { get; set; }
        public ViewModelBase PartialViewModel { get; set; }
    }
}