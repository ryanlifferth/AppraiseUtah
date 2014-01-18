﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppraiseUtah.Models;
using AppraiseUtah.ServiceModels;
using AppraiseUtah.Utilities;
using AppraiseUtah.ViewModels;

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
        public ActionResult Index(AppraisalViewModel appraisalViewModel)
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

            appraisalViewModel.Appraisal.ClientPerson.Phone = Utilities.ScrubData.RemoveNonNumeric(appraisalViewModel.Appraisal.ClientPerson.Phone);
            appraisalViewModel.Appraisal.OccupantPerson.Phone = Utilities.ScrubData.RemoveNonNumeric(appraisalViewModel.Appraisal.OccupantPerson.Phone);

            var appraisalId = _appraisalServiceModel.Save_Appraisal(appraisalViewModel);
            appraisalViewModel.Appraisal.Id = appraisalId;

            // Send the confirmation email

            MailUtility.SendConfirmationEmail(appraisalViewModel);

            return RedirectToAction("Confirmation", new { id = appraisalId });
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

    }
}
