using KFZ_Konfigurator.Helper;
using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.Resources.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class OrderOverviewPageViewModel : ItemViewModelBase
    {
        public string Model { get; }
        public double BasePrice { get; }
        public double ExtrasPrice { get; }
        public string Description { get; }
        public string OrderLink { get; }
        public bool OrderSuccess { get; }
        public ConfigurationOverviewPageViewModel Configuration { get; }

        public OrderOverviewPageViewModel(Order model, string orderLink, bool orderSuccess = false)
            : base(model.Id, model.BasePrice + model.ExtrasPrice)
        {
            var carModel = model.Configuration.EngineSetting.CarModel;
            Model = $"{carModel.SeriesCategory.Name} {carModel.BodyCategory.Name} {carModel.Year}";

            BasePrice = model.BasePrice;
            ExtrasPrice = model.ExtrasPrice;
            Description = model.Description;
            OrderLink = orderLink;
            OrderSuccess = orderSuccess;
            Configuration = new ConfigurationOverviewPageViewModel(model.Configuration);
        }

    }
}