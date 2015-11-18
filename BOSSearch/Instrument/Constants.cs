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

namespace BOSSearch.Function
{
    public class Constants
    {
        #region URL
        /// <summary>
        /// 内网测试URL
        /// </summary>
        public static readonly string APIURL_IntranetTest = "http://10.22.53.151:9080/USMDMV2/REST/party";

        /// <summary>
        /// 公网测试URL
        /// </summary>
        public static readonly string APIURL_PublicTest = "https://apimstg.pwc.com:443/USMDM/REST/party";

        /// <summary>
        /// 产品端用户URL
        /// </summary>
        public static readonly string APIURL_Prodect = "https://apim.pwc.com/USMDM/REST/v1/party";
        #endregion

        #region 公网测试APIKey
        public static readonly string APIkey = "l7xx121e691400364943a67508651dc38c92";
        public static readonly string APIKeySecret = "1d54c5795e224aaaad97fb43569984a8";
        #endregion

        #region 产品端APIKey
        public static readonly string APIkey_Product = "l7xxebb0dc475876434c92e0f05978c800fb";
        public static readonly string APIKeySecret_Product = "7a985141b76e465b8dcff7eeb4edd30c";
        #endregion

    }
}