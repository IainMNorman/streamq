namespace StreamQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Answer", c => c.String());
            AddColumn("dbo.Questions", "State", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "TimeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "TimeStamp");
            DropColumn("dbo.Questions", "State");
            DropColumn("dbo.Questions", "Answer");
        }
    }
}
