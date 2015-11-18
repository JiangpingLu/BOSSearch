//-------------------------------------------------------------------------------
// <Copyright file="Configuration.cs" company="PwC">
// © 2014 PwC. All rights reserved.
// </Copyright>
// "PwC" refers to PricewaterhouseCoopers LLP, a Delaware limited liability 
// partnership, which is a member firm of PricewaterhouseCoopers International 
// Limited, each member firm of which is a separate legal entity.
// ---------------------------------------------------------------------------------
//	File Description	: It's the business controller code for the function of GetCompanyStatus 
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
//          layout format
//	Date Modified		: Nov 18, 2015
//	Changed By		    : AJ
//	Change Description  : Add header description
//  Issue number        : 1.1
/////////////////////////////////////////////////////////////////////////////////////////

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
