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

namespace PWC.US.USTO.BOSSearch.Migrations
{
    using PWC.US.USTO.BOSSearch.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PWC.US.USTO.BOSSearch.Models.BOSSearchContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PWC.US.USTO.BOSSearch.Models.BOSSearchContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Companies.AddOrUpdate(c => c.CompanyId,
               new Company
               {
                   CompanyName = "Morgan Creek International, Inc.",
                   SourcePartyId = "b1fd3502-3b31-465a-a0b8-cd844bd7004a",
                   Address = "4000 Warner Blvd 76",
                   City = "Burbank",
                   State = "CA",
                   Email = "keen.guo@pwc.com",
                   Status = "1"
               },
               new Company
               {
                   CompanyName = "PWC",
                   SourcePartyId = "b1fd3502-3b31-465a-a0b8-cd844bd7004b",
                   Address = "4000 Warner Blvd 76",
                   City = "ShangHai",
                   State = "ShangHai",
                   Email = "keen.guo@pwc.com",
                   Status = "1"
               }
            );
        }
    }
}
