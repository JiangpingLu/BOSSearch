namespace BOSSearch.Migrations
{
    using BOSSearch.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BOSSearch.Models.BOSSearchContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BOSSearch.Models.BOSSearchContext context)
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
