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
            string responcontent = string.Empty;
            string url = string.Empty;
            url = Constants.APIURL_Prodect;
            //url = Constants.APIURL_PublicTest;
            StringBuilder sbParam = new StringBuilder();
            Instrument instrument = new Instrument();
            PartyDetailSearchResult res = new PartyDetailSearchResult();

            if (string.IsNullOrEmpty(sourcePartyId))
            {
                return null;
            }
            sourcePartyId = instrument.HandleSpecialCharacters(sourcePartyId);

            sbParam = GetParams(sourcePartyId);
            url =url + "?" + sbParam.ToString();

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/xml";
            httpWebRequest.Method = "POST";
            //httpWebRequest.Host = "apimstg.pwc.com:443";
            httpWebRequest.Headers.Add("usmdm-action", "GET");
            httpWebRequest.Headers.Add("APIkey", Constants.APIkey_Product);
            httpWebRequest.Headers.Add("APIKeySecret", Constants.APIKeySecret_Product);
            //httpWebRequest.Headers.Add("APIkey", Constants.APIkey);
            //httpWebRequest.Headers.Add("APIKeySecret", Constants.APIKeySecret);
            
            byte[] bs = System.Text.Encoding.Default.GetBytes(sbParam.ToString());
            httpWebRequest.ContentLength = bs.Length;
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(bs, 0, bs.Length);
            }
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamreader = new StreamReader(httpWebResponse.GetResponseStream(), UTF8Encoding.UTF8);
                responcontent = streamreader.ReadToEnd();
                httpWebResponse.Close();
                streamreader.Close();

                if (responcontent.Trim().Length <= 0)
                {
                    return null;
                }
                XmlDocument doc = instrument.GetXmlDocByXmlContent(responcontent);
                string[] paramArray = { };
                string value = instrument.GetNodeValue(doc, "overallStatus", paramArray);
                string desc = instrument.GetNodeValue(doc, "statusMessageDescription", paramArray);

                XDocument xdoc = XDocument.Parse(responcontent);
                if (value.Equals("Success"))
                {
                    res = GetPartyFromXML(xdoc);
                    //res = new JavaScriptSerializer().Serialize(GetPartyFromXML(xdoc));
                }
                else
                {
                    res.IndependenceStatus = desc;
                    res.IndependenceStatusCode = "-1";
                }
            }
            catch (Exception ex)
            {
                res.IndependenceStatusCode = "-1";
                res.IndependenceStatus = ex.Message;
                //T0 DO
            }            
            return res;
        }

        /// <summary>
        /// sourcePartyID和partyId参数整理
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

        private PartyDetailSearchResult GetPartyFromXML(XDocument doc)
        {
            Instrument instrument = new Instrument();
            List<SourceParty> parties = (from sourceParty in doc.Descendants("responseGetParty").Elements("party")
                                         select new SourceParty
                                               {
                                                   independenceRestrictionStatus = new CodeDesc
                                                   {
                                                       Code = instrument.GetElementValue(sourceParty.Element("independenceRestrictionStatus").Element("code")), //需要返回的Independence status code
                                                       Desc = instrument.GetElementValue(sourceParty.Element("independenceRestrictionStatus").Element("desc")), //需要返回的Independence status
                                                   }
                                               }).ToList();
            PartyDetailSearchResult partyDetailSearchResult = new PartyDetailSearchResult
            {
                IndependenceStatus = parties[0].independenceRestrictionStatus.Desc,
                IndependenceStatusCode = parties[0].independenceRestrictionStatus.Code
            };
            return partyDetailSearchResult;
        }
    }
}
