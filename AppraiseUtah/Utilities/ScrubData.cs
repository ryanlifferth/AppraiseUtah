using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppraiseUtah.Utilities
{
    public static class ScrubData
    {

        #region Methods

        /// <summary>
        /// Removes any non-numeric value from the string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveNonNumeric(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        #endregion

    }
}
