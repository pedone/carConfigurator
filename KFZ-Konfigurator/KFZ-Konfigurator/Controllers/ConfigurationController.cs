using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationController : Controller
    {
        [HttpGet]
        [Route("configuration/loadconfigurationviewmodel", Name = Constants.Routes.GetConfigurationViewModel)]
        public JsonResult GetConfigurationViewModel()
        {
            int? modelId = SessionData.ActiveConfiguration.CarModel?.Id;
            if (modelId != null)
                return Json(BuildViewModel(modelId.Value), JsonRequestBehavior.AllowGet);

            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return Json("car model is not set", JsonRequestBehavior.AllowGet);
        }

        private ConfigurationDataViewModel BuildViewModel(int carModelId)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                // accessories
                var accessories = context.Accessories.ToList()
                     .Select(cur => new AccessoryViewModel(cur) { IsSelected = SessionData.ActiveConfiguration.AccessoryIds.Contains(cur.Id) })
                     .OrderBy(cur => cur.Price)
                     .ToList();

                // engine
                var carModel = context.CarModels.First(cur => cur.Id == carModelId);
                var engineSettings = carModel.EngineSettings.ToList()
                    .Select(cur => new EngineSettingsViewModel(cur) { IsSelected = (cur.Id == SessionData.ActiveConfiguration.EngineSettingsId) })
                    .OrderBy(cur => cur.Price)
                    .ToList();

                // paint
                var paints = context.Paints.ToList()
                    .Select(cur => new PaintViewModel(cur) { IsSelected = (cur.Id == SessionData.ActiveConfiguration.PaintId) })
                    .OrderBy(cur => cur.Category)
                    .ToList();

                // rims
                var rims = context.Rims.ToList()
                    .Select(cur => new RimViewModel(cur) { IsSelected = (cur.Id == SessionData.ActiveConfiguration.RimId) })
                    .ToList();

                // model name
                var modelName = $"{carModel.SeriesCategory.Name} {carModel.BodyCategory.Name} {carModel.Year}";

                return new ConfigurationDataViewModel
                {
                    Paints = paints,
                    Rims = rims,
                    Accessories = accessories,
                    EngineSettings = engineSettings,
                    ModelName = modelName,
                    Links = new ConfigurationDataViewModel.ConfigurationLinkData
                    {
                        EngineSettingsLink = Url.RouteUrl(Constants.Routes.EngineSettings, new { id = carModelId }),
                        EngineSettingsLinkPartial = Url.RouteUrl(Constants.Routes.EngineSettingsPartial, new { id = carModelId }),
                        AccessoriesLink = Url.RouteUrl(Constants.Routes.Accessories, new { id = carModelId }),
                        AccessoriesLinkPartial = Url.RouteUrl(Constants.Routes.AccessoriesPartial, new { id = carModelId }),
                        ExteriorLink = Url.RouteUrl(Constants.Routes.Exterior, new { id = carModelId }),
                        ExteriorLinkPartial = Url.RouteUrl(Constants.Routes.ExteriorPartial, new { id = carModelId }),
                        OverviewLink = Url.RouteUrl(Constants.Routes.ConfigurationOverview, new { id = carModelId }),
                        OverviewLinkPartial = Url.RouteUrl(Constants.Routes.ConfigurationOverviewPartial, new { id = carModelId })
                    }
                };
            }
        }
    }
}