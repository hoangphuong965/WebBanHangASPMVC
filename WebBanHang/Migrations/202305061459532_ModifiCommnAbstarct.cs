namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiCommnAbstarct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Adv", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Adv", "CreatedBy", c => c.String());
            AlterColumn("dbo.tb_Category", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Category", "CreatedBy", c => c.String());
            AlterColumn("dbo.tb_News", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_News", "CreatedBy", c => c.String());
            AlterColumn("dbo.tb_Post", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Post", "CreatedBy", c => c.String());
            AlterColumn("dbo.tb_Contact", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Contact", "CreatedBy", c => c.String());
            AlterColumn("dbo.tb_Order", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Order", "CreatedBy", c => c.String());
            AlterColumn("dbo.tb_Product", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Product", "CreatedBy", c => c.String());
            AlterColumn("dbo.tb_ProductCategory", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_ProductCategory", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_ProductCategory", "CreatedBy", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_ProductCategory", "CreatedDate", c => c.String());
            AlterColumn("dbo.tb_Product", "CreatedBy", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Product", "CreatedDate", c => c.String());
            AlterColumn("dbo.tb_Order", "CreatedBy", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Order", "CreatedDate", c => c.String());
            AlterColumn("dbo.tb_Contact", "CreatedBy", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Contact", "CreatedDate", c => c.String());
            AlterColumn("dbo.tb_Post", "CreatedBy", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Post", "CreatedDate", c => c.String());
            AlterColumn("dbo.tb_News", "CreatedBy", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_News", "CreatedDate", c => c.String());
            AlterColumn("dbo.tb_Category", "CreatedBy", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Category", "CreatedDate", c => c.String());
            AlterColumn("dbo.tb_Adv", "CreatedBy", c => c.DateTime(nullable: false));
            AlterColumn("dbo.tb_Adv", "CreatedDate", c => c.String());
        }
    }
}
