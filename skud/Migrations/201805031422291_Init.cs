namespace skud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Uid = c.Long(nullable: false, identity: true),
                        IssueDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Uid)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        Name = c.String(),
                        Middlename = c.String(),
                        Photo = c.String(),
                        PositionId = c.Int(nullable: false),
                        RankId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.Ranks", t => t.RankId, cascadeDelete: true)
                .Index(t => t.PositionId)
                .Index(t => t.RankId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkShifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.Int(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        LeavingTime = c.DateTime(nullable: false),
                        Card_Uid = c.Long(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.Card_Uid)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Card_Uid)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkShifts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.WorkShifts", "Card_Uid", "dbo.Cards");
            DropForeignKey("dbo.Users", "RankId", "dbo.Ranks");
            DropForeignKey("dbo.Users", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Cards", "UserId", "dbo.Users");
            DropIndex("dbo.WorkShifts", new[] { "User_Id" });
            DropIndex("dbo.WorkShifts", new[] { "Card_Uid" });
            DropIndex("dbo.Users", new[] { "DepartmentId" });
            DropIndex("dbo.Users", new[] { "RankId" });
            DropIndex("dbo.Users", new[] { "PositionId" });
            DropIndex("dbo.Cards", new[] { "UserId" });
            DropTable("dbo.WorkShifts");
            DropTable("dbo.Ranks");
            DropTable("dbo.Positions");
            DropTable("dbo.Departments");
            DropTable("dbo.Users");
            DropTable("dbo.Cards");
        }
    }
}
