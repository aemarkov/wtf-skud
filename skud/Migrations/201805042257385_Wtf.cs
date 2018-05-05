namespace skud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Wtf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkShifts", "CardId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkShifts", "CardId");
        }
    }
}
