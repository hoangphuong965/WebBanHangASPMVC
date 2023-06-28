namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFiledAliasPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Post", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Post", "Alias");
        }
    }
}
