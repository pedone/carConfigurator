using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Helper
{
    public static class MiscHelper
    {
        public static string If(bool conditional, string data)
        {
            return conditional ? data : null;
        }

        public static FuelKind EngineKindToFuelKind(EngineKind engineKind)
        {
            switch (engineKind)
            {
                case EngineKind.TDI:
                    return FuelKind.Diesel;
                case EngineKind.TFSI:
                    return FuelKind.Petrol;
                default:
                    throw new NotImplementedException($"unknown engine kind {engineKind}");
            }
        }

        public static string GenerateShortGuid()
        {
            var result = new StringBuilder(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            //replace url unsafe characters
            result.Replace("/", "-");
            result.Replace("+", "_");

            return result.ToString().TrimEnd('=');
        }

        public static string GenerateOrderLink(HttpRequestBase request, UrlHelper url, string guid)
        {
            var projectBaseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url.Content("~"));
            return new Uri(new Uri(projectBaseUrl), url.RouteUrl(Constants.Routes.ViewOrder, new { orderGuid = guid })).AbsoluteUri;
        }

        /// <summary>
        /// taken from https://rhamesconsulting.com/2014/10/27/mvc-updating-multiple-partial-views-from-a-single-ajax-action/
        /// </summary>
        /// <returns></returns>
        public static string RenderRazorViewToString(ControllerContext controllerContext, string viewName, object model)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}