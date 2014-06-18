using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Services;
using System.Web.Services;
using AppraiseUtah.Client.Models;
using AppraiseUtah.Client.ServiceModels;

namespace AppraiseUtah.Web.Controllers
{
    public class RatesController : ApiController
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Rates Get(string id)
        {
            var ratesServiceModel = new RatesServiceModel();
            return ratesServiceModel.GetRatesByState(id);
        }



    }
}
