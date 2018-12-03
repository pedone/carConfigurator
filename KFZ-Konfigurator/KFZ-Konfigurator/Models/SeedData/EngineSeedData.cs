using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CategoryId = KFZ_Konfigurator.Models.SeedData.CategorySeedData.EngineCategoryConstants;

namespace KFZ_Konfigurator.Models.SeedData
{
    public static class EngineSeedData
    {
        public static class EngineIdConstants
        {
            public const int TFSI30_999_85 = 0;
            public const int TFSI35_1498_110 = 1;
            public const int TDI30_1598_85 = 2;
            public const int TDI20_1968_110 = 3;
            public const int TFSI20_1984_140 = 4;
            public const int TFSI35_1984_110 = 5;
            public const int TFSI40_1984_140 = 6;
            public const int TDI35_1968_110 = 7;
            public const int TDI40_1968_140 = 8;
            public const int TDI20_1968_90 = 9;
            public const int TDI30_2967_160 = 10;
            public const int TDI30_2967_210 = 11;
            public const int TDI40_1968_150 = 12;
            public const int TDI45_2967_170 = 13;
            public const int TDI50_2967_210 = 14;
        };

        public static readonly IEnumerable<Engine> Data = new List<Engine>
        {
            new Engine { Id = EngineIdConstants.TFSI30_999_85, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TFSI), Liter = 30, Size = 999, Performance = 85 },
            new Engine { Id = EngineIdConstants.TFSI35_1498_110, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TFSI), Liter = 35, Size = 1498, Performance = 110 },
            new Engine { Id = EngineIdConstants.TDI30_1598_85, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 30, Size = 1598, Performance = 85 },
            new Engine { Id = EngineIdConstants.TDI20_1968_110, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 20, Size = 1968, Performance = 110 },
            new Engine { Id = EngineIdConstants.TFSI20_1984_140, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TFSI), Liter = 20, Size = 1984, Performance = 140 },
            new Engine { Id = EngineIdConstants.TFSI35_1984_110, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TFSI), Liter = 35, Size = 1984, Performance = 110 },
            new Engine { Id = EngineIdConstants.TFSI40_1984_140, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TFSI), Liter = 40, Size = 1984, Performance = 140 },
            new Engine { Id = EngineIdConstants.TDI35_1968_110, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 35, Size = 1968, Performance = 110 },
            new Engine { Id = EngineIdConstants.TDI40_1968_140, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 40, Size = 1968, Performance = 140 },
            new Engine { Id = EngineIdConstants.TDI20_1968_90, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 20, Size = 1968, Performance = 90 },
            new Engine { Id = EngineIdConstants.TDI30_2967_160, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 30, Size = 2967, Performance = 160 },
            new Engine { Id = EngineIdConstants.TDI30_2967_210, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 30, Size = 2967, Performance = 210 },
            new Engine { Id = EngineIdConstants.TDI40_1968_150, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 40, Size = 1968, Performance = 150 },
            new Engine { Id = EngineIdConstants.TDI45_2967_170, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 45, Size = 2967, Performance = 170 },
            new Engine { Id = EngineIdConstants.TDI50_2967_210, Category = CategorySeedData.GetCategoryByName<EngineCategory>(CategoryId.TDI), Liter = 50, Size = 2967, Performance = 210 },
        };

        public static Engine Get(int id)
        {
            var result = Data.FirstOrDefault(cur => cur.Id == id);
            if (result == null)
                throw new ArgumentException($"engine with id '{id}' was not found");

            return result;
        }
    }
}
