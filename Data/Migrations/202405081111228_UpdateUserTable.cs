namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Users", "Name");
        }
    }
}
