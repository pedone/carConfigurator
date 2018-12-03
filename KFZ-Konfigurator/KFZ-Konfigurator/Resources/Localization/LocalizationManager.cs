using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;

namespace KFZ_Konfigurator.Resources.Localization
{
    public static class LocalizationManager
    {
        public static string Localize(string id)
        {
            return GetCurrentCultureResourceManager().GetString(id) ?? throw new ArgumentException($"Resource with the name {id} was not found");
        }

        //public static string LocalizeEnum<T>(string item) where T : struct, IConvertible
        //{
        //    if (!typeof(T).IsEnum)
        //        throw new ArgumentException("Type of T must be an enum");

        //    return Localize($"{typeof(T).Name}_{item}");
        //}

        public static string LocalizeCategory<T>(string name) where T : Category
        {
            return Localize($"{typeof(T).Name}_{name}");
        }

        private static ResourceManager GetCurrentCultureResourceManager()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case "en-US":
                    return en_US.ResourceManager;
                default:
                    return en_US.ResourceManager;
            }
        }
    }
}