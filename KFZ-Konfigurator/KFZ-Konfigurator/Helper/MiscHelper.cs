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
        public static string EngineKindToFuelKind()
        {
            //TODO

            return string.Empty;

            //switch (engineKind)
            //{
            //    case EngineKind.TDI:
            //        return FuelKind.Diesel;
            //    case EngineKind.TFSI:
            //        return FuelKind.Petrol;
            //    default:
            //        throw new NotImplementedException($"unknown engine kind {engineKind}");
            //}
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
    }
}