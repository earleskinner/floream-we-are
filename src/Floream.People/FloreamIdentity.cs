using System.Collections.Generic;
using Floream.People.DataSources.Entities;

namespace Floream.People
{
    public class FloreamIdentity : Nancy.Security.IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public Person Person { get; set; }
    }
}