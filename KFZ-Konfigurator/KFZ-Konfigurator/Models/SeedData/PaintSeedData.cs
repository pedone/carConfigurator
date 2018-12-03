using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CategoryId = KFZ_Konfigurator.Models.SeedData.CategorySeedData.PaintCategoryConstants;

namespace KFZ_Konfigurator.Models.SeedData
{

    public static class PaintSeedData
    {
        public static IEnumerable<Paint> Data
        {
            get
            {
                yield return new Paint { Id = 0, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Uni), Name = "Black", Color = "#040203", Price = 0, IsDefault = true };
                yield return new Paint { Id = 1, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Uni), Name = "White", Color = "#EFEFE7", Price = 0 };
                yield return new Paint { Id = 2, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Metalic), Name = "Silver", Color = "#A6AAAD", Price = 900 };
                yield return new Paint { Id = 3, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Metalic), Name = "White", Color = "#CACFCB", Price = 900 };
                yield return new Paint { Id = 4, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Metalic), Name = "Gray", Color = "#161C1C", Price = 900 };
                yield return new Paint { Id = 5, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Metalic), Name = "Black", Color = "#000201", Price = 900 };
                yield return new Paint { Id = 6, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Metalic), Name = "Red", Color = "#991014", Price = 900 };
                yield return new Paint { Id = 7, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Pearlescent), Name = "Gray", Color = "#434748", Price = 2400 };
                yield return new Paint { Id = 8, Category = CategorySeedData.GetCategoryByName<PaintCategory>(CategoryId.Pearlescent), Name = "Green", Color = "#1E281F", Price = 2400 };
            }
        }
    }
}