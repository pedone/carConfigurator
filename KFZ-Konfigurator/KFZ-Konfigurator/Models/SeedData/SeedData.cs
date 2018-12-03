using KFZ_Konfigurator.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{

    public abstract class BaseSeedData<T>
    {
        public abstract IEnumerable<T> GetData();
    }

    public static class SeedData
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SeedData));

        public static void Initialize()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                //ClearDB(context);

                // let's assume for now that if there's no data in db.carModel, then there's no data yet at all
                if (!context.CarModels.Any())
                {
                    context.CarModels.AddRange(CarModelSeedData.Data);
                    context.Engines.AddRange(EngineSeedData.Data);
                    context.EngineSettings.AddRange(EngineSettingsSeedData.Data);

                    context.Rims.AddRange(RimSeedData.Data);
                    context.Paints.AddRange(PaintSeedData.Data);
                    context.Accessories.AddRange(AccessorySeedData.Data);
                    context.Categories.AddRange(CategorySeedData.GetData());
                }

                if (context.ChangeTracker.HasChanges())
                {
                    Log.Info("adding database seed data");
                    context.SaveChanges();
                    Log.Info("db data was added");
                }
            }
        }

        private static void ClearDB(CarConfiguratorEntityContext context)
        {
            //remove many to many relationships first
            foreach (var cur in context.Configurations)
                cur.Accessories.Clear();

            context.Accessories.RemoveRange(context.Accessories);
            context.EngineSettings.RemoveRange(context.EngineSettings);
            context.CarModels.RemoveRange(context.CarModels);
            context.Engines.RemoveRange(context.Engines);
            context.Paints.RemoveRange(context.Paints);
            context.Rims.RemoveRange(context.Rims);
            context.Orders.RemoveRange(context.Orders);
            context.Categories.RemoveRange(context.Categories);

            Log.Info("resetting db data");
            context.SaveChanges();
            Log.Info("db data was reset");
        }
    }
}