namespace skud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkShifts", "Card_Uid", "dbo.Cards");
            DropPrimaryKey("dbo.Cards");
            AlterColumn("dbo.Cards", "Uid", c => c.Long(nullable: false));
            AlterColumn("dbo.WorkShifts", "ArrivalTime", c => c.DateTime());
            AlterColumn("dbo.WorkShifts", "LeavingTime", c => c.DateTime());
            AddPrimaryKey("dbo.Cards", "Uid");
            AddForeignKey("dbo.WorkShifts", "Card_Uid", "dbo.Cards", "Uid");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkShifts", "Card_Uid", "dbo.Cards");
            DropPrimaryKey("dbo.Cards");
            AlterColumn("dbo.WorkShifts", "LeavingTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WorkShifts", "ArrivalTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cards", "Uid", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cards", "Uid");
            AddForeignKey("dbo.WorkShifts", "Card_Uid", "dbo.Cards", "Uid");
        }
    }
}
