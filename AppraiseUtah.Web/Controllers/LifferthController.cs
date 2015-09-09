using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppraiseUtah.Web.Controllers
{
    public class LifferthController : Controller
    {
        // GET: Lifferth
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}