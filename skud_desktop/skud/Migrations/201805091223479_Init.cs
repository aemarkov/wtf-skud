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
                        Uid = c.Long(nullable: false),
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
                        Surname = c.String(maxLength: 4000),
                        Name = c.String(maxLength: 4000),
                        Middlename = c.String(maxLength: 4000),
                        Photo = c.String(maxLength: 4000),
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
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkShifts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardUid = c.Long(nullable: false),
                        ArrivalTime = c.DateTime(),
                        LeavingTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.CardUid, cascadeDelete: true)
                .Index(t => t.CardUid);            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkShifts", "CardUid", "dbo.Cards");
            DropForeignKey("dbo.Users", "RankId", "dbo.Ranks");
            DropForeignKey("dbo.Users", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Cards", "UserId", "dbo.Users");
            DropIndex("dbo.WorkShifts", new[] { "CardUid" });
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
