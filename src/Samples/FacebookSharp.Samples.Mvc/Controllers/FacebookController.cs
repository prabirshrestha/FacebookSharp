using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacebookSharp.Samples.Mvc.Controllers
{
    // you can dervie from BaseFacebookController which will make your life easier
    // but since most of u will already have another base controller i will not
    // derive from BaseFacebookController
    public class FacebookController : Controller
    {
        //
        // GET: /Facebook/

        public ActionResult Index()
        {
            return View();
        }

    }
}
