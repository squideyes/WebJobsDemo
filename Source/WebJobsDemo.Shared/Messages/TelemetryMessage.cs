using System;
using System.ComponentModel.DataAnnotations;

namespace WebJobsDemo.Shared
{
    public class TelemetryMessage
    {
        [Required]
        public Guid TrackingId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ContextId { get; set; }

        [Required]
        [StringLength(6000)]
        public string Payload { get; set; }

        public override string ToString() =>
            $"UserId: {UserId}, ContextId: {ContextId}";
    }
}
