using Floream.People.DataSources.Entities;
using System.Data.Entity;

namespace Floream.People.DataSources.Context
{
    public class PeopleContext : DbContext
    {
        public PeopleContext() : base("peopleDb")
        {
            
        }

        public DbSet<Person> Persons { get; set; }
    }
}
