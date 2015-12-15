namespace BOSMDMProdConsoleApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BOSMDMProdConsoleApp.Models;

    public partial class CompanyStatusHistoryContext : DbContext
    {
        public CompanyStatusHistoryContext()
            : base("name=CompanyStatusHistoryModel")
        {
        }

        public virtual DbSet<CompanyStatusHistory> CompanyStatusHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyStatusHistory>()
                .Property(e => e.SourcePartyId)
                .IsFixedLength();

            modelBuilder.Entity<CompanyStatusHistory>()
                .Property(e => e.StatusBefore)
                .IsFixedLength();

            modelBuilder.Entity<CompanyStatusHistory>()
                .Property(e => e.StatusAfter)
                .IsFixedLength();
        }
    }
}
