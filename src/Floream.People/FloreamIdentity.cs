using System.Collections.Generic;

namespace Floream.People
{
    public class FloreamIdentity : Nancy.Security.IUserIdentity
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}