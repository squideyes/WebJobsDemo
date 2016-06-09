namespace WebJobsDemo.WebJob.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration 
        : DbMigrationsConfiguration<Entities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Entities context)
        {
        }
    }
}
