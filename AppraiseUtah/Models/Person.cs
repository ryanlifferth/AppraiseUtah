using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppraiseUtah.Models
{
    public class Person
    {

        #region Properties
        public int PersonId { get; set; }

        public string PersonType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        #endregion

    }
}
