using System;
using System.Linq;
using Floream.People.DataSources.Context;

namespace Floream.People
{
    public class FloreamUserMapper : Nancy.Authentication.Forms.IUserMapper
    {
        private readonly PeopleContext _people;

        public FloreamUserMapper(PeopleContext people)
        {
            _people = people;
        }

        /// <summary>
        /// Get user from the database
        /// </summary>
        public Nancy.Security.IUserIdentity GetUserFromIdentifier(Guid identifier, Nancy.NancyContext context)
        {
            var person = _people.People.FirstOrDefault(p => p.Id == identifier);
            if (person == null) return null;

            var user = new FloreamIdentity
            {
                UserName = person.AdUser
            };
            return user;
        }
    }
}