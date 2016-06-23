namespace ReleaseNote.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Jira", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Octopus", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Octopus", c => c.String());
            AlterColumn("dbo.Projects", "Jira", c => c.String());
            AlterColumn("dbo.Projects", "Name", c => c.String());
        }
    }
}
