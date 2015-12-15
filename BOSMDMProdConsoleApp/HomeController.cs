using BOSMDMProdConsoleApp.Instruments;
using BOSMDMProdConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using System.Xml.Linq;

namespace BOSMDMProdConsoleApp
{
    public class HomeController
    {
        public void HomeControl()
        {
            OperateCompanies();
            //System.Timers.Timer aTimer = new System.Timers.Timer();
            //aTimer.Elapsed += new ElapsedEventHandler(TimeEvent);
            //aTimer.Interval = 1000;
            //aTimer.Enabled = true;
        }

        private void OperateCompanies()
        {
            
                //List<Company> list = GetCompanies();
                var companyList = new List<Company>();
                var companies = new List<Company>();
                SendEmailInSendGrid(companies);
                //if (list != null && list.Count > 0)
                //{
                //    foreach (Company company in list)
                //    {
                //        if (company.SourcePartyId != null && company.SourcePartyId.Trim() != "")
                //        {
                //            companyList.Add(company);
                //        }
                //    }
                //}

                //if (companyList != null && companyList.Count > 0)
                //{
                //    foreach (Company company in companyList)
                //    {
                //        PartyDetailSearchResult partyResult = GetPartyDetails(company.SourcePartyId);
                //        if (company.Status != partyResult.IndependenceStatus)
                //        {
                //            companies.Add(company);
                //            Console.WriteLine(company.SourcePartyId);
                //            //PartyMng mng = new PartyMng();
                //            //CompanyStatusHistory history = new CompanyStatusHistory();
                //            //history.CompanyId = company.CompanyId;
                //            //history.CompanyName = company.CompanyName;
                //            //history.SourcePartyId = company.SourcePartyId;
                //            //history.StatusAfter = company.Status;
                //            //history.StatusBefore = partyResult.IndependenceStatus;
                //            //history.UpdateDate = DateTime.Now;
                //            //mng.SaveCompanyInSql(history);
                            
                //        }
                        
                //    }

                //    SendEmailInSendGrid(companies);
                //}
        }

        /// <summary>
        /// Send Emails with SendGrid
        /// </summary>
        private void SendEmailInSendGrid(List<Company> companies)
        {
            //StringBuilder sb = new StringBuilder();
            //if (companies != null && companies.Count > 0)
            //{
            //    sb.Append("<div><p>Change Paty List");
            //    foreach (Company company in companies)
            //    {
            //        sb.Append("<div><p>" + company.CompanyName);
            //        sb.Append("</p></div>");
            //        sb.Append("<div><p>" + company.Status);
            //        sb.Append("</p></div>");
            //    }
            //    sb.Append("</p></div>");
            //}
            List<string> recipients = new List<string>(){
            @"Jiangping Lu <jiangping.lu@pwc.com>"
            };



            SendGridMail mail = new SendGridMail();
            mail.EmailFrom = "jiangping.lu@pwc.com";
            mail.EmailTo = recipients;
            mail.EmailSubject = "Notice: List of Parties status has changed";
            mail.EmailText = "";
            mail.EmailHTML = "<p>hello World!</p>";
            mail.EmailEnableFooter = "";
            
            SendGridHelper sendGridHelper = new SendGridHelper();
            //sendGridHelper.SendEmail(mail);

            sendGridHelper.SendEmailWebApi("Hello I'm testing SendGrid", "jiangping.lu@pwc.com", "ljp123", "ljp888");
        }
        //kevin.washington@businessos.onmicrosoft.com

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Company> GetCompanies()
        {
            PartyMng check = new PartyMng();
            return check.GetCompanies().ToList();
       }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            int intHour = e.SignalTime.Hour;
            int intMin = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;

            int iHour = 06;
            int iMin = 30;
            int iSecond = 00;

            if (intHour == iHour && intMin == iMin)
            {
                OperateCompanies();
            }
        }
        /// <summary>
        /// sample:
        /// partyId = 5fed522f-4f34-4db6-8385-5802ea6e219d
        /// sourcePartyId = b1fd3502-3b31-465a-a0b8-cd844bd7004a
        /// string sbParam = "<getPartyDetailRequest><sourcePartyId>sourcePartyId</sourcePartyId></getPartyDetailRequest>";
        /// string url = "http://10.22.53.151:9080/USMDMV2/REST/party?" + sbParam;
        /// </summary>
        /// <param name="sourcePartyId"></param>
        /// <returns></returns>
        public PartyDetailSearchResult GetPartyDetails(string sourcePartyId)
        {
            //input param check
            if (string.IsNullOrEmpty(sourcePartyId))
            {
                return null;
            }

            //params defines
            string responeContent = string.Empty;
            string serviceURL = string.Empty;
            serviceURL = "https://apimstg.pwc.com:443/USMDM/REST/party";
            StringBuilder sbParam = new StringBuilder();
            Instrument inStrument = new Instrument();
            PartyDetailSearchResult partyResult = new PartyDetailSearchResult();


            //Get URL 
            sourcePartyId = inStrument.HandleSpecialCharacters(sourcePartyId);
            sbParam = GetParams(sourcePartyId);
            serviceURL = serviceURL + "?" + sbParam.ToString();

            //Init http request
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceURL);
            httpWebRequest.ContentType = "application/xml";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("usmdm-action", "GET");
            httpWebRequest.Headers.Add("APIkey", "l7xx121e691400364943a67508651dc38c92");
            httpWebRequest.Headers.Add("APIKeySecret", "1d54c5795e224aaaad97fb43569984a8");

            //send http request
            byte[] bStream = System.Text.Encoding.Default.GetBytes(sbParam.ToString());
            httpWebRequest.ContentLength = bStream.Length;
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(bStream, 0, bStream.Length);
            }
            try
            {
                //Get http response
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), UTF8Encoding.UTF8);
                responeContent = streamReader.ReadToEnd();
                httpWebResponse.Close();
                streamReader.Close();

                if (responeContent.Trim().Length <= 0)
                {
                    return null;
                }

                //rebuild the returned xml data 
                XmlDocument xmlDoc = inStrument.GetXmlDocByXmlContent(responeContent);
                string[] paramArray = { };
                string statusValue = inStrument.GetNodeValue(xmlDoc, "overallStatus", paramArray);
                string statusDesc = inStrument.GetNodeValue(xmlDoc, "statusMessageDescription", paramArray);

                XDocument xdoc = XDocument.Parse(responeContent);
                if (statusValue.Equals("Success"))
                {
                    partyResult = GetPartyFromXML(xdoc);
                    //res = new JavaScriptSerializer().Serialize(GetPartyFromXML(xdoc));
                }
                else
                {
                    partyResult.IndependenceStatus = statusDesc;
                    partyResult.IndependenceStatusCode = "-1";
                }
            }
            catch (Exception ex)
            {
                partyResult.IndependenceStatusCode = "-1";
                partyResult.IndependenceStatus = ex.Message;
            }
            return partyResult;
        }

        /// <summary>
        /// prepare for params of sourcePartyID and partyId
        /// </summary>
        /// <param name="sourcePartyId"></param>
        /// <returns></returns>
        private StringBuilder GetParams(string sourcePartyId)
        {

            StringBuilder sbParam = new StringBuilder();
            sbParam.Append("<getPartyDetailRequest>");
            if (sourcePartyId != "null" && sourcePartyId.Trim().Length > 0)
            {
                sbParam.Append("<sourcePartyId>");
                sbParam.Append(sourcePartyId);
                sbParam.Append("</sourcePartyId>");
            }
            sbParam.Append("</getPartyDetailRequest>");
            return sbParam;
        }

        /// <summary>
        /// Get party details of code and desc in xml
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private PartyDetailSearchResult GetPartyFromXML(XDocument xmlDoc)
        {
            Instrument inStrument = new Instrument();
            List<SourceParty> sourceParties = (from sourceParty in xmlDoc.Descendants("responseGetParty").Elements("party")
                                               select new SourceParty
                                               {
                                                   independenceRestrictionStatus = new CodeDesc
                                                   {
                                                       Code = inStrument.GetElementValue(sourceParty.Element("independenceRestrictionStatus").Element("code")), //需要返回的Independence status code
                                                       Desc = inStrument.GetElementValue(sourceParty.Element("independenceRestrictionStatus").Element("desc")), //需要返回的Independence status
                                                   }
                                               }).ToList();
            PartyDetailSearchResult partyDetailSearchResult = new PartyDetailSearchResult
            {
                IndependenceStatus = sourceParties[0].independenceRestrictionStatus.Desc,
                IndependenceStatusCode = sourceParties[0].independenceRestrictionStatus.Code
            };
            return partyDetailSearchResult;
        }
    }
}
