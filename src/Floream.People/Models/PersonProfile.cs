using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Floream.People.Models
{
    public class PersonProfile
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }

        public string AdUser { get; set; }

        public bool Retired { get; set; }

        public bool Hidden { get; set; }

        public byte[] Picture { get; set; }

        public string PictureExtension { get; set; }
    }
}