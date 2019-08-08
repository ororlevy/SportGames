namespace SportGames.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatemodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coaches", "ImgURL", c => c.String());
            AddColumn("dbo.Leagues", "ImgURL", c => c.String());
            AddColumn("dbo.Leagues", "Description", c => c.String());
            AddColumn("dbo.Teams", "ImgURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "ImgURL");
            DropColumn("dbo.Leagues", "Description");
            DropColumn("dbo.Leagues", "ImgURL");
            DropColumn("dbo.Coaches", "ImgURL");
        }
    }
}
