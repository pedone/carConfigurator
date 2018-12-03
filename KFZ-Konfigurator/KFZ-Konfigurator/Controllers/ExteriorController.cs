using KFZ_Konfigurator.Models;
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
    public class ExteriorController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ExteriorController));

        [Route("configuration/models/model-{id}/exterior", Name = Constants.Routes.Exterior)]
        public ActionResult Index(int id)
        {
            if (!SessionData.ActiveConfiguration.IsValid(id, out string error))
            {
                Log.Error(error);
                return RedirectToRoute(Constants.Routes.ModelOverview);
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                // paint
                var selectedId = SessionData.ActiveConfiguration.PaintId;
                var paints = context.Paints.ToList()
                    .Select(cur => new PaintViewModel(cur) { IsSelected = (cur.Id == selectedId) })
                    .OrderBy(cur => cur.Category)
                    .ToList();

                // rims
                selectedId = SessionData.ActiveConfiguration.RimId;
                var rims = context.Rims.ToList()
                    .Select(cur => new RimViewModel(cur) { IsSelected = (cur.Id == selectedId) })
                    .ToList();

                // accessories
                var accessoryIds = SessionData.ActiveConfiguration.AccessoryIds;
                IEnumerable<AccessoryViewModel> selectedAccessories = null;
                if (accessoryIds.Any())
                {
                    selectedAccessories = context.Accessories.Where(cur => accessoryIds.Contains(cur.Id))
                        .ToList()
                        .Select(cur => new AccessoryViewModel(cur))
                        .ToList();
                }

                // engine
                var selectedEngineSetting = new EngineSettingsViewModel(context.EngineSettings.First(cur => cur.Id == SessionData.ActiveConfiguration.EngineSettingsId));

                // paint categories
                var paintCategories = context.Categories.OfType<PaintCategory>().Select(cur => cur.Name).ToList();

                return View(new ExteriorPageViewModel
                {
                    Paints = paints,
                    Rims = rims,
                    SelectedAccessories = selectedAccessories,
                    SelectedEngineSetting = selectedEngineSetting,
                    PaintCategories = paintCategories
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SetSelectedPaintAndRim([Bind(Include = "PaintId")] int paintId, [Bind(Include = "RimId")] int rimId)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("SetSelectedPaintAndRim was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }

            SessionData.ActiveConfiguration.PaintId = paintId;
            SessionData.ActiveConfiguration.RimId = rimId;
            return string.Empty;
        }
    }
}
