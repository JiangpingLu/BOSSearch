//-------------------------------------------------------------------------------
// <Copyright file="ValidatedAddressResult.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the return data model for the function of ValidateAddress() 
// ---------------------------------------------------------------------------------
//	Date Created		: Nov 05, 2015
//	Author			    : <Jiangping Lu>, SDC Shanghai
// ---------------------------------------------------------------------------------
/////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWC.US.USTO.BOSSearch.Models
{
    public class ValidatedAddressResult
    {
        public string ValidationStatus { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Plus4Zipcode { get; set; }
    }

    public class ValidatedAddressAllResults
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public ValidatedAddressResult Results { get; set; }
    }
}