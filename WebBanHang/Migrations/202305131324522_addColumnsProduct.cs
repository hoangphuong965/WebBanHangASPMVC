namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnsProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "Alias", c => c.String(maxLength: 250));
            AddColumn("dbo.tb_Product", "ProductCode", c => c.String(maxLength: 50));
            AddColumn("dbo.tb_Product", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tb_Product", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "ViewCount");
            DropColumn("dbo.tb_Product", "OriginalPrice");
            DropColumn("dbo.tb_Product", "ProductCode");
            DropColumn("dbo.tb_Product", "Alias");
        }
    }
}
