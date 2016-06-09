using System.Data.Entity;

namespace WebJobsDemo.WebJob.Models
{
    public class Entities : DbContext
    {
        public Entities()
            : base("Name=Entities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TelemetryTable>()
                .Property(p => p.Payload).IsUnicode(false);
        }

        public DbSet<TelemetryTable> Telemetries { get; set; }
    }
}
