using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class AccessoryViewModel : ItemViewModelBase
    {
        public string Category { get; }
        public string Name { get; }

        public AccessoryViewModel(Accessory model)
            : base(model.Id, model.Price)
        {
            Category = model.Category.Name;
            Name = model.Name;
        }
    }
}