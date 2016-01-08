using System;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Security;

namespace Floream.People.Modules
{
    public class Profile : NancyModule
    {
        private readonly PeopleContext _peopleContext;

        public Profile(PeopleContext peopleContext)
        {
            this.RequiresAuthentication();

            _peopleContext = peopleContext;



         Get["/profile/{id:guid}"] = parameters =>
            {
                // call when user visit it's own profile
                // 
                Guid id = Guid.Parse(parameters.value);
                Context.CurrentUser as FloreamIdentity();
                return Response.AsJson(id);

            };
        
        
        }

    }
}