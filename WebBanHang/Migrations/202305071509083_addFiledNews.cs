namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFiledNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_News", "Alias", c => c.String());
            AddColumn("dbo.tb_News", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tb_News", "Title", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_News", "Title", c => c.String());
            DropColumn("dbo.tb_News", "IsActive");
            DropColumn("dbo.tb_News", "Alias");
        }
    }
}
