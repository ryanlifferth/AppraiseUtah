using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AppraiseUtah.Models
{
    public class AppraisalContext : DbContext
    {

        #region Fields
        #endregion

        #region Properties

        public DbSet<Appraisal> Appraisals { get; set; }
        public DbSet<Person> Persons { get; set; }

        #endregion

        #region Constructors

        public AppraisalContext(string connection = null) : base(connection)
        { 
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the appraisal from the database stored proc using the appraisal id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Appraisal GetAppraisal(int id)
        {
            var appraisal = new Appraisal();
            DataTable dataTable = new DataTable();

            // Create and populate the command object, including the input parameter 
            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandText = "GetAppraisal";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Input });

            try
            {
                // Open the connection and execute the reader
                this.Database.Connection.Open();
                var reader = cmd.ExecuteReader();

                // Load the results into a datatable to be able to populate all complex data type objects on the appraisal object
                dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                // Do something
            }

            // Popluate the appraisal object
            if (dataTable.Rows.Count > 0)
            {
                appraisal = PopulateAppraisalFromDataTable(dataTable);
            }

            return appraisal;
        }

        #region Private Methods

        /// <summary>
        /// Populates/hydrates the Appraisal data with data retrieved from the database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private Appraisal PopulateAppraisalFromDataTable(DataTable data)
        {
            // Populate the appraisal object with the data from the Appraisal table
            var reader = data.CreateDataReader();
            var appraisal = ((IObjectContextAdapter)this).ObjectContext.Translate<Appraisal>(reader, "Appraisals", MergeOption.AppendOnly).FirstOrDefault();

            if (appraisal != null)
            {
                appraisal.ClientPerson = PopulatePersonFromDataTable(data, "ClientPerson");         // Populate the Appraisal.ClientPerson object
                appraisal.ClientAddress = PopulateAddressFromDataTable(data, "ClientAddress");      // Populate the Appraisal.ClientAddress object
                appraisal.OccupantPerson = PopulatePersonFromDataTable(data, "OccupantPerson");     // Populate the Appraisal.OccupantPerson object
                appraisal.PropertyAddress = PopulateAddressFromDataTable(data, "PropertyAddress");  // Populate the Appraisal.PropertyAddress object
            }

            return appraisal;
        }

        /// <summary>
        /// Populates/hydrates a person object based on data from the database and a columnPrefix
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnPrefix"></param>
        /// <returns></returns>
        private static Person PopulatePersonFromDataTable(DataTable data, string columnPrefix)
        {
            Person person = null;

            if (data.Rows.Count > 0) 
            {
                person = new Person();
                person.PersonId = (int)data.Rows[0][columnPrefix + "_PersonId"];
                person.PersonType = data.Rows[0][columnPrefix + "_PersonType"].ToString();
                person.FirstName = data.Rows[0][columnPrefix + "_FirstName"].ToString();
                person.LastName = data.Rows[0][columnPrefix + "_LastName"].ToString();
                person.CompanyName = data.Rows[0][columnPrefix + "_CompanyName"].ToString();
                person.Email = data.Rows[0][columnPrefix + "_Email"].ToString();
                person.Phone = data.Rows[0][columnPrefix + "_Phone"].ToString();
            }

            return person;
        }
        
        /// <summary>
        /// Populates/hydrates an address object based on data from the database and a columnPrefix
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnPrefix"></param>
        /// <returns></returns>
        private static Address PopulateAddressFromDataTable(DataTable data, string columnPrefix)
        {
            Address address = null;

            if (data.Rows.Count > 0)
            {
                address = new Address();
                address.AddressId = (int)data.Rows[0][columnPrefix + "_AddressId"];
                address.AddressType = data.Rows[0][columnPrefix + "_AddressType"].ToString();
                address.Address1 = data.Rows[0][columnPrefix + "_Address1"].ToString();
                address.Address2 = data.Rows[0][columnPrefix + "_Address2"].ToString();
                address.City = data.Rows[0][columnPrefix + "_City"].ToString();
                address.StateCode = data.Rows[0][columnPrefix + "_StateCode"].ToString();
                address.PostalCode = data.Rows[0][columnPrefix + "_PostalCode"].ToString();
            }

            return address;
        }

        #endregion

        #endregion

    }
}
