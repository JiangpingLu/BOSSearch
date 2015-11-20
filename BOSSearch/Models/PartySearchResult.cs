//-------------------------------------------------------------------------------
// <Copyright file="PartySearchResult.cs" company="PwC">
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

namespace PWC.US.USTO.BOSSearch.Models
{
    public class PartySearchResult
    {
        //public string PartyId { get; set; }
        //public string PRID { get; set; }
        //public string TPRID { get; set; }
        public string SourcePartyId { get; set; }
        //public string SourcePRID { get; set; }
        public string PartyName { get; set; }
        //public string PartyFirstName { get; set; }
        //public string PartyMiddleName { get; set; }
        //public string PartyFamilyName { get; set; }
        //public CodeDesc PartyType { get; set; }
        //public IEnumerable<Identifier> Identifiers { get; set; }
        public IEnumerable<Address> PrimaryAddresses { get; set; }
        //public string EncryptedDUNS { get; set; }
    }
}