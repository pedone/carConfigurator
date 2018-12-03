using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ExteriorPageViewModel : ViewModelBase
    {
        public IEnumerable<string> PaintCategories { get; set; }
        public IEnumerable<PaintViewModel> Paints { get; set; }
        public IEnumerable<RimViewModel> Rims { get; set; }
        public IEnumerable<AccessoryViewModel> SelectedAccessories { get; set; }
        public EngineSettingsViewModel SelectedEngineSetting { get; set; }
    }
}