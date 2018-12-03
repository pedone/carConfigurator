using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class PaintViewModel: ItemViewModelBase
    {
        public string Category { get; }
        public string Color { get; }
        public string Name { get; }

        public PaintViewModel(Paint model)
            : base(model.Id, model.Price)
        {
            Category = model.Category.Name;
            Color = model.Color;
            Name = model.Name;
        }
    }
}