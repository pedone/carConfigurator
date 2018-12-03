using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CategoryId = KFZ_Konfigurator.Models.SeedData.CategorySeedData.AccessoryCategoryConstants;

namespace KFZ_Konfigurator.Models.SeedData
{
    public static class AccessorySeedData
    {
        public static readonly IEnumerable<Accessory> Data = new List<Accessory>
        {
            new Accessory { Id = 0, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.Comfort), Name = "Electronic Outside Mirror", Price = 345 },
            new Accessory { Id = 1, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.Comfort), Name = "3 Zone Climate Control", Price = 695 },
            new Accessory { Id = 2, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.Comfort), Name = "Anti Theft Warning System", Price = 480 },
            new Accessory { Id = 3, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.Infotainment), Name = "Virtual Cockpit", Price = 500 },
            new Accessory { Id = 4, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.Infotainment), Name = "MMI Navigation", Price = 1500 },
            new Accessory { Id = 5, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.Infotainment), Name = "Audi Sound System", Price = 290 },
            new Accessory { Id = 6, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.Infotainment), Name = "Smartphone Interface", Price = 400 },
            new Accessory { Id = 7, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.AssistenceSystems), Name = "Parking Assistant", Price = 1750 },
            new Accessory { Id = 8, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.AssistenceSystems), Name = "Tour Assistentce Package", Price = 1640 },
            new Accessory { Id = 9, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.AssistenceSystems), Name = "Pre sense basic", Price = 250 },
            new Accessory { Id = 10, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.AssistenceSystems), Name = "Active lane assist", Price = 600 },
            new Accessory { Id = 11, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.AssistenceSystems), Name = "Cruise control", Price = 300 },
            new Accessory { Id = 12, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.AssistenceSystems), Name = "Head-Up Display", Price = 980 },
            new Accessory { Id = 13, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.AssistenceSystems), Name = "Rear view camera", Price = 450 },
            new Accessory { Id = 14, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.TechnologyAndSafety), Name = "Comfort Suspension", Price = 980 },
            new Accessory { Id = 15, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.TechnologyAndSafety), Name = "Sport chassis", Price = 340 },
            new Accessory { Id = 16, Category = CategorySeedData.GetCategoryByName<AccessoryCategory>(CategoryId.TechnologyAndSafety), Name = "Side airbags in the back", Price = 360 },
        };
    }
}