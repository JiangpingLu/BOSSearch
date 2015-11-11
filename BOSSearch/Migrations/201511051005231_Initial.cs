namespace BOSSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        SourcePartyId = c.String(),
                        CompanyName = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Status = c.String(),
                        Self = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            //CreateTable(
            //    "dbo.CompanyStatus",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            sourcePartyId = c.String(),
            //            RestrictionStatus = c.String(),
            //            RestrictionStatusCode = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.CompanyStatus");
            DropTable("dbo.Companies");
        }
    }
}
