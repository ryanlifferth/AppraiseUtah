using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppraiseUtah.Client.Models;
using AppraiseUtah.Client.ServiceModels;
using AppraiseUtah.Client.Utilities;
using AppraiseUtah.Client.ViewModels;
using reCAPTCHA.MVC;

namespace AppraiseUtah.Web.Controllers
{
    public class AppraisalController : Controller
    {

        #region Fields

        AppraisalServiceModel _appraisalServiceModel = new AppraisalServiceModel();

        #endregion


        #region Index

        //
        // GET: /Appraisal/

        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            AppraisalViewModel appraisalViewModel = new AppraisalViewModel();

            if (id != 0)
            {
                appraisalViewModel.Appraisal = _appraisalServiceModel.Get_Appraisal(id);
                //var appraisers = _appraisalServiceModel.Get_Appraisers();
                //var appraiser = _appraisalServiceModel.Get_Appraiser(3);

                //TODO:  Format phone numbers
            }

            return View(appraisalViewModel);
        }

        /*[HttpGet]
        public ActionResult Update(int id)
        {
            AppraisalViewModel appraisalViewModel = new AppraisalViewModel();
            appraisalViewModel.Appraisal = _appraisalServiceModel.Get_Appraisal(id).First();

            return View(appraisalViewModel);
        }*/

        [HttpPost]
        [CaptchaValidator]
        public ActionResult Index(AppraisalViewModel appraisalViewModel, bool captchaValid)
        {

            if (ModelState.IsValid)
            {
                /*var appraisalModel = new Models.Appraisal();
                appraisalViewModel.Appraisal.ClientPerson = new Models.Person { FirstName = "Ryan", LastName = "Test", CompanyName = "Ryan Company", Email = "ryan@test.com", Phone = "8015556565" };
                appraisalViewModel.Appraisal.OccupantPerson = new Models.Person { FirstName = "Sam", LastName = "Occupant" };
                appraisalViewModel.Appraisal.PropertyAddress = new Models.Address { Address1 = "123 Somewhere", City = "SLC", PostalCode = "12345", StateCode = "UT" };
                appraisalViewModel.Appraisal.AppraiserId = 3;
                appraisalViewModel.Appraisal.PropertyTypeCode = "SFR";
                appraisalViewModel.Appraisal.AppraisalTypeCode = "FULL";
                appraisalViewModel.Appraisal.AppraisalPurposeCode = "SC";
                appraisalViewModel.Appraisal.Comments = "Web test";
                appraisalViewModel.Appraisal.ClientAddress.Address1 = "123 ClientAddress Ave";
                appraisalViewModel.Appraisal.ClientAddress.City = "SLC";
                appraisalViewModel.Appraisal.ClientAddress.PostalCode = "12345";
                appraisalViewModel.Appraisal.ClientAddress.StateCode = "UT";*/


                appraisalViewModel.Appraisal.ClientPerson.Phone = AppraiseUtah.Client.Utilities.ScrubData.RemoveNonNumeric(appraisalViewModel.Appraisal.ClientPerson.Phone);
                appraisalViewModel.Appraisal.OccupantPerson.Phone = AppraiseUtah.Client.Utilities.ScrubData.RemoveNonNumeric(appraisalViewModel.Appraisal.OccupantPerson.Phone);
                appraisalViewModel.Appraisal.Client2Person.Phone = AppraiseUtah.Client.Utilities.ScrubData.RemoveNonNumeric(appraisalViewModel.Appraisal.Client2Person.Phone);

                if (Request["AreYouClient"] == "Yes")
                {
                    ClearOrderClientData(ref appraisalViewModel);
                }

                appraisalViewModel.Appraisal.OrderDate = DateTime.UtcNow;

                var appraisalId = _appraisalServiceModel.Save_Appraisal(appraisalViewModel);
                appraisalViewModel.Appraisal.Id = appraisalId;

                // Send the confirmation email

                MailUtility.SendConfirmationEmail(appraisalViewModel);

                return RedirectToAction("Confirmation", new { id = appraisalId });
            }
            else
            {
                if (!captchaValid)
                {
                    ViewBag.Captcha = false;
                }
                return View(appraisalViewModel);
            }
        }

        #endregion Index

        #region Confirmation

        //
        // GET: /Appraisal/Confirmation

        [HttpGet]
        public ActionResult Confirmation(int id = 0)
        {
            AppraisalViewModel appraisalViewModel = new AppraisalViewModel();

            if (id != 0)
            {
                appraisalViewModel.Appraisal = _appraisalServiceModel.Get_Appraisal(id);
                //TODO:  Format phone numbers
            }

            return View(appraisalViewModel);
        }

        #endregion Confirmation

        #region All Appraisal Orders

        // GET: /Appraisal/AllOrders
        //      The stored procedure (dbo.GetAppraisals) only returns the most recent 100 appraisals
        [HttpGet]
        public ActionResult AllOrders()
        {
            var appraisalsViewModel = new AppraisalsViewModel();

            appraisalsViewModel.Appraisals = _appraisalServiceModel.Get_Appraisals();

            return View(appraisalsViewModel);
        }

        #endregion


        #region Test

        //
        // GET: /Appraisal/Test

        [HttpGet]
        public ActionResult Test(int id = 0)
        {
            AppraisalViewModel appraisalViewModel = new AppraisalViewModel();

            if (id != 0)
            {
                appraisalViewModel.Appraisal = _appraisalServiceModel.Get_Appraisal(id);
                //TODO:  Format phone numbers
            }

            return View(appraisalViewModel);
        }

        #endregion Test

        #region Private Methods

        private void ClearOrderClientData(ref AppraisalViewModel appraisalViewModel)
        {
            appraisalViewModel.Appraisal.Client2Person.PersonId = 0;
            appraisalViewModel.Appraisal.Client2Person.FirstName = "";
            appraisalViewModel.Appraisal.Client2Person.LastName = "";
            appraisalViewModel.Appraisal.Client2Person.CompanyName = "";
            appraisalViewModel.Appraisal.Client2Person.Phone = "";
            appraisalViewModel.Appraisal.Client2Person.Email = "";
            appraisalViewModel.Appraisal.Client2Person.PersonType = "";

            appraisalViewModel.Appraisal.Client2Address.AddressId = 0;
            appraisalViewModel.Appraisal.Client2Address.Address1 = "";
            appraisalViewModel.Appraisal.Client2Address.Address2 = "";
            appraisalViewModel.Appraisal.Client2Address.City = "";
            appraisalViewModel.Appraisal.Client2Address.StateCode = "";
            appraisalViewModel.Appraisal.Client2Address.PostalCode = "";
            appraisalViewModel.Appraisal.Client2Address.AddressType = "";
        }

        #endregion

    }
}
