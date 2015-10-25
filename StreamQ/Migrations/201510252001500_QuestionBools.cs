namespace StreamQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionBools : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Rejected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "Answered", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "Archived");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Archived", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "Answered");
            DropColumn("dbo.Questions", "Rejected");
        }
    }
}
