using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppraiseUtah.Client.Models
{
    public class Rates
    {

        #region Properties

        public string State { get; set; }

        public Rate Today { get; set; }

        public Rate LastWeek { get; set; }

        #endregion

    }
}
