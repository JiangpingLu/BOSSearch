//-------------------------------------------------------------------------------
// <Copyright file="Company.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the return data model for the function of GetPartyDetails() 
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
/////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PWC.US.USTO.BOSSearch.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string SourcePartyId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public string Status { get; set; }

        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
                    "api/companies/{0}", this.SourcePartyId);
            }
            set { }
        }
    }
}