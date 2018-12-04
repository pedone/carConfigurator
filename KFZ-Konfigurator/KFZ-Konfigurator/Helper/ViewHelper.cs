using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Helper
{
    public static class ViewHelper
    {
        public static string If(bool conditional, string data)
        {
            return conditional ? data : null;
        }

        /// <summary>
        /// taken from https://rhamesconsulting.com/2014/10/27/mvc-updating-multiple-partial-views-from-a-single-ajax-action/
        /// </summary>
        /// <returns></returns>
        public static string RenderRazorViewToString(ControllerContext context, string viewName, ViewModelBase viewModel)
        {
            context.Controller.ViewData.Model = viewModel;
            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, context.Controller.ViewData, context.Controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(context, viewResult.View);
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public static (string ViewContent, string ScriptContent) BuildPartialViewAndScriptJson(ControllerContext context, string viewPath, ViewModelBase viewModel)
        {
            context.Controller.ViewData.ModelState.Clear();
            context.Controller.ViewData[Constants.PartialView.RenderScripts] = false;
            var viewContent = ViewHelper.RenderRazorViewToString(context, viewPath, viewModel);
            context.Controller.ViewData[Constants.PartialView.RenderScripts] = true;
            var scriptContent = ViewHelper.RenderRazorViewToString(context, viewPath, viewModel);

            return (ViewContent: viewContent, ScriptContent: scriptContent);
        }
    }
}