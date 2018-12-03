using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.ViewModels
{
    public class TestController : Controller
    {
        [Route("test", Name = "Test")]
        public ActionResult Test(bool scriptSection = false)
        {
            ViewData["ScriptSection"] = scriptSection;
            return PartialView("_TestView");
        }
        [Route("test2", Name = "Test2")]
        public ActionResult Test2(bool scriptSection = false)
        {
            ViewData["ScriptSection"] = scriptSection;
            return PartialView("_TestView2");
        }
    }
}