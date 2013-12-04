using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppraiseUtah.Models
{
    public class Address
    {

        #region Properties
        public int AddressId { get; set; }

        public string AddressType { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string StateCode { get; set; }

        public string PostalCode { get; set; }

        #endregion

    }
}
