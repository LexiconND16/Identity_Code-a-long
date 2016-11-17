namespace IdentityDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class socksandname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UsesBlueSocks", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetUsers", "UsesBlueSocks");
        }
    }
}
