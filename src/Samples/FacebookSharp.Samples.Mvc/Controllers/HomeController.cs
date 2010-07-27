using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacebookSharp.Samples.Mvc.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                ViewData["starturl"] = Url.Action("Index", "Facebook");
            else
                ViewData["starturl"] = Url.Action("LogOn", "Account");

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
