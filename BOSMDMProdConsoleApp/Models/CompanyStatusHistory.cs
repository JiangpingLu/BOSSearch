namespace BOSMDMProdConsoleApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyStatusHistory")]
    public partial class CompanyStatusHistory
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        [StringLength(50)]
        public string SourcePartyId { get; set; }

        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string StatusBefore { get; set; }

        [StringLength(50)]
        public string StatusAfter { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
