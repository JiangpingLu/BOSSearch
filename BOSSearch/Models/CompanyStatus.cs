﻿//-------------------------------------------------------------------------------
// <Copyright file="CompanyStatus.cs" company="PwC">
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
using System.Linq;
using System.Web;

namespace BOSSearch.Models
{
    public class CompanyStatus
    {
        public int Id { get; set; }
        public string sourcePartyId { get; set; }
        public string RestrictionStatus { get; set; }
        public string RestrictionStatusCode { get; set; }
    }
}