using System;

namespace Floream.People.DataSources.Entities
{
    public class Person
    {

        public int PersonId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string AdUser { get; set; }

        public bool Retired { get; set; }

        public bool Hidden { get; set; }

        public byte[] Picture { get; set; }
        
    }
}
