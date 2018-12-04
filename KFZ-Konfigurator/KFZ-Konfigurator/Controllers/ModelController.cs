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
    public class ModelController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ModelController));

        [Route("configuration/models", Name = Constants.Routes.ModelOverview)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                return View("~/Views/Configuration/Index.cshtml", new ConfigurationPageViewModel
                {
                    PartialViewName = "~/Views/Model/_Index.cshtml",
                    PartialViewModel = new CarModelPageViewModel
                    {
                        CarModels = context.CarModels.ToList().Select(cur => new CarModelViewModel(cur)).ToList()
                    }
                });
            }
        }

        [HttpGet]
        [Route("configuration/models/partial", Name = Constants.Routes.ModelOverviewPartial)]
        public JsonResult IndexPartial()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var viewModel = new CarModelPageViewModel
                {
                    CarModels = context.CarModels.ToList().Select(cur => new CarModelViewModel(cur)).ToList()
                };
                ViewData[Constants.PartialView.RenderScripts] = false;
                var viewContent = PartialView("~/Views/Model/_Index.cshtml", viewModel);
                ViewData[Constants.PartialView.RenderScripts] = true;
                var scriptContent = PartialView("~/Views/Model/_Index.cshtml", viewModel);

                return Json(new
                {
                    ViewContent = viewContent,
                    ScriptContent = scriptContent
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SetCarModel(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("SetCarModel was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                var selectedCarModel = context.CarModels.FirstOrDefault(cur => cur.Id == id);
                if (selectedCarModel == null)
                    throw new ArgumentException($"Car model with id {id} was not found in database");

                SessionData.ActiveConfiguration.Reset();

                //set model
                SessionData.ActiveConfiguration.CarModel = new CarModelViewModel(selectedCarModel);

                //set default engine settings
                var cheapestSettings = selectedCarModel.EngineSettings
                    .OrderBy(cur => cur.Price)
                    .First();
                SessionData.ActiveConfiguration.EngineSettingsId = cheapestSettings.Id;

                //set default paint
                SessionData.ActiveConfiguration.PaintId = context.Paints.First(cur => cur.IsDefault == true).Id;

                //set default rims
                SessionData.ActiveConfiguration.RimId = context.Rims.First(cur => cur.IsDefault == true).Id;

                return string.Empty;
            }
        }
    }
}