using System;
using System.ComponentModel.DataAnnotations;

namespace WebJobsDemo.Shared
{
    public class SomeClass
    {
        [Required]
        public Guid SomeClassId { get; set; }

        [Required]

        public string Name { get; set; }
    }
}
