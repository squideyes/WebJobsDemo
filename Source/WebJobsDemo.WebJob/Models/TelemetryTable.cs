using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebJobsDemo.WebJob.Models
{
    [Table("Telemetry")]
    public class TelemetryTable
    {
        [Key]
        public Guid TelemetryId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ContextId { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        [Required]
        [StringLength(6000)]
        public string Payload { get; set; }
    }
}
