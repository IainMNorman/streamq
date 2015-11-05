namespace StreamQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiveVote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Votes", "Active");
        }
    }
}
