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