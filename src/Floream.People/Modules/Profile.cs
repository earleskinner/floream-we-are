using System;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Security;

namespace Floream.People.Modules
{
    public class Profile : NancyModule
    {
        private readonly PeopleContext _people;

        public Profile(PeopleContext people)
        {
            this.RequiresAuthentication();

            _people = people;

            Get["/profile"] = parameters =>
            {
                // call when user visit it's own profile
                var identity = Context.CurrentUser as FloreamIdentity;

                return View["profile", identity.Person];

            };
        
        
        }

    }
}