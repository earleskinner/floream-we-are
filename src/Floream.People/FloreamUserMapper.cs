using System;

namespace Floream.People
{
    public class FloreamUserMapper : Nancy.Authentication.Forms.IUserMapper
    {
        public Nancy.Security.IUserIdentity GetUserFromIdentifier(Guid identifier, Nancy.NancyContext context)
        {
            
        }
    }
}