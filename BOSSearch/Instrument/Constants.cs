//-------------------------------------------------------------------------------
// <Copyright file="Constants.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the common defined data for all API 
// ---------------------------------------------------------------------------------
//	Date Created		: Nov 11, 2015
//	Author			    : <Haley Qu>, SDC Shanghai
// ---------------------------------------------------------------------------------
// 	Change History
//          Add description
//	Date Modified		: Nov 17, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 1.o
/////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWC.US.USTO.BOSSearch.Function
{
    public class Constants
    {
        #region URL
        /// <summary>
        /// The URL for company intranet
        /// </summary>
        public static readonly string APIURL_IntranetTest = "http://10.22.53.151:9080/USMDMV2/REST/party";

        /// <summary>
        /// The URL of public Internet
        /// </summary>
        public static readonly string APIURL_Public = "https://apimstg.pwc.com:443/USMDM/REST/party";

        /// <summary>
        /// The URL of product
        /// </summary>
        public static readonly string APIURL_Prodect = "https://apim.pwc.com/USMDM/REST/v1/party";

        /// <summary>
        /// validate address in SmartyStreets
        /// </summary>
        public static readonly string APIURL_SmartyStreets = "https://api.smartystreets.com/street-address";
        #endregion

        #region APIKey for public internet test
        public static readonly string APIkey_Public = "l7xx121e691400364943a67508651dc38c92";
        public static readonly string APIKeySecret_Public = "1d54c5795e224aaaad97fb43569984a8";
        #endregion

        #region APIKey for product using
        public static readonly string APIkey_Product = "l7xxebb0dc475876434c92e0f05978c800fb";
        public static readonly string APIKeySecret_Product = "7a985141b76e465b8dcff7eeb4edd30c";
        #endregion

        #region Auth Id and Token for SmartyStreets
        public static readonly string AuthId = "83f42eea-3154-677c-2f3d-2b4c06fabecb";
        public static readonly string AuthToken = "cibOSU4837WS893nqN7h";
        #endregion

    }
}