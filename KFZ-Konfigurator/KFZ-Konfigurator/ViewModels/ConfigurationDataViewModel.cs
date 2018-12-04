using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationDataViewModel : ViewModelBase
    {
        public class ConfigurationLinkData
        {
            public string EngineSettingsLink { get; set; }
            public string EngineSettingsLinkPartial { get; set; }
            public string AccessoriesLink { get; set; }
            public string AccessoriesLinkPartial { get; set; }
            public string ExteriorLink { get; set; }
            public string ExteriorLinkPartial { get; set; }
            public string OverviewLink { get; set; }
            public string OverviewLinkPartial { get; set; }
        }

        public IEnumerable<PaintViewModel> Paints { get; set; }
        public IEnumerable<RimViewModel> Rims { get; set; }
        public IEnumerable<AccessoryViewModel> Accessories { get; set; }
        public IEnumerable<EngineSettingsViewModel> EngineSettings { get; set; }
        public string ModelName { get; set; }
        public ConfigurationLinkData Links { get; set; }
    }
}