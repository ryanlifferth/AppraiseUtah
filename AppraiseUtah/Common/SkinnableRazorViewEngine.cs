using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppraiseUtah.Common
{
    /// <summary>
    /// Allows partial views to be put in a PartialViews folder under the Views\Controller folder.
    /// </summary>
    public class SkinnableRazorViewEngine : RazorViewEngine
    {

        private static string[] NewPartialViewFormats = new[] { 
                "~/Views/{1}/PartialViews/{0}.cshtml",                
                "~/Views/Shared/PartialViews/{0}.cshtml"
        };

        public SkinnableRazorViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }
    }
}
