namespace StreamQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionOwner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "OwnerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "OwnerId");
        }
    }
}
