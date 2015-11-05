namespace StreamQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Votes1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "VoteQuota");
            DropColumn("dbo.AspNetUsers", "QuestionQuota");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "QuestionQuota", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "VoteQuota", c => c.Int(nullable: false));
        }
    }
}
