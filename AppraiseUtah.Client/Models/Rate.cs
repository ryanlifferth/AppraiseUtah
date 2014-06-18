using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AppraiseUtah.Client.Models
{
    public class Rate
    {

        #region Properties

        [DisplayName("Thirty Year Fixed")]
        public decimal ThirtyYearFixed { get; set; }

        [DisplayName("Thirty Year Fixed Count")]
        public int ThirtyYearFixedCount { get; set; }

        [DisplayName("Fifteen Year Fixed")]
        public decimal FifteenYearFixed { get; set; }

        [DisplayName("Fifteen Year Fixed Count")]
        public int FifteenYearFixedCount { get; set; }

        [DisplayName("Five One ARM")]
        public decimal FiveOneArm { get; set; }

        [DisplayName("Five One ARM Count")]
        public int FiveOneArmCount { get; set; }

        #endregion

    }
}
