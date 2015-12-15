// <Copyright file="SendGridHelper.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the common function for send email with sendgrid
// ---------------------------------------------------------------------------------
//	Date Created		: Nov 11, 2015
//	Author			    : <Jiangping Lu>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          Add description
//	Date Modified		: Dec 2, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 
//  Refer website      : https://github.com/sendgrid/sendgrid-csharp
/////////////////////////////////////////////////////////////////////////////////////////

using BOSMDMProdConsoleApp.Models;
using SendGrid;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using RestSharp;
using System.Configuration;

namespace BOSMDMProdConsoleApp.Instruments
{
    public class SendGridHelper
    {
        /// <summary>
        /// Create Email
        /// </summary>
        private SendGridMessage CreateEmail(SendGridMail mail)
        {
            //Create the email object
            var eMail = new SendGridMessage();

            //add the messsage properties
            eMail.From = new MailAddress(mail.EmailFrom); //"abc@example.com"
            List<string> recipients = mail.EmailTo;
            eMail.AddTo(recipients);
            eMail.Subject = mail.EmailSubject; // "Test the sendgrid Librabry";

            //add the HTML and Text bodies
            eMail.Html = mail.EmailHTML;
            eMail.Text = mail.EmailText; 

            //add a footer to the message
            //eMail.EnableFooter("Plain text footer", "<p><em>Azure Team</em></p><p><em>PhoneNum: 110 </em></p>");
            eMail.EnableTemplateEngine("f362b3d9-e2f2-4dce-b3d0-db6c925988a5");
            //eMail.EnableTemplate("");
            return eMail;
        }

        /// <summary>
        /// Send Email
        /// </summary>
        public void SendEmail(SendGridMail mail)
        {
            //create the email object first
            SendGridMessage eMessage = CreateEmail(mail);

            //create network credentials to access you sendgrid account
            var username = "ljp";
            var pswd = "ljp6298156";

            //create a web transport. Using Credentials
            var credentials = new NetworkCredential(username, pswd);
            var transportWeb = new Web(credentials);

            //send email
            transportWeb.DeliverAsync(eMessage);

            //var username = System.Environment.GetEnvironmentVariable("SENDGRID_USER");
            //var pswd = System.Environment.GetEnvironmentVariable("SENDGRID_PASS");
            //var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            //// create a Web transport, using API Key
            //var apiKey = "your_sendgrid_api_key";
            //var transportWeb = new Web(apiKey);
        }


        /// <summary>
        /// Send Email in WebAPi
        /// {"sub": {":next":["ljp123"],"current":["ljp888"]},"category": "Promotions", "filters": {"templates": {"settings": {"enable": 1,"template_id": "
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="ToEmail"></param>
        /// <param name="currentStepOwner"></param>
        /// <param name="nextStepOwner"></param>
        public void SendEmailWebApi(string subject, string ToEmail, string currentStepOwner, string nextStepOwner)
        {
            //Get parameters from app.config
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var ApiWebSite = config.AppSettings.Settings["ApiWebSite"].Value;
            var ApiUrlAddress = config.AppSettings.Settings["ApiUrlAddress"].Value;
            var SendGridName = config.AppSettings.Settings["SendGridName"].Value;
            var SendGridPassword = config.AppSettings.Settings["SendGridPassword"].Value;
            var EmailFrom = config.AppSettings.Settings["EmailFrom"].Value;
            var templateId = config.AppSettings.Settings["TemplateId"].Value;

            //create json header for sending with template
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"sub\": {\":next\":[\"");
            sb.Append(nextStepOwner);
            sb.Append("\"],\"current\":[\"");
            sb.Append(currentStepOwner);
            sb.Append("\"]},\"category\": \"Promotions\", \"filters\": {\"templates\": {\"settings\": {\"enable\": 1,\"template_id\": \"");
            sb.Append(templateId);
            sb.Append("\"}}}}");

            //Fromat request Parameters
            var client = new RestClient(ApiWebSite);
            var request = new RestRequest(ApiUrlAddress, Method.POST);
            request.AddParameter("api_user", SendGridName);
            request.AddParameter("api_key", SendGridPassword);
            request.AddParameter("to[]", ToEmail);
            request.AddParameter("subject", subject);
            request.AddParameter("from", EmailFrom);
            request.AddParameter("x-smtpapi", sb.ToString());
            request.AddParameter("html", "<!DOCTYPE HTML><html><head></header><body><img src=\"https://marketing-image-production.s3.amazonaws.com/uploads/609daab121cfbea8768eaeca2c6d6033378fc1ad3e32ee8bc5ad5fe3832fe4f9d55c6c54f1deb4b0a9ac700798e6beba5e873a161f7cb9a5e0e727a72e8544e1.jpg\"></img></body></html>"); //The plain text content of your email message.

            // execute the request
            var response = client.Execute(request);
            var content = response.Content;
        }
    }
}
