using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationDataViewModel : ViewModelBase
    {
        public IEnumerable<PaintViewModel> Paints { get; set; }
        public IEnumerable<RimViewModel> Rims { get; set; }
        public IEnumerable<AccessoryViewModel> Accessories { get; set; }
        public IEnumerable<EngineSettingsViewModel> EngineSettings { get; set; }
        public string ModelName { get; set; }
    }
}