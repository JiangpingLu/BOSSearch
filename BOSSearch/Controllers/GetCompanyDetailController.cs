﻿//-------------------------------------------------------------------------------
// <Copyright file="GetCompanyDetailController.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the business controller code for the function of GetPartyDetails 
// ---------------------------------------------------------------------------------
//	Date Created		: ‎Nov ‎11, ‎2015
//	Author			    : <Jiangping Lu>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          Add porduct line URL and APIKey
//	Date Modified		: Nov 16, 2015
//	Changed By	        : Jiangping Lu(AJ)
//	Change Description  : Add Product line URL and change APIKey and APIKeySecret for product line test
//  Issue number        : 1.0
//          layout format
//	Date Modified		: Nov 18, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 1.1
/////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using BOSSearch.Function;
using System.Xml.Linq;
using System.Xml;
using BOSSearch.Models;

namespace BOSSearch.Controllers
{
    public class GetCompanyDetailController : ApiController
    {
        /// <summary>
        /// sample:
        /// partyId = 5fed522f-4f34-4db6-8385-5802ea6e219d
        /// sourcePartyId = b1fd3502-3b31-465a-a0b8-cd844bd7004a
        /// string sbParam = "<getPartyDetailRequest><sourcePartyId>sourcePartyId</sourcePartyId></getPartyDetailRequest>";
        /// string url = "http://10.22.53.151:9080/USMDMV2/REST/party?" + sbParam;
        /// </summary>
        /// <param name="sourcePartyId"></param>
        /// <param name="partyId"></param>
        /// <returns></returns>
        public object GetPartyDetails(string sourcePartyId)
        {
            string responeContent = string.Empty;
            string serviceURL = string.Empty;
            serviceURL = Constants.APIURL_Prodect;
            StringBuilder sbParam = new StringBuilder();
            Instrument inStrument = new Instrument();
            PartyDetailSearchResult partyResult = new PartyDetailSearchResult();

            if (string.IsNullOrEmpty(sourcePartyId))
            {
                return null;
            }
            sourcePartyId = inStrument.HandleSpecialCharacters(sourcePartyId);

            sbParam = GetParams(sourcePartyId);
            serviceURL = serviceURL + "?" + sbParam.ToString();

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceURL);
            httpWebRequest.ContentType = "application/xml";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("usmdm-action", "GET");
            httpWebRequest.Headers.Add("APIkey", Constants.APIkey_Product);
            httpWebRequest.Headers.Add("APIKeySecret", Constants.APIKeySecret_Product);
            
            byte[] bStream = System.Text.Encoding.Default.GetBytes(sbParam.ToString());
            httpWebRequest.ContentLength = bStream.Length;
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(bStream, 0, bStream.Length);
            }
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), UTF8Encoding.UTF8);
                responeContent = streamReader.ReadToEnd();
                httpWebResponse.Close();
                streamReader.Close();

                if (responeContent.Trim().Length <= 0)
                {
                    return null;
                }
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
        /// <param name="partyId"></param>
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
        /// <param name="doc"></param>
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
