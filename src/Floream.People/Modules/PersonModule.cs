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
        }

    }
}