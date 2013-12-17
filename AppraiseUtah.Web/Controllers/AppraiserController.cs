using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppraiseUtah.Models;
using AppraiseUtah.ServiceModels;

namespace AppraiseUtah.Web.Controllers
{
    public class AppraiserController : Controller
    {

        #region Fields

        AppraisalServiceModel _appraisalServiceModel = new AppraisalServiceModel();

        #endregion


        //
        // GET: /Appraiser/

        public ActionResult Index()
        {
            List<Appraiser> appraisers = _appraisalServiceModel.Get_Appraisers();

            return View(appraisers);
        }


        //
        // GET: /Appraiser/Find

        public ActionResult Find(int id = 0)
        {
            Appraiser appraiserModel = new Appraiser();

            if (id != 0)
            {
                appraiserModel = _appraisalServiceModel.Get_Appraiser(id);
            }

            return View(appraiserModel);
        }




        [ChildActionOnly]
        public ActionResult _AppraiserDropdown()
        {
            //List<Appraiser> appraisers = new List<Appraiser>();
            var appraisers = _appraisalServiceModel.Get_Appraisers();

            return PartialView(appraisers);
        }



    }
}
