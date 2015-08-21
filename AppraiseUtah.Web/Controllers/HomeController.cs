using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppraiseUtah.Client.Models;
using AppraiseUtah.Client.ServiceModels;
using AppraiseUtah.Client.ViewModels;
using System.Xml.Linq;

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

        // GET: /Home/Service-Area
        [ActionName("service-area")]
        public ActionResult ServiceArea() 
        {
                return View();
        }

        // GET: /Home/Services-And-Fees
        [ActionName("services-and-fees")]
        public ActionResult ServicesAndFees()
        {
            return View();
        }

        // GET: /Home/About-Us
        [ActionName("about-us")]
        public ActionResult AboutUs()
        {
            XElement staffXml = XElement.Load(Server.MapPath("~/App_Data/Staff.xml"));
            var staffData = from data in staffXml.Elements("Appraiser")
                            select data;
            ViewData["staffData"] = staffData;

            return View();
        }

        // GET: /Home/Staff/ID/
        public ActionResult Staff(String id)
        {
            XElement staffXml = XElement.Load(Server.MapPath("~/App_Data/Staff.xml"));
            var staffData = (from data in staffXml.Elements("Appraiser")
                             where data.Attribute("id").Value == id
                             select new
                             {
                                 ID = data.Attribute("id").Value,
                                 Name = data.Element("Name").Value,
                                 Certifications = data.Element("Certifications").Value,
                                 ImgId = data.Element("ImgId").Value,
                                 Description = data.Element("Description").Value
                             }).SingleOrDefault();

            // If no match was made then redirect to the "AboutUs" view
            if (staffData == null)
            {
                return View("AboutUs");
            }

            ViewData["ID"] = staffData.ID;
            ViewData["Name"] = staffData.Name;
            ViewData["Certifications"] = staffData.Certifications;
            ViewData["ImgId"] = staffData.ImgId;
            ViewData["Description"] = staffData.Description;

            return View();
        }

    }
}
