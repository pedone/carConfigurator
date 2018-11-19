﻿using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class AccessoryViewModel
    {
        public int Id { get; }
        public AccessoryCategory Category { get; }
        public string Name { get; }
        public double Price { get; }
        public AccessorySubCategory SubCategory { get; }

        public AccessoryViewModel(Accessory model)
        {
            Id = model.Id;
            Category = model.Category;
            Name = model.Name;
            Price = model.Price;
            SubCategory = model.SubCategory;
        }
    }
}