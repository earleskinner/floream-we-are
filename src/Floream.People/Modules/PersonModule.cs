using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Floream.People.DataSources.Context;
using Nancy;

namespace Floream.People.Modules
{
    public class PersonModule : NancyModule
    {
        private readonly PeopleContext _peopleContext;

        public PersonModule(PeopleContext peopleContext)
        {
            _peopleContext = peopleContext;

            Get["person"] = parameters =>
            {
                return null;
            };

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