using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AppraiseUtah.Models;
using AppraiseUtah.ViewModels;

namespace AppraiseUtah.ServiceModels
{
    public class AppraisalServiceModel
    {

        #region Fields

        AppraisalContext _db = new AppraisalContext("AppraisalDBContext");
        
        #endregion

        #region Properties
        #endregion

        #region Constructor

        #endregion

        #region Methods

        public virtual Appraisal Get_Appraisal(int id)
        {
            //var results = _db.Database.SqlQuery<Appraisal>("EXEC GetAppraisal {0}", id);
            var appraisal = _db.GetAppraisal(id);
            return appraisal; ;
        }

        public virtual int Save_Appraisal(AppraisalViewModel appraisalViewModel)
        {
            SqlParameter newAppraisalIdParam = new SqlParameter("newAppraisalOrderId", SqlDbType.Int);
            newAppraisalIdParam.Direction = ParameterDirection.Output;

            var orderResult = _db.Database.ExecuteSqlCommand("CreateAppraisalOrder " +
                                                                "@clientFirstName," +
                                                                "@clientLastName," +
                                                                "@clientCompanyName," +
                                                                "@clientEmail," +
                                                                "@clientPhone," +
                                                                "@clientAddress1," +
                                                                "@clientAddress2," +
                                                                "@clientCity," +
                                                                "@clientStateCode," +
                                                                "@clientPostalCode," +
                                                                "@occupantFirstName," +
                                                                "@occupantLastName," +
                                                                "@occupantCompanyName," +
                                                                "@occupantEmail," +
                                                                "@occupantPhone," +
                                                                "@propertyAddress1," +
                                                                "@propertyAddress2," +
                                                                "@propertyCity," +
                                                                "@propertyStateCode," +
                                                                "@propertyPostalCode," +
                                                                "@AppraiserId," +
                                                                "@SalesContractPrice," +
                                                                "@PropertyTypeCode," +
                                                                "@AppraisalTypeCode," +
                                                                "@AppraisalPurposeCode," +
                                                                "@ContactForAccess," +
                                                                "@LegalDescription," +
                                                                "@Comments," +
                                                                "@newAppraisalOrderId OUTPUT",
                    CreateParameter("clientFirstName", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientPerson.FirstName),
                    CreateParameter("clientLastName", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientPerson.LastName),
                    CreateParameter("clientCompanyName", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientPerson.CompanyName),
                    CreateParameter("clientEmail", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientPerson.Email),
                    CreateParameter("clientPhone", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientPerson.Phone),
                    CreateParameter("clientAddress1", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientAddress.Address1),
                    CreateParameter("clientAddress2", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientAddress.Address2),
                    CreateParameter("clientCity", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientAddress.City),
                    CreateParameter("clientStateCode", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientAddress.StateCode),
                    CreateParameter("clientPostalCode", SqlDbType.VarChar, appraisalViewModel.Appraisal.ClientAddress.PostalCode),
                    CreateParameter("occupantFirstName", SqlDbType.VarChar, appraisalViewModel.Appraisal.OccupantPerson.FirstName),
                    CreateParameter("occupantLastName", SqlDbType.VarChar, appraisalViewModel.Appraisal.OccupantPerson.LastName),
                    CreateParameter("occupantCompanyName", SqlDbType.VarChar, appraisalViewModel.Appraisal.OccupantPerson.CompanyName),
                    CreateParameter("occupantEmail", SqlDbType.VarChar, appraisalViewModel.Appraisal.OccupantPerson.Email),
                    CreateParameter("occupantPhone", SqlDbType.VarChar, appraisalViewModel.Appraisal.OccupantPerson.Phone),
                    CreateParameter("propertyAddress1", SqlDbType.VarChar, appraisalViewModel.Appraisal.PropertyAddress.Address1),
                    CreateParameter("propertyAddress2", SqlDbType.VarChar, appraisalViewModel.Appraisal.PropertyAddress.Address2),
                    CreateParameter("propertyCity", SqlDbType.VarChar, appraisalViewModel.Appraisal.PropertyAddress.City),
                    CreateParameter("propertyStateCode", SqlDbType.VarChar, appraisalViewModel.Appraisal.PropertyAddress.StateCode),
                    CreateParameter("propertyPostalCode", SqlDbType.VarChar, appraisalViewModel.Appraisal.PropertyAddress.PostalCode),
                    CreateParameter("appraiserId", SqlDbType.Int, appraisalViewModel.Appraisal.AppraiserId),
                    CreateParameter("salesContractPrice", SqlDbType.Decimal, appraisalViewModel.Appraisal.SalesContractPrice),
                    CreateParameter("propertyTypeCode", SqlDbType.VarChar, appraisalViewModel.Appraisal.PropertyTypeCode),
                    CreateParameter("appraisalTypeCode", SqlDbType.VarChar, appraisalViewModel.Appraisal.AppraisalTypeCode),
                    CreateParameter("appraisalPurposeCode", SqlDbType.VarChar, appraisalViewModel.Appraisal.AppraisalPurposeCode),
                    CreateParameter("contactForAccess", SqlDbType.Bit, appraisalViewModel.Appraisal.ContactForAccess),
                    CreateParameter("legalDescription", SqlDbType.VarChar, appraisalViewModel.Appraisal.LegalDescription),
                    CreateParameter("comments", SqlDbType.NText, appraisalViewModel.Appraisal.Comments),
                    newAppraisalIdParam );

            var appraisalId = (int)newAppraisalIdParam.Value;

            return appraisalId;
        }

        private SqlParameter CreateParameter(string paramName, SqlDbType sqlDbType, object paramValue)
        {
            SqlParameter param = new SqlParameter(paramName, sqlDbType);
            param.Value = (paramValue == null) ? DBNull.Value : paramValue;
            
            return param;
        }

        #endregion

    }
}
