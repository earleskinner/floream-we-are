using System;
using System.Collections.Generic;
using System.Linq;
using Floream.People.DataSources.Context;
using Floream.People.DataSources.Entities;
using Nancy;

namespace Floream.People.Modules
{
    public class HomeModule : NancyModule
    {

        public HomeModule()
        {

            

            Get["/"] = parameters =>
            {
                using (PeopleContext context = new PeopleContext())
                {
                    List<Person> persons = context.People.ToList();
                }

                return View["index"];
            };

            Get["/profile/{id}"] = parameters =>
            {
                // todo
                return Response.AsJson<Guid>(1);

            };

        }

    }
}