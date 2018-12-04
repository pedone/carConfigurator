﻿using KFZ_Konfigurator.Helper;
using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class EngineSettingsController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(EngineSettingsController));

        [Route("configuration/models/model-{id}/enginesettings", Name = Constants.Routes.EngineSettings)]
        public ActionResult Index(int id)
        {
            if (!SessionData.ActiveConfiguration.IsValid(id, out string error))
            {
                Log.Error(error);
                return RedirectToRoute(Constants.Routes.ModelOverview);
            }

            return View(Constants.Views.ConfigurationIndex, new ConfigurationPageViewModel
            {
                PartialViewName = Constants.Views.EngineSettingsPartialIndex,
                PartialViewModel = BuildViewModel(id)
            });
        }

        [HttpGet]
        [Route("configuration/models/model-{id}/enginesettings/partial", Name = Constants.Routes.EngineSettingsPartial)]
        public JsonResult IndexPartial(int id)
        {
            //TODO
            //if (!SessionData.ActiveConfiguration.IsValid(id, out string error))
            //{
            //    Log.Error(error);
            //    return RedirectToRoute(Constants.Routes.ModelOverview);
            //}

            var viewModel = BuildViewModel(id);
            var view = ViewHelper.BuildPartialViewAndScriptJson(ControllerContext, Constants.Views.EngineSettingsPartialIndex, viewModel);
            return Json(new
            {
                ViewContent = view.ViewContent,
                ScriptContent = view.ScriptContent
            }, JsonRequestBehavior.AllowGet);
        }

        private EngineSettingsPageViewModel BuildViewModel(int id)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var carModel = context.CarModels.First(cur => cur.Id == id);
                //engine settings
                var selectedId = SessionData.ActiveConfiguration.EngineSettingsId;
                var settings = carModel.EngineSettings.ToList()
                    .Select(cur => new EngineSettingsViewModel(cur) { IsSelected = (cur.Id == selectedId) })
                    .OrderBy(cur => cur.Price)
                    .ToList();

                //accessories
                var accessoryIds = SessionData.ActiveConfiguration.AccessoryIds;
                IEnumerable<AccessoryViewModel> selectedAccessories = null;
                if (accessoryIds.Any())
                {
                    selectedAccessories = context.Accessories.Where(cur => accessoryIds.Contains(cur.Id))
                        .ToList()
                        .Select(cur => new AccessoryViewModel(cur))
                        .ToList();
                }

                //paint
                PaintViewModel selectedPaint = new PaintViewModel(context.Paints.First(cur => cur.Id == SessionData.ActiveConfiguration.PaintId));

                //rims
                RimViewModel selectedRims = new RimViewModel(context.Rims.First(cur => cur.Id == SessionData.ActiveConfiguration.RimId));

                return new EngineSettingsPageViewModel
                {
                    EngineSettings = settings,
                    SelectedAccessories = selectedAccessories,
                    SelectedPaint = selectedPaint,
                    SelectedRims = selectedRims
                };
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SetSelectedEngineSettings(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("SetSelectedEngineSettings was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }

            SessionData.ActiveConfiguration.EngineSettingsId = id;
            return string.Empty;
        }
    }
}