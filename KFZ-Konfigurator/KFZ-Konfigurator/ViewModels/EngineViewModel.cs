using KFZ_Konfigurator.Models;
using System;

namespace KFZ_Konfigurator.ViewModels
{
    public class EngineViewModel : ItemViewModelBase
    {
        public string FuelKind { get; }
        public string EngineKind { get; }
        public int Liter { get; }
        public int Size { get; }
        public int Performance { get; }
        public int PerformancePS { get; }

        public EngineViewModel(Engine model)
            : base(model.Id)
        {
            FuelKind = model.Category.FuelCategory.Name;
            EngineKind = model.Category.Name;
            Liter = model.Liter;
            Size = model.Size;
            Performance = model.Performance;
            PerformancePS = (int)Math.Round(model.Performance * Constants.KW_to_PS);
        }
    }
}