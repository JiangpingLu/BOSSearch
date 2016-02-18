//-------------------------------------------------------------------------------
// <Copyright file="ValidateAddressController.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the business controller code for the function of GetPartyDetails 
// ---------------------------------------------------------------------------------
//	Date Created		: ‎Nov ‎23, ‎2015
//	Author			    : <Jiangping Lu>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          
//	Date Modified		: 
//	Changed By	        : 
//	Change Description  : 
//  Issue number        : 
/////////////////////////////////////////////////////////////////////////////////////////

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PWC.US.USTO.BOSSearch.Function;
using PWC.US.USTO.BOSSearch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PWC.US.USTO.BOSSearch.Controllers
{
    public class ValidateAddressController : ApiController
    {
        public object ValidateAddress(string address, string city, string state)
        {
            ValidatedAddressAllResults validatedAddressAllResults = new ValidatedAddressAllResults();
            ValidatedAddressResult reValidatedAddress = new ValidatedAddressResult();
            try
            {
                string serviceURL = Constants.APIURL_SmartyStreets;
                StringBuilder sbParam = new StringBuilder();
                Instrument instrument = new Instrument();
                sbParam.Append("?street=" + address);
                sbParam.Append("&city=" + city);
                sbParam.Append("&state=" + state);
                sbParam.Append("&auth-id=" + Constants.AuthId);
                sbParam.Append("&auth-token=" + Constants.AuthToken);
                serviceURL = serviceURL + sbParam.ToString();

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceURL);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/xml";

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), UTF8Encoding.UTF8);
                HttpStatusCode statusCode = httpWebResponse.StatusCode;
                string responeContent = streamReader.ReadToEnd();
                httpWebResponse.Close();
                streamReader.Close();

                //If return a 200 code and the result is [] then it should return 'Invalid' 
               if (statusCode == HttpStatusCode.OK && responeContent.Trim() == "[]")
                {
                    validatedAddressAllResults.IsSuccess = true;
                    validatedAddressAllResults.ErrorMessage = "";
                    reValidatedAddress.ValidationStatus = "Invalid";
                    reValidatedAddress.StreetAddress = "";
                    reValidatedAddress.City = "";
                    reValidatedAddress.State = "";
                    reValidatedAddress.ZipCode = "";
                    reValidatedAddress.Plus4Zipcode = "";
                    validatedAddressAllResults.Results = reValidatedAddress;
                    return validatedAddressAllResults;
                }

                //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                //reValidatedAddress = jsSerializer.Deserialize<ValidatedAddressResult>(responeContent);
                //json format
                JsonSerializer ser = new JsonSerializer();
                TextReader tr = new StringReader(responeContent);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = ser.Deserialize(jtr);

                StringWriter textwriter = new StringWriter();
                JsonTextWriter jswriter = new JsonTextWriter(textwriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                ser.Serialize(jswriter, obj);

                string json = textwriter.ToString();
                json = json.Replace("[", string.Empty);
                json = json.Replace("]", string.Empty);
                var obj1 = JObject.Parse(json);
                reValidatedAddress.StreetAddress = obj1["delivery_line_1"].ToString();
                reValidatedAddress.City = obj1["components"]["city_name"].ToString();
                reValidatedAddress.State = obj1["components"]["state_abbreviation"].ToString();
                reValidatedAddress.ZipCode = obj1["components"]["zipcode"].ToString();
                reValidatedAddress.Plus4Zipcode = obj1["components"]["plus4_code"].ToString();
                reValidatedAddress.ValidationStatus = "Valid";

                validatedAddressAllResults.IsSuccess = true;
                validatedAddressAllResults.ErrorMessage = "";
                validatedAddressAllResults.Results = reValidatedAddress;
            }
            catch (Exception ex)
            {
                validatedAddressAllResults.IsSuccess = false;
                validatedAddressAllResults.ErrorMessage = ex.Message;
                validatedAddressAllResults.Results = null;
            }
            return validatedAddressAllResults;
        }
    }
}
