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

namespace PWC.US.USTO.BOSSearch.Controllers
{
    public class ValidateAddressForTestController : ApiController
    {
        public object ValidateAddress(string authId, string authToken, string address, string city, string state)
        {
            string serviceURL = Constants.APIURL_SmartyStreets;
            ValidatedAddressResult reValidatedAddress = new ValidatedAddressResult();
            StringBuilder sbParam = new StringBuilder();
            Instrument instrument = new Instrument();
            sbParam.Append("?street=" + address);
            sbParam.Append("&city=" + city);
            sbParam.Append("&state=" + state);
            sbParam.Append("&auth-id=" + authId);
            sbParam.Append("&auth-token=" + authToken);
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
                reValidatedAddress.ValidationStatus = "Invalid";
                return reValidatedAddress;
            }

            reValidatedAddress.ValidationStatus = "Valid";

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
            //dynamic o = JsonConvert.DeserializeObject(json);
            //reValidatedAddress.StreetAddress = o.delivery_line_1;
            //reValidatedAddress.City = o[0].components.primary_numbber;
            return reValidatedAddress;
        }
    }
}
