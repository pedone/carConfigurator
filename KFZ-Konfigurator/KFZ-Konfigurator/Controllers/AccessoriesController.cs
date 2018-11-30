﻿using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class AccessoriesController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AccessoriesController));

        [Route("configuration/models/model-{id}/accessories", Name = Constants.Routes.Accessories)]
        public ActionResult Index(int id)
        {
            if (!SessionData.ActiveConfiguration.IsValid(id, out string error))
            {
                Log.Error(error);
                return RedirectToRoute(Constants.Routes.ModelOverview);
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                //accessories
                var selectedIds = SessionData.ActiveConfiguration.AccessoryIds;
                var accessories = context.Accessories.ToList()
                    .Select(cur => new AccessoryViewModel(cur) { IsSelected = selectedIds.Contains(cur.Id) })
                    .OrderBy(cur => cur.Price)
                    .ToList();

                //engine
                var selectedEngineSetting = new EngineSettingsViewModel(context.EngineSettings.First(cur => cur.Id == SessionData.ActiveConfiguration.EngineSettingsId));

                //paint
                PaintViewModel selectedPaint = null;
                if (SessionData.ActiveConfiguration.PaintId != -1)
                    selectedPaint = new PaintViewModel(context.Paints.First(cur => cur.Id == SessionData.ActiveConfiguration.PaintId));

                //rims
                RimViewModel selectedRims = null;
                if (SessionData.ActiveConfiguration.RimId != -1)
                    selectedRims = new RimViewModel(context.Rims.First(cur => cur.Id == SessionData.ActiveConfiguration.RimId));

                return View(new AccessoriesPageViewModel
                {
                    Accessories = accessories,
                    SelectedEngineSetting = selectedEngineSetting,
                    SelectedPaint = selectedPaint,
                    SelectedRims = selectedRims,
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SetSelectedAccessories(string id)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("SetSelectedAccessories was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }

            if (id == null)
            {
                SessionData.ActiveConfiguration.AccessoryIds = null;
            }
            else
            {
                var ids = id.Trim('[', ']').Split(',').Select(cur => int.Parse(cur)).ToList();
                SessionData.ActiveConfiguration.AccessoryIds = ids.ToArray();
            }
            Log.Debug($"AccessoryIds set to {id}");
            return string.Empty; ;
        }
    }
}
