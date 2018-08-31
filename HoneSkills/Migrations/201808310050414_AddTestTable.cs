namespace HoneSkills.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO Tests VALUES (1, 'Test Data')");
        }
        
        public override void Down()
        {
            DropTable("dbo.Tests");
        }
    }
}
