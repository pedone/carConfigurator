using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{
    public static class CategorySeedData
    {
        #region CarSeries Category
        public static class CarSeriesCategoryConstants
        {
            public const string A1 = nameof(A1);
            public const string A3 = nameof(A3);
            public const string A4 = nameof(A4);
            public const string A5 = nameof(A5);
            public const string A6 = nameof(A6);
            public const string A7 = nameof(A7);
            public const string A8 = nameof(A8);
        }

        private static readonly IEnumerable<CarSeriesCategory> _CarSeriesCategoryData = new List<CarSeriesCategory>
        {
            new CarSeriesCategory { Id = 0, Name = CarSeriesCategoryConstants.A1 },
            new CarSeriesCategory { Id = 1, Name = CarSeriesCategoryConstants.A3 },
            new CarSeriesCategory { Id = 2, Name = CarSeriesCategoryConstants.A4 },
            new CarSeriesCategory { Id = 3, Name = CarSeriesCategoryConstants.A5 },
            new CarSeriesCategory { Id = 4, Name = CarSeriesCategoryConstants.A6 },
            new CarSeriesCategory { Id = 5, Name = CarSeriesCategoryConstants.A7 },
            new CarSeriesCategory { Id = 6, Name = CarSeriesCategoryConstants.A8 },
        };
        #endregion

        #region Body Category
        public static class BodyCategoryConstants
        {
            public const string Sportback = nameof(Sportback);
            public const string Limousine = nameof(Limousine);
            public const string Cabriolet = nameof(Cabriolet);
            public const string Avant = nameof(Avant);
            public const string Coupé = nameof(Coupé);
        }

        private static readonly IEnumerable<BodyCategory> _BodyCategoryData = new List<BodyCategory>
        {
            new BodyCategory { Id = 0, Name = BodyCategoryConstants.Sportback },
            new BodyCategory { Id = 1, Name = BodyCategoryConstants.Limousine },
            new BodyCategory { Id = 2, Name = BodyCategoryConstants.Cabriolet },
            new BodyCategory { Id = 3, Name = BodyCategoryConstants.Avant },
            new BodyCategory { Id = 4, Name = BodyCategoryConstants.Coupé },
        };
        #endregion

        #region Fuel Category
        public static class FuelCategoryConstants
        {
            public const string Diesel = nameof(Diesel);
            public const string Petrol = nameof(Petrol);
        }

        private static readonly IEnumerable<FuelCategory> _FuelCategoryData = new List<FuelCategory>
        {
            new FuelCategory { Id = 0, Name = FuelCategoryConstants.Diesel },
            new FuelCategory { Id = 1, Name = FuelCategoryConstants.Petrol },
        };
        #endregion

        #region Engine Category
        public static class EngineCategoryConstants
        {
            public const string TFSI = nameof(TFSI);
            public const string TDI = nameof(TDI);
        }

        private static readonly IEnumerable<EngineCategory> _EngineCategoryData = new List<EngineCategory>
        {
            new EngineCategory { Id = 0, Name = EngineCategoryConstants.TFSI, FuelCategory = GetCategoryByName<FuelCategory>(FuelCategoryConstants.Petrol, _FuelCategoryData) },
            new EngineCategory { Id = 1, Name = EngineCategoryConstants.TDI, FuelCategory = GetCategoryByName<FuelCategory>(FuelCategoryConstants.Diesel, _FuelCategoryData) },
        };
        #endregion

        #region Paint Category
        public static class PaintCategoryConstants
        {
            public const string Uni = nameof(Uni);
            public const string Metalic = nameof(Metalic);
            public const string Pearlescent = nameof(Pearlescent);
        }

        private static readonly IEnumerable<PaintCategory> _PaintCategoryData = new List<PaintCategory>
        {
            new PaintCategory { Id = 0, Name = PaintCategoryConstants.Uni },
            new PaintCategory { Id = 1, Name = PaintCategoryConstants.Metalic },
            new PaintCategory { Id = 2, Name = PaintCategoryConstants.Pearlescent },
        };
        #endregion

        #region Accessory Category
        public static class AccessoryCategoryConstants
        {
            public const string Comfort = nameof(Comfort);
            public const string Infotainment = nameof(Infotainment);
            public const string AssistenceSystems = nameof(AssistenceSystems);
            public const string TechnologyAndSafety = nameof(TechnologyAndSafety);
        }

        private static readonly IEnumerable<AccessoryCategory> _AccessoryCategoryData = new List<AccessoryCategory>
        {
            new AccessoryCategory { Id = 0, Name = AccessoryCategoryConstants.Comfort },
            new AccessoryCategory { Id = 1, Name = AccessoryCategoryConstants.Infotainment },
            new AccessoryCategory { Id = 2, Name = AccessoryCategoryConstants.AssistenceSystems },
            new AccessoryCategory { Id = 3, Name = AccessoryCategoryConstants.TechnologyAndSafety },
        };
        #endregion

        private static readonly Dictionary<Type, IEnumerable<Category>> _CategoryDataByType = new Dictionary<Type, IEnumerable<Category>>
        {
            { typeof(CarSeriesCategory), _CarSeriesCategoryData },
            { typeof(EngineCategory), _EngineCategoryData },
            { typeof(BodyCategory), _BodyCategoryData },
            { typeof(PaintCategory), _PaintCategoryData },
            { typeof(AccessoryCategory), _AccessoryCategoryData },
            { typeof(FuelCategory), _FuelCategoryData },
        };

        public static T GetCategoryByName<T>(string name) where T : class
        {
            var data = _CategoryDataByType[typeof(T)];
            if (data == null)
                throw new ArgumentException($"data for a category of type {typeof(T).Name} was not found");

            return GetCategoryByName<T>(name, data);
        }

        private static T GetCategoryByName<T>(string name, IEnumerable<Category> data) where T : class
        {
            T result = data.FirstOrDefault(cur => cur.Name == name) as T;
            if (result == null)
                throw new ArgumentException($"a category of type {typeof(T).Name} with the name {name} was not found");

            return result;
        }

        public static IEnumerable<Category> GetData()
        {
            return _CategoryDataByType.Values.SelectMany(cur => cur).ToList();
        }
    }
}