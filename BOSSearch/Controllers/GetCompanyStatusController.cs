//-------------------------------------------------------------------------------
// <Copyright file="GetCompanyStatusController.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the business controller code for the function of GetCompanyStatus 
// ---------------------------------------------------------------------------------
//	Date Created		: Nov 05, 2015
//	Author			    : <Keen Guo>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          Add description
//	Date Modified		: Nov 17, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 1.0
//          layout format
//	Date Modified		: Nov 18, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 1.1
//          add test data
//	Date Modified		: Nov 25, 2015
//	Changed By		    : AJ
//	Change Description  : Add test data of Restricted and Unrestricted
//  Issue number        : 1.2
/////////////////////////////////////////////////////////////////////////////////////////

using PWC.US.USTO.BOSSearch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using PWC.US.USTO.BOSSearch.Function;
using System.Xml.Linq;
using System.Web.Script.Serialization;


namespace PWC.US.USTO.BOSSearch.Controllers
{
    public class GetCompanyStatusController : ApiController
    {
        //private const string DATA = "<accountSearchRequest><partyName>Morgan</partyName><city>Burbank</city><state>CA</state><country>usa</country><searchMode>02</searchMode></accountSearchRequest>";
        private const string DATA = "<accountSearchRequest><partyName>{0}</partyName><city>{1}</city><state>{2}</state><country>usa</country><searchMode>02</searchMode></accountSearchRequest>";
        
        /// <summary>
        /// api/GetCompanyStatus?partyname=Morgan&city=Burbank&state=CA
        /// </summary>
        /// <param name="partyName">Morgan</param>
        /// <param name="city">Burbank</param>
        /// <param name="state">CA</param>
        /// <returns></returns>
        public List<PartySearchResult> GetParties(string partyName, string city, string state)
        {
            //build request
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Constants.APIURL_Public);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/xml";
            httpWebRequest.Headers.Add("usmdm-action", "SB4C");
            httpWebRequest.Headers.Add("APIkey", Constants.APIkey_Public);
            httpWebRequest.Headers.Add("APIKeySecret", Constants.APIKeySecret_Public);

            //pass data
            Instrument inStrument = new Instrument();
            string sbParam = string.Format(DATA, inStrument.HandleSpecialCharacters(partyName), inStrument.HandleSpecialCharacters(city), inStrument.HandleSpecialCharacters(state));
            byte[] formData = UTF8Encoding.UTF8.GetBytes(sbParam);
            httpWebRequest.ContentLength = formData.Length;
            using (Stream postStream = httpWebRequest.GetRequestStream())
            {
                postStream.Write(formData, 0, formData.Length);
            }

            //get response
            string response = string.Empty;
            try
            {
                WebResponse webResponse = httpWebRequest.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception)
            {
                //To do log the exception
            }
            XDocument xDoc = XDocument.Parse(response);
            //return ReadXML(xDoc);

            //Add Test data
            List<PartySearchResult> partyRes = ReadXML(xDoc);
            if (partyName == "Morgan" && city == "Burbank" && state == "CA")
            {
                PartySearchResult res = new PartySearchResult();
                res.SourcePartyId = "pwc1234567890";
                res.PartyName = "pwc";
                List<Address> addressses = new List<Address>();
                Address add = new Address();
                add.AddressLine = "UnRestricted Address";
                add.City = "UnRestricted";
                add.State = "CA";
                add.ZipCode = "10000";
                addressses.Add(add);
                res.PrimaryAddresses = addressses;
                partyRes.Add(res);

                PartySearchResult res1 = new PartySearchResult();
                List<Address> addressses1 = new List<Address>();
                Address add1 = new Address();
                res1.SourcePartyId = "pwc1234567891";
                res1.PartyName = "pwc1";
                add1.AddressLine = "Restricted Address";
                add1.City = "Restricted";
                add1.State = "CA";
                add1.ZipCode = "10001";
                addressses1.Add(add1);
                res1.PrimaryAddresses = addressses1;
                partyRes.Add(res1);
            }
            return partyRes;


            //var XMLLoadfullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XMLFile_Hardcode_Source.xml");
            //XDocument xdoc = XDocument.Load(XMLLoadfullPath);
            //List<PartySearchResult> Parties = ReadXML(xdoc);
            //string output = new JavaScriptSerializer().Serialize(Parties);
            //return output;
        }

        /// <summary>
        /// Get Params of sourcePartyId,partyName,Address from returned xml data
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        private List<PartySearchResult> ReadXML(XDocument xDoc)
        {
            Instrument instrument = new Instrument();
            List<PartySearchResult> partyResults = (from party in xDoc.Descendants("partySearchResults").Elements("partySearchResult")
                                               select new PartySearchResult
                                               {
                                                   SourcePartyId = party.Element("sourcePartyId").Value,
                                                   PartyName = party.Element("partyName").Value,
                                                   PrimaryAddresses = (from address in party.Element("primaryAddresses").Elements("address")
                                                                       select new Address
                                                                       {
                                                                           AddressLine = (instrument.GetElementValue(address.Element("addressLine1")) + " " + instrument.GetElementValue(address.Element("addressLine2"))).Trim(),
                                                                           City = instrument.GetElementValue(address.Element("city")),
                                                                           
                                                                           State = instrument.GetElementValue(address.Element("state").Element("code")),
                                                                           
                                                                           ZipCode = instrument.GetElementValue(address.Element("postalCode"))
                                                                       }).ToList(),
                                               }).ToList();
            return partyResults;
        }


    }
}



//private List<PartySearchResult> ReadXML(XDocument xdoc)
//{
//    List<PartySearchResult> Parties = (from party in xdoc.Descendants("partySearchResults").Elements("partySearchResult")
//                   select new PartySearchResult
//                   {
//                       PartyId = party.Element("partyId").Value,
//                       PRID = party.Element("PRID").Value,
//                       TPRID = party.Element("TPRID").Value,
//                       SourcePartyId = party.Element("sourcePartyId").Value,
//                       SourcePRID = party.Element("sourcePRID").Value,
//                       PartyName = party.Element("partyName").Value,
//                       PartyFirstName = party.Element("partyFirstName").Value,
//                       PartyMiddleName = party.Element("partyMiddleName").Value,
//                       PartyFamilyName = party.Element("partyFamilyName").Value,
//                                           PartyType = new CodeDesc
//                                           {
//                                               Code = GetElementValue(party.Element("partyType").Element("code")),
//                                               Desc = GetElementValue(party.Element("partyType").Element("desc"))
//                                           },
//                                           Identifiers = (from identifier in party.Element("identifiers").Elements("identifier")
//                                                          select new Identifier
//                                                          {
//                                                              IdentifierType = new CodeDesc
//                                                              {
//                                                                  Code = GetElementValue(identifier.Element("identifierType").Element("code")),
//                                                                  Desc = GetElementValue(identifier.Element("identifierType").Element("desc"))
//                                                              },
//                                                              IdentifierValue = identifier.Element("identifierValue").Value
//                                                          }).ToList(),
//                                           PrimaryAddresses = (from address in party.Element("primaryAddresses").Elements("address")
//                                                               select new Address
//                                                               {
//                                                                   AddressUsageType = new CodeDesc
//                                                                   {
//                                                                       Code = address.Element("addressUsageType") != null ? GetElementValue(address.Element("addressUsageType").Element("code")) : "",
//                                                                       Desc = address.Element("addressUsageType") != null ? GetElementValue(address.Element("addressUsageType").Element("desc")) : ""
//                                                                   },
//                                                                   AddressLine1 = GetElementValue(address.Element("addressLine1")),
//                                                                   AddressLine2 = GetElementValue(address.Element("addressLine2")),
//                                                                   City = GetElementValue(address.Element("city")),
//                                                                   State = new CodeDesc
//                                                                   {
//                                                                       Code = GetElementValue(address.Element("state").Element("code")),
//                                                                       Desc = GetElementValue(address.Element("state").Element("desc"))
//                                                                   },
//                                                                   Country = new CodeDesc
//                                                                   {
//                                                                       Code = GetElementValue(address.Element("country").Element("code")),
//                                                                       Desc = GetElementValue(address.Element("country").Element("desc"))
//                                                                   },
//                                                                   PostalCode = GetElementValue(address.Element("postalCode"))
//                                                               }).ToList(),
//                       EncryptedDUNS = party.Element("encryptedDUNS").Value
//                   }).ToList();

//    return Parties;
//}