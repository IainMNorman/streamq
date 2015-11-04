namespace StreamQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Votes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteValue = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Voter_Id = c.String(maxLength: 128),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Voter_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Voter_Id)
                .Index(t => t.Question_Id);
            
            AddColumn("dbo.Questions", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "Questioner_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Questions", "Questioner_Id");
            AddForeignKey("dbo.Questions", "Questioner_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Questions", "UpVotes");
            DropColumn("dbo.Questions", "DownVotes");
            DropColumn("dbo.Questions", "OwnerId");
            DropColumn("dbo.Questions", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "State", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "OwnerId", c => c.String());
            AddColumn("dbo.Questions", "DownVotes", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "UpVotes", c => c.Int(nullable: false));
            DropForeignKey("dbo.Votes", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Votes", "Voter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "Questioner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Votes", new[] { "Question_Id" });
            DropIndex("dbo.Votes", new[] { "Voter_Id" });
            DropIndex("dbo.Questions", new[] { "Questioner_Id" });
            DropColumn("dbo.Questions", "Questioner_Id");
            DropColumn("dbo.Questions", "Deleted");
            DropTable("dbo.Votes");
        }
    }
}
