using System;
using System.Collections.Generic;
using System.Data.Entity;
using AppraiseUtah.Common.Constants;

namespace AppraiseUtah.Models
{

    public class Appraisal
    {

        #region Fields
        
        DbContext _db = new DbContext("DBConn");
        
        #endregion

        #region Properties

        public int Id { get; set; }

        public string Status { get; set; }

        public int AppraiserId { get; set; }

        public Person ClientPerson { get; set; }

        public Address ClientAddress { get; set; }

        public Person OccupantPerson { get; set; }

        public Address PropertyAddress { get; set; }

        public decimal SalesContractPrice { get; set; }

        public string PropertyTypeCode { get; set; }

        public string AppraisalTypeCode { get; set; }

        public string AppraisalPurposeCode { get; set; }

        public bool ContactForAccess { get; set; }

        public string LegalDescription { get; set; }

        public string Comments { get; set; }

        #endregion

        #region Methods

        public virtual IEnumerable<Appraisal> Get_Appraisal(int id)
        {
            var results = _db.Database.SqlQuery<Appraisal>("EXEC GetAppraisal {0}", id);
            return null;
        }

        #endregion


    }

}
