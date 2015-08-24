using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using AppraiseUtah.Client.Models;

namespace AppraiseUtah.Client.ServiceModels
{
    public class RatesServiceModel
    {

        #region Fields

        private string _zwsid = "X1-ZWz1bazgl9xxjf_1acr8";

        #endregion

        #region Properties
        #endregion

        #region Constructor
        #endregion

        #region Methods

        public Rates GetRatesByState(string state)
        {
            var rates = new Rates() { State = state, Today = new Rate(), LastWeek = new Rate() };

            try
            {
                // TODO:  Add rates to cache (24 hour cache)

                String uri = String.Format("http://www.zillow.com/webservice/GetRateSummary.htm?zws-id={0}&state={1}", _zwsid, state);

                // Make the HTTP request / get the response
                HttpWebRequest Request = (System.Net.HttpWebRequest)HttpWebRequest.Create(uri);
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();

                // Parse the HTTP response into an XML document
                XmlDocument xml = new XmlDocument();
                xml.Load(Response.GetResponseStream());
                XmlElement root = xml.DocumentElement;

                // Get today's rates via XPath
                XmlNode xmlNode = root.SelectSingleNode("//response/today/rate[@loanType='thirtyYearFixed']");
                rates.Today.ThirtyYearFixed = decimal.Parse(xmlNode.InnerText);
                xmlNode = root.SelectSingleNode("//response/today/rate[@loanType='fifteenYearFixed']");
                rates.Today.FifteenYearFixed = decimal.Parse(xmlNode.InnerText);
                xmlNode = root.SelectSingleNode("//response/today/rate[@loanType='fiveOneARM']");
                rates.Today.FiveOneArm = decimal.Parse(xmlNode.InnerText);

                // Get last weeks's rates via XPath
                xmlNode = root.SelectSingleNode("//response/lastWeek/rate[@loanType='thirtyYearFixed']");
                rates.LastWeek.ThirtyYearFixed = decimal.Parse(xmlNode.InnerText);
                xmlNode = root.SelectSingleNode("//response/lastWeek/rate[@loanType='fifteenYearFixed']");
                rates.LastWeek.FifteenYearFixed = decimal.Parse(xmlNode.InnerText);
                xmlNode = root.SelectSingleNode("//response/lastWeek/rate[@loanType='fiveOneARM']");
                rates.LastWeek.FiveOneArm = decimal.Parse(xmlNode.InnerText);

                Response.Close();

            }
            catch (Exception ex)
            {
            }

            return rates;
        }

        #endregion

    }
}
