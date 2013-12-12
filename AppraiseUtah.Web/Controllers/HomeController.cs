using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppraiseUtah.Models;
using AppraiseUtah.ServiceModels;
using AppraiseUtah.ViewModels;

namespace AppraiseUtah.Web.Controllers
{
    public class HomeController : Controller
    {

        #region Fields

        AppraisalServiceModel _appraisalServiceModel = new AppraisalServiceModel();

        #endregion


        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _Appraisers()
        {
            //List<Appraiser> appraisers = new List<Appraiser>();
            var appraisers = _appraisalServiceModel.Get_Appraisers();

            return PartialView(appraisers);
        }


        //
        // GET: /Home/

        public ActionResult Appraiser(int id = 0)
        {
            Appraiser appraiserModel = new Appraiser();

            if (id != 0)
            {
                appraiserModel = _appraisalServiceModel.Get_Appraiser(id);
                //var appraisers = _appraisalServiceModel.Get_Appraisers();
            }

            return View(appraiserModel);
        }



    }
}
