using System;
using System.Collections.Generic;
using System.Linq;
using Floream.People.DataSources.Context;
using Floream.People.DataSources.Entities;
using Nancy;
using Nancy.Security;

namespace Floream.People.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly PeopleContext _people;

        public HomeModule(PeopleContext people)
        {
            this.RequiresAuthentication();

            _people = people;

            Get["/"] = parameters =>
            {
                // call when user visit the home page
                // 
                //
                //using (PeopleContext context = new PeopleContext())
                //{
                //    List<Person> persons = context.People.ToList();
                    
                //}

                return View["index", people.People.ToList()];
            };

           

        }

    }
}