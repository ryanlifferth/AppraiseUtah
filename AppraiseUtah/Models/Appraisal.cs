using System;
using System.Collections.Generic;
using System.Data.Entity;
using AppraiseUtah.Common.Constants;
using AppraiseUtah.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AppraiseUtah.Models
{

    public class Appraisal
    {

        #region Fields

        
        #endregion

        #region Properties

        public int Id { get; set; }

        public string StatusCode { get; set; }

        [DisplayName("Appraiser")]
        [Required(ErrorMessage = "Please select an appraiser.")]
        //[StringLength(160)]
        public int AppraiserId { get; set; }

        public Person ClientPerson { get; set; }

        public Address ClientAddress { get; set; }

        public Person OccupantPerson { get; set; }

        public Address PropertyAddress { get; set; }

        public decimal? SalesContractPrice { get; set; }

        public string PropertyTypeCode { get; set; }

        [DisplayName("Appraisal Type")]
        [Required(ErrorMessage = "Please select an Appraisal Type")]
        public string AppraisalTypeCode { get; set; }

        public string AppraisalPurposeCode { get; set; }

        public bool? ContactForAccess { get; set; }

        public string LegalDescription { get; set; }

        public string Comments { get; set; }

        #endregion

        #region Methods

        

        #endregion

    }

}
