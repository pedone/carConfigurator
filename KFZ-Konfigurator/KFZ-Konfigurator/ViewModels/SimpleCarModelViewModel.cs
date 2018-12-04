using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class SimpleCarModelViewModel : ItemViewModelBase
    {
        public string BodyType { get; }
        public string Series { get; }
        public int Year { get; }
        public double Consumption { get; }
        public int Emissions { get; }
        [DisplayFormat(DataFormatString = Constants.CurrencyFormat)]
        public double MinPrice { get; }

        public SimpleCarModelViewModel(CarModel model)
            : base(model.Id)
        {
            BodyType = model.BodyCategory.Name;
            Series = model.SeriesCategory.Name;
            Year = model.Year;

            var baseSettings = GetBaseSettings(model.EngineSettings);
            Consumption = baseSettings.Consumption;
            Emissions = baseSettings.Emission;
            MinPrice = baseSettings.Price;
        }

        private EngineSettings GetBaseSettings(IEnumerable<EngineSettings> settings)
        {
            var minPrice = settings.Min(cur => cur.Price);
            return settings.FirstOrDefault(cur => cur.Price == minPrice);
        }
    }
}