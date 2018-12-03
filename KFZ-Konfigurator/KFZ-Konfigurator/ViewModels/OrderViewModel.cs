using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.Resources.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class OrderViewModel : ItemViewModelBase
    {
        public string Model { get; }
        public string Guid { get; }
        public string Description { get; }
        public double BasePrice { get; }
        public double ExtrasPrice { get; }
        public string LinkUrl { get; }

        public OrderViewModel(Order model, string orderLink)
            : base(model.Id, model.BasePrice + model.ExtrasPrice)
        {
            var carModel = model.Configuration.EngineSetting.CarModel;
            Model = $"{carModel.SeriesCategory.Name} {carModel.BodyCategory.Name} {carModel.Year}";
            Guid = model.Guid;
            Description = model.Description;
            BasePrice = model.BasePrice;
            ExtrasPrice = model.ExtrasPrice;
            LinkUrl = orderLink;
        }
    }
}