using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Floream.People.DataSources.Context;
using Nancy;
using Nancy.Security;

namespace Floream.People.Modules
{
    public class Api : NancyModule
    {
        private readonly PeopleContext _people;

        public Api(PeopleContext people)
            : base("/api")
        {

            _people = people;

            Get["/people"] = parameters =>
            {
                
                people

                return Negotiate.WithModel(null);
            };

        }

    }
}