using System;
using System.ComponentModel.DataAnnotations;

namespace Floream.People.DataSources.Entities
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public string AdUser { get; set; }

        public bool Retired { get; set; }

        public bool Hidden { get; set; }

        public byte[] Picture { get; set; }
    }
}
