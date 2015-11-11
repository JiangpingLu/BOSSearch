using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOSSearch.Models
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