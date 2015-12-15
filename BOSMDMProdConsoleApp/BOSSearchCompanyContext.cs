namespace BOSMDMProdConsoleApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BOSMDMProdConsoleApp.Models;

    public partial class BOSSearchCompanyContext : DbContext
    {
        public BOSSearchCompanyContext()
            : base("name=BOSSearchCompanyContext")
        {
        }

        public virtual DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
