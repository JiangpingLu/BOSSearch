using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BOSSearch.Models
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