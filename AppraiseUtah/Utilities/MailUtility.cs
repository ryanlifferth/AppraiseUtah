using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppraiseUtah.ViewModels;
using System.Net.Mail;

namespace AppraiseUtah.Utilities
{
    public static class MailUtility
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods

        public static void SendConfirmationEmail(AppraisalViewModel appraisal)
        {
            SendConfirmationEmailToClient(appraisal);
            SendOrderEmailToAppraiser(appraisal);
        }

        #endregion

        #region Private Methods

        private static void SendConfirmationEmailToClient(AppraisalViewModel appraisal)
        {
            MailMessage message = new MailMessage();

            if (string.IsNullOrEmpty(appraisal.Appraisal.ClientPerson.Email))
            {
                throw new Exception("Client email is null or empty.  Cannot complete order");
            }

            //message.From = new MailAddress("WebOrders@appraiseutah.com", "Web Order");
            //message.To.Add(new MailAddress("orders@appraiseutah.com"));
            message.From = new MailAddress("ryan@lifferth.com", "AppraiseUtah.com");
            message.To.Add(new MailAddress(appraisal.Appraisal.ClientPerson.Email));
            message.CC.Add(new MailAddress("orders@appraiseutah.com"));

            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "AppraisalUtah.com Order Confirmation - ID: " + appraisal.Appraisal.Id;
            message.Body = BuildConfirmationBody(appraisal, true);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(message);
        }

        private static void SendOrderEmailToAppraiser(AppraisalViewModel appraisal)
        {
            MailMessage message = new MailMessage();
            var appraiser = GetAppraiserForDisplay.GetAppraiser(appraisal.Appraisal.AppraiserId);
            if (string.IsNullOrEmpty(appraiser.Email))
            {
                throw new Exception("Appraiser email is null or empty.  Cannot complete order");
            }

            //message.From = new MailAddress("WebOrders@appraiseutah.com", "Web Order");
            //message.To.Add(new MailAddress("orders@appraiseutah.com"));
            message.From = new MailAddress("ryan@lifferth.com", "AppraiseUtah.com");
            message.To.Add(new MailAddress(appraiser.Email));
            message.CC.Add(new MailAddress("orders@appraiseutah.com"));

            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "New appraisal order from AppraisalUtah.com - " + appraisal.Appraisal.PropertyAddress.Address1 + ", " + appraisal.Appraisal.PropertyAddress.City + " (ID: " + appraisal.Appraisal.Id + ")";
            message.Body = BuildConfirmationBody(appraisal, false);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(message);
        }

        public static string BuildConfirmationBody(AppraisalViewModel appraisal, bool clientConfirmation)
        {
            var appraiser = GetAppraiserForDisplay.GetAppraiser(appraisal.Appraisal.AppraiserId);       // gets the AppraiserModel for the selected appraiser
            var professionalDesignations = (!string.IsNullOrEmpty(appraiser.ProfessionalDesignations)) ? " " + appraiser.ProfessionalDesignations : "";
            var propertyType = DropDownData.GetPropertyTypeList().Where(x => x.PropertyTypeCode == appraisal.Appraisal.PropertyTypeCode).FirstOrDefault();
            var appraisalPurpose = DropDownData.GetAppraisalPurposeList().Where(x => x.AppraisalPurposeCode == appraisal.Appraisal.AppraisalPurposeCode).FirstOrDefault();
            var propertyAddress = appraisal.Appraisal.PropertyAddress;
            var property = appraisal.Appraisal;
            var occupant = appraisal.Appraisal.OccupantPerson;
            bool legalSalesData = ((property.SalesContractPrice != null) || (!string.IsNullOrEmpty(property.LegalDescription))) ? true : false;

            var body = new StringBuilder();

            // Build the CSS
            body.Append(@"<link href=""http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,400,700"" rel=""stylesheet"" type=""text/css"" />");
            body.Append(@"<link href=""http://fonts.googleapis.com/css?family=Lato:400,700,900,400italic"" rel=""stylesheet"" type=""text/css"" />");
            body.Append("<style>");
            body.Append(@"html,body{background-color:#f8f8f8;font-family:""Open Sans"", Calibri, Candara, Arial, sans-serif;font-size:15px;height:100%;margin:10px 20px;padding:0}");
            body.Append(@".green-header{color:#6abe59}");
            body.Append(@"h1,h2,h3,h4,h5,h6,.h1,.h2,.h3,.h4,.h5,.h6{font-weight:500;line-height:1.1;color:inherit}");
            body.Append(@"h2{font-size:32px}");
            body.Append(@"h1,h2,h3{margin-top:21px;margin-bottom:10.5px}");
            body.Append(@"h4{font-size:19px}");
            body.Append(@"h4,h5,h6{margin-top:10.5px;margin-bottom:10.5px}");
            body.Append(@"#ConfirmationInfo{border:none;border-bottom:1px dashed #a3a3a3;border-top:1px dashed #a3a3a3;margin-top:15px;min-height:200px;padding:0 10px}");
            body.Append(@"fieldset{border:1px solid silver;margin:0 2px;padding:.35em .625em .75em}");
            body.Append(@"legend{display:block;line-height:inherit;color:#333;border-bottom:1px solid #e5e5e5}");
            body.Append(@"#ConfirmationInfo > legend{border:none;font-size:18px;margin-bottom:5px;margin-left:15px;width:auto;padding:0 4px 4px}");
            body.Append(@"#ConfirmationInfo .confirmation-section{font-size:13px;overflow:auto;margin:0 0 8px;padding:5px 0}");
            body.Append(@"#ConfirmationInfo .confirmation-section .section-label{color:#19aacf;float:left;font-weight:700;vertical-align:top;width:150px}");
            body.Append(@"#appraiserDisplay .appraiser-name{margin-bottom:3px;margin-top:0}");
            body.Append(@"#appraiserDisplay .appraiser-data{clear:both;font-size:13px;line-height:normal;margin-top:0;overflow:auto}");
            body.Append(@"address{display:block;font-style:normal;width:180px}");
            body.Append(@".pull-left{float:left!important}");
            body.Append(@"abbr[title],abbr[data-original-title]{cursor:help;border-bottom:1px dotted #999}");
            body.Append(@"abbr[title]{border-bottom:1px dotted}");
            body.Append(@"abbr{margin-right:6px}");
            body.Append(@".email{color:#428bca}");
            body.Append(@"a{color:#428bca;text-decoration:none}");
            body.Append(@"a:hover,a:focus{color:#2a6496;text-decoration:underline}");
            body.Append(@"#ConfirmationInfo .confirmation-section .name{font-size:16px}");
            body.Append(@"#ConfirmationInfo .occupant-label{color:#707070;font-style:italic;float:left;margin-right:10px;vertical-align:top}");
            body.Append(@"#ConfirmationInfo .occupant-label span{border-bottom:1px dotted #999}");
            body.Append(@".trailing-label{color:#707070;font-style:italic}");
            body.Append(@"strong{margin:0 5px}");
            body.Append(@"#additionalComments{margin-left:0}");
            body.Append(@"#ConfirmationInfo .confirmation-section .section-data,#ConfirmationInfo .confirmation-section .section-data-col-2,#ConfirmationInfo .confirmation-section .section-data-col-1,#ConfirmationInfo .occupant-data{float:left}");
            body.Append(@"#ConfirmationInfo .contact-for-access,#ConfirmationInfo .confirmation-section .sales-info{clear:both;margin-top:4px}");
            body.Append(@"#OrderDisclaimer{color:#959595;font-size:13px;font-style:italic;text-align:center;}");
            body.Append(@".clearfix:before, .clearfix:after {content: "" "";display: table;}");
            body.Append("</style>");

            // Header
            if (clientConfirmation)
            {
                body.Append(@"<h2 class=""green-header"">Thank you for your order at <em>AppraiseUtah.com</em></h2>");
                body.Append(@"<h4 class=""green-header"">Your appraisal order number is:  <strong>" + appraisal.Appraisal.Id + "</strong></h4>");
            }
            else
            {
                body.Append(@"<h2 class=""green-header"">You have received a new Appraisal Order through <em>AppraiseUtah.com</em></h2>");
                body.Append(@"<h4 class=""green-header"">The appraisal order number is:  <strong>" + appraisal.Appraisal.Id + "</strong></h4>");
            }

            body.Append(@"<fieldset id=""ConfirmationInfo"">");
            body.Append(@"<legend>Appraisal Order Information</legend>");

            // Build the APPRAISER section
            body.Append(@"<div class=""confirmation-section"">");
            body.Append(@"<div class=""section-label col-xs-2"">Appraiser</div>");
            body.Append(@"<div id=""appraiserDisplay"" class=""section-data col-xs-10"">");
            body.Append(@"<h4 class=""appraiser-name"">" + appraiser.CompanyName + @"<span class=""designation"">" + professionalDesignations + "</span></h4>");
            body.Append(@"<div class=""appraiser-data"">");
            body.Append(@"<address class=""pull-left"">");
            body.Append(appraiser.Address.Address1).Append("<br />");
            if (!string.IsNullOrEmpty(appraiser.Address.Address2))
            {
                body.Append(appraiser.Address.Address2).Append("<br />");
            }
            body.Append(appraiser.Address.City).Append(",");
            body.Append(appraiser.Address.StateCode).Append("  ");
            body.Append(appraiser.Address.PostalCode);
            body.Append(@"</address>");
            body.Append(@"<div class=""contact pull-left"">");
            body.Append(buildContactItems(appraiser));
            body.Append(@"</div>");
            body.Append(@"</div>");
            body.Append(@"</div>");
            body.Append(@"</div>");

            // Build the CLIENT section
            body.Append(@"<div class=""confirmation-section"">");
            body.Append(@"<div class=""section-label col-xs-2"">");
            if (clientConfirmation)
            {
                body.Append("Your Info");
            }
            else
            {
                body.Append("Client Info");
            }            
            body.Append(@"</div>");
            body.Append(@"<div class=""section-data col-xs-10"">");
            if (!string.IsNullOrEmpty(appraisal.Appraisal.ClientPerson.CompanyName))
            {
                body.Append(@"<div class=""name"">" + appraisal.Appraisal.ClientPerson.CompanyName + "</div>");
            }
            body.Append(@"<div class=""name"">").Append(appraisal.Appraisal.ClientPerson.FirstName).Append(" ").Append(appraisal.Appraisal.ClientPerson.LastName).Append("</div>");
            body.Append(@"<address class=""pull-left"">");
            body.Append(appraisal.Appraisal.ClientAddress.Address1).Append("<br />");
            if (!string.IsNullOrEmpty(appraisal.Appraisal.ClientAddress.Address2))
            {
                body.Append(appraisal.Appraisal.ClientAddress.Address2).Append("<br />");
            }
            body.Append(appraisal.Appraisal.ClientAddress.City).Append(",");
            body.Append(appraisal.Appraisal.ClientAddress.StateCode).Append("  ");
            body.Append(appraisal.Appraisal.ClientAddress.PostalCode);
            body.Append(@"</address>");
            body.Append(@"<div class=""section-data-col-2"">");
            body.Append(@"<abbr title=""phone"">P:</abbr>" + appraisal.Appraisal.ClientPerson.Phone + "<br />");
            body.Append(@"<a href=""mailto:" + appraisal.Appraisal.ClientPerson.Email + @""" class=""email"">" + appraisal.Appraisal.ClientPerson.Email + "</a>");
            body.Append(@"</div>");
            body.Append(@"</div>");
            body.Append(@"</div>");

            // Build the PROPERTY section
            body.Append(@"<div class=""confirmation-section"">");
            body.Append(@"<div class=""section-label col-xs-2"">Property Info</div>");
            body.Append(@"<div class=""section-data col-xs-10"">");

            body.Append(@"<address class=""pull-left"">");
            body.Append(propertyType.PropertyTypeDescription).Append("<br />");
            body.Append(propertyAddress.Address1).Append("<br />");
            if (!string.IsNullOrEmpty(propertyAddress.Address2))
            {
                body.Append(propertyAddress.Address2).Append("<br />");
            }
            body.Append(propertyAddress.City).Append(", ").Append(propertyAddress.StateCode).Append(" ").Append(propertyAddress.PostalCode);
            body.Append("</address>");

            body.Append(@"<div class=""section-data-col-2"">");

            var dataPresent = false;
            if (OccupantDataPresent(appraisal))
            {
                body.Append(@"<div class=""occupant-label"">");
                body.Append(@"<span>Occupant:</span>");
                body.Append(@"</div>");
                body.Append(@"<div class=""occupant-data"">");
                if (!string.IsNullOrEmpty(occupant.FirstName) || !string.IsNullOrEmpty(occupant.LastName))
                {
                    body.Append(@"<span class=""occupant"">").Append(occupant.FirstName).Append(" ").Append(occupant.LastName).Append("</span>");
                    dataPresent = true;
                }
                if (!string.IsNullOrEmpty(occupant.Phone))
                {
                    if (dataPresent)
                    {
                        body.Append(@"<br />");
                    }
                    body.Append(@"<abbr title=""phone"">P:</abbr>").Append(occupant.Phone);
                    dataPresent = true;
                }

                if (!string.IsNullOrEmpty(occupant.Email))
                {
                    if (dataPresent)
                    {
                        body.Append(@"<br />");
                    }
                    body.Append(@"<a href=""mailto:").Append(occupant.Email).Append(@""" class=""email"">").Append(occupant.Email).Append("</a>");
                }
                body.Append(@"</div>");
            }
            body.Append(@"</div>");

            body.Append(@"<div class=""clearfix""></div>");

            if (appraisal.Appraisal.ContactForAccess)
            {
                body.Append(@"<div class=""contact-for-access"">Contact owner/occupant for access?  <strong>Yes</strong></div>");
            }

            if (legalSalesData)
            {
                body.Append(@"<div class=""sales-info"">");
                if (property.SalesContractPrice != null)
                {
                    body.Append(@"<div class=""price"">");
                    body.Append(string.Format("{0:C0}", property.SalesContractPrice));
                    body.Append(@"<span class=""trailing-label"">- sales/contract price</span>");
                    body.Append(@"</div>");
                }

                if (!string.IsNullOrEmpty(property.LegalDescription))
                {
                    body.Append(@"<div class=""legal-description"">");
                    body.Append(@"<strong>Legal description:</strong>");
                    body.Append(property.LegalDescription);
                    body.Append(@"</div>");
                }
                body.Append(@"</div>");
            }
            body.Append(@"</div>");
            body.Append(@"</div>");


            // Build the COMMENTS section
            body.Append(@"<div class=""confirmation-section bottom"">");
            body.Append(@"<div class=""section-label col-xs-2"">Comments</div>");
            body.Append(@"<div class=""section-data col-xs-10"">");
            if (appraisalPurpose != null)
            {
                body.Append(@"<div>");
                body.Append(appraisalPurpose.AppraisalPurposeDescription).Append(@"<span class=""trailing-label"">- Appraisal purpose</span>");
                body.Append(@"</div>");
            }
            if (!string.IsNullOrEmpty(appraisal.Appraisal.Comments))
            {
                body.Append(@"<div>");
                body.Append(@"<strong id=""additionalComments"">Additional comments:</strong>");
                body.Append(appraisal.Appraisal.Comments);
                body.Append(@"</div>");
            }
            if (string.IsNullOrEmpty(appraisal.Appraisal.Comments) && string.IsNullOrEmpty(appraisal.Appraisal.AppraisalPurposeCode))
            {
                body.Append(@"<span>--</span>");
            }
            body.Append(@"</div>");
            body.Append(@"</div>");
            
            body.Append(@"</fieldset>");


            // Build disclaimer
            body.Append(@"<div id=""OrderDisclaimer"">");
            if (clientConfirmation)
            {
                body.Append(BuildUserDisclaimer());
            }
            else
            {
                body.Append(BuildAppraiserDisclaimer());
            }
            body.Append("</div>");

            return body.ToString();
        }

        private static string buildContactItems(AppraiseUtah.Models.Appraiser appraiser)
        {
            var contact = "";

            contact = (!string.IsNullOrEmpty(appraiser.Phone)) ? @"<abbr title=""Phone"">P:</abbr>" + appraiser.Phone : "";

            if (contact != "")
            {
                contact += (!string.IsNullOrEmpty(appraiser.Fax)) ? @"<br /><abbr title=""Fax"">F:</abbr>" + appraiser.Fax : "";
            }
            else
            {
                contact += (!string.IsNullOrEmpty(appraiser.Fax)) ? @"<abbr title=""Fax"">F:</abbr>" + appraiser.Fax : "";
            }

            if (contact != "")
            {
                contact += (!string.IsNullOrEmpty(appraiser.Email)) ? @"<br /><a href=""mailto:" + appraiser.Email + @""" class=""email"">" + appraiser.Email + "</a>" : "";
            }
            else
            {
                contact += (!string.IsNullOrEmpty(appraiser.Email)) ? @"<a href=""mailto:" + appraiser.Email + @""" class=""email"">" + appraiser.Email + "</a>" : "";
            }

            return contact;
        }

        private static bool OccupantDataPresent(AppraisalViewModel appraisal)
        {
            // Check to see if Occupant data is present
            bool occupantData = false;
            var occupant = appraisal.Appraisal.OccupantPerson;
            if (occupant != null)
            {
                if (!string.IsNullOrEmpty(occupant.FirstName) ||
                    !string.IsNullOrEmpty(occupant.LastName) ||
                    !string.IsNullOrEmpty(occupant.Phone) ||
                    !string.IsNullOrEmpty(occupant.Email))
                {
                    occupantData = true;
                }
            }

            return occupantData;
        }

        private static string BuildUserDisclaimer()
        {
            var msg = new StringBuilder("This is the only communication you will recieve from AppraiseUtah.com.  The Appraiser should contact you soon to arrange all appraisal details, including payment.  AppraiseUtah.com is not responsible for the appraisal in any way.");

            return msg.ToString();
        }

        private static string BuildAppraiserDisclaimer()
        {
            var msg = new StringBuilder("This is the only communication you will receive from AppraiseUtah.com.  As the appraiser, you should reach out to the client to arrange all appraisal details, including payment.  AppraiseUtah.com is not responsible for the appraisal in any way.");

            return msg.ToString();
        }

        #endregion

    }
}
