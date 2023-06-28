namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFiledPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Post", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Post", "IsActive");
        }
    }
}
