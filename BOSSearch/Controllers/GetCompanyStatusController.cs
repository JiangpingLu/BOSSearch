using BOSSearch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using BOSSearch.Function;
using System.Xml.Linq;
using System.Web.Script.Serialization;


namespace BOSSearch.Controllers
{
    public class GetCompanyStatusController : ApiController
    {
        //private const string DATA = "<accountSearchRequest><partyName>Morgan</partyName><city>Burbank</city><state>CA</state><country>usa</country><searchMode>02</searchMode></accountSearchRequest>";
        private const string DATA = "<accountSearchRequest><partyName>{0}</partyName><city>{1}</city><state>{2}</state><country>usa</country><searchMode>02</searchMode></accountSearchRequest>";
        
        //api/GetCompanyStatus?partyname=Morgan&city=Burbank&state=CA
        public List<PartySearchResult> GetParties(string partyname, string city, string state)
        {
            //build request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Constants.APIURL_PublicTest);
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.Headers.Add("usmdm-action", "SB4C");
            request.Headers.Add("APIkey", Constants.APIkey);
            request.Headers.Add("APIKeySecret", Constants.APIKeySecret);

            //pass data
            Instrument instrument = new Instrument();
            byte[] formData = UTF8Encoding.UTF8.GetBytes(string.Format(DATA, instrument.HandleSpecialCharacters(partyname), instrument.HandleSpecialCharacters(city), instrument.HandleSpecialCharacters(state)));
            request.ContentLength = formData.Length;
            using (Stream post = request.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            //get response
            string response = string.Empty;
            try
            {
                WebResponse webResponse = request.GetResponse();
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
            catch (Exception e)
            {
                //To do log the exception
            }


            XDocument xdoc = XDocument.Parse(response);
            //var XMLLoadfullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XMLFile_Hardcode_Source.xml");
            //XDocument xdoc = XDocument.Load(XMLLoadfullPath);
            
            return ReadXML(xdoc);
            //List<PartySearchResult> Parties = ReadXML(xdoc);
            //string output = new JavaScriptSerializer().Serialize(Parties);

            //return output;
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

        private List<PartySearchResult> ReadXML(XDocument xdoc)
        {
            Instrument instrument = new Instrument();
            List<PartySearchResult> Parties = (from party in xdoc.Descendants("partySearchResults").Elements("partySearchResult")
                                               select new PartySearchResult
                                               {
                                                   //PartyId = party.Element("partyId").Value,
                                                   //PRID = party.Element("PRID").Value,
                                                   //TPRID = party.Element("TPRID").Value,
                                                   SourcePartyId = party.Element("sourcePartyId").Value,
                                                   //SourcePRID = party.Element("sourcePRID").Value,
                                                   PartyName = party.Element("partyName").Value,
                                                   //PartyFirstName = party.Element("partyFirstName").Value,
                                                   //PartyMiddleName = party.Element("partyMiddleName").Value,
                                                   //PartyFamilyName = party.Element("partyFamilyName").Value,
                                                   //PartyType = new CodeDesc
                                                   //{
                                                   //    Code = GetElementValue(party.Element("partyType").Element("code")),
                                                   //    Desc = GetElementValue(party.Element("partyType").Element("desc"))
                                                   //},
                                                   //Identifiers = (from identifier in party.Element("identifiers").Elements("identifier")
                                                   //               select new Identifier
                                                   //               {
                                                   //                   IdentifierType = new CodeDesc
                                                   //                   {
                                                   //                       Code = GetElementValue(identifier.Element("identifierType").Element("code")),
                                                   //                       Desc = GetElementValue(identifier.Element("identifierType").Element("desc"))
                                                   //                   },
                                                   //                   IdentifierValue = identifier.Element("identifierValue").Value
                                                   //               }).ToList(),
                                                   PrimaryAddresses = (from address in party.Element("primaryAddresses").Elements("address")
                                                                       select new Address
                                                                       {
                                                                           //AddressUsageType = new CodeDesc
                                                                           //{
                                                                           //    Code = address.Element("addressUsageType") != null ? GetElementValue(address.Element("addressUsageType").Element("code")) : "",
                                                                           //    Desc = address.Element("addressUsageType") != null ? GetElementValue(address.Element("addressUsageType").Element("desc")) : ""
                                                                           //},
                                                                           //AddressLine1 = GetElementValue(address.Element("addressLine1")),
                                                                           //AddressLine2 = GetElementValue(address.Element("addressLine2")),
                                                                           AddressLine = (instrument.GetElementValue(address.Element("addressLine1")) + " " + instrument.GetElementValue(address.Element("addressLine2"))).Trim(),
                                                                           City = instrument.GetElementValue(address.Element("city")),
                                                                           //State = new CodeDesc
                                                                           //{
                                                                           //    Code = GetElementValue(address.Element("state").Element("code")),
                                                                           //    Desc = GetElementValue(address.Element("state").Element("desc"))
                                                                           //},
                                                                           State = instrument.GetElementValue(address.Element("state").Element("code")),
                                                                           //Country = new CodeDesc
                                                                           //{
                                                                           //    Code = GetElementValue(address.Element("country").Element("code")),
                                                                           //    Desc = GetElementValue(address.Element("country").Element("desc"))
                                                                           //},
                                                                           //PostalCode = GetElementValue(address.Element("postalCode"))
                                                                       }).ToList(),
                                                   //EncryptedDUNS = party.Element("encryptedDUNS").Value
                                               }).ToList();

            return Parties;
        }

       
    }
}
