using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeriesId = KFZ_Konfigurator.Models.SeedData.CategorySeedData.CarSeriesCategoryConstants;
using BodyId = KFZ_Konfigurator.Models.SeedData.CategorySeedData.BodyCategoryConstants;

namespace KFZ_Konfigurator.Models.SeedData
{
    public static class CarModelSeedData
    {
        public static class CarModelIdConstants
        {
            public const int A1Sportback2018 = 0;
            public const int A3Sportback2018 = 1;
            public const int A3Sportback2019 = 2;
            public const int A3Limousine2019 = 3;
            public const int A3Cabriolet2019 = 4;
            public const int A3Limousine2018 = 5;
            public const int A3Cabriolet2018 = 6;
            public const int A4Limousine2019 = 7;
            public const int A4Limousine2018 = 8;
            public const int A4Avant2019 = 9;
            public const int A4Avant2018 = 10;
            public const int A5Coupe2018 = 11;
            public const int A5Sportback2019 = 12;
            public const int A5Sportback2018 = 13;
            public const int A5Cabriolet2019 = 14;
            public const int A5Cabriolet2018 = 15;
            public const int A6Limousine2019 = 16;
            public const int A6Avant2019 = 17;
            public const int A7Sportback2019 = 18;
            public const int A8Limousine2019 = 19;
        }

        public static readonly IEnumerable<CarModel> Data = new List<CarModel>
        {
            // A1
            new CarModel { Id = CarModelIdConstants.A1Sportback2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A1), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Sportback), Year = 2018 },
            // A3
            new CarModel { Id = CarModelIdConstants.A3Sportback2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A3), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Sportback), Year = 2018 },
            new CarModel { Id = CarModelIdConstants.A3Sportback2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A3), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Sportback), Year = 2019 },
            new CarModel { Id = CarModelIdConstants.A3Limousine2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A3), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Limousine), Year = 2019 },
            new CarModel { Id = CarModelIdConstants.A3Cabriolet2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A3), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Cabriolet), Year = 2019 },
            new CarModel { Id = CarModelIdConstants.A3Limousine2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A3), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Limousine), Year = 2018 },
            new CarModel { Id = CarModelIdConstants.A3Cabriolet2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A3), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Cabriolet), Year = 2018 },
            // A4
            new CarModel { Id = CarModelIdConstants.A4Limousine2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A4), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Limousine), Year = 2019 },
            new CarModel { Id = CarModelIdConstants.A4Limousine2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A4), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Limousine), Year = 2018 },
            new CarModel { Id = CarModelIdConstants.A4Avant2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A4), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Avant), Year = 2019 },
            new CarModel { Id = CarModelIdConstants.A4Avant2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A4), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Avant), Year = 2018 },
            // A5
            new CarModel { Id = CarModelIdConstants.A5Coupe2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A5), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Coupé), Year = 2018 },
            new CarModel { Id = CarModelIdConstants.A5Sportback2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A5), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Sportback), Year = 2019 },
            new CarModel { Id = CarModelIdConstants.A5Sportback2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A5), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Sportback), Year = 2018 },
            new CarModel { Id = CarModelIdConstants.A5Cabriolet2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A5), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Cabriolet), Year = 2019 },
            new CarModel { Id = CarModelIdConstants.A5Cabriolet2018, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A5), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Cabriolet), Year = 2018 },
            // A6
            new CarModel { Id = CarModelIdConstants.A6Limousine2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A6), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Limousine), Year = 2019 },
            new CarModel { Id = CarModelIdConstants.A6Avant2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A6), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Avant), Year = 2019 },
            // A7
            new CarModel { Id = CarModelIdConstants.A7Sportback2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A7), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Sportback), Year = 2019 },
            // A8
            new CarModel { Id = CarModelIdConstants.A8Limousine2019, SeriesCategory = CategorySeedData.GetCategoryByName<CarSeriesCategory>(SeriesId.A6), BodyCategory = CategorySeedData.GetCategoryByName<BodyCategory>(BodyId.Limousine), Year = 2019 },
        };

        public static CarModel Get(int id)
        {
            var result = Data.FirstOrDefault(cur => cur.Id == id);
            if (result == null)
                throw new ArgumentException($"model with id '{id}' was not found");

            return result;
        }
    }
}
