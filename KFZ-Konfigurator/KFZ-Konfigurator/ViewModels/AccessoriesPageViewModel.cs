using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class AccessoriesPageViewModel : ViewModelBase
    {
        public IEnumerable<AccessoryViewModel> Accessories { get; set; }
        public EngineSettingsViewModel SelectedEngineSetting { get; set; }
        public PaintViewModel SelectedPaint { get; set; }
        public RimViewModel SelectedRims { get; set; }
        public IEnumerable<string> AccessoryCategories { get; set; }
    }
}