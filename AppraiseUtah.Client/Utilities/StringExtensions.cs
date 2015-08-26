using System;
using System.Text;

namespace AppraiseUtah.Client.Utilities
{
    public static class StringExtensions
    {

        public static string FormatPhone(this string phoneNumber)
        {
            if (phoneNumber.Length == 10)
            {
                StringBuilder newPhone = new StringBuilder("(");
                newPhone.Append(phoneNumber.Substring(0, 3)).Append(") ");
                newPhone.Append(phoneNumber.Substring(3, 3)).Append("-");
                newPhone.Append(phoneNumber.Substring(6, 4));

                return newPhone.ToString();
            }

            return phoneNumber;
        }

    }
}
