namespace WebJobsDemo.WebJob.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Telemetry",
                c => new
                    {
                        TelemetryId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ContextId = c.Guid(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        Payload = c.String(nullable: false, maxLength: 6000, unicode: false),
                    })
                .PrimaryKey(t => t.TelemetryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Telemetry");
        }
    }
}
