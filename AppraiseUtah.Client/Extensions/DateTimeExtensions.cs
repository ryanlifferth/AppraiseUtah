using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppraiseUtah.Client.Extensions
{
    public static class DateTimeExtensions
    {

        public static string ToFormattedLocal(this DateTime dateTime)
        {
            //var formattedDate = dateTime.ToLocalTime().ToString("MMMM d, yyyy h:mm tt");  // ToLocalTime is the time of the server, this is hosted on Azure and so will be in the timezone of that server
            var formattedDate = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time")).ToString("MMMM d, yyyy h:mm tt");
            
            return formattedDate;
        }

    }
}
