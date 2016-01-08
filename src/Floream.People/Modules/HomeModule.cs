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

        public HomeModule()
        {
            this.RequiresAuthentication();
            

            Get["/"] = parameters =>
            {
                // call when user visit the home page
                // 
                //
                using (PeopleContext context = new PeopleContext())
                {
                    List<Person> persons = context.People.ToList();
                    
                }

                return View["index"];
            };

           

        }

    }
}